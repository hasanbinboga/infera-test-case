using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class ProductInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Manifacturer { get; set; }
        public virtual ProductInventoryType Type { get; set; }
        public virtual int Size { get; set; }
        public virtual double SalePrice { get; set; }
        public virtual string? Notes { get; set; }
        public virtual Collection<WarehouseInventory> WarehouseInventories { get; protected set; } //Sub collection
        public virtual Collection<Accounting> Accountings { get; protected set; } //Sub collection
        public virtual Collection<Issue> Issues { get; protected set; } //Sub collection

        protected ProductInventory()
        {

        }

        internal ProductInventory(
                            Guid id,
                            [NotNull] string name,
                            string manifacturer,
                            ProductInventoryType type,
                            int size,
                            double salePrice,
                            string notes) : base(id)
        {
            SetName(name);
            SetManifacturer(manifacturer);
            Type = type;
            Size = size;
            SalePrice = salePrice;
            SetNotes(notes);

            //initialize the collections
            WarehouseInventories = new Collection<WarehouseInventory>();
            Accountings = new Collection<Accounting>();
            Issues = new Collection<Issue>();
        }

        internal void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ProductInventoryConsts.MaxNameLength, ProductInventoryConsts.MinNameLength);
            Name = name;
        }

        internal void SetManifacturer(string manifacturer)
        {
            Check.Length(manifacturer, nameof(manifacturer), ProductInventoryConsts.MaxManifacturerLength);
            Manifacturer = manifacturer;
        }

        internal void SetNotes(string notes)
        {
            Check.Length(notes, nameof(notes), ProductInventoryConsts.MaxNotesLength);
            Notes = notes;
        }

    }
}
