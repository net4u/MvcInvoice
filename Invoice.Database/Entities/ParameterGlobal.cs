using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Database
{
    [MetadataType(typeof(ParameterGlobalMetaData))]
    public partial class ParameterGlobal
    {
        internal sealed class ParameterGlobalMetaData
        {
            [Key]
            public string KeyName
            {
                get;
                set;
            }

        }
    }

}
