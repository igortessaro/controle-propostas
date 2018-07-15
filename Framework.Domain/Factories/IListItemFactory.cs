using Framework.Domain.Core.Factory;
using Framework.Domain.Dtos;

namespace Framework.Domain.Factories
{
    public interface IListItemFactory : IFactory
    {
        ListItemDto Create(int key, string value);
    }
}
