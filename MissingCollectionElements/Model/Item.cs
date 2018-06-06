using System.ComponentModel.DataAnnotations;

namespace MissingCollectionElements.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
    }
}