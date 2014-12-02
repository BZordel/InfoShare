using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tingle_WebForms.Models
{
    public class FormContext : DbContext
    {
        public FormContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ExpeditedOrderForm> ExpeditedOrderForms { get; set; }

        public DbSet<PriceChangeRequestForm> PriceChangeRequestForms { get; set; }

        public DbSet<OrderCancellationForm> OrderCancellationForms { get; set; }

        public DbSet<HotRushForm> HotRushForms { get; set; }

        public DbSet<LowInventoryForm> LowInventoryForms { get; set; }

        public DbSet<SampleRequestForm> SampleRequestForms { get; set; }

        public DbSet<DirectOrderForm> DirectOrderForms { get; set; }

        public DbSet<RequestForCheckForm> RequestForCheckForms { get; set; }

        public DbSet<MustIncludeForm> MustIncludeForms { get; set; }

        public DbSet<ExpediteCode> ExpediteCodes { get; set; }

        public DbSet<TForm> TForms { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<SystemUsers> SystemUsers { get; set; }

        public DbSet<FavoriteForm> FavoriteForms { get; set; }

        public DbSet<PurchaseOrderStatus> POStatuses { get; set; }

    }
}