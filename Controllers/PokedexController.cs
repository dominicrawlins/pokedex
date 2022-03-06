using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        private readonly ILogger<PokedexController> logger;
        private readonly IPokeApiService pokeApiService;

        public PokedexController(ILogger<PokedexController> logger, IPokeApiService pokeApiService)
        {
            this.logger = logger;
            this.pokeApiService = pokeApiService;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await pokeApiService.GetAllTranslatedPokemon();
        }
    }
}
