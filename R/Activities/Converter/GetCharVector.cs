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
    public sealed class GetCharVector : CodeActivity
    {
        #region .    UiPath Properties    .

        [RequiredArgument, Category("Input"), Description("The SymbolicExpression variable returned from RunRScript activity.")]
        public InArgument<SymbolicExpression> Input
        {
            get;
            set;
        }

        [Category("Output"), Description("The char vector converted out of the SymbolicExpression (will throw an exception if cast is not valid).")]
        public OutArgument<CharacterVector> Output
        {
            get;
            set;
        }

        #endregion           

        #region .    Methods    .

        public GetCharVector()
        {
            this.DisplayName = "Get Character Vector";
        }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                SymbolicExpression input = this.Input.Get(context);
                this.Output.Set(context, input.AsCharacter());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

}
