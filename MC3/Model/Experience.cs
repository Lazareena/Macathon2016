using System;
using System.Collections.Generic;

namespace MC3
{
	public class Experience
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string OrganizationName { get; set; }
		public int OrganizationId { get; set; }
		public List<int> studentIds { get; set; }
		public TimePeriod TimeFrame { get; set; } 
		public bool Paid { get; set; }
	}

	public enum TimePeriod {
		Summer,
		JTerm,
		SchoolYear
	}
}

