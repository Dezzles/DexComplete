using DexComplete.Data;

namespace DexComplete.Dto
{
	public class Collection
	{
		public string Title { get; set; }
		public string CollectionId { get; set; }
		public Data.CollectionType Type { get; set; }

		public Collection(Data.Collection v)
		{
			this.Title = v.Title;
			this.CollectionId = v.CollectionId;
			this.Type = v.Type;
		}
	}
}