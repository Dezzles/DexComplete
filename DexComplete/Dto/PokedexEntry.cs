namespace DexComplete.Dto
{
	public class PokedexEntry
	{
		public int Index { get; set; }
		public string Name { get; set; }

		public PokedexEntry(Data.PokedexEntry Entry)
		{
			this.Index = Entry.Index;
			this.Name = Entry.Pokemon.Name;
		}
	}
}