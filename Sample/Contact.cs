using Bogus;

namespace Sample {
	public record class Contact {
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required int Age { get; set; }
		public Address[] Address { get; set; } = [];
		public string[] Email { get; set; } = [];
		public string[] Phone { get; set; } = [];
		public int[] Scores { get; set; } = [];

		public static Contact Random(Faker faker) {
			return new Contact {
				FirstName = faker.Name.FirstName(),
				LastName = faker.Name.LastName(),
				Age = faker.Random.Int(1, 100),
				Address = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => Sample.Address.Random(faker)).ToArray(),
				Email = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => faker.Internet.Email()).ToArray(),
				Phone = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => faker.Phone.PhoneNumber()).ToArray(),
				Scores = Enumerable.Range(1, faker.Random.Int(1, 10)).Select(_ => faker.Random.Int(0, 100)).ToArray()
			};
		}
	}
}