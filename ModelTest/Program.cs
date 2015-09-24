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
			/*using (PokedexModel mdlOld = new PokedexModel("oldModel"))
			{
				using (PokedexModel mdlNew = new PokedexModel("newModel"))
				{
					foreach (var u in mdlOld.Users)
					{
						var usr = mdlNew.Users.Create();
						mdlNew.Users.Add(usr);

						usr.Email = u.Email;
						usr.Password = u.Password;
						usr.Salt = u.Salt;
						usr.Username = u.Username;

						foreach ( var g in u.Saves)
						{
							var save = mdlNew.Saves.Create();
							usr.Saves.Add(save);

							save.AbilityData = g.AbilityData;
							save.BerryData = g.BerryData;
							save.Code = g.Code;
							save.DittoData = g.DittoData;
							save.EggGroupData = g.EggGroupData;
							save.GameId = g.GameId;
							save.SaveName = g.SaveName;
							save.TMData = g.TMData;
							save.User = usr;
						}
					}

					mdlNew.SaveChanges();
				}
			}
			/**/
		}
	}
}
