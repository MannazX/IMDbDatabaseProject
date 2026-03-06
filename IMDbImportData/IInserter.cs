using Microsoft.Data.SqlClient;

namespace IMDbImportData
{
	public interface IInserter
	{
		void InsertTitles(List<TitleModel> titles, SqlConnection sqlConn);
	}
}