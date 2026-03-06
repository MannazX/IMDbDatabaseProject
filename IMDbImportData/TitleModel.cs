using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace IMDbImportData
{
	public class TitleModel
	{
		public int TConst { get; set; }
		public string TitleType { get; set; }
		public string PrimaryTitle { get; set; }
		public string OriginalTitle { get; set; }
		public bool IsAdult { get; set; }
		public int? StartYear { get; set; }
		public int? EndYear { get; set; }
		public int? RunTimeMinutes { get; set; }

		public TitleModel(string[] titleInfo)
		{
			TConst = Convert.ToInt32(titleInfo[0].Substring(2));
			TitleType = titleInfo[1];
			PrimaryTitle = titleInfo[2].Replace("'", "''");
			OriginalTitle = titleInfo[3].Replace("'", "''");
			IsAdult = titleInfo[4] == "1";
			StartYear = ParseNullableInt(titleInfo[5]);
			EndYear = ParseNullableInt(titleInfo[6]);
			RunTimeMinutes = ParseNullableInt(titleInfo[7]);
		}

		private int? ParseNullableInt(string value)
		{
			if (int.TryParse(value, out int result))
			{
				return result;
			}
			return null;
		}

		public override string ToString()
		{
			return TConst + " - " + PrimaryTitle;
		}
	}
}