namespace Munros.Core.Entities
{
    public class QueryParameters
    {
        public string Category { get; set; }
        public int ResultsLimit { get; set; }
        public double? MinHeight { get; set; }
        public double? MaxHeight { get; set; }
    }
}
