using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CampgroundDBTests
	{
		public const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";
		private TransactionScope transaction;

		[TestInitialize]
		public void SetupData()
		{
			transaction = new TransactionScope();

			string sql = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "database.sql"));

			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
			}
		}

		[TestCleanup]
		public void CleanupData()
		{
			transaction.Dispose();
		}
	}
}
