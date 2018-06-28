using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class SiteSqlDALTests : CampgroundDBTests
	{
		[TestMethod]
		public void GetSitesTest()
		{
			SiteSqlDAL dal = new SiteSqlDAL(ConnectionString);

			var site = dal.GetSites("1");

			Assert.AreEqual(1, site.Count);


		}
	}
}
