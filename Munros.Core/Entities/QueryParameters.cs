namespace Munros.Core.Entities
{
    public class QueryParameters
    {
        public string Category { get; set; }
        public int ResultsLimit { get; set; }
        public double? MinHeight { get; set; }
        public double? MaxHeight { get; set; }

        public string SortBy { get; set; } = "Name";

        private string _sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}
