using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Statistical.R;

namespace Statistical.R.Design
{
    /// <summary>
    /// Interaction logic for GetNextBusinessDayDesigner.xaml
    /// </summary>
    public partial class RScopeDesigner
    {
        public RScopeDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder) {
            string activityDescription = "Container for R activities.";
            builder.AddCustomAttributes(typeof(RScope), new DesignerAttribute(typeof(RScopeDesigner)));
            builder.AddCustomAttributes(typeof(RScope), new DescriptionAttribute(activityDescription));
        }
    }
}
