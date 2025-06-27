namespace Sample {
	public record class Contact {
		public string Name { get; set; } = string.Empty;
		public int Age { get; set; }
		public Address? Address { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
	}
}