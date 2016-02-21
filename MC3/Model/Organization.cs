using System;
using System.Collections.Generic;

namespace MC3
{
	public class Organization
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public List<User> Students { get; set; }
		public List<Experience> Positions { get; set; }
	}
}

