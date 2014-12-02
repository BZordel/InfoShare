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
    public partial class ReportSampleRequest : System.Web.UI.Page
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

        public Tingle_WebForms.Models.SampleRequestForm GetFormDetails([Control("gvReport")] int? RecordId)
        {
            var myForm = ctx.SampleRequestForms.FirstOrDefault();
            if (RecordId != null)
            {
                myForm = ctx.SampleRequestForms.FirstOrDefault(f => f.RecordId == RecordId);
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

        public IQueryable<Tingle_WebForms.Models.SampleRequestForm> GetSampleRequestForms(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("ddlCompany")] string Company,
            [Control("txtProjectName")] string ProjectName,
            [Control("txtItemNumber")] string ItemNumber,
            [Control("txtCustomer")] string Customer,
            [Control("txtStyleNameAndColor")] string StyleNameAndColor,
            [Control("txtAccountNumber")] string AccountNumber,
            [Control("txtSize")] string Size,
            [Control("txtContact")] string Contact,
            [Control("txtQuantity")] string Quantity,
            [Control("txtPhoneNumber")] string PhoneNumber,
            [Control("rblDealerAwareOfCost")] string DealerAwareOfCost,
            [Control("rblDealerAwareOfFreight")] string DealerAwareOfFreight,
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

            IQueryable<Tingle_WebForms.Models.SampleRequestForm> SampleRequestFormList = ctx.SampleRequestForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                SampleRequestFormList = SampleRequestFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(ProjectName))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ProjectName.Contains(ProjectName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ItemNumber))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ItemNumber.Contains(ItemNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StyleNameAndColor))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.StyleNameColor.Contains(StyleNameAndColor.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AccountNumber))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Size))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Size.Contains(Size.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Contact))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Contact.Contains(Contact.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Quantity))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Quantity.Equals(Quantity));
            }
            if (!String.IsNullOrWhiteSpace(PhoneNumber))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.PhoneNumber.Contains(PhoneNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(DealerAwareOfCost))
            {
                if (DealerAwareOfCost == "Yes")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.DealerAwareOfCost.Equals(true));
                }
                else if (DealerAwareOfCost == "No")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.DealerAwareOfCost.Equals(false));
                }
            }
            if (!String.IsNullOrWhiteSpace(DealerAwareOfFreight))
            {
                if (DealerAwareOfFreight == "Yes")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.DealerAwareOfFreight.Equals(true));
                }
                else if (DealerAwareOfFreight == "No")
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.DealerAwareOfFreight.Equals(false));
                }
            }
            if (!String.IsNullOrWhiteSpace(ShipToName))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ShipToName.Contains(ShipToName.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToAddress))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ShipToAddress.Contains(ShipToAddress.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToCity))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ShipToCity.Contains(ShipToCity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToState))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ShipToState.Contains(ShipToState.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ShipToZip))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.ShipToZip.Contains(ShipToZip.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestHandler))
            {
                SampleRequestFormList = SampleRequestFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StatusId.ToString()))
            {
                if (StatusId > 0)
                {
                    SampleRequestFormList = SampleRequestFormList.Where(forms => forms.Status.StatusId.Equals(StatusId));
                }
            }

            return SampleRequestFormList;
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

            Tingle_WebForms.Models.SampleRequestForm myForm = ctx.SampleRequestForms.FirstOrDefault(eof => eof.RecordId == id);

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            TextBox txtProjectName = (TextBox)fvReport.FindControl("txtProjectNameEdit");
            TextBox txtItemNumber = (TextBox)fvReport.FindControl("txtItemNumberEdit");
            TextBox txtCustomer = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtStyleNameAndColor = (TextBox)fvReport.FindControl("txtStyleNameAndColorEdit");
            TextBox txtAccountNumber = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtSize = (TextBox)fvReport.FindControl("txtSizeEdit");
            TextBox txtContact = (TextBox)fvReport.FindControl("txtContactEdit");
            TextBox txtQuantity = (TextBox)fvReport.FindControl("txtQuantityEdit");
            TextBox txtPhoneNumber = (TextBox)fvReport.FindControl("txtPhoneNumberEdit");
            CheckBox cbDealerAwareOfCost = (CheckBox)fvReport.FindControl("cbDealerAwareOfCostEdit");
            CheckBox cbDealerAwareOfFreight = (CheckBox)fvReport.FindControl("cbDealerAwareOfFreightEdit");
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

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }

            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

            myForm.Company = ddlCompanyEdit.SelectedValue;
            myForm.ProjectName = txtProjectName.Text;
            myForm.ItemNumber = txtItemNumber.Text;
            myForm.Customer = txtCustomer.Text;
            myForm.StyleNameColor = txtStyleNameAndColor.Text;
            myForm.AccountNumber = txtAccountNumber.Text;
            myForm.Size = txtSize.Text;
            myForm.Contact = txtContact.Text;
            myForm.Quantity = txtQuantity.Text;
            myForm.PhoneNumber = txtPhoneNumber.Text;
            myForm.DealerAwareOfCost = cbDealerAwareOfCost.Checked;
            myForm.DealerAwareOfFreight = cbDealerAwareOfFreight.Checked;
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

            ctx.SampleRequestForms.Attach(myForm);
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
            Tingle_WebForms.Models.SampleRequestForm myForm = ctx.SampleRequestForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Sample Request");

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
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Sample Reuqest Form Completed</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Project Name:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ProjectName).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Item #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ItemNumber).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Customer).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Style Name & Color:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.StyleNameColor).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.AccountNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Size:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Size).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Contact:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Contact).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Quantity).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Phone Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.PhoneNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Dealer Aware Of Cost:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.DealerAwareOfCost.ToString()).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Dealer Aware Of Freight Charges:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.DealerAwareOfFreight.ToString()).AppendLine("</td>")
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

            bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Sample Request Form Completion", bodyHtml.ToString(), submittedForm, true);
            msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Sample Request Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCFormToEmail, "Sample Request Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCCompletedFormToEmail, "Sample Request Form Completion", bodyHtml.ToString(), submittedForm, false);
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
                    var sampleRequestForm = ctx.SampleRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = sampleRequestForm.Status.StatusId.ToString();
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
            TextBox txtProjectNameEdit = (TextBox)fvReport.FindControl("txtProjectNameEdit");
            TextBox txtItemNumberEdit = (TextBox)fvReport.FindControl("txtItemNumberEdit");
            TextBox txtCustomerEdit = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtStyleNameAndColorEdit = (TextBox)fvReport.FindControl("txtStyleNameAndColorEdit");
            TextBox txtAccountNumberEdit = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtSizeEdit = (TextBox)fvReport.FindControl("txtSizeEdit");
            TextBox txtContactEdit = (TextBox)fvReport.FindControl("txtContactEdit");
            TextBox txtQuantityEdit = (TextBox)fvReport.FindControl("txtQuantityEdit");
            TextBox txtPhoneNumberEdit = (TextBox)fvReport.FindControl("txtPhoneNumberEdit");
            CheckBox cbDealerAwareOfCostEdit = (CheckBox)fvReport.FindControl("cbDealerAwareOfCostEdit");
            CheckBox cbDealerAwareOfFreightEdit = (CheckBox)fvReport.FindControl("cbDealerAwareOfFreightEdit");
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
                txtProjectNameEdit.Enabled = false;
                txtItemNumberEdit.Enabled = false;
                txtCustomerEdit.Enabled = false;
                txtStyleNameAndColorEdit.Enabled = false;
                txtAccountNumberEdit.Enabled = false;
                txtSizeEdit.Enabled = false;
                txtContactEdit.Enabled = false;
                txtQuantityEdit.Enabled = false;
                txtPhoneNumberEdit.Enabled = false;
                cbDealerAwareOfCostEdit.Enabled = false;
                cbDealerAwareOfFreightEdit.Enabled = false;
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
                    txtProjectNameEdit.Enabled = true;
                    txtItemNumberEdit.Enabled = true;
                    txtCustomerEdit.Enabled = true;
                    txtStyleNameAndColorEdit.Enabled = true;
                    txtAccountNumberEdit.Enabled = true;
                    txtSizeEdit.Enabled = true;
                    txtContactEdit.Enabled = true;
                    txtQuantityEdit.Enabled = true;
                    txtPhoneNumberEdit.Enabled = true;
                    cbDealerAwareOfCostEdit.Enabled = true;
                    cbDealerAwareOfFreightEdit.Enabled = true;
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
                    txtProjectNameEdit.Enabled = false;
                    txtItemNumberEdit.Enabled = false;
                    txtCustomerEdit.Enabled = false;
                    txtStyleNameAndColorEdit.Enabled = false;
                    txtAccountNumberEdit.Enabled = false;
                    txtSizeEdit.Enabled = false;
                    txtContactEdit.Enabled = false;
                    txtQuantityEdit.Enabled = false;
                    txtPhoneNumberEdit.Enabled = false;
                    cbDealerAwareOfCostEdit.Enabled = false;
                    cbDealerAwareOfFreightEdit.Enabled = false;
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
            tr9.Visible = true;
            tr10.Visible = true;
            tr11.Visible = true;
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