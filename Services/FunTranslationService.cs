using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class FunTranslationService : ITranslationService{
    private readonly HttpClient httpClient;
    private static Dictionary<string, string> cachedTranslations;
    private static DateTime cacheExpiry;

    public FunTranslationService(){
        httpClient = new HttpClient();
        cachedTranslations = new Dictionary<string, string>();
    }

    public async Task<IEnumerable<Pokemon>> TranslateAllPokemon(IEnumerable<Pokemon> pokemons){
        var translationsNeeded = pokemons.Select(p => p.Description);
        if(cacheExpiry < DateTime.UtcNow){
            await UpdateCache(translationsNeeded);
        }        
        UpdatePokemonDescriptions(pokemons.ToList());
        
        return pokemons;
    }

    private async Task UpdateCache(IEnumerable<string> translationsNeeded){
        cachedTranslations = new Dictionary<string, string>();
        cacheExpiry = DateTime.UtcNow.AddHours(1);

        var translations = await GetTranslations(translationsNeeded);
        if(translations == null){
            return;
        }

        for(int i = 0; i < translations.Count; i++){
            var originalText = translationsNeeded.ElementAt(i);
            var translation = translations[i];

            if(cachedTranslations.ContainsKey(originalText)){
                cachedTranslations[originalText] = translation.ToString();
                continue;
            }

            cachedTranslations.Add(originalText, (string)translation);
        }
    }

    private async Task<JArray> GetTranslations(IEnumerable<string> translationsNeeded){
        var requestMessage = new HttpRequestMessage();
        requestMessage.Method = HttpMethod.Post;
        requestMessage.RequestUri = new System.Uri("https://api.funtranslations.com/translate/yoda");
        requestMessage.Content = JsonContent.Create(new { Text = translationsNeeded });

        var response = await httpClient.SendAsync( requestMessage);

        if(response.StatusCode != System.Net.HttpStatusCode.OK){
            return null;
        }


        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

        return jsonResponse.contents.translated;
    }

    private List<Pokemon> UpdatePokemonDescriptions(List<Pokemon> pokemons){
        foreach(var pokemon in pokemons){
            if(cachedTranslations.ContainsKey(pokemon.Description)){
                pokemon.Description = cachedTranslations[pokemon.Description];
            }            
        }

        return pokemons;
    }
}