using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Site
	{
		public int SiteId { get; set; }
		public int MaxOccupancy { get; set; }
		public int SiteNumber { get; set; }
		public bool IsAccessible { get; set; }
		public int MaxRVLength { get; set; }
		public bool HasUtilities { get; set; }
		public int CampgroundId { get; set; }
		public decimal DailyFee { get; set; }

		public string CampgroundName { get; set; }
		public int CampgroundOpenMonth { get; set; }
		public int CampgroundCloseMonth { get; set; }
		public int ParkId { get; set; }
		public string ParkName { get; set; }
		public string ParkLocation { get; set; }
	}
}
