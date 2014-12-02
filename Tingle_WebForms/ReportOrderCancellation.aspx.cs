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
    public partial class ReportOrderCancellation : System.Web.UI.Page
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

        public Tingle_WebForms.Models.OrderCancellationForm GetFormDetails([Control("gvReport")] int? RecordId)
        {
            var myForm = ctx.OrderCancellationForms.FirstOrDefault();
            if (RecordId != null)
            {
                myForm = ctx.OrderCancellationForms.FirstOrDefault(f => f.RecordId == RecordId);
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

        public IEnumerable<PurchaseOrderStatus> GetPOStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.POStatuses.ToList();

                return StatusList;
            }
        }

        public IQueryable<Tingle_WebForms.Models.OrderCancellationForm> GetOrderCancellationForms(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("txtDateRequiredFrom")] string DateRequiredFrom,
            [Control("txtDateRequiredTo")] string DateRequiredTo,
            [Control("ddlCompany")] string Company,
            [Control("txtOrderNumber")] string OrderNumber,
            [Control("txtArmstrongReference")] string ArmstrongReference,
            [Control("txtCustomer")] string Customer,
            [Control("txtPO")] string PO,
            [Control("txtSKU")] string SKU,
            [Control("ddlStatusOfPO")] int POStatusId,
            [Control("txtLine")] string Line,
            [Control("txtLineOfPO")] string LineOfPO,
            [Control("txtSize")] string Size,
            [Control("txtShipVia")] string ShipVia,
            [Control("txtSerial")] string Serial,
            [Control("txtTruckRoute")] string TruckRoute,
            [Control("txtAdditionalInfo")] string AdditionalInfo,
            [Control("txtRequestHandler")] string RequestHandler,
            [Control("ddlStatus")] int StatusId,
            [Control("ddlGlobalStatus")] string GlobalStatus,
            [QueryString("formId")] Nullable<Int32> formId
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;
            DateTime dtDateRequiredFrom;
            DateTime dtDateRequiredTo;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.OrderCancellationForm> OrderCancellationFormList = ctx.OrderCancellationForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(DateRequiredFrom))
            {
                DateTime.TryParse(DateRequiredFrom, out dtDateRequiredFrom);
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.DateRequired >= dtDateRequiredFrom);
            }
            if (!String.IsNullOrWhiteSpace(DateRequiredTo))
            {
                DateTime.TryParse(DateRequiredTo, out dtDateRequiredTo);
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.DateRequired <= dtDateRequiredTo);
            }
            if (!String.IsNullOrWhiteSpace(Company))
            {
                if (Company != "Any")
                {
                    OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Company.Equals(Company));
                }
            }
            if (!String.IsNullOrWhiteSpace(OrderNumber))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.OrderNumber.Contains(OrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(ArmstrongReference))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.ArmstrongReference.Contains(ArmstrongReference.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(PO))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.PO.Contains(PO.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(SKU))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.SKU.Contains(SKU.Trim()));
            }

            if (!String.IsNullOrWhiteSpace(POStatusId.ToString()))
            {
                if (POStatusId > 0)
                {
                    OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.POStatus.RecordId.Equals(POStatusId));
                }
            }

            if (!String.IsNullOrWhiteSpace(Line))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Line.Contains(Line.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(LineOfPO))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.LineOfPO.Contains(LineOfPO.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Size))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Size.Contains(Size));
            }
            if (!String.IsNullOrWhiteSpace(ShipVia))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.ShipVia.Contains(ShipVia.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Serial))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Serial.Contains(Serial.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(TruckRoute))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.TruckRoute.Contains(TruckRoute.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AdditionalInfo))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(RequestHandler))
            {
                OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(StatusId.ToString()))
            {
                if (StatusId > 0)
                {
                    OrderCancellationFormList = OrderCancellationFormList.Where(forms => forms.Status.StatusId.Equals(StatusId));
                }
            }

            return OrderCancellationFormList;
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

            Tingle_WebForms.Models.OrderCancellationForm myForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == id);

            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            TextBox txtOrderNumber = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            TextBox txtArmstrongReference = (TextBox)fvReport.FindControl("txtArmstrongReferenceEdit");
            TextBox txtCustomer = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtPO = (TextBox)fvReport.FindControl("txtPOEdit");
            TextBox txtSKU = (TextBox)fvReport.FindControl("txtSKUEdit");
            DropDownList ddlStatusOfPO = (DropDownList)fvReport.FindControl("ddlStatusOfPOEdit");
            TextBox txtLine = (TextBox)fvReport.FindControl("txtLineEdit");
            TextBox txtLineOfPO = (TextBox)fvReport.FindControl("txtLineOfPOEdit");
            TextBox txtSize = (TextBox)fvReport.FindControl("txtSizeEdit");
            System.Web.UI.HtmlControls.HtmlInputText txtDateRequired = (System.Web.UI.HtmlControls.HtmlInputText)fvReport.FindControl("txtDateRequiredEdit");
            TextBox txtShipVia = (TextBox)fvReport.FindControl("txtShipViaEdit");
            TextBox txtSerial = (TextBox)fvReport.FindControl("txtSerialEdit");
            TextBox txtTruckRoute = (TextBox)fvReport.FindControl("txtTruckRouteEdit");
            TextBox txtAdditionalInfo = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandler = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");

            DropDownList ddlStatus = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            int poStatusId = Convert.ToInt32(ddlStatusOfPO.SelectedValue);

            DateTime tryDateRequired;
            Nullable<DateTime> dateRequired = null;
            DateTime.TryParse(txtDateRequired.Value, out tryDateRequired);

            if (tryDateRequired.Year > 0001)
            {
                dateRequired = tryDateRequired;
            }

            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);
            var poStatusCode = ctx.POStatuses.FirstOrDefault(p => p.RecordId == poStatusId);

            myForm.Company = ddlCompanyEdit.SelectedValue;
            myForm.OrderNumber = txtOrderNumber.Text;
            myForm.ArmstrongReference = txtArmstrongReference.Text;
            myForm.Customer = txtCustomer.Text;
            myForm.PO = txtPO.Text;
            myForm.SKU = txtSKU.Text;
            myForm.POStatus = poStatusCode;
            myForm.Line = txtLine.Text;
            myForm.LineOfPO = txtLineOfPO.Text;
            myForm.Size = txtSize.Text;
            myForm.DateRequired = dateRequired.Value;
            myForm.ShipVia = txtShipVia.Text;
            myForm.Serial = txtSerial.Text;
            myForm.TruckRoute = txtTruckRoute.Text;
            myForm.AdditionalInfo = txtAdditionalInfo.Text;
            myForm.RequestHandler = txtRequestHandler.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = System.Web.HttpContext.Current.User.Identity.Name.Substring(System.Web.HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);

            if (statusCode.StatusText == "Completed")
            {
                myForm.CompletedNotes = txtCompletionNotes.Text;
                myForm.CCCompletedFormToEmail = txtCCCompletedFormToEmail.Text;
            }

            ctx.OrderCancellationForms.Attach(myForm);
            ctx.Entry(myForm).State = EntityState.Modified;

            ctx.SaveChanges();

            if (myForm.Status.StatusText == "Completed")
            {
                SendCompletedEmail(myForm.RecordId);
            }

            gvReport.DataBind();
            fvReport.DataBind();
            ddlStatusOfPO.DataBind();
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;

        }

        public void SendCompletedEmail(Int32 FormId)
        {
            Tingle_WebForms.Models.OrderCancellationForm myForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Order Cancellation");

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
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Order Cancellation Form Completed</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.OrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Armstrong Reference:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ArmstrongReference).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Customer).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">PO:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.PO).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">SKU:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SKU).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Status of PO:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.POStatus.Status).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Line).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line of PO:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.LineOfPO).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Size:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Size).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Date Required:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.DateRequired.ToShortDateString()).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Ship Via:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.ShipVia).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Serial:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Serial).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Truck Route:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.TruckRoute).AppendLine("</td>")
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

            bool result = msg.SendMail("InfoShare@wctingle.com", "rzordel@gmail.com", "Order Cancellation Form Completion", bodyHtml.ToString(), submittedForm, true);
            msg.SendMail("InfoShare@wctingle.com", submittedByuserEmail, "Order Cancellation Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCFormToEmail, "Order Cancellation Form Completion", bodyHtml.ToString(), submittedForm, false);
            msg.SendMail("InfoShare@wctingle.com", myForm.CCCompletedFormToEmail, "Order Cancellation Form Completion", bodyHtml.ToString(), submittedForm, false);
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
                    var orderCancellationForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = orderCancellationForm.Status.StatusId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
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
            TextBox txtOrderNumberEdit = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            TextBox txtArmstrongReferenceEdit = (TextBox)fvReport.FindControl("txtArmstrongReferenceEdit");
            TextBox txtCustomerEdit = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtPOEdit = (TextBox)fvReport.FindControl("txtPOEdit");
            TextBox txtSKUEdit = (TextBox)fvReport.FindControl("txtSKUEdit");
            DropDownList ddlStatusOfPOEdit = (DropDownList)fvReport.FindControl("ddlStatusOfPOEdit");
            TextBox txtLineEdit = (TextBox)fvReport.FindControl("txtLineEdit");
            TextBox txtLineOfPOEdit = (TextBox)fvReport.FindControl("txtLineOfPOEdit");
            TextBox txtSizeEdit = (TextBox)fvReport.FindControl("txtSizeEdit");
            System.Web.UI.HtmlControls.HtmlInputText txtDateRequiredEdit = (System.Web.UI.HtmlControls.HtmlInputText)fvReport.FindControl("txtDateRequiredEdit");
            TextBox txtShipViaEdit = (TextBox)fvReport.FindControl("txtShipViaEdit");
            TextBox txtSerialEdit = (TextBox)fvReport.FindControl("txtSerialEdit");
            TextBox txtTruckRouteEdit = (TextBox)fvReport.FindControl("txtTruckRouteEdit");
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
                txtOrderNumberEdit.Enabled = false;
                txtArmstrongReferenceEdit.Enabled = false;
                txtCustomerEdit.Enabled = false;
                txtPOEdit.Enabled = false;
                txtSKUEdit.Enabled = false;
                ddlStatusOfPOEdit.Enabled = false;
                txtLineEdit.Enabled = false;
                txtLineOfPOEdit.Enabled = false;
                txtSizeEdit.Enabled = false;
                txtDateRequiredEdit.Disabled = true;
                txtShipViaEdit.Enabled = false;
                txtSerialEdit.Enabled = false;
                txtTruckRouteEdit.Enabled = false;
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
                    txtOrderNumberEdit.Enabled = true;
                    txtArmstrongReferenceEdit.Enabled = true;
                    txtCustomerEdit.Enabled = true;
                    txtPOEdit.Enabled = true;
                    txtSKUEdit.Enabled = true;
                    ddlStatusOfPOEdit.Enabled = true;
                    txtLineEdit.Enabled = true;
                    txtLineOfPOEdit.Enabled = true;
                    txtSizeEdit.Enabled = true;
                    txtDateRequiredEdit.Disabled = false;
                    txtShipViaEdit.Enabled = true;
                    txtSerialEdit.Enabled = true;
                    txtTruckRouteEdit.Enabled = true;
                    txtAdditionalInfoEdit.Enabled = true;
                    txtRequestHandlerEdit.Enabled = true;
                    txtCompletionNotes.Enabled = true;
                    txtCCCompletedFormToEmail.Enabled = true;
                    
                }
                else
                {
                    ddlCompanyEdit.Enabled = false;
                    txtOrderNumberEdit.Enabled = false;
                    txtArmstrongReferenceEdit.Enabled = false;
                    txtCustomerEdit.Enabled = false;
                    txtPOEdit.Enabled = false;
                    txtSKUEdit.Enabled = false;
                    ddlStatusOfPOEdit.Enabled = false;
                    txtLineEdit.Enabled = false;
                    txtLineOfPOEdit.Enabled = false;
                    txtSizeEdit.Enabled = false;
                    txtDateRequiredEdit.Disabled = true;
                    txtShipViaEdit.Enabled = false;
                    txtSerialEdit.Enabled = false;
                    txtTruckRouteEdit.Enabled = false;
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

        protected void ddlStatusOfPOEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlStatusOfPOEdit = (DropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var orderCancellationForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatusOfPOEdit.SelectedValue = orderCancellationForm.POStatus.RecordId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}