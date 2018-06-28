using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CampgroundSqlDALTests : CampgroundDBTests
	{
		[TestMethod]
		public void GetCampgroundsTest()
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(ConnectionString);

			var campground = dal.GetCampgrounds(1);

			Assert.AreEqual(1, campground.Count);
		}
	}
}
