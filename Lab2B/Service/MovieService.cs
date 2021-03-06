﻿using Lab2B.Models;
using Lab2B.ViewModels;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2B.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        PaginatedList<MovieGetModel> GetAll(int page, DateTime? from = null, DateTime? to = null);
        Movie GetById(int id);
        Movie Create(MoviePostModel movie, User addedBy);
        Movie Upsert(int id, Movie movie);
        Movie Delete(int id);
    }
    public class MovieService : IMovieService
    {
        private MoviesDbContext context;


        public MovieService(MoviesDbContext context)
        {
            this.context = context;
        }

        public Movie Create(MoviePostModel movie, User addedBy)
        {
            Movie toAdd = MoviePostModel.ToMovie(movie);
            toAdd.Owner = addedBy;
            context.Movies.Add(toAdd);
            context.SaveChanges();
            return toAdd;
        }


        public Movie Delete(int id)
        {
            var existing = context.Movies.Include(m => m.Comments).FirstOrDefault(movie => movie.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Movies.Remove(existing);
            context.SaveChanges();

            return existing;
        }

        public PaginatedList<MovieGetModel> GetAll(int page,DateTime? from = null, DateTime? to = null)
        {
            IQueryable<Movie> result = context
                .Movies
                .OrderBy(m => m.Id)
                .Include(m => m.Comments);
            PaginatedList<MovieGetModel> paginatedResult = new PaginatedList<MovieGetModel>();
            paginatedResult.CurrentPage = page;

            if (from != null)
            {
                result = result.Where(m => m.Date >= from);
            }
            if (to != null)
            {
                result = result.Where(m => m.Date <= to);
            }
            
                paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<MovieGetModel>.EntriesPerPage + 1;
                result = result
                    .Skip((page - 1) * PaginatedList<MovieGetModel>.EntriesPerPage)
                    .Take(PaginatedList<MovieGetModel>.EntriesPerPage);
                paginatedResult.Entries = result.Select(m => MovieGetModel.FromMovie(m)).ToList();
                return paginatedResult;
            }

       

        public Movie GetById(int id)
            {
                return context.Movies
                    .Include(m => m.Comments)
                    .FirstOrDefault(m => m.Id == id);
            }

            public Movie Upsert(int id, Movie movie)
            {
                var existing = context.Movies.AsNoTracking().FirstOrDefault(m => m.Id == id);
                if (existing == null)
                {
                    context.Movies.Add(movie);
                    context.SaveChanges();
                    return movie;
                }
                movie.Id = id;
                context.Movies.Update(movie);
                context.SaveChanges();
                return movie;
            }
        }

    }     
