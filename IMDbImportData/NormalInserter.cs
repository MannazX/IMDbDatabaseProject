using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class NormalInserter : IInserter
	{
		public void InsertGenres(List<GenreModel> genres, SqlConnection sqlConn)
		{
			foreach (GenreModel genre in genres)
			{
				string query = "INSERT INTO Genres (" +
					"TConst, " +
					"Genre) " +
								"VALUES (" + genre.TConst + ", " +
								"'" + genre.Genre + "'" + ")";
				try
				{
					SqlCommand sqlComm = new SqlCommand(query, sqlConn);
					sqlComm.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Console.WriteLine(query);
					Console.WriteLine("Error: " + ex.Message);
				}
			}
		}

		public void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn)
		{
			foreach (TitleModel movie in titles)
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
								"VALUES (" +
								movie.TConst + ", " +
								"'" + movie.TitleType + "', " +
								"'" + movie.PrimaryTitle + "', " +
								"'" + movie.OriginalTitle + "', " +
								"'" + movie.IsAdult + "', " +
								ConvertIntToString(movie.StartYear) + ", " +
								ConvertIntToString(movie.EndYear) + ", " +
								ConvertIntToString(movie.RunTimeMinutes) + ")";
				try
				{
					SqlCommand sqlComm = new SqlCommand(query, sqlConn);
					sqlComm.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Console.WriteLine(query);
					Console.WriteLine("Error: " + ex.Message);
				}
			}
		}
		private string ConvertIntToString(int? value)
		{
			return value.HasValue ? value.Value.ToString() : "Null";
		}
	}
}
