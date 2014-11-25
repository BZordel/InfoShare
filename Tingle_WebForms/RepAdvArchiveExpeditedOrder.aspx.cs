using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Data.Objects;
using System.Web.ModelBinding;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using Tingle_WebForms.Logic;

namespace Tingle_WebForms.Reports
{
    public partial class RepAdvArchiveExpeditedOrderForm : System.Web.UI.Page
    {
        FormContext ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                gvReport.DataBind();
            }
        }

        public Tingle_WebForms.Models.ExpeditedOrderForm GetFormDetails([Control("gvReport")] int? ExpeditedOrderFormID)
        {
            var myForm = ctx.ExpeditedOrderForms.FirstOrDefault();
            if (ExpeditedOrderFormID != null)
            {
                myForm = ctx.ExpeditedOrderForms.Single(f => f.ExpeditedOrderFormID == ExpeditedOrderFormID);
            }
            return myForm;
        }

        public IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> GetExpeditedOrderForms(
            [Control("txtFromDate")] string FromDate, 
            [Control("txtToDate")] string ToDate, 
            [Control("txtInstallFrom")] string InstallFrom, 
            [Control("txtInstallTo")] string InstallTo,
            [Control("ddlCompany")] string Company, 
            [Control("txtCustomer")] string Customer, 
            [Control("txtOowOrderNumber")] string OowOrderNumber,
            [Control("txtPurchaseOrderNumber")] string PurchaseOrderNumber,
            [Control("txtAccountNumber")] string AccountNumber, 
            [Control("txtContactName")] string ContactName,
            [Control("txtPhoneNumber")] string PhoneNumber,
            [Control("txtMaterialSku")] string MaterialSku, 
            [Control("ddlExpediteCode")] int ExpediteCodeId, 
            [Control("txtSM")] string Sm,
            [Control("txtQuantityOrdered")] string QuantityOrdered,
            [Control("txtShipToName")] string ShipToName, 
            [Control("txtShipToAddress")] string ShipToAddress, 
            [Control("txtShipToCity")] string ShipToCity,
            [Control("txtShipToState")] string ShipToState,
            [Control("txtShipToZip")] string ShipToZip, 
            [Control("txtAdditionalInfo")] string AdditionalInfo, 
            [Control("txtExpeditorHandling")] string ExpeditorHandling
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;
            DateTime dtInstallFrom;
            DateTime dtInstallTo;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> ExpeditedOrderFormList = ctx.ExpeditedOrderForms.Where(forms => forms.Status.StatusText == "Completed").OrderByDescending(forms => forms.Timestamp);

            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(InstallFrom))
            {
                DateTime.TryParse(InstallFrom, out dtInstallFrom);
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.InstallDate >= dtInstallFrom);
            }
            if (!String.IsNullOrWhiteSpace(InstallTo))
            {
                DateTime.TryParse(InstallTo, out dtInstallTo);
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.InstallDate <= dtInstallTo);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(OowOrderNumber))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.OowOrderNumber.Contains(OowOrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(PurchaseOrderNumber))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.PurchaseOrderNumber.Contains(PurchaseOrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AccountNumber))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ContactName))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ContactName.Contains(ContactName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(PhoneNumber))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.PhoneNumber.Contains(PhoneNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(MaterialSku))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.MaterialSku.Contains(MaterialSku.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ExpediteCodeId.ToString()))
            {
                if (ExpediteCodeId > 0)
                {
                    ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ExpediteCode.ExpediteCodeID.Equals(ExpediteCodeId));
                }
            }
            if (!String.IsNullOrWhiteSpace(Sm))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.SM.Contains(Sm.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(QuantityOrdered))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.QuantityOrdered.Equals(QuantityOrdered.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToName))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ShipToName.Contains(ShipToName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToAddress))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ShipToAddress.Contains(ShipToAddress.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToCity))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ShipToCity.Contains(ShipToCity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToState))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ShipToState.Contains(ShipToState.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToZip))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ShipToZip.Contains(ShipToZip.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ExpeditorHandling))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ExpeditorHandling.Contains(ExpeditorHandling.Trim()));
            }

            return ExpeditedOrderFormList;
        }

        public IEnumerable<ExpediteCode> GetExpediteCodes()
        {
            FormContext ctx = new FormContext();
            var ExpediteCodeList = ctx.ExpediteCodes.Where(c => c.Status == 1).OrderBy(c => c.ExpediteCodeID).Select(c => new { Code = c.Code + " - " + c.Description, ExpediteCodeID = c.ExpediteCodeID }).ToList()
                .Select(x => new ExpediteCode { ExpediteCodeID = x.ExpediteCodeID, Code = x.Code });

            return ExpediteCodeList;

        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                pnlDetails.Visible = true;
                pnlReport.Visible = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            gvReport.DataBind();
        }

        protected void ddlExpediteCodeEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlExpediteCodeEdit = (DropDownList)sender;
                Label lblExpeditedOrderFormID = (Label)fvReport.FindControl("lblExpeditedOrderFormID");
                int expeditedOrderFormId;
                Int32.TryParse(lblExpeditedOrderFormID.Text, out expeditedOrderFormId);

                using (var ctx = new FormContext())
                {
                    var expeditedOrderForm = ctx.ExpeditedOrderForms.Where(eof => eof.ExpeditedOrderFormID == expeditedOrderFormId).FirstOrDefault();

                    ddlExpediteCodeEdit.SelectedValue = expeditedOrderForm.ExpediteCode.ExpediteCodeID.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            gvReport.PageIndex = 0;
            gvReport.SelectedIndex = -1;

        }

        protected void ddlStatusEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                Label lblExpeditedOrderFormID = (Label)fvReport.FindControl("lblExpeditedOrderFormID");
                int expeditedOrderFormId;
                Int32.TryParse(lblExpeditedOrderFormID.Text, out expeditedOrderFormId);

                using (var ctx = new FormContext())
                {
                    var expeditedOrderForm = ctx.ExpeditedOrderForms.Where(eof => eof.ExpeditedOrderFormID == expeditedOrderFormId).FirstOrDefault();

                    ddlStatus.SelectedValue = expeditedOrderForm.Status.StatusId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void fvReport_DataBound(object sender, EventArgs e)
        {
            FormView fvReport = (FormView)sender;
            Button btnUpdate = (Button)fvReport.FindControl("btnUpdate");
            Button btnCancel = (Button)fvReport.FindControl("btnCancel");
            Button btnBack = (Button)fvReport.FindControl("btnDetailsBack");

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            Label lblCompanyEdit = (Label)fvReport.FindControl("lblCompanyEdit");

            ddlCompanyEdit.SelectedValue = lblCompanyEdit.Text;
        }

    } 
}