using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRESTClient
{
	public class Person
	{
		public Int32 Id { get; set; }
		public String LName { get; set; }
		public String FName { get; set; }
		public Decimal PayRate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public String EmailAddress { get; set; }
	}
}
