using Albatross.Text.Table;
using System;
using System.IO;
using Xunit;

namespace Albatross.Text.Test {
	public class TestPrint {
		public class Product {
			public string Name { get; set; }
			public string? Category { get; set; }
			public int Weight { get; set; }
			public DateTime CreatedDateTime { get; set; }
			public DateTime Expired { get; set; }
			public Product(string name) {
				this.Name = name;
			}
		}

		public static Product[] GetProducts() {
			return new Product[] {
				new Product("apple") {
					Weight = 1000,
					Category = "fruits",
					CreatedDateTime = new DateTime(2000, 1,1, 23,1,50),
					Expired = new DateTime(2000, 7,1).AddDays(1),
				},
				new Product("orange") {
					Weight = 800,
					CreatedDateTime = new DateTime(2000, 7,1, 2,3,3),
					Expired = new DateTime(2000, 7,1).AddDays(2),
				},
				new Product("desk") {
					Weight = 200,
					Category = "furniture",
					CreatedDateTime = new DateTime(2000, 2,1, 5,5,4),
					Expired = new DateTime(2000, 7,1).AddDays(3),
				}
			};
		}
		const string expectedPrintTableWithDefault =
@"
Name   Category  Weight CreatedDateTime     Expired   
------------------------------------------------------
apple  fruits    1000   2000-01-01 23:01:50 2000-07-02
orange           800    2000-07-01 02:03:03 2000-07-03
desk   furniture 200    2000-02-01 05:05:04 2000-07-04
------------------------------------------------------
";

		[Fact]
		public void PrintTableWithDefaultOptions() {
			StringWriter writer = new StringWriter();
			var products = GetProducts();
			var options = new TableOptions<Product>()
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Category)
				.SetColumn(x => x.Weight)
				.SetColumn(x => x.CreatedDateTime)
				.Format(x => x.CreatedDateTime, (_, value) => new TextValue($"{value:yyyy-MM-dd HH:mm:ss}"))
				.SetColumn(x => x.Expired)
				.Format(x => x.Expired, (_, value) => new TextValue($"{value:yyyy-MM-dd}"));

			products.StringTable(options).Print(writer);
			Assert.Equal(expectedPrintTableWithDefault.TrimStart(), writer.ToString());
		}

		const string expectedPrintTableWithTruncate =
		@"
Name   Category  Weight CreatedDateTime     Expire
--------------------------------------------------
apple  fruits    1000   2000-01-01 23:01:50 2000-0
orange           800    2000-07-01 02:03:03 2000-0
desk   furniture 200    2000-02-01 05:05:04 2000-0
--------------------------------------------------
";

		[Fact]
		public void PrintTableWithTruncate() {
			StringWriter writer = new StringWriter();
			var products = GetProducts();
			var options = new TableOptions<Product>()
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Category)
				.SetColumn(x => x.Weight)
				.SetColumn(x => x.CreatedDateTime)
				.Format(x => x.CreatedDateTime, (_, value) => new TextValue($"{value:yyyy-MM-dd HH:mm:ss}"))
				.SetColumn(x => x.Expired)
				.Format(x => x.Expired, (_, value) => new TextValue($"{value:yyyy-MM-dd}"));

			products.StringTable(options).SetWidthLimit(50).Print(writer);
			Assert.Equal(expectedPrintTableWithTruncate.TrimStart(), writer.ToString());
		}

		const string expectedPrintTableWithHiddenColumn =
		@"
Name   Weight Created
---------------------
apple  1000   2000-01
orange 800    2000-07
desk   200    2000-02
---------------------
";

		[Fact]
		public void PrintTableWithHiddenColumn() {
			StringWriter writer = new StringWriter();
			var products = GetProducts();
			var options = new TableOptions<Product>()
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Category)
				.SetColumn(x => x.Weight)
				.SetColumn(x => x.CreatedDateTime)
				.Format(x => x.CreatedDateTime, (_, value) => new TextValue($"{value:yyyy-MM-dd HH:mm:ss}"))
				.SetColumn(x => x.Expired)
				.Format(x => x.Expired, (_, value) => new TextValue($"{value:yyyy-MM-dd}"));

			products.StringTable(options)
				.ChangeColumn(x => x.Name == "Category", x => x.MinWidth = 0)
				.SetWidthLimit(21).Print(writer);
			Assert.Equal(expectedPrintTableWithHiddenColumn.TrimStart(), writer.ToString());
		}
		const string expectedPrintTableWithTruncatedColumn=
		@"
Name   Category  Weight CreatedDat Expi
---------------------------------------
apple  fruits    1000   2000-01-01 2000
orange           800    2000-07-01 2000
desk   furniture 200    2000-02-01 2000
---------------------------------------
";

		[Fact]
		public void PrintTableWithTruncatedColumn() {
			StringWriter writer = new StringWriter();
			var products = GetProducts();
			var options = new TableOptions<Product>()
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Category)
				.SetColumn(x => x.Weight)
				.SetColumn(x => x.CreatedDateTime)
				.Format(x => x.CreatedDateTime, (_, value) => new TextValue($"{value:yyyy-MM-dd HH:mm:ss}"))
				.SetColumn(x => x.Expired)
				.Format(x => x.Expired, (_, value) => new TextValue($"{value:yyyy-MM-dd}"));

			products.StringTable(options)
				.ChangeColumn(x => x.Name == "CreatedDateTime", x => x.MinWidth = 10)
				.ChangeColumn(x => x.Name == "Expired", x => x.MinWidth = 4)
				.SetWidthLimit(39)
				.Print(writer);
			Assert.Equal(expectedPrintTableWithTruncatedColumn.TrimStart(), writer.ToString());
		}
	}
}
