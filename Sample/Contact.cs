using Bogus;

namespace Sample {
	public record class Contact {
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required int AgeInDays { get; set; }
		public Address[] Address { get; set; } = [];
		public string[] Email { get; set; } = [];
		public string[] Phone { get; set; } = [];
		public int[] Scores { get; set; } = [];
		public Job? Job { get; set; }

		public DateOnly Dob => DateOnly.FromDateTime(DateTime.Today).AddDays(AgeInDays * -1);

		public static Contact Create(Faker faker) {
			var hasJob = faker.Random.Int(0, 10) < 6;
			return new Contact {
				FirstName = faker.Name.FirstName(),
				LastName = faker.Name.LastName(),
				AgeInDays = faker.Random.Int(1, 100 * 365),
				Address = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => Sample.Address.Create(faker)).ToArray(),
				Email = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => faker.Internet.Email()).ToArray(),
				Phone = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => faker.Phone.PhoneNumber()).ToArray(),
				Scores = Enumerable.Range(1, faker.Random.Int(1, 3)).Select(_ => faker.Random.Int(0, 100)).ToArray(),
				Job = hasJob ? Job.Random(faker) : null,
			};
		}
	}
}