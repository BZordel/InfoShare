﻿using System;
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
    public partial class RepAdvArchivePriceChangeRequest : System.Web.UI.Page
    {
        FormContext ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                gvReport.DataBind();
            }
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.Where(s => s.StatusText == "Completed").ToList();

            return StatusList;
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            gvReport.PageIndex = 0;
            gvReport.SelectedIndex = -1;
        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                pnlDetails.Visible = true;
                pnlReport.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            gvReport.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
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

                using (FormContext ctx = new FormContext())
                {
                    var priceChangeRequestForm = ctx.PriceChangeRequestForms.FirstOrDefault(p => p.RecordId == recordId);

                    ddlStatus.SelectedValue = priceChangeRequestForm.Status.StatusId.ToString();
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
            TextBox txtCCFormToEdit = (TextBox)fvReport.FindControl("txtCCFormToEdit");
            DropDownList ddlStatusEdit = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            TextBox txtRequestHandlerEdit = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            DropDownList ddlCompanyEdit = (DropDownList)fvReport.FindControl("ddlCompanyEdit");
            Label lblCompanyEdit = (Label)fvReport.FindControl("lblCompanyEdit");

            ddlCompanyEdit.SelectedValue = lblCompanyEdit.Text;

        }

        protected void fvReport_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            int id = Convert.ToInt32(((Label)fvReport.FindControl("lblRecordId")).Text);

            Tingle_WebForms.Models.PriceChangeRequestForm myForm = ctx.PriceChangeRequestForms.FirstOrDefault(p => p.RecordId == id);

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
            TextBox txtCCFormTo = (TextBox)fvReport.FindControl("txtCCFormToEdit");
            DropDownList ddlStatus = (DropDownList)fvReport.FindControl("ddlStatusEdit");
            TextBox txtRequestHandler = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

            string accountNumber = txtAccountNumber.Text;
            int accountNumberLength = txtAccountNumber.Text.Length;

            while (accountNumberLength < 6)
            {
                accountNumber = "0" + accountNumber;
                accountNumberLength++;
            }

            var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

            myForm.Customer = txtCustomer.Text;
            myForm.LineNumber = txtLineNumber.Text;
            myForm.AccountNumber = accountNumber;
            myForm.Quantity = txtQuantity.Text;
            myForm.SalesRep = txtSalesRep.Text;
            myForm.Product = txtProduct.Text;
            myForm.OrderNumber = txtOrderNumber.Text;
            myForm.Price = txtPrice.Text;
            myForm.CrossReferenceOldOrderNumber = txtCrossRefOldOrderNumber.Text;
            myForm.AdditionalInfo = txtAdditionalInfo.Text;
            myForm.Status = statusCode;
            myForm.ModifiedByUser = currentUser.UserName;
            myForm.RequestHandler = txtRequestHandler.Text;

            ctx.PriceChangeRequestForms.Attach(myForm);
            ctx.Entry(myForm).State = EntityState.Modified;

            ctx.SaveChanges();
            gvReport.DataBind();
            fvReport.DataBind();
            pnlDetails.Visible = false;
            pnlReport.Visible = true;

        }

        public void UpdateForm(int RecordId)
        {



        }

        public IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> GetPriceChangeRequestForms
        (
        [Control("txtCustomer")] string Customer,
        [Control("txtLineNumber")] string LineNumber,
        [Control("txtFromDate")] string FromDate,
        [Control("txtToDate")] string ToDate,
        [Control("ddlCompany")] string Company, 
        [Control("txtAccountNumber")] string AccountNumber,
        [Control("txtQuantity")] string Quantity,
        [Control("txtSalesRep")] string SalesRep,
        [Control("txtProduct")] string Product,
        [Control("txtOrderNumber")] string OrderNumber,
        [Control("txtPrice")] string Price,
        [Control("txtCrossRefOldOrderNumber")] string CrossReferenceOldOrderNumber,
        [Control("txtAdditionalInfo")] string AdditionalInfo,
        [Control("txtRequestHandler")] string RequestHandler
        )
        {
            DateTime dtFromDate;
            DateTime dtToDate;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            if (ctx.PriceChangeRequestForms.Any(f => f.Status.StatusText == "Completed"))
            {
                IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> priceChangeRequestFormList = ctx.PriceChangeRequestForms
                    .Where(f => f.Status.StatusText == "Completed").OrderByDescending(f => f.Timestamp);

                if (currentUser.UserRole.RoleName == "ReportsUser")
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(eofl => eofl.SubmittedByUser == currentUser.UserName);
                }

                if (!String.IsNullOrWhiteSpace(Customer))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Customer.Contains(Customer));
                }
                if (!String.IsNullOrWhiteSpace(LineNumber))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.LineNumber.Contains(LineNumber));
                }
                if (!String.IsNullOrWhiteSpace(FromDate))
                {
                    DateTime.TryParse(FromDate, out dtFromDate);
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Timestamp >= dtFromDate);
                }
                if (!String.IsNullOrWhiteSpace(ToDate))
                {
                    DateTime.TryParse(ToDate, out dtToDate);
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Timestamp <= dtToDate);
                }
                if (!String.IsNullOrWhiteSpace(Company))
                {
                    if (Company != "Any")
                    {
                        priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Company.Equals(Company));
                    }
                }
                if (!String.IsNullOrWhiteSpace(AccountNumber))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber));
                }
                if (!String.IsNullOrWhiteSpace(Quantity))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Quantity.Contains(Quantity));
                }
                if (!String.IsNullOrWhiteSpace(SalesRep))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.SalesRep.Contains(SalesRep));
                }
                if (!String.IsNullOrWhiteSpace(Product))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Product.Contains(Product));
                }
                if (!String.IsNullOrWhiteSpace(OrderNumber))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.OrderNumber.Contains(OrderNumber));
                }
                if (!String.IsNullOrWhiteSpace(Price))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.Price.Contains(Price));
                }
                if (!String.IsNullOrWhiteSpace(CrossReferenceOldOrderNumber))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.CrossReferenceOldOrderNumber.Contains(CrossReferenceOldOrderNumber));
                }
                if (!String.IsNullOrWhiteSpace(AdditionalInfo))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.AdditionalInfo.Contains(AdditionalInfo));
                }
                if (!String.IsNullOrWhiteSpace(RequestHandler))
                {
                    priceChangeRequestFormList = priceChangeRequestFormList.Where(forms => forms.RequestHandler.Contains(RequestHandler));
                }

                return priceChangeRequestFormList;
            }
            else
            {
                return null;
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
    }
}