namespace Framework.Domain.Dtos
{
    public class ResponseDto
    {
        private const string messageErrorDefault = "Fail.";
        private const string messageSuccessDefault = "Success";

        public ResponseDto(bool success)
        {
            this.Success = success;
            this.Error = success ? messageSuccessDefault : messageErrorDefault;
        }

        public ResponseDto(bool success, string error)
            : this(success)
        {
            this.Error = error;
        }

        public bool Success { get; }

        public string Error { get; }
    }
}
