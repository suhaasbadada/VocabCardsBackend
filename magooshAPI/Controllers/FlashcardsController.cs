
using magooshAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;


namespace magooshAPI.Controllers
{
    [Route("api/flashcards")]
    [ApiController]
    public class FlashcardsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FlashcardsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id:int}")]
        public async Task<Flashcard> Get(int id)
        {
            var flashcard = await _context.Flashcards.FirstOrDefaultAsync(x => x.Id == id);
            return flashcard;
        }
        
        [HttpGet("letter/{c}")]
        public ActionResult<IEnumerable<Flashcard>> Get(char c)
        {
           // var flashcards = _context.Flashcards.FromSqlRaw($"SELECT * FROM dbo.Flashcards WHERE WORD LIKE '{c}%'").ToList();
            var flashcards= _context.Flashcards.Where(f => f.Word.StartsWith(c.ToString())).ToList();
            return flashcards;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Flashcard flashcard)
        {
            flashcard.Id = id;
            _context.Entry(flashcard).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("totallearnt")]
        public ActionResult<int> TotalLearnt()
        {
            int result = _context.Flashcards.Count(f => f.Learnt == "true");
            return result;
        }

        [HttpGet("alllearnt")]
        public ActionResult<IEnumerable<Flashcard>> GetAllLearnt()
        {
            var flashcards= _context.Flashcards.Where(f => f.Learnt == "true").ToList();
            return flashcards;
        }

        [HttpGet("random")]
        public async Task<Flashcard> Get()
        {
            Random random = new Random();
            int id = random.Next(1,1068);
            var flashcard = await _context.Flashcards.FirstOrDefaultAsync(x => x.Id == id);
            return flashcard;
        }


    }
}
    