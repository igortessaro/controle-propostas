using Framework.Domain.Dtos;
using Framework.Domain.Factories;

namespace Framework.Infrastructure.Factories
{
    public class ListItemFactory : IListItemFactory
    {
        public ListItemDto Create(int key, string value)
        {
            return new ListItemDto(key, value);
        }
    }
}
