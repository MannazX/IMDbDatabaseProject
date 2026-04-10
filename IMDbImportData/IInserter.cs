using IMDbImportData.Models;
using Microsoft.Data.SqlClient;

namespace IMDbImportData
{
	public interface IInserter
	{
		void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn);
		void InsertGenres(List<TitleGenreModel> genres, SqlConnection sqlConn);
		void InsertNames(List<NameModel> titles, SqlConnection sqlConn);
		void InsertProfessions(List<ProfessionModel> professions, SqlConnection sqlConn);
		void InsertNameTitles(List<NameTitleModel> titleModels, SqlConnection sqlConn);
		void InsertCrewDirectors(List<CrewDirectorModel> directors, SqlConnection sqlConn);
		void InsertCrewWriters(List<CrewWriterModel> writers, SqlConnection sqlConn);
	}
}