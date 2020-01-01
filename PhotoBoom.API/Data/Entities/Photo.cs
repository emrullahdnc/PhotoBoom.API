using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.API.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public ICollection<Tag> PhotoTags { get; set; }
    }
}
