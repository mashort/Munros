using System.Text.Json.Serialization;

namespace Munros.Core.Entities
{
    public class Munro
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public string Category { get; set; }
        public string GridReference { get; set; }
    }
}
