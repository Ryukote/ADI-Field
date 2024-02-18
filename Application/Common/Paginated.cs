namespace Application.Common
{
    public class Paginated<T>
        where T : IGetDTO
    {
        public required ICollection<T> Result { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
    }
}
