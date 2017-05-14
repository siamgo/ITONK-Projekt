using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Broker.Models;

namespace Broker.Migrations
{
    [DbContext(typeof(BrokerContext))]
    partial class BrokerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Broker.Model.Buyer", b =>
                {
                    b.Property<int>("BuyerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("Name");

                    b.HasKey("BuyerId");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("Broker.Model.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("Name");

                    b.HasKey("SellerId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("Broker.Model.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("BuyerId");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int?>("SellerId");

                    b.HasKey("StockId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Broker.Model.Stock", b =>
                {
                    b.HasOne("Broker.Model.Buyer")
                        .WithMany("WishList")
                        .HasForeignKey("BuyerId");

                    b.HasOne("Broker.Model.Seller")
                        .WithMany("Stocks")
                        .HasForeignKey("SellerId");
                });
        }
    }
}
