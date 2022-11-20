using LaLocanda.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Persistence.Contexts
{
    public class RestaurantContext:DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder m)
        {
            #region Tables
            m.Entity<Dish>().ToTable("Dishes");
            m.Entity<Ingredient>().ToTable("Ingredients");
            m.Entity<Order>().ToTable("Orders");
            m.Entity<Table>().ToTable("Tables");
            #endregion

            #region Keys
            m.Entity<Dish>().HasKey(t => t.Id);
            m.Entity<Ingredient>().HasKey(t => t.Id);
            m.Entity<Order>().HasKey(t => t.Id);
            m.Entity<Table>().HasKey(t => t.Id);
            #endregion

            #region Relations
            //Table-Orders
            m.Entity<Table>()
                .HasMany<Order>(table => table.Orders)
                .WithOne(order => order.Table)
                .HasForeignKey(order => order.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            //Ingredients-Dishes
            m.Entity<Ingredient>()
                .HasMany(ing => ing.Dishes)
                .WithMany(dish => dish.Ingredients)
                .UsingEntity<IngredientDish>(
                    j => j
                        .HasOne(ingdish => ingdish.JDish)
                        .WithMany(ing => ing.IngredientDishes)
                        .HasForeignKey(ingdish => ingdish.DishId),
                    j => j
                        .HasOne(ingdish => ingdish.JIngredient)
                        .WithMany(dish => dish.IngredientDishes)
                        .HasForeignKey(ingdish => ingdish.IngredientId),
                    j =>
                    {
                        j.ToTable("IngredientDish");
                        j.HasKey(t => new { t.DishId, t.IngredientId });
                    }
                );

            //Orders-Dishes
            m.Entity<Order>()
                .HasMany(ord => ord.Dishes)
                .WithMany(dish => dish.Orders)
                .UsingEntity<OrderDish>(
                    j => j
                        .HasOne(ordish => ordish.JDish)
                        .WithMany(ord => ord.OrderDishes)
                        .HasForeignKey(ordish => ordish.DishId),
                    j => j
                        .HasOne(ordish => ordish.JOrder)
                        .WithMany(dish => dish.OrderDishes)
                        .HasForeignKey(ordish => ordish.OrderId),
                    j =>
                    {
                        j.ToTable("OrderDish");
                        j.HasKey(t => new { t.DishId, t.OrderId });
                    }
                );
            #endregion

            #region Props

            #region Dish
            m.Entity<Dish>().Property(t => t.Name).IsRequired();
            m.Entity<Dish>().Property(t => t.Price).IsRequired();
            m.Entity<Dish>().Property(t => t.CantPeople).IsRequired();
            m.Entity<Dish>().Property(t => t.Category).IsRequired();
            #endregion

            #region Ingredient
            m.Entity<Ingredient>().Property(t => t.Name).IsRequired();
            #endregion

            #region Order
            m.Entity<Order>().Property(t => t.TableId).IsRequired();
            m.Entity<Order>().Property(t => t.TotalPrice).IsRequired();
            m.Entity<Order>().Property(t => t.Status).IsRequired();
            #endregion

            #region Table
            m.Entity<Table>().Property(t => t.MaxClients).IsRequired();
            m.Entity<Table>().Property(t => t.Description).IsRequired();
            m.Entity<Table>().Property(t => t.Status).IsRequired();
            #endregion

            #endregion
        }
    }
}
