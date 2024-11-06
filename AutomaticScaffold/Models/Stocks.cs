namespace AutomaticScaffold.Models
{
    public class Stocks
    {
        public required int Id { get; set; }
        public string? CName { get; set; }
        public string? CDescription { get; set; }
        public required decimal Price { get; set; }
        public string? PercentageChangeOver12Hours {  get; set; }

    }
}
