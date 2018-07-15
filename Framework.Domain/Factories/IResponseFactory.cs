using Framework.Domain.Core.Factory;
using Framework.Domain.Dtos;

namespace Framework.Domain.Factories
{
    public interface IResponseFactory : IFactory
    {
        ResponseDto Fail();

        ResponseDto Fail(string message);

        ResponseDto Success();

        ResponseDto Success(string message);
    }
}