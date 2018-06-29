using System;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class ReservationSqlDALTests : CampgroundDBTests
	{
		[TestMethod]
		public void GetReservationsTest()
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(ConnectionString);

			var reservations = dal.GetReservations(1, 1, 1);

			Assert.AreEqual(1, reservations.Count);
		}

		[TestMethod]
		public void AddReservationTest()
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(ConnectionString);

			int initialCount = GetRowCount();

			Reservation reservation = new Reservation();

			reservation.ReservationId = 2;
			reservation.SiteId = 1;
			reservation.Name = "Art Vandelay Reservation";
			reservation.StartDate = new DateTime(2018, 07, 01);
			reservation.EndDate = new DateTime(2018, 07, 10);
			reservation.CreateDate = DateTime.Now;

			DateTime[] dateRange = new DateTime[2] { reservation.StartDate, reservation.EndDate };

			var reservations = dal.AddReservation(reservation.SiteId, reservation.Name, dateRange);

			int finalCount = GetRowCount();

			Assert.AreEqual(initialCount + 1, finalCount);
		}

		private int GetRowCount()
		{
			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM reservation", conn);

				int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

				return rowCount;
			}
		}
	}
}
