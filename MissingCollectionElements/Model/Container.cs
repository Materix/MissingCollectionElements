using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissingCollectionElements.Model
{
    public class Container
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Link> ItemLinks { get; set; }
    }
}