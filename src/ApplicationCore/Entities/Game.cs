using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Game : BaseEntity
    {
        public string GameName { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MinimumAge { get; set; }
        public string TrailerUrl { get; set; }
        public string ImagePath { get; set; }
        public string GameRequirements { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Product> Products { get; set; }
    }
}