using RDotNet;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Statistical.R
{
    /// <summary>
    /// This custom activity does something.
    /// </summary>
    public sealed class RunRScript : RActivity
    {
        #region .    UiPath Properties    .

        [RequiredArgument]
        [Category("Input")]
        [Description("The path for the R script file which extension should be '.R'")]
        public InArgument<string> File
        {
            get;
            set;
        }      

        [Category("Output"), Description("Object")]
        public OutArgument<SymbolicExpression> Output
        {
            get;
            set;
        }

        #endregion

        #region .    Methods    .

        public RunRScript()
        {
            this.DisplayName = "Run R Script";
        }

        protected override void Execute(CodeActivityContext context)
        {
            REngine engine = RScope.GetEngine(context);
            RNet rApp = new RNet(engine);            

            string scriptPath = this.File.Get(context);
            scriptPath = scriptPath.Replace(@"\", @"/");

            string scriptDirectory = scriptPath.Remove(scriptPath.LastIndexOf(@"/"));
            string scriptFunction = scriptPath.Split('/').LastOrDefault();

            string currentWd = Environment.CurrentDirectory;

            try
            {
                Environment.CurrentDirectory = scriptDirectory;
                rApp.Source(scriptFunction);

                scriptFunction = scriptFunction.Remove(scriptFunction.LastIndexOf("."));

                this.Output.Set(context, engine.Evaluate(string.Format("{0}();", scriptFunction)));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Something went wrong during script execution. {0}.", e.Message));
            }
            finally
            {
                //Return the current working directory to the original one.
                Environment.CurrentDirectory = currentWd;
            }


            #endregion
        }

    }
}
