using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DexComplete.Data;
namespace ModelTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (PokedexModel ctr = new PokedexModel())
			{
				var ability = ctr.Abilities.Single(e => e.Id==2);
				var entry = ctr.AbilityEntries.Where(e => e.AbilityId == ability.Id);
				if (ability.Entries == null)
					throw new Exception();
			}
		}
	}
}
