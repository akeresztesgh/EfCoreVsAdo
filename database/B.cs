using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    public class B
    {
        public long Id { get; set; }
        public string Name { get; set; }        
        public long AId { get; set; }
        [ForeignKey("AId")]
        public A A { get; set; }
    }
}
