﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Text;

namespace Tingle_WebForms.Forms
{
    public partial class ExpeditedOrderForm : System.Web.UI.Page
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

        public IEnumerable<ExpediteCode> GetExpediteCodes()
        {
            FormContext ctx = new FormContext();
            var ExpediteCodeList = ctx.ExpediteCodes.Where(c => c.Status == 1).OrderBy(c => c.ExpediteCodeID).Select(c => new { Code = c.Code + " - " + c.Description, ExpediteCodeID = c.ExpediteCodeID }).ToList()
                .Select(x => new ExpediteCode { ExpediteCodeID = x.ExpediteCodeID, Code = x.Code } );
       
            return ExpediteCodeList;
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime tryInstallDate;
            Nullable<DateTime> installDate = null;
            Int32 formId;
            int statusId;
            int expediteCodeId;
            

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);



            DateTime.TryParse(txtInstallDate.Value, out tryInstallDate);

            if (tryInstallDate.Year > 0001)
            {
                installDate = tryInstallDate;
            }
                
            AddExpeditedOrderForms ExpeditedOrderForm = new AddExpeditedOrderForms();

            expediteCodeId = Convert.ToInt32(ddlExpediteCode.SelectedValue);
            statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            FormContext ctx = new FormContext();
            var expediteCode = ctx.ExpediteCodes.Where(c => c.ExpediteCodeID.Equals(expediteCodeId)).FirstOrDefault();
            var status = ctx.Statuses.Where(s => s.StatusId.Equals(statusId)).FirstOrDefault();

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }
                
            string submittedByUser = currentUser.UserName;
            string submittedByuserEmail = submittedByUser.Substring(currentUser.UserName.IndexOf(@"\") + 1);

            if (submittedByUser.IndexOf("@") == -1)
            {
                submittedByuserEmail += "@wctingle.com";
            }

            bool submitSuccess = ExpeditedOrderForm.AddExpeditedOrderForm(txtOowOrderNumber.Text, txtCustomer.Text, accountNumber, expediteCode, txtPurchaseOrderNumber.Text,
                txtMaterialSku.Text, txtQuantityOrdered.Text, installDate, txtSM.Text, txtContactName.Text, txtPhoneNumber.Text, txtShipToName.Text, 
                txtShipToAddress.Text, txtShipToCity.Text, txtShipToState.Text, txtShipToZip.Text, txtAdditionalInfo.Text, status, submittedByUser, txtCCFormTo.Text, ddlCompany.SelectedValue, out formId);

            if (submitSuccess)
            {
                TForm submittedForm = ctx.TForms.Where(tf => tf.FormName == "Expedited Order Form").SingleOrDefault();

                SendEmail msg = new SendEmail();
                StringBuilder bodyHtml = new StringBuilder();

                bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                    .Append("A new Expedited Order Form has been submitted.  The details below can also be found at the following link: <a href=\"http://infoshare.wctingle.com/RepExpeditedOrderForm.aspx?formId=")
                    .Append(formId).AppendLine("\">Order ID: ").Append(formId).AppendLine("</a>. <br /><br />")
                    .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Expedited Order Form</td>")
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
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Expedite Code:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(expediteCode.Code).Append(" - ").Append(expediteCode.Description).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Contact Name:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtContactName.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Phone Number:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPhoneNumber.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Material SKU#:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtMaterialSku.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity Ordered:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtQuantityOrdered.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtAccountNumber.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Purchase Order #:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPurchaseOrderNumber.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">OOW Order Number:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtOowOrderNumber.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">S/M:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtSM.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Install Date:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtInstallDate.Value).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Status:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(status.StatusText).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;border: 4px solid #d0604c;color:#FFF; background-color:#bc4445;\" colspan=\"4\">Ship To (If different than default)</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Name:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipToName.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Street Address:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipToAddress.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">City: </td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipToCity.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">State:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipToState.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Zip:</td>")
                    .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipToZip.Text).AppendLine("</td>")
                    .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                    .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .AppendLine("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#bc4445;\">Additional Info: </td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;color:#000;\">").Append(txtAdditionalInfo.Text).AppendLine("</td>")
                    .AppendLine("    </tr>")
                    .AppendLine("    <tr>")
                    .Append("        <td colspan=\"4\" style=\"text-align:center;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Submitted By: <span style=\"color:#000\">")
                    .Append(submittedByUser).AppendLine("</span></td>")
                    .AppendLine("    </tr>")
                    .AppendLine("</table></div><br /><br /><br /><br />");

                bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Expedited Order Form Submission", bodyHtml.ToString(), submittedForm, true);
                //bool result = true;
                result = msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Expedited Order Form Submission", bodyHtml.ToString(), submittedForm, false);

                txtAccountNumber.Enabled = false;
                txtAdditionalInfo.Enabled = false;
                txtContactName.Enabled = false;
                txtCustomer.Enabled = false;
                txtInstallDate.Disabled = true;
                txtMaterialSku.Enabled = false;
                txtOowOrderNumber.Enabled = false;
                txtPhoneNumber.Enabled = false;
                txtPurchaseOrderNumber.Enabled = false;
                txtQuantityOrdered.Enabled = false;
                txtShipToAddress.Enabled = false;
                txtShipToCity.Enabled = false;
                txtShipToName.Enabled = false;
                txtShipToState.Enabled = false;
                txtShipToZip.Enabled = false;
                txtSM.Enabled = false;
                ddlExpediteCode.Enabled = false;
                ddlCompany.Enabled = false;

                string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                Response.Redirect(pageUrl + "?FormAction=add&sendEmail=" + result.ToString());
            }
            else
            {
                lblMessage.Text = "Unable to submit form.  Please try again or contact your system administrator.";
            }
        }

        protected void lbStartOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
            lblMessage.Visible = false;

            lbStartOver.Visible = false;
            txtAccountNumber.Enabled = true;
            txtAdditionalInfo.Enabled = true;
            txtContactName.Enabled = true;
            txtCustomer.Enabled = true;
            txtInstallDate.Disabled = false;
            txtMaterialSku.Enabled = true;
            txtOowOrderNumber.Enabled = true;
            txtPhoneNumber.Enabled = true;
            txtPurchaseOrderNumber.Enabled = true;
            txtQuantityOrdered.Enabled = true;
            txtShipToAddress.Enabled = true;
            txtShipToCity.Enabled = true;
            txtShipToName.Enabled = true;
            txtShipToState.Enabled = true;
            txtShipToZip.Enabled = true;
            txtSM.Enabled = true;
            ddlExpediteCode.Enabled = true;
            ddlCompany.Enabled = true;
        }

    }
}