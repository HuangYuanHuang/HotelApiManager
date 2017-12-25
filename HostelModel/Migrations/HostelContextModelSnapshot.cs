﻿// <auto-generated />
using HostelModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HostelModel.Migrations
{
    [DbContext(typeof(HostelContext))]
    partial class HostelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("HostelModel.AccoutModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccoutType");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("HotelId");

                    b.Property<DateTime>("LastTime");

                    b.Property<int?>("Level");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("Pwd")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("T_Hostel_Accout");
                });

            modelBuilder.Entity("HostelModel.AreaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("City")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Mark");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_Area");
                });

            modelBuilder.Entity("HostelModel.DepartmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Duty")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_Department");
                });

            modelBuilder.Entity("HostelModel.HotelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int>("AreaId");

                    b.Property<string>("Bank")
                        .IsRequired();

                    b.Property<string>("BankAccount")
                        .IsRequired();

                    b.Property<string>("BankAddress")
                        .IsRequired();

                    b.Property<string>("CODE")
                        .IsRequired();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("MailingAddress")
                        .IsRequired();

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<int?>("Sort");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("T_Hostel_Hotel");
                });

            modelBuilder.Entity("HostelModel.HotelWorkOrderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Billing")
                        .IsRequired();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("DepartID");

                    b.Property<DateTime>("End");

                    b.Property<string>("Examine");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("HotelId");

                    b.Property<string>("KeyWord");

                    b.Property<string>("Mark");

                    b.Property<int?>("Max");

                    b.Property<int?>("Min");

                    b.Property<int>("Num");

                    b.Property<int?>("OrderType");

                    b.Property<int>("ScheduleId");

                    b.Property<DateTime>("Start");

                    b.Property<int?>("Status");

                    b.Property<int>("WorkTypeId");

                    b.HasKey("Id");

                    b.HasIndex("DepartID");

                    b.HasIndex("HotelId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("T_Hostel_WorkOrder");
                });

            modelBuilder.Entity("HostelModel.MessageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Context");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("From");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("To");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_Message");
                });

            modelBuilder.Entity("HostelModel.PersonEmployModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int?>("Evaluate");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("HotelComment");

                    b.Property<int?>("HotelEvaluate");

                    b.Property<int>("HotelOrderId");

                    b.Property<int>("PersonId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("HotelOrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("T_Hostel_PersonEmploy");
                });

            modelBuilder.Entity("HostelModel.PersonOrderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplyNum");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.Property<int>("OrderId");

                    b.Property<int>("PersonId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("T_Hostel_PersonOrder");
                });

            modelBuilder.Entity("HostelModel.ScheduleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("End");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Start");

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_Schedule");
                });

            modelBuilder.Entity("HostelModel.ServicePersonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Health");

                    b.Property<string>("ICardBack");

                    b.Property<string>("ICardPositive");

                    b.Property<string>("Icon");

                    b.Property<string>("IdentityCard")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Pwd")
                        .HasMaxLength(250);

                    b.Property<string>("RealName")
                        .HasMaxLength(50);

                    b.Property<string>("Sex")
                        .HasMaxLength(25);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_ServicePerson");
                });

            modelBuilder.Entity("HostelModel.UserOnlineModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccoutType");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Device");

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("IMEI");

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("Phone");

                    b.Property<string>("SoftVersion");

                    b.Property<string>("SystemType");

                    b.Property<string>("Token");

                    b.Property<string>("UserGUID");

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_UserOnline");
                });

            modelBuilder.Entity("HostelModel.WorkTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Duty")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("GUID")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Mark")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Qualification")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("T_Hostel_WorkType");
                });

            modelBuilder.Entity("HostelModel.AccoutModel", b =>
                {
                    b.HasOne("HostelModel.HotelModel", "Hotel")
                        .WithMany("Accouts")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HostelModel.HotelModel", b =>
                {
                    b.HasOne("HostelModel.AreaModel", "Area")
                        .WithMany("Hotels")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HostelModel.HotelWorkOrderModel", b =>
                {
                    b.HasOne("HostelModel.DepartmentModel", "Department")
                        .WithMany("WorkTypeOrders")
                        .HasForeignKey("DepartID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HostelModel.HotelModel", "Hotel")
                        .WithMany("HotelOrders")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HostelModel.ScheduleModel", "Schedule")
                        .WithMany("ScheduleOrders")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HostelModel.WorkTypeModel", "WorkType")
                        .WithMany("WorkTypeOrders")
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HostelModel.PersonEmployModel", b =>
                {
                    b.HasOne("HostelModel.HotelWorkOrderModel", "HoterlOrder")
                        .WithMany()
                        .HasForeignKey("HotelOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HostelModel.ServicePersonModel", "Person")
                        .WithMany("Employs")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HostelModel.PersonOrderModel", b =>
                {
                    b.HasOne("HostelModel.HotelWorkOrderModel", "HotelOrder")
                        .WithMany("Orders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HostelModel.ServicePersonModel", "Person")
                        .WithMany("Orders")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
