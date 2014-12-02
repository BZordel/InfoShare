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
    public partial class ReportRequestForCheck : System.Web.UI.Page
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

        public Tingle_WebForms.Models.RequestForCheckForm GetFormDetails([Control("gvReport")] int? RecordId)
        {
            var myForm = ctx.RequestForCheckForms.FirstOrDefault();
            if (RecordId != null)
            {
                myForm = ctx.RequestForCheckForms.FirstOrDefault(f => f.RecordId == RecordId);
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

        public IQueryable<Tingle_WebForms.Models.RequestForCheckForm> GetRequestForCheckForms(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("ddlCompany")] string Company,
            [Control("txtPayableTo")] string PayableTo,
            [Control("txtChargeTo")] string ChargeTo,
            [Control("txtAmount")] string Amount,
            [Control("txtFor")] string For,
            [Control("txtRequestedBy")] string RequestedBy,
            [Control("txtApprovedBy")] string ApprovedBy,
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

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.RequestForCheckForm> RequestForCheckFormList = ctx.RequestForCheckForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(PayableTo))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.PayableTo.Contains(PayableTo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ChargeTo))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ChargeToAccountNumber.Contains(ChargeTo.Trim()) || forms.ChargeToOther.Contains(ChargeTo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(For))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.For.Contains(For.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Amount))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Amount.Contains(Amount.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestedBy))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.RequestedBy.Contains(RequestedBy.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ApprovedBy))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ApprovedBy.Contains(ApprovedBy.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToName))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ShipToName.Contains(ShipToName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToAddress))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ShipToAddress.Contains(ShipToAddress.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToCity))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ShipToCity.Contains(ShipToCity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToState))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ShipToState.Contains(ShipToState.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToZip))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.ShipToZip.Contains(ShipToZip.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestHandler))
            {
                RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StatusId.ToString()))
            {
                if (StatusId > 0)
                {
                    RequestForCheckFormList = RequestForCheckFormList.Where(forms => forms.Status.StatusId.Equals(StatusId));
                }
            }

            return RequestForCheckFormList;
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

            Tingle_WebForms.Models.RequestForCheckForm myForm = ctx.RequestForCheckForms.FirstOrDefault(eof => eof.RecordId == id);

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            TextBox txtPayableToEdit = (TextBox)fvReport.FindControl("txtPayableToEdit");
            RadioButtonList rblChargeToEdit = (RadioButtonList)fvReport.FindControl("rblChargeToEdit");
            TextBox txtChargeToEdit = (TextBox)fvReport.FindControl("txtChargeToEdit");
            TextBox txtAmountEdit = (TextBox)fvReport.FindControl("txtAmountEdit");
            TextBox txtForEdit = (TextBox)fvReport.FindControl("txtForEdit");
            TextBox txtRequestedByEdit = (TextBox)fvReport.FindControl("txtRequestedByEdit");
            TextBox txtApprovedByEdit = (TextBox)fvReport.FindControl("txtApprovedByEdit");
            TextBox txtShipToNameEdit = (TextBox)fvReport.FindControl("txtShipToNameEdit");
            TextBox txtShipToAddressEdit = (TextBox)fvReport.FindControl("txtShipToAddressEdit");
            TextBox txtShipToCityEdit = (TextBox)fvReport.FindControl("txtShipToCityEdit");
            TextBox txtShipToStateEdit = (TextBox)fvReport.FindControl("txtShipToStateEdit");
            TextBox txtShipToZipEdit = (TextBox)fvReport.FindControl("txtShipToZipEdit");
            TextBox txtAdditionalInfoEdit = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandlerEdit = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");

            DropDownList ddlStatus = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

            myForm.Company = ddlCompanyEdit.SelectedValue;
            myForm.PayableTo = txtPayableToEdit.Text;
            myForm.ChargeToAccountNumber = "";
            myForm.ChargeToOther = "";
            myForm.Amount = txtAmountEdit.Text;
            myForm.For = txtForEdit.Text;
            myForm.RequestedBy = txtRequestedByEdit.Text;
            myForm.ApprovedBy = txtApprovedByEdit.Text;
            myForm.ShipToName = txtShipToNameEdit.Text;
            myForm.ShipToAddress = txtShipToAddressEdit.Text;
            myForm.ShipToCity = txtShipToCityEdit.Text;
            myForm.ShipToState = txtShipToStateEdit.Text;
            myForm.ShipToZip = txtShipToZipEdit.Text;
            myForm.AdditionalInfo = txtAdditionalInfoEdit.Text;
            myForm.RequestHandler = txtRequestHandlerEdit.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = System.Web.HttpContext.Current.User.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);

            if (rblChargeToEdit.SelectedValue == "A")
            {
                myForm.ChargeToAccountNumber = txtChargeToEdit.Text;
            }
            else if (rblChargeToEdit.SelectedValue == "O")
            {
                myForm.ChargeToOther = txtChargeToEdit.Text;
            }

            if (statusCode.StatusText == "Completed")
            {
                myForm.CompletedNotes = txtCompletionNotes.Text;
                myForm.CCCompletedFormToEmail = txtCCCompletedFormToEmail.Text;
            }

            ctx.RequestForCheckForms.Attach(myForm);
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
            Tingle_WebForms.Models.RequestForCheckForm myForm = ctx.RequestForCheckForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Request For Check");

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
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Request For Check Form Completed</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Payable To:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.PayableTo).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\" rowspan=\"2\">Charge To:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">");

            if (!String.IsNullOrEmpty(myForm.ChargeToAccountNumber))
            {
                bodyHtml.Append("Account #");
            }
            else if (!String.IsNullOrEmpty(myForm.ChargeToOther))
            {
                bodyHtml.Append("Other");
            }

            bodyHtml.AppendLine("    </td></tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Amount:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Amount).AppendLine("</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">");
            
            if (!String.IsNullOrEmpty(myForm.ChargeToAccountNumber))
            {
                bodyHtml.Append(myForm.ChargeToAccountNumber);
            }
            else if (!String.IsNullOrEmpty(myForm.ChargeToOther))
            {
                bodyHtml.Append(myForm.ChargeToOther);
            }

            bodyHtml.AppendLine("    </td></tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">For:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.For).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
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

            bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Request For Check Form Completion", bodyHtml.ToString(), submittedForm, true);
            msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Request For Check Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCFormToEmail, "Request For Check Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCCompletedFormToEmail, "Request For Check Form Completion", bodyHtml.ToString(), submittedForm, false);
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
                    var requestForCheckForm = ctx.RequestForCheckForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = requestForCheckForm.Status.StatusId.ToString();
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
            TextBox txtPayableToEdit = (TextBox)fvReport.FindControl("txtPayableToEdit");
            RadioButtonList rblChargeToEdit = (RadioButtonList)fvReport.FindControl("rblChargeToEdit");
            TextBox txtChargeToEdit = (TextBox)fvReport.FindControl("txtChargeToEdit");
            TextBox txtAmountEdit = (TextBox)fvReport.FindControl("txtAmountEdit");
            TextBox txtForEdit = (TextBox)fvReport.FindControl("txtForEdit");
            TextBox txtRequestedByEdit = (TextBox)fvReport.FindControl("txtRequestedByEdit");
            TextBox txtApprovedByEdit = (TextBox)fvReport.FindControl("txtApprovedByEdit");
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
                txtPayableToEdit.Enabled = false;
                rblChargeToEdit.Enabled = false;
                txtChargeToEdit.Enabled = false;
                txtAmountEdit.Enabled = false;
                txtForEdit.Enabled = false;
                txtRequestedByEdit.Enabled = false;
                txtApprovedByEdit.Enabled = false;
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
                    txtPayableToEdit.Enabled = true;
                    rblChargeToEdit.Enabled = true;
                    txtChargeToEdit.Enabled = true;
                    txtAmountEdit.Enabled = true;
                    txtForEdit.Enabled = true;
                    txtRequestedByEdit.Enabled = true;
                    txtApprovedByEdit.Enabled = true;
                    txtShipToNameEdit.Enabled = true;
                    txtShipToAddressEdit.Enabled = true;
                    txtShipToCityEdit.Enabled = true;
                    txtShipToStateEdit.Enabled = true;
                    txtShipToZipEdit.Enabled = true;
                    txtAdditionalInfoEdit.Enabled = true;
                    txtRequestHandlerEdit.Enabled = true;
                    txtCompletionNotes.Enabled = true;
                    txtCCCompletedFormToEmail.Enabled = true;
                }
                else
                {
                    ddlCompanyEdit.Enabled = false;
                    txtPayableToEdit.Enabled = false;
                    rblChargeToEdit.Enabled = false;
                    txtChargeToEdit.Enabled = false;
                    txtAmountEdit.Enabled = false;
                    txtForEdit.Enabled = false;
                    txtRequestedByEdit.Enabled = false;
                    txtApprovedByEdit.Enabled = false;
                    txtShipToNameEdit.Enabled = false;
                    txtShipToAddressEdit.Enabled = false;
                    txtShipToCityEdit.Enabled = false;
                    txtShipToStateEdit.Enabled = false;
                    txtShipToZipEdit.Enabled = false;
                    txtAdditionalInfoEdit.Enabled = false;
                    txtRequestHandlerEdit.Enabled = false;
                    txtCompletionNotes.Enabled = false;
                    txtCCCompletedFormToEmail.Enabled = false;
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

        protected void rblChargeToEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadioButtonList rblChargeToEdit = (RadioButtonList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                TextBox txtChargeToEdit = (TextBox)fvReport.FindControl("txtChargeToEdit");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var requestForCheckForm = ctx.RequestForCheckForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    if (!String.IsNullOrEmpty(requestForCheckForm.ChargeToOther))
                    {
                        rblChargeToEdit.SelectedValue = "O";
                        txtChargeToEdit.Text = requestForCheckForm.ChargeToOther;
                    }
                    else if (!String.IsNullOrEmpty(requestForCheckForm.ChargeToAccountNumber))
                    {
                        rblChargeToEdit.SelectedValue = "A";
                        txtChargeToEdit.Text = requestForCheckForm.ChargeToAccountNumber;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}