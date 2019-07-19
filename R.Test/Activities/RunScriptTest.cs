using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDotNet;

namespace Statistical.R.Test
{
    [TestClass]
    public class RunScriptTest
    {            
        [TestMethod]
        public void RunScript_ReturnDataVector_Success_()
        {
            string resourceName = "GetSampleData.R";
            DynamicActivity wfRScriptTester = RNetTest.GetActivity_RunScript(resourceName);
            IDictionary<string,object> output = WorkflowInvoker.Invoke(wfRScriptTester);

            Assert.IsTrue(output["OutRResult"] != null && output["OutRResult"] is SymbolicExpression);
        }
    }
}
