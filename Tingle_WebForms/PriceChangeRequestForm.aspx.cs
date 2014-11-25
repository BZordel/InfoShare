using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Text;

namespace Tingle_WebForms
{
    public partial class PriceChangeRequestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";

            string formAction = Request.QueryString["FormAction"];
            if (!String.IsNullOrEmpty(formAction))
            {
                if (formAction == "add")
                {
                    msg = "Expedited Order Form successfully submitted.";
                    pnlCompleted.Visible = true;
                    pnlForm.Visible = false;
                }
                else
                {
                    pnlCompleted.Visible = false;
                    pnlForm.Visible = true;
                }
            }

            string sendEmail = Request.QueryString["sendEmail"];
            if (!String.IsNullOrEmpty(sendEmail))
            {
                if (sendEmail.ToLower() == "false")
                {
                    msg += " The email notification to WC Tingle failed.  Please contact support@wctingle.com.";
                }
            }

            lblMessage.Text = msg;
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        protected void lbStartOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
            lblMessage.Visible = false;

            lbStartOver.Visible = false;
            txtCustomer.Enabled = true;
            txtLineNumber.Enabled = true;
            txtAccountNumber.Enabled = true;
            txtQuantity.Enabled = true;
            txtSalesRep.Enabled = true;
            txtProduct.Enabled = true;
            txtOrderNumber.Enabled = true;
            txtPrice.Enabled = true;
            txtCrossRefOldOrderNumber.Enabled = true;
            txtAdditionalInfo.Enabled = true;
            txtCCFormTo.Enabled = true;
            ddlStatus.Enabled = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Int32 formId;

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);
            
            string submittedByuserEmail = currentUser.UserName.Substring(currentUser.UserName.IndexOf(@"\") + 1);
            if (currentUser.UserName.IndexOf("@") == -1)
            {
                submittedByuserEmail += "@wctingle.com";
            }

            using (FormContext ctx = new FormContext())
            {
                Models.PriceChangeRequestForm newPriceChangeRequestForm = new Models.PriceChangeRequestForm
                {
                    Timestamp = DateTime.Now,
                    Company = ddlCompany.SelectedValue,
                    Customer = txtCustomer.Text,
                    LineNumber = txtLineNumber.Text,
                    AccountNumber = accountNumber,
                    Quantity = txtQuantity.Text,
                    SalesRep = txtSalesRep.Text,
                    Product = txtProduct.Text,
                    OrderNumber = txtOrderNumber.Text,
                    Price = txtPrice.Text,
                    CrossReferenceOldOrderNumber = txtCrossRefOldOrderNumber.Text,
                    AdditionalInfo = txtAdditionalInfo.Text,
                    CCFormToEmail = txtCCFormTo.Text,
                    Status = ctx.Statuses.FirstOrDefault(s => s.StatusText == ddlStatus.SelectedItem.Text),
                    SubmittedByUser = currentUser.UserName
                };

                ctx.PriceChangeRequestForms.Add(newPriceChangeRequestForm);
                ctx.SaveChanges();

                formId = newPriceChangeRequestForm.RecordId;

                TForm submittedForm = ctx.TForms.FirstOrDefault(t => t.FormName == "Price Change Request");

                SendEmail msg = new SendEmail();
                StringBuilder bodyHtml = new StringBuilder();

                bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                    .Append("A new Price Change Request Form has been submitted.  The details below can also be found at the following link: <a href=\"http://infoshare.wctingle.com/RepPriceChangeRequestForm.aspx?formId=")
                    .Append(formId).AppendLine("\">Order ID: ").Append(formId).AppendLine("</a>. <br /><br />")
                    .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Price Change Request Form</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(ddlCompany.SelectedValue).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                    .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtCustomer.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line #:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtLineNumber.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtAccountNumber.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtQuantity.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Sales Rep:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtSalesRep.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Product:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtProduct.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtOrderNumber.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Price:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPrice.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Cross Reference Old Order #:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtCrossRefOldOrderNumber.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#bc4445;\">Additional Info: </td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#000;\">").Append(txtAdditionalInfo.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Submitted By: <span style=\"color:#000\">")
                    .Append(currentUser.UserName).AppendLine("</span></td>")
                    .AppendLine("    </tr>")
                    .AppendLine("</table></div><br /><br /><br /><br />");

                bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Price Change Request Form Submission", bodyHtml.ToString(), submittedForm, true);
                result = msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Price Change Request Form Submission", bodyHtml.ToString(), submittedForm, false);

                txtCustomer.Enabled = false;
                txtLineNumber.Enabled = false;
                txtAccountNumber.Enabled = false;
                txtQuantity.Enabled = false;
                txtSalesRep.Enabled = false;
                txtProduct.Enabled = false;
                txtOrderNumber.Enabled = false;
                txtPrice.Enabled = false;
                txtCrossRefOldOrderNumber.Enabled = false;
                txtAdditionalInfo.Enabled = false;
                txtCCFormTo.Enabled = false;
                ddlStatus.Enabled = false;
                ddlCompany.Enabled = false;

                string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                Response.Redirect(pageUrl + "?FormAction=add&sendEmail=" + result.ToString());
            }
        }
    }
}