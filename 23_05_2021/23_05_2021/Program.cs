using _23_05_2021.HelpMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using test_23_05_2021.models;

namespace test_23_05_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            using (Context db = new Context(options))
            {
                //5 засидировать клиентов
                var book = new Book() { Name = "пустая книга", Description = "совсем пустая книга", Price = 1200.4m };
                var book2 = new Book() { Name = "одностраничная книга", Description = "почти не пустая", Price = 1600.6m };
                var book3 = new Book() { Name = "book4999", Description = "почти не пустая", Price = 4900.6m };
                var book4 = new Book() { Name = "book5001", Description = "почти не пустая", Price = 5001.6m };

                
                var author = new Author() { Name = "Линч" };
                var author2 = new Author() { Name = "Сергей Виталич с третьего подъезда" };

                var cli = new Client() { Email = "nocheckedepochta", Name = "boostedUser" };
                var cli2 = new Client() { Email = "nocheckedepochta2", Name = "boostedUser2" };
                var cli3 = new Client() { Email = "nocheckedepochta3", Name = "boostedUser3" };
                var cli4 = new Client() { Email = "nocheckedepochta4", Name = "boostedUser4" };
                var cli5 = new Client() { Email = "nocheckedepochta5", Name = "boostedUser5" };

                book.Authors = new List<Author>() { author };
                book.Authors.Add(author2);

                book2.Authors = new List<Author>() { author };

                book3.Authors = new List<Author>() { author };
                book3.Authors.Add(author2);

                book4.Authors = new List<Author>() { author };
                book4.Authors.Add(author2);

                db.Books.Add(book);
                db.Books.Add(book2);
                db.Books.Add(book3);
                db.Books.Add(book4);
                db.ChangeTracker.DetectChanges();

                foreach (var entry in db.ChangeTracker.Entries())
                {
                    if ((entry.State == EntityState.Added || entry.State == EntityState.Modified) && entry.Metadata.ClrType.Name.Contains("Book"))
                    {
                        entry.Property("mark").CurrentValue = "checkSPUpdate";
                    }
                }


                db.Clients.Add(cli);
                db.Clients.Add(cli2);
                db.Clients.Add(cli3);
                db.Clients.Add(cli4);
                db.Clients.Add(cli5);

                //var result = db.SaveChanges();

                //1 клиент
                var order = new Order() { Books = new List<Book>() { book}, Client = cli, Date = DateTime.Today, Sum = book.Price };
                var order1 = new Order() { Books = new List<Book>() { book }, Client = cli, Date = DateTime.Today, Sum = book.Price };
                var order2 = new Order() { Books = new List<Book>() { book }, Client = cli, Date = DateTime.Today, Sum = book.Price };
                //2 клиент
                var order3 = new Order() { Books = new List<Book>() { book2 }, Client = cli2, Date = DateTime.Today, Sum = book2.Price };
                var order4 = new Order() { Books = new List<Book>() { book2 }, Client = cli2, Date = DateTime.Today, Sum = book2.Price };
                //3 клиент
                var order5 = new Order() { Books = new List<Book>() { book3 }, Client = cli3, Date = DateTime.Today, Sum = book3.Price };
                var order6 = new Order() { Books = new List<Book>() { book2 }, Client = cli3, Date = DateTime.Today, Sum = book2.Price };
                //4 клиент
                var order7 = new Order() { Books = new List<Book>() { book4 }, Client = cli4, Date = DateTime.Today, Sum = book4.Price };
                //5 клиент
                var order8 = new Order() { Books = new List<Book>() { book4 }, Client = cli5, Date = DateTime.Today, Sum = book4.Price };
                //6 так много, чтобы запрос последний проверить
                OrderService.CreateOrUpdateOrder(order, db);
                OrderService.CreateOrUpdateOrder(order1, db);
                OrderService.CreateOrUpdateOrder(order2, db);
                OrderService.CreateOrUpdateOrder(order3, db);
                OrderService.CreateOrUpdateOrder(order4, db);
                OrderService.CreateOrUpdateOrder(order5, db);
                OrderService.CreateOrUpdateOrder(order6, db);
                OrderService.CreateOrUpdateOrder(order7, db);
                OrderService.CreateOrUpdateOrder(order8, db);
                db.ChangeTracker.DetectChanges();
                db.SaveChanges();
                var books = db.Books.ToList();
                foreach (var sdf in books)
                {
                    Console.WriteLine($"{sdf.Description}.{sdf.Name}");
                }
            }
        }
    }
}
