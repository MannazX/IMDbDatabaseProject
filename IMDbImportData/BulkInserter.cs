using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class BulkInserter : IInserter
	{
		public void InsertGenres(List<GenreModel> genres, SqlConnection sqlConn)
		{
			DataTable genreTable = new DataTable();
			genreTable.Columns.Add("TConst", typeof(int));
			genreTable.Columns.Add("Genre", typeof(string));

			foreach (GenreModel genre in genres)
			{
				genreTable.Rows.Add(genre.TConst, genre.Genre);
			}
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn)
			{
				DestinationTableName = "Genres"
			};
			bulkCopy.WriteToServer(genreTable);
		}

		public void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn)
		{
			DataTable titleTable = new DataTable();
			titleTable.Columns.Add("TConst", typeof(int));
			titleTable.Columns.Add("TitleType", typeof(string));
			titleTable.Columns.Add("PrimaryTitle", typeof(string));
			titleTable.Columns.Add("OriginalTitle", typeof(string));
			titleTable.Columns.Add("IsAdult", typeof(bool));
			titleTable.Columns.Add("StartYear", typeof(int));
			titleTable.Columns.Add("EndYear", typeof(int));
			titleTable.Columns.Add("RunTimeMinutes", typeof(int));

			foreach (TitleModel movie in titles)
			{
				titleTable.Rows.Add(movie.TConst,
					movie.TitleType,
					movie.PrimaryTitle,
					movie.OriginalTitle,
					movie.IsAdult,
					movie.StartYear ?? (object)DBNull.Value,
					movie.EndYear ?? (object)DBNull.Value,
					movie.RunTimeMinutes ?? (object)DBNull.Value);
			}
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn)
			{
				DestinationTableName = "Titles"
			};
			bulkCopy.WriteToServer(titleTable);
		}
	}
}
