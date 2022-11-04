using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BlogApi.Models
{
	public class BlogContext : DbContext
	{
		public BlogContext(DbContextOptions<BlogContext> options) : base(options)
		{
			
		}
		
		public DbSet<BlogItem> BlogItem{ get; set; } = null!;
    }
}