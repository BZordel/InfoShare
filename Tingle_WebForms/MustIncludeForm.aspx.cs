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
    public partial class MustIncludeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";

            string formAction = Request.QueryString["FormAction"];
            if (!String.IsNullOrEmpty(formAction))
            {
                if (formAction == "add")
                {
                    msg = "Must Include Form successfully submitted.";
                    pnlCompleted.Visible = true;
                    pnlForm.Visible = false;
                }
                else
                {
                    msg = "Form Submission Failed.  Please try again or contact your System Administrator for more information.";
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
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.Statuses.ToList();

                return StatusList;
            }
        }

        protected void lbStartOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
            lblMessage.Visible = false;

            lbStartOver.Visible = false;
            ddlCompany.Enabled = true;
            txtPO.Enabled = true;
            txtArmstrongReference.Enabled = true;
            txtPattern.Enabled = true;
            txtLine.Enabled = true;
            txtOrderNumber.Enabled = true;
            txtCustomer.Enabled = true;
            txtWarehouse.Enabled = true;
            txtAdditionalInfo.Enabled = true;
            txtCCFormTo.Enabled = true;
            ddlStatus.Enabled = true;
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 formId;
                int statusId;

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                statusId = Convert.ToInt32(ddlStatus.SelectedValue);

                string submittedByUser = currentUser.UserName;
                string submittedByuserEmail = submittedByUser.Substring(currentUser.UserName.IndexOf(@"\") + 1);

                if (submittedByUser.IndexOf("@") == -1)
                {
                    submittedByuserEmail += "@wctingle.com";
                }

                using (FormContext ctx = new FormContext())
                {
                    var status = ctx.Statuses.FirstOrDefault(s => s.StatusId.Equals(statusId));

                    Models.MustIncludeForm newForm = new Models.MustIncludeForm
                    {
                        Timestamp = DateTime.Now,
                        Company = ddlCompany.SelectedValue,
                        PO = txtPO.Text,
                        ArmstrongReference = txtArmstrongReference.Text,
                        Pattern = txtPattern.Text,
                        Line = txtLine.Text,
                        OrderNumber = txtOrderNumber.Text,
                        Customer = txtCustomer.Text,
                        Warehouse = txtWarehouse.Text,
                        AdditionalInfo = txtAdditionalInfo.Text,
                        CCFormToEmail = txtCCFormTo.Text,
                        Status = status,
                        SubmittedByUser = currentUser.UserName
                    };

                    ctx.MustIncludeForms.Add(newForm);
                    ctx.SaveChanges();

                    formId = newForm.RecordId;

                    TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Must Include");

                    SendEmail msg = new SendEmail();
                    StringBuilder bodyHtml = new StringBuilder();

                    bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                        .Append("A new Must Include Form has been submitted.  The details below can also be found at the following link: <a href=\"http://infoshare.wctingle.com/ReportMustIncludeForm.aspx?formId=")
                        .Append(formId).AppendLine("\">Order ID: ").Append(formId).AppendLine("</a>. <br /><br />")
                        .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Must Include Form</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(ddlCompany.SelectedValue).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">PO:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPO.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Armstrong Reference:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtArmstrongReference.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Pattern:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPattern.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtLine.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtOrderNumber.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtCustomer.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Warehouse:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtWarehouse.Text).AppendLine("</td>")
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

                    bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Must Include Form Submission", bodyHtml.ToString(), submittedForm, true);
                    msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Must Include Form Submission", bodyHtml.ToString(), submittedForm, false);
                    msg.SendMail("InfoShare@wctingle.com", txtCCFormTo.Text, "Must Include Form Submission", bodyHtml.ToString(), submittedForm, false);

                    ddlCompany.Enabled = false;
                    txtPO.Enabled = false;
                    txtArmstrongReference.Enabled = false;
                    txtPattern.Enabled = false;
                    txtLine.Enabled = false;
                    txtOrderNumber.Enabled = false;
                    txtCustomer.Enabled = false;
                    txtWarehouse.Enabled = false;
                    txtAdditionalInfo.Enabled = false;
                    txtCCFormTo.Enabled = false;
                    ddlStatus.Enabled = false;

                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?FormAction=add&sendEmail=" + result.ToString());
                }
            }
            catch (Exception ex)
            {
                pnlCompleted.Visible = true;
                pnlForm.Visible = false;
                lblMessage.Text = "An error occured during submission of this form.  <br /><br />It is possible that the form was completed before this error occurred, <br />so please contact your System Administrator before re-submitting.";
            }
        }


    }
}