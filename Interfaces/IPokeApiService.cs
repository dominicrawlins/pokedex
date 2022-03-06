using System.Threading.Tasks;
using System.Collections.Generic;

public interface IPokeApiService{
    Task<IEnumerable<Pokemon>> GetAllPokemon();

    Task<IEnumerable<Pokemon>> GetAllTranslatedPokemon();
}