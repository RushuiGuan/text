using Bogus;

namespace Sample {
	public record class Job {
		public required string Company { get; set; }
		public required string Title { get; set; }
		public required int Years { get; set; }

		public static Job Random(Faker faker) {
			return new Job {
				Company = faker.Company.CompanyName(),
				Title   = faker.Name.JobTitle(),
				Years   = faker.Random.Int(1, 20)
			};
		}
	}
}