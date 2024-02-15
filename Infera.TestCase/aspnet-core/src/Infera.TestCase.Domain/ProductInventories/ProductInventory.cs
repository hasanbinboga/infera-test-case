using System;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class ProductInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string  Manifacturer { get; set; }
        public virtual ProductInventoryType Type { get; set; }
        public virtual int Size { get; set; }
        public virtual double SalePrice { get; set; }
        public virtual string? Notes { get; set; }
        public virtual Collection<WarehouseInventory> WarehouseInventories { get; protected set; } //Sub collection

        protected ProductInventory()
        {
            
        }

        public ProductInventory(
                            Guid id,
                            string name,
                            string manifacturer,
                            ProductInventoryType type,
                            int size,
                            double salePrice,
                            string notes
            ): base(id)
        {
            SetName(name);
            SetManifaturer(manifacturer);
            Type = type;
            Size = size;
            SalePrice = salePrice;
            SetNotes(notes);

            //initialize the collections
            WarehouseInventories = new Collection<WarehouseInventory>();
        }

        internal void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), 255, 3);
            Name = name;
        }

        internal void SetManifaturer(string manifacturer)
        {
            Check.Length(manifacturer, nameof(manifacturer), 255);
            Manifacturer = manifacturer;
        }

        internal void SetNotes(string notes)
        {
            Check.Length(notes, nameof(notes), 500, 0);
            Notes = notes;
        }

    }
}
