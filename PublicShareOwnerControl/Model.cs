using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PublicShareOwnerControl
{
    public class ShareOwnershipContext : DbContext
    {
        public ShareOwnershipContext():base(){
        }
        public DbSet<ShareOwner> ShareOwners {get;set;}
        public DbSet<Share> Shares {get;set;}

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("publicshareownership");
        }
    }

    public class ShareOwner
    {
        public string Name { get; set; }
        public int ShareHolderId {get; set;}
        public List<Share> Shares {get; set;}
    }

    public class Share
    {
        public string Name {get; set;}
        public int ShareId {get;set;}
        
        
    }
}