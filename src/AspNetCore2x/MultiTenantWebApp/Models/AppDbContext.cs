using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class AppDbContext : DbContext {
		// todo: 初期化用SQL
		/*
drop table if exists dbo.Blog;

create table dbo.Blog(
	Id int,
	TenantId int not null,
	Title nvarchar(20) not null,
	Content nvarchar(max) not null,
	constraint PK_Blog primary key(Id)
);

insert into dbo.Blog(Id, TenantId, Title, Content)
output inserted.*
values
	(1, 1, N'ブログはじめました', N''),
	(2, 2, N'冷やし中華はじめました', N'');
		*/

		private static readonly string _connectionString
			= new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();

		private readonly ITenantIdProvider<int> _tenantIdProvider;

		public AppDbContext(ITenantIdProvider<int> tenantIdProvider) {
			_tenantIdProvider = tenantIdProvider;
		}

		protected int TenantId => _tenantIdProvider.GetTenantId();

		public DbSet<Blog> Blogs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder
				// インメモリDBだとフィルタが効かないのかも
				//.UseInMemoryDatabase(nameof(AppDbContext))
				.UseSqlServer(_connectionString)
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Blog>()
				.ToTable(nameof(Blog))
				// テナントIDによるフィルタ
				.HasQueryFilter(blog => blog.TenantId == TenantId);
		}
	}
}
