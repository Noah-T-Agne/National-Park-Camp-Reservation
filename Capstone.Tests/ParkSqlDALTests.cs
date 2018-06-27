using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class ParkSqlDALTests:CampgroundDBTests
	{
		[TestMethod]
		public void GetParksTest()
		{
			ParkSqlDAL dal = new ParkSqlDAL(ConnectionString);

			var park = dal.GetParks();

			Assert.AreEqual(1, park.Count);

		}
	}
}
