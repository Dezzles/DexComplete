using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Dto
{
	public class AbilityEntry
	{
		public int Index { get; set; }
		public String Note { get; set; }
		public String Pokemon { get; set; }
		public String Ability { get; set; }

		public AbilityEntry(Data.AbilityEntry Entry)
		{
			this.Index = Entry.Index;
			this.Note = Entry.Note;
			this.Pokemon = Entry.Pokemon.Name;
			this.Ability = Entry.Ability.Name;
		}
	}
}