using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MissingCollectionElements.API
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ContainerId))]
        public Container Container { get; set; }

        [ForeignKey(nameof(Container))]
        public int ContainerId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Link link &&
                   ContainerId == link.ContainerId &&
                   ItemId == link.ItemId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ContainerId, ItemId);
        }
    }
}
