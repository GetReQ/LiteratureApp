using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Literature.Models
{
    public class Publication
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Code {get; set; }
    }
}
