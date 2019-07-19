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
    public sealed class GetIntegerVector : CodeActivity
    {
        #region .    UiPath Properties    .

        [RequiredArgument, Category("Input"), Description("The SymbolicExpression variable returned from RunRScript activity.")]
        public InArgument<SymbolicExpression> Input
        {
            get;
            set;
        }

        [Category("Output"), Description("The integer vector converted out of the SymbolicExpression (will throw an exception if cast is not valid).")]
        public OutArgument<IntegerVector> Output
        {
            get;
            set;
        }

        #endregion           

        #region .    Methods    .

        public GetIntegerVector()
        {
            this.DisplayName = "Get Integer Vector";
        }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                SymbolicExpression input = this.Input.Get(context);
                this.Output.Set(context, input.AsInteger());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

}
