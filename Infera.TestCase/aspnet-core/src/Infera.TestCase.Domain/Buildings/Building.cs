using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Building : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }
        public virtual string No { get; protected set; }
        public virtual string? Addres { get; protected set; }

        public virtual Collection<Room> Rooms { get; protected set; } //Sub collection
        public virtual Collection<BuildingWarehouse> BuildingWarehouses { get; protected set; } //Sub collection
        public virtual Collection<Warehouse> Warehouses { get; protected set; } //Sub collection
        public virtual Collection<Issue> Issues { get; protected set; } //Sub collection

        protected Building()
        {
            /* This constructor is for ORMs to be used while getting the entity from database.
             * - No need to initialize the Rooms or Warehouses collection
             *   since it will be overrided from the database.
             * - It's protected since proxying and deserialization tools
             *   may not work with private constructors.
            */ 
        }

        //Primary constructor
        internal Building(Guid id, 
                        [NotNull] string name,
                        [NotNull] string no, 
                        string addres): base(id) 
        {
            SetName(name);
            SetNo(no);
            SetAddres(addres);

            //initialize the collections

            Rooms = new Collection<Room>(); 
            Warehouses = new Collection<Warehouse>();
            BuildingWarehouses = new Collection<BuildingWarehouse>();
            Issues = new Collection<Issue>();

        }


        internal void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name), BuildingConsts.MaxNameLength, BuildingConsts.MinNameLength);
            Name = name;
        }

        internal void SetNo(string no)
        {
            Check.NotNullOrWhiteSpace(no, nameof(no), BuildingConsts.MaxNoLength);
            No = no;
        }

        internal void SetAddres(string addres)
        {
            Check.Length(addres, nameof(addres), BuildingConsts.MaxAddresLength);
            Addres = addres;
        } 
    }
}
