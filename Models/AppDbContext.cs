using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace guia_2.Models;
    public class AppDbContext : IdentityDbContext<IdentityUser> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DBName;Integrated Security=True");
        }

        public DbSet<Autos> Autos { get; set; }
        public DbSet<MarcasdeAutos> MarcasdeAutos { get; set; }
        public DbSet<Tiposdeautos> Tiposdeautos { get; set; }
        public DbSet<Imagesauto> Imagesauto { get; set;}
        public DbSet<ImagesAutos> ImagesAutos { get; set;}
    }