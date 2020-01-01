using Microsoft.EntityFrameworkCore;
using PhotoBoom.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBoom.API.Data.Context
{
    public class PhotoBoomContext : DbContext
    {
        public PhotoBoomContext(DbContextOptions<PhotoBoomContext> options) : base(options)
        {
        }


        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }


}
