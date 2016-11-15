using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using DexComplete.Dto;

namespace DexComplete.Test.Services
{
	[TestFixture]
	public class AbilityService
	{
		[Test]
		public void ReturnsList()
		{
			SharpLogging.SharpLoggingStub mockStub = new SharpLogging.SharpLoggingStub();
			var Log = mockStub.Instance();
			var moq = new Moq.Mock<DexComplete.Repository.Abilities>( new object[] { null });
			var lst = new List<DexComplete.Dto.AbilitySet>();
			lst.Add(new DexComplete.Dto.AbilitySet(Constructs.Data.AbilitySet.Data_001()));
			lst.Add(new DexComplete.Dto.AbilitySet(Constructs.Data.AbilitySet.Data_002()));
			moq.Setup(u => u.GetAbilitiesByGame("")).Returns(lst);/**/
			var test = new DexComplete.Services.AbilityService(moq.Object, Log);/**/
			var result = test.GetAbilitiesByGame("");

			Assert.That(result.Count(), Is.EqualTo(2));
		}
	}
}
