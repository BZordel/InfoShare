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
    public partial class OrderCancellationForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";

            string formAction = Request.QueryString["FormAction"];
            if (!String.IsNullOrEmpty(formAction))
            {
                if (formAction == "add")
                {
                    msg = "Order Cancellation Form successfully submitted.";
                    pnlCompleted.Visible = true;
                    pnlForm.Visible = false;
                }
                else
                {
                    msg = "Form Submission Failed.  Please try again or contact your System Administrator for more information.";
                    pnlCompleted.Visible = true;
                    pnlForm.Visible = false;
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

        public IEnumerable<PurchaseOrderStatus> GetPOStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.POStatuses.ToList();

                return StatusList;
            }
        }

        protected void lbStartOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
            lblMessage.Visible = false;

            lbStartOver.Visible = false;
            txtOrderNumber.Enabled = true;
            txtAdditionalInfo.Enabled = true;
            txtArmstrongReference.Enabled = true;
            txtCustomer.Enabled = true;
            txtPO.Enabled = true;
            txtSKU.Enabled = true;
            ddlStatusPO.Enabled = true;
            txtLine.Enabled = true;
            txtLineOfPO.Enabled = true;
            txtSize.Enabled = true;
            txtDateRequired.Disabled = false;
            txtShipVia.Enabled = true;
            txtSerial.Enabled = true;
            txtTruckRoute.Enabled = true;
            txtCCFormTo.Enabled = true;
            ddlCompany.Enabled = true;
            ddlStatus.Enabled = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tryDateRequired;
                Nullable<DateTime> dateRequired = null;
                Int32 formId;
                int statusId, poStatusId;

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                DateTime.TryParse(txtDateRequired.Value, out tryDateRequired);

                if (tryDateRequired.Year > 0001)
                {
                    dateRequired = tryDateRequired;
                }

                statusId = Convert.ToInt32(ddlStatus.SelectedValue);
                poStatusId = Convert.ToInt32(ddlStatusPO.SelectedValue);

                string submittedByUser = currentUser.UserName;
                string submittedByuserEmail = submittedByUser.Substring(currentUser.UserName.IndexOf(@"\") + 1);

                if (submittedByUser.IndexOf("@") == -1)
                {
                    submittedByuserEmail += "@wctingle.com";
                }

                using (FormContext ctx = new FormContext())
                {
                    var status = ctx.Statuses.FirstOrDefault(s => s.StatusId.Equals(statusId));
                    var poStatus = ctx.POStatuses.FirstOrDefault(p => p.RecordId.Equals(poStatusId));

                    Models.OrderCancellationForm newForm = new Models.OrderCancellationForm
                    {
                        Timestamp = DateTime.Now,
                        Company = ddlCompany.SelectedValue,
                        OrderNumber = txtOrderNumber.Text,
                        ArmstrongReference = txtArmstrongReference.Text,
                        Customer = txtCustomer.Text,
                        PO = txtPO.Text,
                        SKU = txtSKU.Text,
                        POStatus = poStatus,
                        Line = txtLine.Text,
                        LineOfPO = txtLineOfPO.Text,
                        Size = txtSize.Text,
                        DateRequired = dateRequired.Value,
                        ShipVia = txtShipVia.Text,
                        Serial = txtSerial.Text,
                        TruckRoute = txtTruckRoute.Text,
                        AdditionalInfo = txtAdditionalInfo.Text,
                        CCFormToEmail = txtCCFormTo.Text,
                        Status = ctx.Statuses.FirstOrDefault(s => s.StatusText == ddlStatus.SelectedItem.Text),
                        SubmittedByUser = currentUser.UserName
                    };

                    ctx.OrderCancellationForms.Add(newForm);
                    ctx.SaveChanges();

                    formId = newForm.RecordId;

                    TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Order Cancellation");

                    SendEmail msg = new SendEmail();
                    StringBuilder bodyHtml = new StringBuilder();

                    bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                        .Append("A new Order Cancellation Form has been submitted.  The details below can also be found at the following link: <a href=\"http://infoshare.wctingle.com/ReportOrderCancellation.aspx?formId=")
                        .Append(formId).AppendLine("\">Order ID: ").Append(formId).AppendLine("</a>. <br /><br />")
                        .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Order Cancellation Form</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(ddlCompany.SelectedValue).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtOrderNumber.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Armstrong Reference:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtArmstrongReference.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtCustomer.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">PO:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtPO.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">SKU:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtSKU.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Status of PO:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(ddlStatusPO.SelectedItem.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtLine.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line of PO:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtLineOfPO.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Size:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtSize.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;border: 4px solid #d0604c;color:#FFF; background-color:#bc4445;\" colspan=\"4\"></td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Date Required:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtDateRequired.Value).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Ship Via:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtShipVia.Text).AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Serial:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtSerial.Text).AppendLine("</td>")
                        .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Truck Route:</td>")
                        .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(txtTruckRoute.Text).AppendLine("</td>")
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
                    msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Expedited Order Form Submission", bodyHtml.ToString(), submittedForm, false);
                    msg.SendMail("InfoShare@wctingle.com", txtCCFormTo.Text, "Expedited Order Form Submission", bodyHtml.ToString(), submittedForm, false);

                    txtOrderNumber.Enabled = false;
                    ddlCompany.Enabled = false;
                    txtArmstrongReference.Enabled = false;
                    txtCustomer.Enabled = false;
                    txtPO.Enabled = false;
                    txtSKU.Enabled = false;
                    ddlStatusPO.Enabled = false;
                    txtLine.Enabled = false;
                    txtLineOfPO.Enabled = false;
                    txtSize.Enabled = false;
                    txtDateRequired.Disabled = true;
                    txtShipVia.Enabled = false;
                    txtSerial.Enabled = false;
                    txtTruckRoute.Enabled = false;
                    txtAdditionalInfo.Enabled = false;
                    txtCCFormTo.Enabled = false;
                    ddlStatus.Enabled = false;
                    txtAdditionalInfo.Enabled = false;

                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?FormAction=add&sendEmail=" + result.ToString(), false);
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