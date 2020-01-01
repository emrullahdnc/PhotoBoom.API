using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.API.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }


     
        public int PhotoId { get; set; }

        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }
         
    }
}
