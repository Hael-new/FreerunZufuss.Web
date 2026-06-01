using FreerunZufuss.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreerunZufuss.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {

       
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripPhoto> TripPhotos { get; set; }
    }
}



