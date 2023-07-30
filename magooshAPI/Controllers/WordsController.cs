using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace magooshAPI.Controllers
{
    [Route("api/words")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public WordsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("local-storage")]
        public IActionResult GetLocalStorageValue()
        {
            if (Request.Headers.TryGetValue("LocalStorageData", out var localStorageData))
            {
                var parsedData = JsonConvert.DeserializeObject<Dictionary<int, bool>>(localStorageData);
                return Ok(parsedData);
            }

            return Ok(new Dictionary<int, bool>());
        }

        [HttpGet("{id}")]
        public IActionResult GetWordDetails(int id)
        {
            var wordDetails = _dbContext.Flashcards.FirstOrDefault(w => w.Id == id);

            if (wordDetails == null)
            {
                return NotFound();
            }

            var localStorageData = Request.Headers["LocalStorageData"].FirstOrDefault();

            var learntWords = !string.IsNullOrEmpty(localStorageData)
                ? JsonConvert.DeserializeObject<Dictionary<int, bool>>(localStorageData)
                : null;

            wordDetails.Learnt = (learntWords?.ContainsKey(id) == true && learntWords[id]).ToString();

            return Ok(wordDetails);
        }

    }
}
