using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HomeTheater.Models;

namespace HomeTheater.EF
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
                   : base(options)
        { }

        public DbSet<Music> Musics { get; set; }
    }
}