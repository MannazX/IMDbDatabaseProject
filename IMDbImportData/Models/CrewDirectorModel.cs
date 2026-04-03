using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class CrewDirectorModel
	{
		public int TConst { get; set; }
		public int NConst { get; set; }

		public CrewDirectorModel(string[] directors)
		{
			TConst = Convert.ToInt32(directors[0].Substring(2));
			NConst = Convert.ToInt32(directors[1].Substring(2));
		}

		public override string ToString()
		{
			return TConst + " - " + NConst;
		}
	}
}
