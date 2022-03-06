using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

public class PokeApiService : IPokeApiService{
    private readonly HttpClient httpClient;
    private readonly ITranslationService translator;
    public PokeApiService(ITranslationService translator){
        httpClient = new HttpClient();
        this.translator = translator;
    }

    public async Task<IEnumerable<Pokemon>> GetAllPokemon(){
        var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon-species");
        var responseContent = await response.Content.ReadAsStringAsync();

        var allPokemon = JsonConvert.DeserializeObject<PokemonEndpointResponse>(responseContent);

        var firstPokemonUrl = allPokemon.Results.First().Url;
        var pokemonTasks = allPokemon.Results.Select(r => GetPokemonTask(r.Url));

        await Task.WhenAll(pokemonTasks);

        return pokemonTasks.Select(task => task.Result);        
    }

    public async Task<IEnumerable<Pokemon>> GetAllTranslatedPokemon(){
        var pokemons = await GetAllPokemon();
        pokemons = await translator.TranslateAllPokemon(pokemons);
        return pokemons;
    }

    private async Task<Pokemon> GetPokemonTask(string url){
        var response = await httpClient.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var pokemonResponse = JsonConvert.DeserializeObject<PokemonInformationResponse>(responseContent);

        return new Pokemon(pokemonResponse);
    }
}