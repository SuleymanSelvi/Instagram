global using Instagram.Domain;
global using Microsoft.EntityFrameworkCore;
using Instagram.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Infrastructure
{
    public class InstagramDbContext : DbContext
    {
        //constructere
        public InstagramDbContext(DbContextOptions<InstagramDbContext> options) : base(options)
        {

        }

        // Rebuild Solution
        // EntityFrameworkCore\Add-Migration
        // EntityFrameworkCore\update-database -verbose

        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<PostsLike> PostsLike { get; set; }
        public DbSet<PostsComment> PostsComment { get; set; }
        public DbSet<PostsCommentLike> PostsCommentLike { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<UserBlock> UserBlock { get; set; }
        public DbSet<SavedPosts> SavedPosts { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Storys> Storys { get; set; }
        public DbSet<StoryView> StoryView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
