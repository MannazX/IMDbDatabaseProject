using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData
{
	public class GenreModel
	{
		public int TConst { get; set; }
		public string Genre { get; set; }

		public GenreModel(string[] genreInfo)
		{
			TConst = int.Parse(genreInfo[0].Substring(2));
			Genre = genreInfo[1];
		}

		public override string ToString()
		{
			return TConst + " - " + Genre;
		}
	}
}
