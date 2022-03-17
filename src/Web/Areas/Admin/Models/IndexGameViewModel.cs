using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class IndexGameViewModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
        public List<Genre> Genres { get; set; }
        public string GenreNames { get; set; }
    }
}