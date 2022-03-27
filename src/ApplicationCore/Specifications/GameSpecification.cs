using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class GameSpecification : Specification<Game>
    {
        public GameSpecification()
        {
            Query.Include(x => x.Genres);
        }
        public GameSpecification(string gameName)
        {
            Query.Where(x => x.GameName.ToLower() == gameName.ToLower());
        }
        public GameSpecification(int gameId)
        {
            Query.Where(x => x.Id == gameId)
                .Include(x => x.Genres);
            Query.Where(x => x.Id == gameId)
                .Include(x => x.Products);
        }
    }
}