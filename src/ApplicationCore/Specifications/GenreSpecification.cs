using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class GenreSpecification : Specification<Genre>
    {
        public GenreSpecification(string genreName)
        {
            Query.Where(x => x.GenreName.ToLower() == genreName.ToLower());
        }
    }
}
