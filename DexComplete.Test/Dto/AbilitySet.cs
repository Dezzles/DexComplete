using NUnit.Framework;
using System.Linq;
namespace DexComplete.Test.Dto
{
	[TestFixture]
	class AbilitySet
	{
		[Test]
		public void LoadsAllDataCorrectly()
		{
			var data = Constructs.Data.AbilitySet.Data_001();
			var test = new DexComplete.Dto.AbilitySet(data);

			Assert.That(test.AbilityId, Is.EqualTo(data.AbilityId));
			Assert.That(test.PokemonId, Is.EqualTo(data.PokemonId));
			Assert.That(test.Entries.Count(), Is.EqualTo(data.Entries.Count));
			Assert.That(test.Entries.ElementAt(0), Is.Not.EqualTo(data.Entries.ElementAt(1)));
			
		}
	}
}
