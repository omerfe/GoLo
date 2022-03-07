using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class ProductsFilterSpecification : Specification<Product>
    {
        public ProductsFilterSpecification(int? genreId, int? platformId)
        {
            //Query.Include(x => x.Game);
            Query.Include(x => x.Game.Genres);
            Query.Include(x => x.Discounts);

            if (genreId.HasValue)
                Query.Where(x => x.Game.Genres.Any(x => x.Id == genreId));// <= TODO Burası patlayacak(Include()) Include(x => x.Game)
            if (platformId.HasValue)
                Query.Where(x => x.PlatformId == platformId);
        }


        public ProductsFilterSpecification(int? genreId, int? platformId, int skip, int take) : this(genreId, platformId)
        {
            Query.Skip(skip).Take(take);
        }
    }
}
