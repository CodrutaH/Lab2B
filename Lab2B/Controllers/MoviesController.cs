﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2B.Services;
using Lab2B.Models;
using Lab2B.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Lab2B.Service;

namespace Lab2B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private IMovieService movieService;
        private IUsersService userService;
        public MoviesController(IMovieService movieService,IUsersService userService)
        {
            this.movieService = movieService;
            this.userService = userService;
        }
        /// <summary>
        /// Gets all the movies
        /// </summary>
        /// <param name="from">Optional, filter by minimum Date.</param>
        /// <param name="to">Optional, filter by maximum Date</param>
         /// <param name="page"></param>
        /// <returns></returns>
        // GET: api/Flowers
        [HttpGet]
        public PaginatedList<MovieGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to,[FromQuery]int page = 1)
        {
            // TODO: make pagination work with /api/flowers/page/<page number>
            page = Math.Max(page, 1);
            return movieService.GetAll(page, from, to);
        }


        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var existing = movieService.GetById(id); 
            if (existing == null)
            {
                return NotFound();
            }

            return Ok(existing);
        }


        /// <summary>
        /// Add a Movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  POST/Movie
        /// 
        /// {
        ///    "title": "MARA10",
        ///    "description": "a big boat",
        ///    "genre": "action",
        ///    "duration": 100,
        ///    "year": 2001,
        ///    "director": "Paul ",
        ///    "date": "2019-03-10T17:34:19.8376731",
        ///    "rating": 10,
        ///    "watched": "0",
        ///    "comments": [
        ///	   {    
        ///		"text": "Bad",
        ///		"important": false
        ///     }
        ///    ]
        ///   }
        ///    
        ///    
        /// </remarks>
        ///    <param name="movie">The movie to add</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Products
        [Authorize(Roles = "Admin,Regular")]
        [HttpPost]
        public void Post([FromBody] MoviePostModel movie)
        {
            User addedBy = userService.GetCurrentUser(HttpContext);
            movieService.Create(movie,addedBy);
        }

        // PUT: api/Flowers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            var result = movieService.Upsert(id, movie);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = movieService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}