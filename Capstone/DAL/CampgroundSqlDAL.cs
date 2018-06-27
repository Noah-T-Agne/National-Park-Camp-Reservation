using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class CampgroundSqlDAL
	{
		private readonly string connectionString;

		public CampgroundSqlDAL(string databaseConnection)
		{
			connectionString = databaseConnection;
		}

		//public IList<Campground> GetCampgrounds()
		//{

		//}



	}
}
