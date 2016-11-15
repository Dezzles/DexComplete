namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PokedexEntry
    {
        public int PokedexEntryId { get; set; }

        public int Index { get; set; }

        public string PokedexId { get; set; }

        public string PokemonId { get; set; }

        public virtual Pokemon Pokemon { get; set; }

        public virtual Pokedex Pokedex { get; set; }
    }
}
