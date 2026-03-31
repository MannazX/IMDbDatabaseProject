using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class GenreModel
	{
		public int TConst { get; set; }
		public string Genre { get; set; }

		public GenreModel(string[] genreInfo)
		{
			TConst = Convert.ToInt32(genreInfo[0].Substring(2));
			Genre = genreInfo[1];
		}

		public override string ToString()
		{
			return TConst + " - " + Genre;
		}
	}
}
