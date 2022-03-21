using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MinimumAge { get; set; }
        public string TrailerUrl { get; set; }
        public string ImagePath { get; set; }
        public IFormFile GameImage { get; set; }
        public string GameRequirements { get; set; }
        public List<int> GenreIds { get; set; }
        public List<SelectListItem> AllGenres { get; set; }
        public List<Product> Products { get; set; }
    }
}
