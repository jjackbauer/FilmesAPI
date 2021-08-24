using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController: ControllerBase
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;
        public MovieController(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);

            _movieContext.Add(movie);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }
        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_movieContext.Movies);
        }
        [HttpGet("{id}")]
        public IActionResult GetMovieById([FromRoute] int id)
        {
            Movie movie = _movieContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return NotFound($"O filme com o Id = {id} não existe dentro do sistema!");

            ReadMovieDTO movieDTO = _mapper.Map<ReadMovieDTO>(movie);
            movieDTO.QueryTime = DateTime.UtcNow;

            return Ok(movieDTO);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieDTO updatedMovieDTO)
        {
            Movie movie = _movieContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return NotFound($"O filme com o Id = {id} não existe dentro do sistema!");
           
            _mapper.Map(updatedMovieDTO, movie);
          
            _movieContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie([FromRoute] int id)
        {
            Movie movie = _movieContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return NotFound($"O filme com o Id = {id} não existe dentro do sistema!");

            _movieContext.Remove(movie);
            _movieContext.SaveChanges();

            return NoContent();
        }
    }
}
