using System;
using System.Activities;
using System.Activities.DynamicUpdate;
using System.Activities.Runtime;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Activities.Statements;
using RDotNet;

namespace Statistical.R
{
    public class RScope : NativeActivity
    {
        public REngine rEngine;

        [Browsable(false)]
        public ActivityAction<REngine> Body
        {
            get;
            set;
        }

        [RequiredArgument, Category("Input"), Description(@"The full path for the R dll file. E.g.: C:\Program Files\R\R-3.4.3\bin\i386\R.dll. ** IMPORTANT NOTE: Not compatible with R versions > 3.4.3 **")]
        public InArgument<string> Path
        {
            get;
            set;
        }

        [Category("Output"), DefaultValue(null)]
        public OutArgument<REngine> Engine
        {
            get;
            set;
        }

        internal static string RScopePropertyTag
        {
            get
            {
                return "RScopeProperty";
            }
        }

        public RScope()
        {
            this.DisplayName = "R Scope";
            this.Body = new ActivityAction<REngine>
            {
                Argument = new DelegateInArgument<REngine>(RScope.RScopePropertyTag),
                Handler = new Sequence
                {
                    DisplayName = "Using R Engine"
                }
            };
        }

        protected override void Execute(NativeActivityContext context)
        {            
            string dllPath = this.Path.Get(context);
            rEngine = REngine.GetInstance(dllPath);
        

            if (this.Body != null)
            {
                context.ScheduleAction<REngine>(this.Body, this.rEngine, new CompletionCallback(this.OnCompleted), new FaultCallback(this.OnFaulted));
            }

            this.Engine.Set(context, this.rEngine);
        }

        internal static REngine GetEngine(ActivityContext context)
        {
            PropertyDescriptor prop = context.DataContext.GetProperties()[RScopePropertyTag];
            if (prop != null)
                return prop.GetValue(context.DataContext) as REngine;
            else 
                throw new InvalidOperationException("The R engine has not been found.");
        }

        private void OnCompleted(NativeActivityContext context, ActivityInstance activityInstance)
        {            
            if (rEngine != null)
            {
                this.RDispose();  
            }

            this.Engine.Set(context, this.rEngine);
        }

        private void OnFaulted(NativeActivityFaultContext context, Exception exception, ActivityInstance source)
        {
            if (rEngine != null)
                this.RDispose();
            context.CancelChildren();
        }

        private void RDispose() {
            //rEngine.Dispose(); // For some reason, if the dispose method is invoked for the second time the R Engine throws an exception. 
            rEngine = null;
        }
    }
}
