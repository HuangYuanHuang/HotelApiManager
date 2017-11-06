using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
namespace HostelModel
{
    public class HostelContext : DbContext
    {
        private static readonly IServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkMySql().BuildServiceProvider();
        public DbSet<AreaModel> Areas { set; get; }

        public DbSet<HotelModel> Hotels { set; get; }

        public DbSet<AccoutModel> Accouts { set; get; }

        public DbSet<ScheduleModel> Schedules { set; get; }

        public DbSet<WorkTypeModel> WorkTypes { get; set; }

        public DbSet<HotelWorkOrderModel> HotelOrders { get; set; }

        public DbSet<ServicePersonModel> ServicePersons { set; get; }

        public DbSet<DepartmentModel> Departs { set; get; }

        public DbSet<PersonOrderModel> PersonOrders { set; get; }

        public DbSet<PersonEmployModel> PersonEmploys { set; get; }

        public DbSet<UserOnlineModel> UserOnlines { set; get; }

        public DbSet<MessageModel> Messages { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInternalServiceProvider(serviceProvider).UseMySql(@"Server=123.56.15.145;database=hostel_manager;uid=root;pwd=Password01!");




        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<HotelWorkOrderModel>().HasRequired(d=>d.);
        }



    }
}
