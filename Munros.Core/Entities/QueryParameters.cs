namespace Munros.Core.Entities
{
    public class QueryParameters
    {
        public string Category { get; set; }
        public int ResultsLimit { get; set; }
        public double? MinHeight { get; set; }
        public double? MaxHeight { get; set; }

        public string PrimarySortBy { get; set; } = "Name";

        private string _primarySortOrder = "asc";
        public string PrimarySortOrder
        {
            get
            {
                return _primarySortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _primarySortOrder = value;
                }
            }
        }

        public string SecondarySortBy { get; set; }

        private string _secondarySortOrder;
        public string SecondarySortOrder
        {
            get
            {
                return _secondarySortOrder;
            }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _secondarySortOrder = value;
                }
            }
        }

        public bool MinMaxHeightValid()
        {
            if (MinHeight != null && MaxHeight != null)
            {
                if (MaxHeight <= MinHeight)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
