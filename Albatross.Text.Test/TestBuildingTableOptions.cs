using Albatross.Text.Table;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Albatross.Text.Test {
	public class TestBuildingTableOptions {
		public class TestClass {
			public int Id { get; set; }
			public string? Name { get; set; }
			public double Value { get; set; }
		}
		[Fact]
		public void TestDefaultFormat() {
			object? obj = null;
			TableOptions.DefaultFormat(obj).Should().Be("");
			obj = 1;
			TableOptions.DefaultFormat(obj).Should().Be("1");
			obj = 1.0M;
			TableOptions.DefaultFormat(obj).Should().Be("1");
			obj = "1";
			TableOptions.DefaultFormat(obj).Should().Be("1");
			obj = 1.0;
			TableOptions.DefaultFormat(obj).Should().Be("1");
			obj = new DateOnly(2000, 1, 1);
			TableOptions.DefaultFormat(obj).Should().Be("2000-01-01");
			obj = new DateTime(2000, 1, 1, 1, 1, 1);
			TableOptions.DefaultFormat(obj).Should().Be("2000-01-01T01:01:01");
			obj = DateTime.SpecifyKind(new DateTime(2000, 1, 1, 1, 1, 1), DateTimeKind.Utc);
			TableOptions.DefaultFormat(obj).Should().Be("2000-01-01T01:01:01Z");
			obj = new DateTimeOffset(new DateTime(2000, 1, 1, 1, 1, 1), TimeSpan.FromHours(1));
			TableOptions.DefaultFormat(obj).Should().Be("2000-01-01T01:01:01+01:00");
		}

		[Fact]
		public void TestColumnOrders() {
			var options = new TableOptions<TestClass>().BuildColumnsByReflection().Cast<TestClass>();
			options.ColumnOptions.Select(x => new { x.Property, x.Order }).Should().BeEquivalentTo(new[] {
				new { Property = nameof(TestClass.Id), Order = 0 },
				new { Property = nameof(TestClass.Name), Order = 1 },
				new { Property = nameof(TestClass.Value), Order = 2 },
			});
			// test order by the Order property
			options.ColumnOrder(x => x.Value, -1);
			
			options.ColumnOptions.Select(x => new { x.Property, x.Order }).Should().BeEquivalentTo(new[] {
				new { Property = nameof(TestClass.Value), Order = -1 },
				new { Property = nameof(TestClass.Id), Order = 0 },
				new { Property = nameof(TestClass.Name), Order = 1 },
			});
			// test order by header
			options.ColumnOrder(x => x.Id, 0);
			options.ColumnOrder(x => x.Name, 0);
			options.ColumnOrder(x => x.Value, 0);
			options.ColumnHeader(x => x.Id, "C");
			options.ColumnHeader(x => x.Name, "A");
			options.ColumnHeader(x => x.Value, "B");

			options.ColumnOptions.Select(x => new { x.Property, x.Order, x.Header }).Should().BeEquivalentTo(new[] {
				new { Property = nameof(TestClass.Name), Order = 0, Header = "A" },
				new { Property = nameof(TestClass.Value), Order = 0, Header = "B" },
				new { Property = nameof(TestClass.Id), Order = 0, Header = "C" },
			});
			// test order by property
			options.ColumnOrder(x => x.Id, 0);
			options.ColumnOrder(x => x.Name, 0);
			options.ColumnOrder(x => x.Value, 0);
			options.ColumnHeader(x => x.Id, "C");
			options.ColumnHeader(x => x.Name, "C");
			options.ColumnHeader(x => x.Value, "C");

			options.ColumnOptions.Select(x => new { x.Property, x.Order, x.Header }).Should().BeEquivalentTo(new[] {
				new { Property = nameof(TestClass.Id), Order = 0, Header = "C" },
				new { Property = nameof(TestClass.Name), Order = 0, Header = "C" },
				new { Property = nameof(TestClass.Value), Order = 0, Header = "C" },
			});
		}

		[Fact]
		public void TestHeaders() {
			var options = new TableOptions<TestClass>().BuildColumnsByReflection().Cast<TestClass>();
			options.ColumnHeader(x => x.Id, "C");
			options.ColumnHeader(x => x.Name, "A");
			options.ColumnHeader(x => x.Value, "B");
			options.ColumnOptions.Select(x => new { x.Property, x.Header }).Should().BeEquivalentTo(new[] {
				new { Property = nameof(TestClass.Id), Header = "C" },
				new { Property = nameof(TestClass.Name), Header = "A" },
				new { Property = nameof(TestClass.Value), Header = "B" },
			});
		}

		[Fact]
		public void TestGetValueDelegate() {
			var options = new TableOptions<TestClass>().BuildColumnsByReflection();
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0 };
			var values = options.GetValue(obj);
			values.Select(x => x.Text).Should().BeEquivalentTo(new[] { "1", "name", "1" });
		}

		[Fact]
		public void TestFormatter() {
			var options = new TableOptions<TestClass>().BuildColumnsByReflection().Cast<TestClass>();
			options.Format(x => x.Value, "0.00");
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0 };
			var values = options.GetValue(obj);
			values.Select(x => x.Text).Should().BeEquivalentTo(new[] { "1", "name", "1.00" });
		}

		[Fact]
		public void TestBuilderFactory() {
			var options = new TableOptions<TestClass>().BuildColumnsByReflection();
			Assert.NotNull(options);
			TableOptionFactory.Instance.Register(new TableOptions<TestClass>());
			var options2 = TableOptionFactory.Instance.Get<TestClass>();
			Assert.NotSame(options, options2);
		}
	}
}