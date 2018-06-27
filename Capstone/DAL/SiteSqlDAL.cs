using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
	public class SiteSqlDAL
	{
		private readonly string connectionString;

		public SiteSqlDAL(string databaseConnectionString)
		{
			connectionString = databaseConnectionString;
		}

		//public IList<Site> GetSites()
		//{

		//}



	}
}
