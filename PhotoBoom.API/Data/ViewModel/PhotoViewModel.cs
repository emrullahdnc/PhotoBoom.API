using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.API.Data.ViewModel
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<string> Tag { get; set; }
        public IFormFile Url { get; set; }
    }
}
