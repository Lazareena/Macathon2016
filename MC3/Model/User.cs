using System;
using System.Collections.Generic;

namespace MC3
{
	public class User
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Major { get; set; }
		public int ClassYear { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public List<Experience> Experiences { get; set; }
	}
}

