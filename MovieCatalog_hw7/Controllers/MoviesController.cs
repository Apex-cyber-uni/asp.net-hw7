using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalog_hw7.Models;

namespace MovieCatalog_hw7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static List<Movie> Movies = new List<Movie>() { 
            new Movie(){Id=1, Title="StarWars", Genre="Fantasy", Rating=5,Year=1977},
            new Movie(){Id=2, Title="Iron Man", Genre="Fantasy", Rating=5,Year=2008},
            new Movie(){Id=3, Title="Chornobyl", Genre="Documental", Rating=3,Year=2021},
        };
        [HttpGet]
        public ActionResult<List<Movie>> GetMovies()
        {
            return Ok(Movies);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            var movie = Movies.Find(m => m.Id == id);
            if (movie == null) return NotFound("There is no movies for this query");
            return Ok(movie);
        }
        [HttpGet("by-title/{title}")]
        public ActionResult<Movie> GetMovieByTitle(string title)
        {
            var movie = Movies.Find(m => m.Title.ToLower() == title.ToLower());
            if (movie == null) return NotFound("There is no movies for this query");
            return Ok(movie);
        }
        [HttpGet("search")]
        // api/movie/search?genre=Fantasy OR api/movie/search?genre=Fantasy&minRating=3 
        public ActionResult<List<Movie>> GetMovieBySearch(string genre, int minRating=0)
        {
            var movie = Movies.FindAll(m => m.Genre.ToLower() == genre.ToLower() && m.Rating>minRating);
            if (movie.Count == 0) return NotFound("There is no movies for this query");
            return Ok(movie);
        }
        [HttpPost]
        public ActionResult<string> AddMovie(Movie movie)
        {
            var newId = Movies.Max(m => m.Id)+1;
            movie.Id = newId;
            Movies.Add(movie);
            return Ok("Movie added successfully");
        }
        [HttpDelete("{id:int}")]
        public ActionResult<string> DelMovie(int id)
        {
            var movie = Movies.Find(m=>m.Id==id);
            if (movie == null) return NotFound("There is no movies for this query");
            Movies.Remove(movie);
            return Ok("Movie removed successfully");
        }
    }
}
