using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Campground
	{
		public int CampgroundId { get; set; }
		public string Name { get; set; }
		public int OpenMonth { get; set; }
		public int CloseMonth { get; set; }
		public decimal DailyFee { get; set; }
		public int ParkId { get; set; }


	}
}
