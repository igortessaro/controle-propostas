namespace Framework.Domain.Dtos
{
    public class ResponseTokenDto
    {
        public ResponseTokenDto(
            bool authenticated,
            System.DateTime created,
            System.DateTime expiration,
            string accessToken)
        {
            this.Authenticated = authenticated;
            this.Created = created.ToString("yyyy-MM-dd HH:mm:ss");
            this.Expiration = expiration.ToString("yyyy-MM-dd HH:mm:ss");
            this.AccessToken = accessToken;
        }

        public bool Authenticated { get; }

        public string Created { get; }

        public string Expiration { get; }

        public string AccessToken { get; }
    }
}
