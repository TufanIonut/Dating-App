using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;

namespace DatingAplication.Models
{
    public class DatingAppDb:DbContext
    {
        public DbSet<CancelledMatch> CancelledMatches { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<DeletedConversation> DeletedConversations { get; set; }
        public DbSet<DeletedMessage> DeletedMessages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Configuration.GetConnectionString("DatingApp");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
