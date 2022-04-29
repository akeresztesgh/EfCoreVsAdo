using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public  class A
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<B> Bs { get; set; } = new List<B>();
    }
}
