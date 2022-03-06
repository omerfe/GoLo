using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public List<Game> Games { get; set; }
    }
}