using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class PreparedInserter : IInserter
	{
		public void InsertGenres(List<GenreModel> genres, SqlConnection sqlConn)
		{
			string query = "INSERT INTO Genres (" +
				"TConst, " +
				"Genre)" +
					"VALUES (@TConst, @Genre)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (GenreModel genre in genres)
			{
				sqlComm.Parameters.AddWithValue("@TConst", genre.TConst);
				sqlComm.Parameters.AddWithValue("@Genre", genre.Genre);
			}
			try
			{
				sqlComm.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Error inserting query:\r\n" + query);
			}
		}

		public void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn)
		{
			string query = "INSERT INTO Titles (" +
					"TConst, " +
					"TitleType, " +
					"PrimaryTitle, " +
					"OriginalTitle, " +
					"IsAdult, " +
					"StartYear, " +
					"EndYear, " +
					"RunTimeMinutes) " +
								"VALUES (@TConst, @TitleType, @PrimaryTitle, " +
								"@OriginalTitle, @IsAdult, @StartYear, @EndYear, " +
								"@RunTimeMinutes)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (TitleModel movie in titles)
			{
				sqlComm.Parameters.Clear();
				sqlComm.Parameters.AddWithValue("@TConst", movie.TConst);
				sqlComm.Parameters.AddWithValue("@TitleType", movie.TitleType);
				sqlComm.Parameters.AddWithValue("@PrimaryTitle", movie.PrimaryTitle);
				sqlComm.Parameters.AddWithValue("@OriginalTitle", movie.OriginalTitle);
				sqlComm.Parameters.AddWithValue("@IsAdult", movie.IsAdult);
				sqlComm.Parameters.AddWithValue("StartYear", movie.StartYear);
				sqlComm.Parameters.AddWithValue("@EndYear", movie.EndYear);
				sqlComm.Parameters.AddWithValue("@RuntimeMinutes", movie.RunTimeMinutes);
			}
			try
			{
				sqlComm.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Error inserting query:\r\n" + query);
			}
		}
	}
}
