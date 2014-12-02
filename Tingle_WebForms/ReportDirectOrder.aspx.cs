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
using System.Web.UI.HtmlControls;

namespace Tingle_WebForms
{
    public partial class ReportDirectOrder : System.Web.UI.Page
    {
        FormContext ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                gvReport.DataBind();
            }
            else
            {
                if (Request.QueryString["formId"] != null)
                {
                    ddlCompany.SelectedValue = "Any";
                    ddlGlobalStatus.SelectedValue = "All";
                    pnlFilter.Visible = false;
                    pnlReport.Visible = false;
                    pnlDetails.Visible = true;
                }
                else
                {
                    if (Session["Company"] != null)
                    {
                        if (Session["Company"].ToString() == "Tingle")
                        {
                            ddlCompany.SelectedValue = "Tingle";
                        }
                        else if (Session["Company"].ToString() == "Summit")
                        {
                            ddlCompany.SelectedValue = "Summit";
                        }
                        else if (Session["Company"].ToString() == "Any")
                        {
                            ddlCompany.SelectedValue = "Any";
                        }
                    }

                    if (Session["GlobalStatus"] != null)
                    {
                        if (Session["GlobalStatus"].ToString() == "Active")
                        {
                            ddlGlobalStatus.SelectedValue = "Active";
                        }
                        else if (Session["GlobalStatus"].ToString() == "Archive")
                        {
                            ddlGlobalStatus.SelectedValue = "Archive";
                        }
                        else if (Session["GlobalStatus"].ToString() == "All")
                        {
                            ddlGlobalStatus.SelectedValue = "All";
                        }
                    }
                }
            }
        }

        public Tingle_WebForms.Models.DirectOrderForm GetFormDetails([Control("gvReport")] int? RecordId, [QueryString("formId")] Nullable<Int32> formId)
        {
            var myForm = ctx.DirectOrderForms.FirstOrDefault();

            if (RecordId == null)
            {
                if (formId != null)
                {
                    myForm = ctx.DirectOrderForms.FirstOrDefault(f => f.RecordId == formId);
                }
            }
            else
            {
                myForm = ctx.DirectOrderForms.FirstOrDefault(f => f.RecordId == RecordId);
            }

            return myForm;
        }

        public IEnumerable<Status> GetStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.Statuses.ToList();

                return StatusList;
            }
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.Statuses.ToList();

                return StatusList;
            }
        }

        public IQueryable<Tingle_WebForms.Models.DirectOrderForm> GetDirectOrderForms(
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
            [Control("txtShipVia")] string ShipVia,
            [Control("txtReserve")] string Reserve,
            [Control("txtShipToName")] string ShipToName,
            [Control("txtShipToAddress")] string ShipToAddress,
            [Control("txtShipToCity")] string ShipToCity,
            [Control("txtShipToState")] string ShipToState,
            [Control("txtShipToZip")] string ShipToZip,
            [Control("txtAdditionalInfo")] string AdditionalInfo,
            [Control("txtRequestHandler")] string RequestHandler,
            [Control("ddlStatus")] int StatusId,
            [Control("ddlGlobalStatus")] string GlobalStatus,
            [QueryString("formId")] Nullable<Int32> formId
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;
            DateTime dtInstallFrom;
            DateTime dtInstallTo;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.DirectOrderForm> DirectOrderFormList = ctx.DirectOrderForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                DirectOrderFormList = DirectOrderFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(InstallFrom))
            {
                DateTime.TryParse(InstallFrom, out dtInstallFrom);
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.InstallDate >= dtInstallFrom);
            }
            if (!String.IsNullOrWhiteSpace(InstallTo))
            {
                DateTime.TryParse(InstallTo, out dtInstallTo);
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.InstallDate <= dtInstallTo);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(OowOrderNumber))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.OowOrderNumber.Contains(OowOrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(PurchaseOrderNumber))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.PurchaseOrderNumber.Contains(PurchaseOrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AccountNumber))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ContactName))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ContactName.Contains(ContactName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(PhoneNumber))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.PhoneNumber.Contains(PhoneNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(MaterialSku))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.MaterialSku.Contains(MaterialSku.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ExpediteCodeId.ToString()))
            {
                if (ExpediteCodeId > 0)
                {
                    DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ExpediteCode.ExpediteCodeID.Equals(ExpediteCodeId));
                }
            }
            if (!String.IsNullOrWhiteSpace(Sm))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.SM.Contains(Sm.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(QuantityOrdered))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.QuantityOrdered.Contains(QuantityOrdered.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipVia))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipVia.Contains(ShipVia.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Reserve))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Reserve.Contains(Reserve.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToName))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipToName.Contains(ShipToName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToAddress))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipToAddress.Contains(ShipToAddress.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToCity))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipToCity.Contains(ShipToCity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToState))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipToState.Contains(ShipToState.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToZip))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.ShipToZip.Contains(ShipToZip.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestHandler))
            {
                DirectOrderFormList = DirectOrderFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StatusId.ToString()))
            {
                if (StatusId > 0)
                {
                    DirectOrderFormList = DirectOrderFormList.Where(forms => forms.Status.StatusId.Equals(StatusId));
                }
            }

            return DirectOrderFormList;
        }

        public IEnumerable<ExpediteCode> GetExpediteCodes()
        {
            FormContext ctx = new FormContext();
            var ExpediteCodeList = ctx.ExpediteCodes.Where(c => c.Status == 1).OrderBy(c => c.ExpediteCodeID).Select(c => new { Code = c.Code + " - " + c.Description, ExpediteCodeID = c.ExpediteCodeID }).ToList()
                .Select(x => new ExpediteCode { ExpediteCodeID = x.ExpediteCodeID, Code = x.Code });

            return ExpediteCodeList;

        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                pnlDetails.Visible = true;
                pnlReport.Visible = false;
                pnlFilter.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;
            gvReport.DataBind();
        }

        protected void fvReport_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(((Label)fvReport.FindControl("lblRecordId")).Text);

            Tingle_WebForms.Models.DirectOrderForm myForm = ctx.DirectOrderForms.FirstOrDefault(eof => eof.RecordId == id);

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
            TextBox txtShipVia = (TextBox)fvReport.FindControl("txtShipViaEdit");
            TextBox txtReserve = (TextBox)fvReport.FindControl("txtReserveEdit");
            System.Web.UI.HtmlControls.HtmlInputText txtInstallDate = (System.Web.UI.HtmlControls.HtmlInputText)fvReport.FindControl("txtInstallDateEdit");
            TextBox txtShipToName = (TextBox)fvReport.FindControl("txtShipToNameEdit");
            TextBox txtShipToAddress = (TextBox)fvReport.FindControl("txtShipToAddressEdit");
            TextBox txtShipToCity = (TextBox)fvReport.FindControl("txtShipToCityEdit");
            TextBox txtShipToState = (TextBox)fvReport.FindControl("txtShipToStateEdit");
            TextBox txtShipToZip = (TextBox)fvReport.FindControl("txtShipToZipEdit");
            TextBox txtAdditionalInfo = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandler = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");

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

            var expediteCode = ctx.ExpediteCodes.FirstOrDefault(ec => ec.ExpediteCodeID == expediteCodeId);
            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

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
            myForm.ShipVia = txtShipVia.Text;
            myForm.Reserve = txtReserve.Text;
            myForm.InstallDate = installDate;
            myForm.ShipToName = txtShipToName.Text;
            myForm.ShipToAddress = txtShipToAddress.Text;
            myForm.ShipToCity = txtShipToCity.Text;
            myForm.ShipToState = txtShipToState.Text;
            myForm.ShipToZip = txtShipToZip.Text;
            myForm.AdditionalInfo = txtAdditionalInfo.Text;
            myForm.RequestHandler = txtRequestHandler.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = System.Web.HttpContext.Current.User.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);

            if (statusCode.StatusText == "Completed")
            {
                myForm.CompletedNotes = txtCompletionNotes.Text;
                myForm.CCCompletedFormToEmail = txtCCCompletedFormToEmail.Text;
            }

            ctx.DirectOrderForms.Attach(myForm);
            ctx.Entry(myForm).State = EntityState.Modified;

            ctx.SaveChanges();

            if (myForm.Status.StatusText == "Completed")
            {
                SendCompletedEmail(myForm.RecordId);
            }

            gvReport.DataBind();
            fvReport.DataBind();
            ddlExpediteCode.DataBind();
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;

        }

        public void SendCompletedEmail(Int32 FormId)
        {
            Tingle_WebForms.Models.DirectOrderForm myForm = ctx.DirectOrderForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Direct Order");

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            string submittedByUser = currentUser.UserName;
            string submittedByuserEmail = submittedByUser.Substring(currentUser.UserName.IndexOf(@"\") + 1);

            if (submittedByUser.IndexOf("@") == -1)
            {
                submittedByuserEmail += "@wctingle.com";
            }

            SendEmail msg = new SendEmail();
            StringBuilder bodyHtml = new StringBuilder();

            bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                .AppendLine("    <tr>")
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid d0604c; color:#FFF; background-color:#bc4445;\">Direct Order Form Completed</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Customer).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Expedite Code:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ExpediteCode.Code).Append(" - ").Append(myForm.ExpediteCode.Description).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Contact Name:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ContactName).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Phone Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.PhoneNumber).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Material SKU#:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.MaterialSku).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity Ordered:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.QuantityOrdered).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.AccountNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Purchase Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.PurchaseOrderNumber).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">OOW Order Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.OowOrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">S/M:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SM).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Ship Via:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipVia).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Reserve:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Reserve).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Install Date:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.InstallDate.Value.ToShortDateString()).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Status:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Status.StatusText).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;border: 4px solid #d0604c;color:#FFF; background-color:#bc4445;\" colspan=\"4\">Ship To (If different than default)</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Name:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipToName).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Street Address:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipToAddress).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">City: </td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipToCity).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">State:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipToState).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Zip:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipToZip).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#bc4445;\">Additional Info: </td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#000;\">").Append(myForm.AdditionalInfo).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#bc4445;\">Request Handler: </td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#000;\">").Append(myForm.RequestHandler).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#bc4445;\">Completion Notes: </td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#000;\">").Append(myForm.CompletedNotes).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Submitted By: <span style=\"color:#000\">")
                .Append(myForm.SubmittedByUser).AppendLine("</span></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Last Modified By: <span style=\"color:#000\">")
                .Append(myForm.ModifiedByUser).AppendLine("</span></td>")
                .AppendLine("    </tr>")
                .AppendLine("</table></div><br /><br /><br /><br />");

            bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Direct Order Form Completion", bodyHtml.ToString(), submittedForm, true);
            msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Direct Order Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCFormToEmail, "Direct Order Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCCompletedFormToEmail, "Direct Order Form Completion", bodyHtml.ToString(), submittedForm, false);
        }

        public void UpdateForm(int RecordId)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;
            gvReport.DataBind();
        }

        protected void ddlStatusEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatus = (DropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var directOrderForm = ctx.DirectOrderForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = directOrderForm.Status.StatusId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlExpediteCodeEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlExpediteCodeEdit = (DropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var directOrderForm = ctx.DirectOrderForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlExpediteCodeEdit.SelectedValue = directOrderForm.ExpediteCode.ExpediteCodeID.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
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
            TextBox txtCustomerEdit = (TextBox)fvReport.FindControl("txtCustomerEdit");
            DropDownList ddlExpediteCodeEdit = (DropDownList)fvReport.FindControl("ddlExpediteCodeEdit");
            TextBox txtContactNameEdit = (TextBox)fvReport.FindControl("txtContactNameEdit");
            TextBox txtPhoneNumberEdit = (TextBox)fvReport.FindControl("txtPhoneNumberEdit");
            TextBox txtMaterialSkuEdit = (TextBox)fvReport.FindControl("txtMaterialSkuEdit");
            TextBox txtQuantityOrderedEdit = (TextBox)fvReport.FindControl("txtQuantityOrderedEdit");
            TextBox txtAccountNumberEdit = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtPurchaseOrderNumberEdit = (TextBox)fvReport.FindControl("txtPurchaseOrderNumberEdit");
            TextBox txtOowOrderNumberEdit = (TextBox)fvReport.FindControl("txtOowOrderNumberEdit");
            TextBox txtSMEdit = (TextBox)fvReport.FindControl("txtSMEdit");
            TextBox txtShipViaEdit = (TextBox)fvReport.FindControl("txtShipViaEdit");
            TextBox txtReserveEdit = (TextBox)fvReport.FindControl("txtReserveEdit");
            HtmlInputText txtInstallDateEdit = (HtmlInputText)fvReport.FindControl("txtInstallDateEdit");
            TextBox txtShipToNameEdit = (TextBox)fvReport.FindControl("txtShipToNameEdit");
            TextBox txtShipToAddressEdit = (TextBox)fvReport.FindControl("txtShipToAddressEdit");
            TextBox txtShipToCityEdit = (TextBox)fvReport.FindControl("txtShipToCityEdit");
            TextBox txtShipToStateEdit = (TextBox)fvReport.FindControl("txtShipToStateEdit");
            TextBox txtShipToZipEdit = (TextBox)fvReport.FindControl("txtShipToZipEdit");
            TextBox txtAdditionalInfoEdit = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandlerEdit = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");
            DropDownList ddlStatusEdit = (DropDownList)fvReport.FindControl("ddlStatusEdit");

            HtmlTableRow trCompleted1 = (HtmlTableRow)fvReport.FindControl("trCompleted1");
            HtmlTableRow trCompleted2 = (HtmlTableRow)fvReport.FindControl("trCompleted2");
            HtmlTableRow trCompleted3 = (HtmlTableRow)fvReport.FindControl("trCompleted3");

            ddlCompanyEdit.SelectedValue = lblCompanyEdit.Text;

            if (ddlStatusEdit.SelectedItem.Text == "Completed")
            {
                trCompleted1.Visible = true;
                trCompleted2.Visible = true;
                trCompleted3.Visible = true;
            }
            else
            {
                trCompleted1.Visible = false;
                trCompleted2.Visible = false;
                trCompleted3.Visible = false;
            }

            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                ddlCompanyEdit.Enabled = false;
                txtCustomerEdit.Enabled = false;
                ddlExpediteCodeEdit.Enabled = false;
                txtContactNameEdit.Enabled = false;
                txtPhoneNumberEdit.Enabled = false;
                txtMaterialSkuEdit.Enabled = false;
                txtQuantityOrderedEdit.Enabled = false;
                txtAccountNumberEdit.Enabled = false;
                txtPurchaseOrderNumberEdit.Enabled = false;
                txtOowOrderNumberEdit.Enabled = false;
                txtSMEdit.Enabled = false;
                txtShipViaEdit.Enabled = false;
                txtReserveEdit.Enabled = false;
                txtInstallDateEdit.Disabled = true;
                txtShipToNameEdit.Enabled = false;
                txtShipToAddressEdit.Enabled = false;
                txtShipToCityEdit.Enabled = false;
                txtShipToStateEdit.Enabled = false;
                txtShipToZipEdit.Enabled = false;
                txtAdditionalInfoEdit.Enabled = false;
                txtRequestHandlerEdit.Enabled = false;
                txtCompletionNotes.Enabled = false;
                txtCCCompletedFormToEmail.Enabled = false;
                ddlStatusEdit.Enabled = false;
            }

            if (currentUser.UserRole.RoleName == "ReportsAdmin" || currentUser.UserRole.RoleName == "SuperUser")
            {
                ddlStatusEdit.Enabled = true;

                if (ddlStatusEdit.SelectedItem.Text != "Completed")
                {
                    ddlCompanyEdit.Enabled = true;
                    txtCustomerEdit.Enabled = true;
                    ddlExpediteCodeEdit.Enabled = true;
                    txtContactNameEdit.Enabled = true;
                    txtPhoneNumberEdit.Enabled = true;
                    txtMaterialSkuEdit.Enabled = true;
                    txtQuantityOrderedEdit.Enabled = true;
                    txtAccountNumberEdit.Enabled = true;
                    txtPurchaseOrderNumberEdit.Enabled = true;
                    txtOowOrderNumberEdit.Enabled = true;
                    txtSMEdit.Enabled = true;
                    txtShipViaEdit.Enabled = true;
                    txtReserveEdit.Enabled = true;
                    txtInstallDateEdit.Disabled = false;
                    txtShipToNameEdit.Enabled = true;
                    txtShipToAddressEdit.Enabled = true;
                    txtShipToCityEdit.Enabled = true;
                    txtShipToStateEdit.Enabled = true;
                    txtShipToZipEdit.Enabled = true;
                    txtAdditionalInfoEdit.Enabled = true;
                    txtCompletionNotes.Enabled = true;
                    txtCCCompletedFormToEmail.Enabled = true;
                    txtRequestHandlerEdit.Enabled = true;

                }
                else
                {
                    ddlCompanyEdit.Enabled = false;
                    txtCustomerEdit.Enabled = false;
                    ddlExpediteCodeEdit.Enabled = false;
                    txtContactNameEdit.Enabled = false;
                    txtPhoneNumberEdit.Enabled = false;
                    txtMaterialSkuEdit.Enabled = false;
                    txtQuantityOrderedEdit.Enabled = false;
                    txtAccountNumberEdit.Enabled = false;
                    txtPurchaseOrderNumberEdit.Enabled = false;
                    txtOowOrderNumberEdit.Enabled = false;
                    txtSMEdit.Enabled = false;
                    txtShipViaEdit.Enabled = false;
                    txtReserveEdit.Enabled = false;
                    txtInstallDateEdit.Disabled = true;
                    txtShipToNameEdit.Enabled = false;
                    txtShipToAddressEdit.Enabled = false;
                    txtShipToCityEdit.Enabled = false;
                    txtShipToStateEdit.Enabled = false;
                    txtShipToZipEdit.Enabled = false;
                    txtAdditionalInfoEdit.Enabled = false;
                    txtCompletionNotes.Enabled = false;
                    txtCCCompletedFormToEmail.Enabled = false;
                    txtRequestHandlerEdit.Enabled = false;
                }
            }
        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            tr1.Visible = true;
            tr2.Visible = true;
            tr3.Visible = true;
            tr4.Visible = true;
            tr5.Visible = true;
            tr6.Visible = true;
            tr7.Visible = true;
            tr8.Visible = true;
            tr9.Visible = true;
            tr10.Visible = true;
            tr11.Visible = true;
            tr12.Visible = true;
            btnAdvancedSearch.Visible = false;
            btnBasicSearch.Visible = true;
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e)
        {
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
            tr4.Visible = false;
            tr5.Visible = false;
            tr6.Visible = false;
            tr7.Visible = false;
            tr8.Visible = false;
            tr9.Visible = false;
            tr10.Visible = false;
            tr11.Visible = false;
            tr12.Visible = true;
            btnAdvancedSearch.Visible = true;
            btnBasicSearch.Visible = false;
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            PlaceHolder phFVCompleted = (PlaceHolder)fvReport.FindControl("phFVCompleted");
            PlaceHolder phFVDetails = (PlaceHolder)fvReport.FindControl("phFVDetails");
            HiddenField hfCompleted = (HiddenField)fvReport.FindControl("hfCompleted");

            phFVCompleted.Visible = false;
            phFVDetails.Visible = true;
            hfCompleted.Value = "0";
        }

        protected void ddlStatusEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStatusEdit = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            HtmlTableRow trCompleted1 = (HtmlTableRow)fvReport.FindControl("trCompleted1");
            HtmlTableRow trCompleted2 = (HtmlTableRow)fvReport.FindControl("trCompleted2");
            HtmlTableRow trCompleted3 = (HtmlTableRow)fvReport.FindControl("trCompleted3");

            if (ddlStatusEdit != null)
            {
                if (ddlStatusEdit.SelectedItem.Text == "Completed")
                {
                    trCompleted1.Visible = true;
                    trCompleted2.Visible = true;
                    trCompleted3.Visible = true;
                }
                else
                {
                    trCompleted1.Visible = false;
                    trCompleted2.Visible = false;
                    trCompleted3.Visible = false;
                }
            }
            else
            {
                trCompleted1.Visible = false;
                trCompleted2.Visible = false;
                trCompleted3.Visible = false;
            }
        }

    }
}