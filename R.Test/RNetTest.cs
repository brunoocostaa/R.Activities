using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDotNet;

namespace Statistical.R.Test
{
    [TestClass]
    public class RNetTest
    {
        static string pathR = @"C:\Program Files\R\R-3.4.3\bin\i386\R.dll";

        #region .    Test Methods    .
        [TestMethod]
        public void GetWd_ReturnPath_Success()
        {
            REngine engine = REngine.GetInstance(pathR);
            RNet rApp = new RNet(engine);

            Assert.IsNotNull(rApp.GetWd());
        }
       

        #endregion


        #region .    Workflow Samples    .

        public static DynamicActivity GetActivity_RunScript(string rsrcName)
        {
            string scriptFile = GetScriptPath_Resources(rsrcName);

            Variable<SymbolicExpression> result = new Variable<SymbolicExpression>("rScriptResult");
            OutArgument<RDotNet.SymbolicExpression> out_ScriptResult = new OutArgument<RDotNet.SymbolicExpression>();
            DynamicActivity wfRScriptTester;

            try
            {
                wfRScriptTester = new DynamicActivity()
                {
                    Properties = {
                            new DynamicActivityProperty{
                                Name="OutRResult",
                                Type=typeof(OutArgument<RDotNet.SymbolicExpression>),
                                Value = out_ScriptResult
                            },
                        },
                    Implementation = () => new Sequence
                    {
                        Variables =
                            {
                                result
                            },
                        Activities =
                            {
                                new RScope
                                {
                                    Path = pathR,
                                    Body = {
                                        Handler = new RunRScript()
                                        {
                                            File = scriptFile,
                                            Output = result
                                        }
                                    }
                                },
                                new Assign<RDotNet.SymbolicExpression>{
                                    To = new ArgumentReference<RDotNet.SymbolicExpression>{ ArgumentName = "OutRResult" },
                                    Value = new InArgument<RDotNet.SymbolicExpression>((env)=>(result.Get(env)))
                                },
                                new WriteLine()
                                {
                                    Text = "Completed RunRScript activity test."
                                }
                            }
                    }
                };
            }
            catch (Exception e)
            {
                throw e;
            }       

            return wfRScriptTester;
        }

        private static string GetScriptPath_Resources(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceNameSpace = "Statistical.R.Test.Sample";            
            string tempFile = string.Empty;
            string resourceContents = string.Empty;

            try
            {
                tempFile = Path.Combine(Path.GetTempPath(), resourceName);

                using (Stream stream = assembly.GetManifestResourceStream(resourceNameSpace + "." + resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    resourceContents = reader.ReadToEnd();
                }

                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
            catch (Exception e)
            {
                throw e;
            }

            File.WriteAllText(tempFile, resourceContents);

            return tempFile;
        }
        #endregion
    }
}
