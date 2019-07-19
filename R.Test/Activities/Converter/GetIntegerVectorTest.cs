using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using Statistical.R.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDotNet;

namespace Statistical.R.Test.Activities.Converter
{
    [TestClass]
    public class GetIntegerVectorTest
    {
        [TestMethod]
        public void GetIntegerVector_Return10L_Success()
        {
            string resourceName = "GetSampleData.R";
            DynamicActivity wfRScriptTester = RNetTest.GetActivity_RunScript(resourceName);
            IDictionary<string, object> outputRunScript = WorkflowInvoker.Invoke(wfRScriptTester);

            if (outputRunScript["OutRResult"] != null && outputRunScript["OutRResult"] is SymbolicExpression)
            {
                IDictionary<string, object> inputs = new Dictionary<string, object>();
                inputs.Add("Input", outputRunScript["OutRResult"] as SymbolicExpression);
                IDictionary<string, object> output = WorkflowInvoker.Invoke(new GetIntegerVector(), inputs);

                Assert.IsTrue((output["Output"] as IntegerVector).Length == 10);
            }
            else
            {
                Assert.Fail();
            }
        }      
    }
}
