namespace Tingle_WebForms.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data;
    using Tingle_WebForms.Models;
    using Tingle_WebForms.Forms;

    internal sealed class Configuration : DbMigrationsConfiguration<Tingle_WebForms.Models.FormContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tingle_WebForms.Models.FormContext context)
        {

            //Seed Roles
            UserRoles roleUser = new UserRoles{RoleName = "User",RoleDescription = "User can submit forms."};
            UserRoles roleReportsUser = new UserRoles{RoleName = "User",RoleDescription = "User can view reports, but not edit them."};
            UserRoles roleReportsAdmin = new UserRoles{RoleName = "User",RoleDescription = "User can view and edit reports."};
            UserRoles roleSuperUser = new UserRoles { RoleName = "User", RoleDescription = "User has full administrative rights." };

            context.UserRoles.AddOrUpdate(u => u.RoleName, roleUser);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleReportsUser);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleReportsAdmin);
            context.UserRoles.AddOrUpdate(u => u.RoleName, roleSuperUser);


            //Seed Forms
            TForm expeditedOrder = new TForm
            {
                FormCreator = "Admin",
                FormName = "Expedited Order",
                FormNameHtml = "Expedited Order",
                FormUrl = "ExpeditedOrderForm.aspx",
                Notes = "Expedited Order Form",
                Status = 1,
                Timestamp = DateTime.Now
            };

            TForm priceChangeRequest = new TForm
            {
                FormCreator = "Admin",
                FormName = "Price Change Request",
                FormNameHtml = "Price Change<br />Request",
                FormUrl = "PriceChangeRequestForm.aspx",
                Notes = "Price Change Request Form Notes",
                Status = 1,
                Timestamp = DateTime.Now
            };

            TForm orderCancellation = new TForm
            {
                FormCreator = "Admin",
                FormName = "Order Cancellation",
                FormNameHtml = "Order Cancellation",
                FormUrl = "OrderCancellationForm.aspx",
                Notes = "Order Cancellation Form Notes",
                Status = 1,
                Timestamp = DateTime.Now
            };

            context.TForms.AddOrUpdate(t => t.FormName, orderCancellation);
            context.TForms.AddOrUpdate(t => t.FormName, expeditedOrder);
            context.TForms.AddOrUpdate(t => t.FormName, priceChangeRequest);


            //Seed ExpediteCodes
            ExpediteCode expCode1 = new ExpediteCode { Code = "EXP100", Description = "Mill Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode2 = new ExpediteCode { Code = "EXP200", Description = "Customer Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode3 = new ExpediteCode { Code = "EXP300", Description = "Tingle Error", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode4 = new ExpediteCode { Code = "EXP400", Description = "Install Date", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode5 = new ExpediteCode { Code = "EXP500", Description = "Can't wait on production date", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode6 = new ExpediteCode { Code = "EXP777", Description = "General", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode7 = new ExpediteCode { Code = "EXP800", Description = "Direct Order", Status = 1, Timestamp = DateTime.Now };
            ExpediteCode expCode8 = new ExpediteCode { Code = "EXP911", Description = "Immediate Attn", Status = 1, Timestamp = DateTime.Now };

            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode1);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode2);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode3);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode4);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode5);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode6);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode7);
            context.ExpediteCodes.AddOrUpdate(e => e.Code, expCode8);


            //Seed Statuses
            Status status1 = new Status { StatusText = "In Progress" };
            Status status2 = new Status { StatusText = "On Hold" };
            Status status3 = new Status { StatusText = "Completed" };

            context.Statuses.AddOrUpdate(s => s.StatusText, status1);
            context.Statuses.AddOrUpdate(s => s.StatusText, status2);
            context.Statuses.AddOrUpdate(s => s.StatusText, status3);


            //Seed PO Statuses
            PurchaseOrderStatus poStatus1 = new PurchaseOrderStatus { Status = "Open" };
            PurchaseOrderStatus poStatus2 = new PurchaseOrderStatus { Status = "Closed" };

            context.POStatuses.AddOrUpdate(p => p.Status, poStatus1);
            context.POStatuses.AddOrUpdate(p => p.Status, poStatus2);

            base.Seed(context);
        }
    }
}