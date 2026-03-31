using IMDbImportData.Models;
using Microsoft.Data.SqlClient;

namespace IMDbImportData
{
	public interface IInserter
	{
		void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn);
		void InsertGenres(List<GenreModel> genres, SqlConnection sqlConn);
		void InsertNames(List<NameModel> titles, SqlConnection sqlConn);
		void InsertProfessions(List<ProfessionModel> professions, SqlConnection sqlConn);
		void NameTitleProfessions(List<NameTitleModel> titleModels, SqlConnection sqlConn);
	}
}