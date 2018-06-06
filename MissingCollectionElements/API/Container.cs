using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MissingCollectionElements.API
{
    public class Container
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Link> ItemLinks { get; set; }
    }
}
