using Newtonsoft.Json;

public class PokemonNameResponse{
    public PokemonUrlResponse Language { get; set; }
    public string Name { get; set; }

    public PokemonNameResponse(){ }
}