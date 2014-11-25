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

namespace Tingle_WebForms
{
    public partial class RepArchiveExpeditedOrder : System.Web.UI.Page
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
                myForm = ctx.ExpeditedOrderForms.SingleOrDefault(f => f.ExpeditedOrderFormID == ExpeditedOrderFormID);
            }
            return myForm;
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.Where(s => s.StatusText != "Completed").ToList();

            return StatusList;
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        public IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> GetExpeditedOrderForms
            (
            [Control("ddlCompany")] string Company,
            [Control("txtCustomer")] string Customer,
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("txtMaterialSku")] string MaterialSku,
            [Control("txtExpeditorHandling")] string ExpeditorHandling
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> ExpeditedOrderFormList = ctx.ExpeditedOrderForms.Where(forms => forms.Status.StatusText == "Completed").OrderByDescending(forms => forms.Timestamp);

            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
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
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.Customer.Contains(Customer));
            }
            if (!String.IsNullOrWhiteSpace(MaterialSku))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.MaterialSku.Contains(MaterialSku));
            }
            if (!String.IsNullOrWhiteSpace(ExpeditorHandling))
            {
                ExpeditedOrderFormList = ExpeditedOrderFormList.Where(forms => forms.ExpeditorHandling.Contains(ExpeditorHandling));
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

            return ExpeditedOrderFormList;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            gvReport.PageIndex = 0;
            gvReport.SelectedIndex = -1;

        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {


                pnlReport.Visible = false;
                pnlDetails.Visible = true;
            }
        }

        public IEnumerable<ExpediteCode> GetExpediteCodes()
        {
            FormContext ctx = new FormContext();
            var ExpediteCodeList = ctx.ExpediteCodes.Where(c => c.Status == 1).OrderBy(c => c.ExpediteCodeID).Select(c => new { Code = c.Code + " - " + c.Description, ExpediteCodeID = c.ExpediteCodeID }).ToList()
                .Select(x => new ExpediteCode { ExpediteCodeID = x.ExpediteCodeID, Code = x.Code });

            return ExpediteCodeList;

        }

        protected void fvReport_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            int id = Convert.ToInt32(((Label)fvReport.FindControl("lblExpeditedOrderFormID")).Text);

            Tingle_WebForms.Models.ExpeditedOrderForm myForm = ctx.ExpeditedOrderForms.Where(eof => eof.ExpeditedOrderFormID == id).FirstOrDefault();

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            TextBox txtCustomer = (TextBox)fvReport.FindControl("txtCustomerEdit");
            DropDownList ddlExpediteCode = (DropDownList)fvReport.FindControl("ddlExpediteCodeEdit");
            int expediteCodeId = Convert.ToInt32(ddlExpediteCode.SelectedValue);
            TextBox txtContactName = (TextBox)fvReport.FindControl("txtContactNameEdit");
            TextBox txtPhoneNumber = (TextBox)fvReport.FindControl("txtPhoneNumberEdit");
            TextBox txtMaterialSku = (TextBox)fvReport.FindControl("txtMaterialSkuEdit");
            TextBox txtQuantityOrdered = (TextBox)fvReport.FindControl("txtQuantityOrderedEdit");
            TextBox txtAccountNumber = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtPurchaseOrderNumber = (TextBox)fvReport.FindControl("txtPurchaseOrderNumberEdit");
            TextBox txtOowOrderNumber = (TextBox)fvReport.FindControl("txtOowOrderNumberEdit");
            TextBox txtSM = (TextBox)fvReport.FindControl("txtSMEdit");
            System.Web.UI.HtmlControls.HtmlInputText txtInstallDate = (System.Web.UI.HtmlControls.HtmlInputText)fvReport.FindControl("txtInstallDateEdit");
            TextBox txtShipToName = (TextBox)fvReport.FindControl("txtShipToNameEdit");
            TextBox txtShipToAddress = (TextBox)fvReport.FindControl("txtShipToAddressEdit");
            TextBox txtShipToCity = (TextBox)fvReport.FindControl("txtShipToCityEdit");
            TextBox txtShipToState = (TextBox)fvReport.FindControl("txtShipToStateEdit");
            TextBox txtShipToZip = (TextBox)fvReport.FindControl("txtShipToZipEdit");
            TextBox txtAdditionalInfo = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtExpeditorHandling = (TextBox)fvReport.FindControl("txtExpeditorHandlingEdit");
            DropDownList ddlStatus = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            DateTime tryInstallDate;
            Nullable<DateTime> installDate = null;
            DateTime.TryParse(txtInstallDate.Value, out tryInstallDate);

            if (tryInstallDate.Year > 0001)
            {
                installDate = tryInstallDate;
            }

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }

            var expediteCode = ctx.ExpediteCodes.Where(ec => ec.ExpediteCodeID == expediteCodeId).FirstOrDefault();
            var statusCode = ctx.Statuses.Where(s => s.StatusId == statusId).FirstOrDefault();

            myForm.Company = ddlCompanyEdit.SelectedValue;
            myForm.Customer = txtCustomer.Text;
            myForm.ExpediteCode = expediteCode;
            myForm.ContactName = txtContactName.Text;
            myForm.PhoneNumber = txtPhoneNumber.Text;
            myForm.MaterialSku = txtMaterialSku.Text;
            myForm.QuantityOrdered = txtQuantityOrdered.Text;
            myForm.AccountNumber = accountNumber;
            myForm.PurchaseOrderNumber = txtPurchaseOrderNumber.Text;
            myForm.OowOrderNumber = txtOowOrderNumber.Text;
            myForm.SM = txtSM.Text;
            myForm.InstallDate = installDate;
            myForm.ShipToName = txtShipToName.Text;
            myForm.ShipToAddress = txtShipToAddress.Text;
            myForm.ShipToCity = txtShipToCity.Text;
            myForm.ShipToState = txtShipToState.Text;
            myForm.ShipToZip = txtShipToZip.Text;
            myForm.AdditionalInfo = txtAdditionalInfo.Text;
            myForm.ExpeditorHandling = txtExpeditorHandling.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = currentUser.UserName;

            ctx.ExpeditedOrderForms.Attach(myForm);
            ctx.Entry(myForm).State = EntityState.Modified;

            ctx.SaveChanges();
            gvReport.DataBind();
            fvReport.DataBind();
            ddlExpediteCode.DataBind();
            pnlDetails.Visible = false;
            pnlReport.Visible = true;

        }

        public void UpdateForm(int ExpeditedOrderFormID)
        {
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            gvReport.DataBind();
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

        protected void btnDetailsBack_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            gvReport.DataBind();
        }

        protected void fvReport_DataBound(object sender, EventArgs e)
        {
            FormView fvReport = (FormView)sender;
            Button btnUpdate = (Button)fvReport.FindControl("btnUpdate");
            Button btnCancel = (Button)fvReport.FindControl("btnCancel");
            Button btnBack = (Button)fvReport.FindControl("btnDetailsBack");

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            Label lblCompanyEdit = (Label)fvReport.FindControl("lblCompanyEdit");

            ddlCompanyEdit.SelectedValue = lblCompanyEdit.Text;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

        }

        protected void ddlExpediteCodeEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlExpediteCode = (DropDownList)sender;
                Label lblExpeditedOrderFormID = (Label)fvReport.FindControl("lblExpeditedOrderFormID");
                int expeditedOrderFormId;
                Int32.TryParse(lblExpeditedOrderFormID.Text, out expeditedOrderFormId);

                using (var ctx = new FormContext())
                {
                    var expeditedOrderForm = ctx.ExpeditedOrderForms.Where(eof => eof.ExpeditedOrderFormID == expeditedOrderFormId).FirstOrDefault();

                    ddlExpediteCode.SelectedValue = expeditedOrderForm.ExpediteCode.ExpediteCodeID.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}