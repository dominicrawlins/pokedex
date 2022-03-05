using System.Linq;
using System.Collections.Generic;

public class Pokemon{
    public string Name { get; private set; }
    public string Description { get; set; }
    public string Habitat { get; private set; }
    public bool IsLegendary { get; private set; }

    public Pokemon(PokemonInformationResponse pokemonDto){
        Name = ExtractName(pokemonDto.Names);
        Description = ExtractDescription(pokemonDto.FlavorTextEntries);
        Habitat = pokemonDto.Habitat.Name;
        IsLegendary = pokemonDto.IsLegendary;
    }

    private string ExtractName(IEnumerable<PokemonNameResponse> names){
        return names?.Where(n => n.Language.Name == "en").FirstOrDefault()?.Name ?? "Unknown name";
    }

    private string ExtractDescription(IEnumerable<FlavorTextEntryResponse> flavorTextEntries){
        var rawDescription = flavorTextEntries?.Where(f => f.Language.Name == "en").FirstOrDefault()?.FlavorText ?? "Unknown description";
        return rawDescription.Clean();
    }
}