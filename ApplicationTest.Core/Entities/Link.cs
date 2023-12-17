using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest.Domain.Entities
{
    // not used to HATEOAS  so I will have to research better where to include this class and make use of it.
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Action { get; set; }
        public string[] Types { get; set; }
    }
}
