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
    public class GetListTest
    {
        [TestMethod]
        public void GetListTest_Return10LString_Success()
        {
            string resourceName = "GetSampleData_List.R";
            DynamicActivity wfRScriptTester = RNetTest.GetActivity_RunScript(resourceName);
            IDictionary<string, object> outputRunScript = WorkflowInvoker.Invoke(wfRScriptTester);

            if (outputRunScript["OutRResult"] != null && outputRunScript["OutRResult"] is SymbolicExpression)
            {
                IDictionary<string, object> inputs = new Dictionary<string, object>();
                inputs.Add("Input", outputRunScript["OutRResult"] as SymbolicExpression);
                IDictionary<string, object> output = WorkflowInvoker.Invoke(new GetList<string>(), inputs);

                Assert.IsTrue((output["Output"] as List<string>).Count == 10);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetListTest_Return10LInteger_Success()
        {
            string resourceName = "GetSampleData_List.R";
            DynamicActivity wfRScriptTester = RNetTest.GetActivity_RunScript(resourceName);
            IDictionary<string, object> outputRunScript = WorkflowInvoker.Invoke(wfRScriptTester);

            if (outputRunScript["OutRResult"] != null && outputRunScript["OutRResult"] is SymbolicExpression)
            {
                IDictionary<string, object> inputs = new Dictionary<string, object>();
                inputs.Add("Input", outputRunScript["OutRResult"] as SymbolicExpression);
                IDictionary<string, object> output = WorkflowInvoker.Invoke(new GetList<Int32>(), inputs);

                Assert.IsTrue((output["Output"] as List<Int32>).Count == 10);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
