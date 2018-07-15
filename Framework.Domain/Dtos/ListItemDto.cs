namespace Framework.Domain.Dtos
{
    public class ListItemDto
    {
        public ListItemDto(int key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int Key { get; }

        public string Value { get; }
    }
}
