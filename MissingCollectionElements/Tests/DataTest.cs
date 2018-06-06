using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MissingCollectionElements.Model;
using Xunit;

namespace MissingCollectionElements.Tests
{
    public class DataTest
    {
        public DataTest()
        {
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);

            _options = optionsBuilder.Options;

            using (var context = CreateContext())
            {
                context.Database.EnsureCreated();
            }
        }

        private readonly DbContextOptions<DataContext> _options;

        private DataContext CreateContext()
        {
            return new DataContext(_options);
        }


        [Fact]
        public void AddAndRemoveFromCollection()
        {
            // Given
            var container = new Container();
            var itemA = new Item();
            var itemB = new Item();

            container.ItemLinks = new List<Link>
            {
                new Link
                {
                    Item = itemA
                }
            };

            using (var context = CreateContext())
            {
                context.AddRange(itemA, itemB, container);
                context.SaveChanges();
            }

            // When
            using (var context = CreateContext())
            {
                var dbContainer = context.Set<Container>()
                                         .Include(c => c.ItemLinks)
                                         .ThenInclude(l => l.Item)
                                         .Single(c => c.Id == container.Id);

                dbContainer.ItemLinks = new List<Link>
                {
                    new Link
                    {
                        ItemId = itemA.Id
                    },
                    new Link
                    {
                        ItemId = itemB.Id
                    }
                };

                dbContainer.ItemLinks.Should().HaveCount(2);

                context.SaveChanges();

                // dbContainer.ItemLinks.Should().HaveCount(2); // <<-- Fail here because ItemLinks collection have just one element

                var dbContainer2 = context.Set<Container>()
                                          .Include(c => c.ItemLinks)
                                          .ThenInclude(l => l.Item)
                                          .Single(c => c.Id == container.Id);

                // dbContainer2.ItemLinks.Should().HaveCount(2); // <<-- Fail here because ItemLinks collection have just one element
            }

            // Then
            using (var context = CreateContext())
            {
                context.Set<Link>().Should().HaveCount(2);
                var dbContainer = context.Set<Container>()
                                         .Include(c => c.ItemLinks)
                                         .ThenInclude(l => l.Item)
                                         .Single(c => c.Id == container.Id);

                dbContainer.ItemLinks.Should().HaveCount(2);
            }
        }
    }
}