using IMDbImportData.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Inserters
{
	public class PreparedInserter : IInserter
	{
		public void InsertGenres(List<GenreModel> genres, SqlConnection sqlConn)
		{
			string query = "INSERT INTO Genres (" +
				"TConst, " +
				"Genre) " +
					"VALUES (@TConst, @Genre)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (GenreModel genre in genres)
			{
				sqlComm.Parameters.Clear();
				sqlComm.Parameters.AddWithValue("@TConst", genre.TConst);
				sqlComm.Parameters.AddWithValue("@Genre", genre.Genre);
				try
				{
					sqlComm.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Console.WriteLine("Error inserting query:\r\n" + query + " " + ex.Message);
				}
			}
		}

		public void InsertNames(List<NameModel> names, SqlConnection sqlConn)
		{
			string query = "INSERT INTO Names (" +
							"NConst, " +
							"PrimaryName, " +
							"BirthYear, " +
							"DeathYear) " +
								"VALUES (@NConst, @PrimaryName, @BirthYear, @DeathYear)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (NameModel name in names)
			{
				sqlComm.Prepare();
				sqlComm.Parameters.AddWithValue("@NConst", name.NConst);
				sqlComm.Parameters.AddWithValue("@PrimaryName", name.PrimaryName);
				sqlComm.Parameters.AddWithValue("@BirthYear", (object)name.BirthYear ?? DBNull.Value);
				sqlComm.Parameters.AddWithValue("@DeathYear", (object)name.DeathYear ?? DBNull.Value);
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

		public void InsertProfessions(List<ProfessionModel> professions, SqlConnection sqlConn)
		{
			string query = "INSERT INTO Professions (" +
							"NConst, " +
							"PrimaryProfession) " +
								"VALUES (@NConst, @PrimaryProfession)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (ProfessionModel profession in professions)
			{
				sqlComm.Prepare();
				sqlComm.Parameters.AddWithValue("@NConst", profession.NConst);
				sqlComm.Parameters.AddWithValue("@PrimaryProfession", profession.PrimaryProfession);
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
				sqlComm.Parameters.AddWithValue("@StartYear", (object)movie.StartYear ?? DBNull.Value);
				sqlComm.Parameters.AddWithValue("@EndYear", (object)movie.EndYear ?? DBNull.Value);
				sqlComm.Parameters.AddWithValue("@RuntimeMinutes", (object)movie.RunTimeMinutes ?? DBNull.Value);
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

		public void InsertNameTitles(List<NameTitleModel> nameTitleModels, SqlConnection sqlConn)
		{
			string query = "INSERT INTO NameTitles (" +
							"NConst, " +
							"TConst) " +
								"VALUES (@NConst, @TConst)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (NameTitleModel nameTitle in nameTitleModels)
			{
				sqlComm.Prepare();
				sqlComm.Parameters.AddWithValue("@NConst", nameTitle.NConst);
				sqlComm.Parameters.AddWithValue("@TConst", nameTitle.TConst);
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

		public void InsertCrewDirectors(List<CrewDirectorModel> directors, SqlConnection sqlConn)
		{
			string query = "INSERT INTO CrewDirectors (" +
				"TConst, " +
				"NConst) " +
					"VALUES (@TConst, @NConst)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (CrewDirectorModel director in directors)
			{
				sqlComm.Parameters.Clear();
				sqlComm.Parameters.AddWithValue("@TConst", director.TConst);
				sqlComm.Parameters.AddWithValue("@NConst", director.NConst);
				try
				{
					sqlComm.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Console.WriteLine("Error inserting query:\r\n" + query + " " + ex.Message);
				}
			}
		}

		public void InsertCrewWriters(List<CrewWriterModel> writers, SqlConnection sqlConn)
		{
			string query = "INSERT INTO CrewWriters (" +
				"TConst, " +
				"NConst) " +
					"VALUES (@TConst, @NConst)";
			SqlCommand sqlComm = new SqlCommand(query, sqlConn);
			sqlComm.Prepare();
			foreach (CrewWriterModel writer in writers)
			{
				sqlComm.Parameters.Clear();
				sqlComm.Parameters.AddWithValue("@TConst", writer.TConst);
				sqlComm.Parameters.AddWithValue("@NConst", writer.NConst);
				try
				{
					sqlComm.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Console.WriteLine("Error inserting query:\r\n" + query + " " + ex.Message);
				}
			}
		}
	}
}
