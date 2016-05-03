
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite ;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidInterview
{
			
	public class SQLite :SQLiteException
	{
		private string createDatabase(string path)
		{
			try
			{
				var connection = new SQLiteAsyncConnection(path);
				connection.CreateTableAsync<FeedItem>();
					return "Database created";
				}
				catch (SQLiteException ex)
				{
					return ex.Message;
				}
			}

		private string insertUpdateData(FeedItem data, string path)
		{
			try
			{
				var db = new SQLiteAsyncConnection(path);
				if (db.InsertAsync(data) != 0)
					db.UpdateAsync(data);
				return "Single data file inserted or updated";
			}
			catch (SQLiteException ex)
			{
				return ex.Message;
			}
		}
		private int findNumberRecords(string path)
		{
			try
			{
				var db = new SQLiteAsyncConnection(path);
				// this counts all records in the database, it can be slow depending on the size of the database
				var count = db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person");

				// for a non-parameterless query
				// var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

				return count;
			}
			catch (SQLiteException ex)
			{
				return -1;
			}
		}
	}
}

