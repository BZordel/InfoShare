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
    public partial class ReportPriceChangeRequest : System.Web.UI.Page
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

        public Tingle_WebForms.Models.PriceChangeRequestForm GetFormDetails([Control("gvReport")] int? RecordId)
        {
            var myForm = ctx.PriceChangeRequestForms.FirstOrDefault();

            if (RecordId != null)
            {
                myForm = ctx.PriceChangeRequestForms.FirstOrDefault(f => f.RecordId == RecordId);
            }
            return myForm;
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        public IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> GetPriceChangeRequestForms(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("ddlCompany")] string Company,
            [Control("txtCustomer")] string Customer,
            [Control("txtLineNumber")] string LineNumber,
            [Control("txtAccountNumber")] string AccountNumber,
            [Control("txtQuantity")] string Quantity,
            [Control("txtSalesRep")] string SalesRep,
            [Control("txtMaterialSku")] string MaterialSku,
            [Control("txtProduct")] string Product,
            [Control("txtOrderNumber")] string OrderNumber,
            [Control("txtPrice")] string Price,
            [Control("txtCrossRefOldOrderNumber")] string CrossReferenceOldOrderNumber,
            [Control("txtAdditionalInfo")] string AdditionalInfo,
            [Control("txtRequestHandler")] string RequestHandler,
            [Control("ddlStatus")] int StatusId,
            [Control("ddlGlobalStatus")] string GlobalStatus,
            [QueryString("formId")] Nullable<Int32> formId
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> PriceChangeRequestFormList = ctx.PriceChangeRequestForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(LineNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.LineNumber.Contains(LineNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AccountNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Quantity))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Quantity.Contains(Quantity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(SalesRep))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.SalesRep.Contains(SalesRep.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Product))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Product.Contains(Product.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(OrderNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.OrderNumber.Contains(OrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Price))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Price.Contains(Price));
            }
            if (!String.IsNullOrWhiteSpace(CrossReferenceOldOrderNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.CrossReferenceOldOrderNumber.Contains(CrossReferenceOldOrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestHandler))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StatusId.ToString()))
            {
                if (StatusId > 0)
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusId.Equals(StatusId));
                }
            }

            return PriceChangeRequestFormList;
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

            Tingle_WebForms.Models.PriceChangeRequestForm myForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == id);

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            TextBox txtCustomer = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtLineNumber = (TextBox)fvReport.FindControl("txtLineNumberEdit");
            TextBox txtAccountNumber = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtQuantity = (TextBox)fvReport.FindControl("txtQuantityEdit");
            TextBox txtSalesRep = (TextBox)fvReport.FindControl("txtSalesRepEdit");
            TextBox txtProduct = (TextBox)fvReport.FindControl("txtProductEdit");
            TextBox txtOrderNumber = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            TextBox txtPrice = (TextBox)fvReport.FindControl("txtPriceEdit");
            TextBox txtCrossRefOldOrderNumber = (TextBox)fvReport.FindControl("txtCrossRefOldOrderNumberEdit");
            TextBox txtAdditionalInfo = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandler = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");

            DropDownList ddlStatus = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }

            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

            myForm.Company = ddlCompanyEdit.SelectedValue;
            myForm.Customer = txtCustomer.Text;
            myForm.LineNumber = txtLineNumber.Text;
            myForm.AccountNumber = txtAccountNumber.Text;
            myForm.Quantity = txtQuantity.Text;
            myForm.SalesRep = txtSalesRep.Text;
            myForm.Product = txtProduct.Text;
            myForm.OrderNumber = txtOrderNumber.Text;
            myForm.Price = txtPrice.Text;
            myForm.CrossReferenceOldOrderNumber = txtCrossRefOldOrderNumber.Text;
            myForm.AdditionalInfo = txtAdditionalInfo.Text;
            myForm.RequestHandler = txtRequestHandler.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = System.Web.HttpContext.Current.User.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);

            if (statusCode.StatusText == "Completed")
            {
                myForm.CompletedNotes = txtCompletionNotes.Text;
                myForm.CCCompletedFormToEmail = txtCCCompletedFormToEmail.Text;
            }

            ctx.PriceChangeRequestForms.Attach(myForm);
            ctx.Entry(myForm).State = EntityState.Modified;

            ctx.SaveChanges();

            if (myForm.Status.StatusText == "Completed")
            {
                SendCompletedEmail(myForm.RecordId);
            }

            gvReport.DataBind();
            fvReport.DataBind();
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;
        }

        public void SendCompletedEmail(Int32 FormId)
        {
            Tingle_WebForms.Models.PriceChangeRequestForm myForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Price Change Request");

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
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Price Change Request Form Completed</td>")
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
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.LineNumber).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.AccountNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Quantity).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Sales Rep:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SalesRep).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Product:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Product).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.OrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Price:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Price).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Cross Reference Old Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.CrossReferenceOldOrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
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

            bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Price Change Request Form Completion", bodyHtml.ToString(), submittedForm, true);
            msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Price Change Request Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCFormToEmail, "Price Change Request Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCCompletedFormToEmail, "Price Change Request Form Completion", bodyHtml.ToString(), submittedForm, false);
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
                    var priceChangeRequestForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = priceChangeRequestForm.Status.StatusId.ToString();
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
            TextBox txtLineNumberEdit = (TextBox)fvReport.FindControl("txtLineNumberEdit");
            TextBox txtAccountNumberEdit = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtQuantityEdit = (TextBox)fvReport.FindControl("txtQuantityEdit");
            TextBox txtSalesRepEdit = (TextBox)fvReport.FindControl("txtSalesRepEdit");
            TextBox txtProductEdit = (TextBox)fvReport.FindControl("txtProductEdit");
            TextBox txtOrderNumberEdit = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            TextBox txtPriceEdit = (TextBox)fvReport.FindControl("txtPriceEdit");
            TextBox txtCrossRefOldOrderNumberEdit = (TextBox)fvReport.FindControl("txtCrossRefOldOrderNumberEdit");
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
                txtLineNumberEdit.Enabled = false;
                txtAccountNumberEdit.Enabled = false;
                txtQuantityEdit.Enabled = false;
                txtSalesRepEdit.Enabled = false;
                txtProductEdit.Enabled = false;
                txtOrderNumberEdit.Enabled = false;
                txtPriceEdit.Enabled = false;
                txtCrossRefOldOrderNumberEdit.Enabled = false;
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
                    txtLineNumberEdit.Enabled = true;
                    txtAccountNumberEdit.Enabled = true;
                    txtQuantityEdit.Enabled = true;
                    txtSalesRepEdit.Enabled = true;
                    txtProductEdit.Enabled = true;
                    txtOrderNumberEdit.Enabled = true;
                    txtPriceEdit.Enabled = true;
                    txtCrossRefOldOrderNumberEdit.Enabled = true;
                    txtAdditionalInfoEdit.Enabled = true;
                    txtRequestHandlerEdit.Enabled = true;
                    txtCompletionNotes.Enabled = true;
                    txtCCCompletedFormToEmail.Enabled = true;
                    ddlStatusEdit.Enabled = true;
                }
                else
                {
                    ddlCompanyEdit.Enabled = false;
                    txtCustomerEdit.Enabled = false;
                    txtLineNumberEdit.Enabled = false;
                    txtAccountNumberEdit.Enabled = false;
                    txtQuantityEdit.Enabled = false;
                    txtSalesRepEdit.Enabled = false;
                    txtProductEdit.Enabled = false;
                    txtOrderNumberEdit.Enabled = false;
                    txtPriceEdit.Enabled = false;
                    txtCrossRefOldOrderNumberEdit.Enabled = false;
                    txtAdditionalInfoEdit.Enabled = false;
                    txtRequestHandlerEdit.Enabled = false;
                    txtCompletionNotes.Enabled = false;
                    txtCCCompletedFormToEmail.Enabled = false;
                    ddlStatusEdit.Enabled = false;
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