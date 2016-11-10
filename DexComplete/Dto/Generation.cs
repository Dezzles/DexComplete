using DexComplete.Data;
using System.Collections.Generic;

namespace DexComplete.Dto
{
	public class Generation
	{
		public string GenerationId { get; set; }
		public IEnumerable<Berry> Berries { get; set; }
		public IEnumerable<AbilitySet> AbilitySets { get; set; }
		public Generation(Data.Generation generation)
		{
			GenerationId = generation.GenerationId;
			var berries = new List<Berry>();
			var abilities = new List<AbilitySet>();
			this.Berries = berries;
			this.AbilitySets = abilities;
			foreach (var v in generation.Berries)
			{
				berries.Add(new Berry(v.Berry));
			}

			foreach (var v in generation.AbilitySets)
			{
				abilities.Add(new AbilitySet(v));
			}
		}
	}
}