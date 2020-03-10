﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    [ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
    public partial class MyProjectNameDbContext : AbpDbContext<MyProjectNameDbContext>, IMyProjectNameDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureMyProjectName();

            base.OnModelCreating(builder);
        }
    }
}