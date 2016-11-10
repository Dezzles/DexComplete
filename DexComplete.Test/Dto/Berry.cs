using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Dto
{
	[TestFixture]
	class Berry
	{
		[Test, TestOf(typeof(DexComplete.Dto.Berry))]
		public void LoadsAllDataCorrectly()
		{
			var data = Constructs.Data.Berry.Data_001();
			var test = new DexComplete.Dto.Berry(data);

			Assert.That(test.BerryId, Is.EqualTo(data.BerryId));
			Assert.That(test.Index, Is.EqualTo(data.Index));
			Assert.That(test.Name, Is.EqualTo(data.Name));
		}
	}
}
