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
    public partial class RequestForCheckForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";

            string formAction = Request.QueryString["FormAction"];
            if (!String.IsNullOrEmpty(formAction))
            {
                if (formAction == "add")
                {
                    msg = "Request For Check Form successfully submitted.";
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
            txtPayableTo.Enabled = true;
            txtChargeTo.Enabled = true;
            rblChargeTo.Enabled = true;
            txtAmount.Enabled = true;
            txtFor.Enabled = true;
            txtRequestedBy.Enabled = true;
            txtApprovedBy.Enabled = true;
            txtShipToAddress.Enabled = true;
            txtShipToCity.Enabled = true;
            txtShipToName.Enabled = true;
            txtShipToState.Enabled = true;
            txtShipToZip.Enabled = true;
            txtAdditionalInfo.Enabled = true;
            txtCCFormTo.Enabled = true;
            ddlStatus.Enabled = true;
        }

        protected void cbShipToSection_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShipToSection.Checked == true)
            {
                tr1.Visible = true;
                tr2.Visible = true;
                tr3.Visible = true;
            }
            else
            {
                tr1.Visible = false;
                tr2.Visible = false;
                tr3.Visible = false;
            }

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

                    Models.RequestForCheckForm newForm = new Models.RequestForCheckForm
                    {
                        Timestamp = DateTime.Now,
                        Company = ddlCompany.SelectedValue,
                        PayableTo = txtPayableTo.Text,
                        Amount = txtAmount.Text,
                        For = txtFor.Text,
                        RequestedBy = txtRequestedBy.Text,
                        ApprovedBy = txtApprovedBy.Text,
                        ShipToName = txtShipToName.Text,
                        ShipToAddress = txtShipToAddress.Text,
                        ShipToCity = txtShipToCity.Text,
                        ShipToState = txtShipToState.Text,
                        ShipToZip = txtShipToZip.Text,
                        AdditionalInfo = txtAdditionalInfo.Text,
                        CCFormToEmail = txtCCFormTo.Text,
                        Status = status,
                        SubmittedByUser = currentUser.UserName
                    };

                    if (rblChargeTo.SelectedValue == "A")
                    {
                        newForm.ChargeToAccountNumber = txtChargeTo.Text;
                    }
                    else
                    {
                        newForm.ChargeToOther = txtChargeTo.Text;
                    }

                    ctx.RequestForCheckForms.Add(newForm);
                    ctx.SaveChanges();

                    formId = newForm.RecordId;

                    TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Request For Check");

                    SendEmail msg = new SendEmail();
                    StringBuilder bodyHtml = new StringBuilder();

                    bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                        .Append("A new Request For Check Form has been submitted.  The details below can also be found at the following link: <a href=\"http://infoshare.wctingle.com/ReportRequestForCheckForm.aspx?formId=")
                        .Append(formId).AppendLine("\">Order ID: ").Append(formId).AppendLine("</a>. <br /><br />")
                        .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Request For Check Form</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(ddlCompany.SelectedValue).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Payable To:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPayableTo.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\" rowspan=\"2\">Charge To:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(rblChargeTo.SelectedItem.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Amount:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtAmount.Text).AppendLine("</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtChargeTo.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">For:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtFor.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Requested By:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtRequestedBy.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Approved By:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtApprovedBy.Text).AppendLine("</td>")
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

                    bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Request For Check Form Submission", bodyHtml.ToString(), submittedForm, true);
                    msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Request For Check Form Submission", bodyHtml.ToString(), submittedForm, false);
                    msg.SendMail("InfoShare@wctingle.com", txtCCFormTo.Text, "Request For Check Form Submission", bodyHtml.ToString(), submittedForm, false);


                    ddlCompany.Enabled = false;
                    txtPayableTo.Enabled = false;
                    txtChargeTo.Enabled = false;
                    rblChargeTo.Enabled = false;
                    txtAmount.Enabled = false;
                    txtFor.Enabled = false;
                    txtRequestedBy.Enabled = false;
                    txtApprovedBy.Enabled = false;
                    txtShipToAddress.Enabled = false;
                    txtShipToCity.Enabled = false;
                    txtShipToName.Enabled = false;
                    txtShipToState.Enabled = false;
                    txtShipToZip.Enabled = false;
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