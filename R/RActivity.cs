using System;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Validation;
using System.Collections.Generic;
using System.ComponentModel;

namespace Statistical.R
{
    public abstract class RActivity : CodeActivity
    {
        protected RActivity()
        {
            base.Constraints.Add(ActivityNotInRScopeWarning());
        }

        /// <summary>
        /// Method that handles the use of the R activities outside the R Scope, which should not be accepted. 
        /// </summary>
        private static Constraint ActivityNotInRScopeWarning() {
            DelegateInArgument<RActivity> element = new DelegateInArgument<RActivity>();
            DelegateInArgument<ValidationContext> context = new DelegateInArgument<ValidationContext>();
            Variable<bool> result = new Variable<bool>();
            DelegateInArgument<Activity> parent = new DelegateInArgument<Activity>();

            return new Constraint<RActivity>
            {
                Body = new ActivityAction<RActivity, ValidationContext>
                {
                    Argument1 = element,
                    Argument2 = context,
                    Handler = new Sequence
                    {
                        Variables =
                    {
                        result
                    },
                        Activities =
                    {
                        new ForEach<Activity>
                        {
                            Values = new GetParentChain
                            {
                                ValidationContext = context
                            },
                            Body = new ActivityAction<Activity>
                            {
                                Argument = parent,
                                Handler = new If()
                                {
                                    Condition = new InArgument<bool>((env) => object.Equals(parent.Get(env).GetType(),typeof(RScope))),
                                    Then = new Assign<bool>
                                    {
                                        Value = true,
                                        To = result
                                    }
                                }
                            }
                        },
                        new AssertValidation
                        {
                            Assertion = new InArgument<bool>(result),
                            Message = new InArgument<string> ("R activities should be used only inside R scope."),
                        }
                    }
                    }
                }
            };
        }       
    }
}
