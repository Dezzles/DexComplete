using System;
using NUnit.Framework;

namespace DexComplete.Test.Dto
{
	[TestFixture]
	public class AbilityEntry
	{
		[Test]
		public void LoadsAllDataCorrectly()
		{
			var data = Constructs.Data.AbilityEntry.Data_001();
			var test = new DexComplete.Dto.AbilityEntry(data);

			Assert.That(test.Ability, Is.EqualTo(data.Ability.Name));
			Assert.That(test.Pokemon, Is.EqualTo(data.Pokemon.Name));
			Assert.That(test.Index, Is.EqualTo(data.Index));
			Assert.That(test.Note, Is.EqualTo(data.Note));
		}
	}
}
