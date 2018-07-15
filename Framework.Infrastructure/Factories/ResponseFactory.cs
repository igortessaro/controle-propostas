using Framework.Domain.Dtos;
using Framework.Domain.Factories;

namespace Framework.Infrastructure.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public ResponseDto Success()
        {
            return new ResponseDto(true);
        }

        public ResponseDto Success(string message)
        {
            return new ResponseDto(true, message);
        }

        public ResponseDto Fail()
        {
            return new ResponseDto(false);
        }

        public ResponseDto Fail(string message)
        {
            return new ResponseDto(false, message);
        }
    }
}
