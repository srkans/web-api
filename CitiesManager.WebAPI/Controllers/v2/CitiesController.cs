﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.Entities;
using CitiesManager.WebAPI.Models;

namespace CitiesManager.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of only city names from cities table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                                  .OrderBy(temp => temp.CityName)
                                  .Select(temp => temp.CityName)
                                  .ToListAsync();
            return cities;
        }
    }
}
