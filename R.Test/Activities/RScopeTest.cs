using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Activities;

namespace Statistical.R.Test
{
    /// <summary>
    /// Summary description for RScopeTest
    /// </summary>
    [TestClass]
    public class RScopeTest
    {
        private RScope rScope;

        public RScopeTest()
        {
            rScope = new RScope();
        }

        [TestMethod]
        public void InitializeRScope_DllPath_Connected()
        {
            WorkflowInvoker.Invoke(new RScope()
            {
                Path = @"C:\Program Files\R\R-3.4.3\bin\i386\R.dll",             
            });
        }
    }
}
