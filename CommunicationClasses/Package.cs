using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationClasses
{
    [Serializable]
    public class Package
    {
        public object Item { get; set; }
        public List<object> ItemList { get; set; }
        public Operation Operation { get; set; }
    }
}
