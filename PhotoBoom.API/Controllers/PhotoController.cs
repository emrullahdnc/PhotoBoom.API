using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoBoom.API.Data.Context;
using PhotoBoom.API.Data.Entities;
using PhotoBoom.API.Data.ViewModel;

namespace PhotoBoom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoBoomContext _context;
        private readonly IHostingEnvironment _env;

        public PhotoController(PhotoBoomContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Photo>> Get()
        {
            return _context.Photos.Include(x => x.PhotoTags).ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<Photo> Get(int id)
        {
            return _context.Photos.Include(x => x.PhotoTags).FirstOrDefault(x => x.Id == id);
        }



        // POST api/values
        [HttpPost("photoSave")]
        public IActionResult PhotoSave([FromForm] PhotoViewModel request)
        {
            string fileUrl = string.Empty;
            if (request.Url != null)
            {
                var filePath = Path.GetExtension(request.Url.FileName); 

                if (filePath != "") // Path Control
                {

                }

                var constPath = string.Concat(_env.ContentRootPath, "/", "External/Photo"); //root
                var fileName = string.Concat(request.Url.FileName, "_", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5)); //New File Name
                var isDirectoryExist = Directory.Exists(constPath);

                if (!isDirectoryExist)
                    Directory.CreateDirectory(constPath);

                var dirPath = string.Concat(constPath, "/", fileName, filePath);
                using (var stream = new FileStream(dirPath, FileMode.Create))
                {
                    request.Url.CopyTo(stream);
                }
                fileUrl = fileName + filePath;
            }


            Photo photo = new Photo()
            {
                Title = request.Title,
                Url = fileUrl
            };

            _context.Photos.Add(photo);
            if (_context.SaveChanges() > 0)
            {
                if (request.Tag.Count > 0)
                {
                    List<Tag> tagList = new List<Tag>();
                    foreach (var tags in request.Tag)
                    {
                        Tag tag = new Tag()
                        {
                            Name = tags,
                            PhotoId = photo.Id
                        };
                        tagList.Add(tag);
                    }

                    _context.Tags.AddRange(tagList);
                    if(_context.SaveChanges() > 0)
                    {
                        return Ok("Tag ve Photo Başarıyla Kayıt Edildi");
                    }
                    return Ok("Tag Kayıt Edilemedi,Photo Kaydı Başarılı");
                }
            }

            return Ok("Kayıt Sırasında Bir Sorun Oluştu");
              
        }


        // DELETE api/values/5
        [HttpDelete("deletePhoto/{id}")]
        public void Delete(int id)
        {

            var photo = _context.Photos.Include(x => x.PhotoTags).FirstOrDefault(x => x.Id == id);

            _context.Photos.Remove(photo);
            _context.SaveChanges();
        }





    }
}