using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbImportData.Models
{
	public class ProfessionModel
	{
		public int NConst { get; set; }
		public string PrimaryProfession { get; set; }

		public ProfessionModel(string[] professionInfo)
		{
			NConst = int.Parse(professionInfo[0].Substring(2));
			PrimaryProfession = professionInfo[1];
		}

		public override string ToString()
		{
			return NConst + " - " + PrimaryProfession;
		}
	}
}
