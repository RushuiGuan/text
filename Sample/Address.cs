using Bogus;

namespace Sample {
	public record class Address {
		public required string Street { get; set; }
		public required string City { get; set; }
		public required string State { get; set; }
		public required string Zip { get; set; }
		public required string Country { get; set; }


		public static Address Create(Faker faker) {
			return new Address {
				Street = faker.Address.StreetAddress(),
				City = faker.Address.City(),
				State = faker.Address.State(),
				Zip = faker.Address.ZipCode(),
				Country = faker.Address.Country()
			};
		}
	}
}
