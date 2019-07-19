using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Statistical.R.Design
{
    // Interaction logic for RunRScriptDesigner.xaml
    public partial class RunRScriptDesigner
    {
        public RunRScriptDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            string activityDescription = "Runs scripts created in R. This activity needs to be used inside the R Scope activity.";
            builder.AddCustomAttributes(typeof(RunRScript), new DesignerAttribute(typeof(RunRScriptDesigner)));
            builder.AddCustomAttributes(typeof(RunRScript), new DescriptionAttribute(activityDescription));
        }
    }
}
