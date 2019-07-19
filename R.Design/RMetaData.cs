using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistical.R.Design
{
    public sealed class DateMetadata : IRegisterMetadata
    {
        public void Register()
        {
            RegisterAll();
        }

        private void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
           
            RScopeDesigner.RegisterMetadata(builder);                    
            RunRScriptDesigner.RegisterMetadata(builder); 
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
