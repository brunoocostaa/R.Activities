using RDotNet;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;

namespace Statistical.R.Converter
{
    /// <summary>
    /// This custom activity does something.
    /// </summary>
    public sealed class GetList<T> : CodeActivity
    {
        #region .    UiPath Properties    .

        [RequiredArgument, Category("Input"), Description("The SymbolicExpression variable returned from RunRScript activity.")]
        public InArgument<SymbolicExpression> Input
        {
            get;
            set;
        }

        [Category("Output"), Description("The list converted out of the SymbolicExpression (will throw an exception if cast is not valid).")]
        public OutArgument<List<T>> Output
        {
            get;
            set;
        }

        #endregion           

        #region .    Methods    .

        public GetList()
        {
            this.DisplayName = "Get List";
        }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                SymbolicExpression input = this.Input.Get(context);
                List<T> output = new List<T>();

                foreach (var a in input.AsList()) {
                    output.Add((T) Convert.ChangeType(a.AsRaw()[0],typeof(T)));
                }

                this.Output.Set(context, output);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

}
