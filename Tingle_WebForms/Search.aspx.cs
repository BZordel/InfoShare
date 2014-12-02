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
    public partial class Search : System.Web.UI.Page
    {
        private int RowCount
        {
            get
            {
                if (ViewState["RowCount"] != null)
                {
                    return (int)ViewState["RowCount"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["RowCount"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SearchValue"] != null)
                {
                    txtSearch.Text = Session["SearchValue"].ToString();
                    SearchForText(10, 0);
                }
            }
            else
            {
                plcPaging.Controls.Clear();
                CreatePagingControl();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["SearchValue"] = txtSearch.Text;
            SearchForText(10, 0);
        }

        private void SearchForText(int takeCount, int skipCount)
        {
            if (txtSearch.Text != "")
            {
                using (FormContext ctx = new FormContext())
                {
                    LiteralControl lineBreak = new LiteralControl("<br />");
                    LiteralControl divBegin = new LiteralControl("<div style=\"width:100%; float:left\">");
                    LiteralControl divEnd = new LiteralControl("</div>");

                    var searchResults = new List<SearchModel>();

                    int count = 0;
                    int resultCount = 0;

                    IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderForms = ctx.ExpeditedOrderForms
                        .Where(f => f.AccountNumber != null && f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.AdditionalInfo != null && f.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.CCCompletedFormToEmail != null && f.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.CCFormToEmail != null && f.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.Company != null && f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.CompletedNotes != null && f.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ContactName != null && f.ContactName.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.Customer != null && f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ExpediteCode.Code != null && f.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ExpediteCode.Description != null && f.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.MaterialSku != null && f.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ModifiedByUser != null && f.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.OowOrderNumber != null && f.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.PhoneNumber != null && f.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.PurchaseOrderNumber != null && f.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.QuantityOrdered != null && f.QuantityOrdered.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.RequestHandler != null && f.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ShipToAddress != null && f.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ShipToCity != null && f.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ShipToName != null && f.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ShipToState != null && f.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.ShipToZip != null && f.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.SM != null && f.SM.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.Status.StatusText != null && f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                        || f.SubmittedByUser != null && f.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())
                    );

                    IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderForms = ctx.DirectOrderForms
                        .Where(f => f.Company != null && f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.AdditionalInfo != null && f.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.RequestHandler != null && f.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CCFormToEmail != null && f.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CCCompletedFormToEmail != null && f.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CompletedNotes != null && f.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ModifiedByUser != null && f.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.Status.StatusText != null && f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.SubmittedByUser != null && f.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())

                            || f.AccountNumber != null && f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ContactName != null && f.ContactName.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.Customer != null && f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ExpediteCode.Code != null && f.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ExpediteCode.Description != null && f.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.MaterialSku != null && f.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.OowOrderNumber != null && f.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.PhoneNumber != null && f.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.PurchaseOrderNumber != null && f.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.QuantityOrdered != null && f.QuantityOrdered.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ShipToAddress != null && f.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ShipToCity != null && f.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ShipToName != null && f.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ShipToState != null && f.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ShipToZip != null && f.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.SM != null && f.SM.ToLower().Contains(txtSearch.Text.ToLower())
                    );

                    IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushForms = ctx.HotRushForms
                        .Where(f => f.Company != null && f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.AdditionalInfo != null && f.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.RequestHandler != null && f.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CCFormToEmail != null && f.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CCCompletedFormToEmail != null && f.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.CompletedNotes != null && f.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.ModifiedByUser != null && f.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.Status.StatusText != null && f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.SubmittedByUser != null && f.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())

                            || f.Customer != null && f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.EntireOrderOrLineNumber != null && f.EntireOrderOrLineNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || f.OrderNumber != null && f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                    );

                    count += expeditedOrderForms.Count();
                    count += directOrderForms.Count();
                    count += hotRushForms.Count();

                    lblResults.Text = count + " Results Found";

                    FindExpeditedOrderForms(searchResults, ref resultCount, expeditedOrderForms);
                    FindDirectOrderForms(searchResults, ref resultCount, directOrderForms);
                    FindHotRushForms(searchResults, ref resultCount, hotRushForms);

                    var pagedSearchResults = searchResults.Take(takeCount).Skip(skipCount).ToList();

                    PagedDataSource page = new PagedDataSource();
                    page.AllowCustomPaging = true;
                    page.AllowPaging = true;
                    page.DataSource = pagedSearchResults.ToList();
                    page.PageSize = 10;
                    rptrSearchResults.DataSource = page;
                    rptrSearchResults.DataBind();

                    RowCount = searchResults.ToList().Count;
                    plcPaging.Controls.Clear();
                    CreatePagingControl();
                }
            }
        }

        private void FindDirectOrderForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderForms)
        {
            foreach (Tingle_WebForms.Models.DirectOrderForm form in directOrderForms)
            {
                string textMatches = "";

                if (form.Company != null) { if (form.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.Company + ", "; } }
                if (form.AdditionalInfo != null) { if (form.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Additional Info:</b> " + form.AdditionalInfo + ", "; } }
                if (form.CCFormToEmail != null) { if (form.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Form To:</b> " + form.CCFormToEmail + ", "; } }
                if (form.Status != null) { if (form.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.Status.StatusText + ", "; } }
                if (form.RequestHandler != null) { if (form.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expeditor Handler:</b> " + form.RequestHandler + ", "; } }
                if (form.CompletedNotes != null) { if (form.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Completion Notes:</b> " + form.CompletedNotes + ", "; } }
                if (form.CCCompletedFormToEmail != null) { if (form.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Receipt To:</b> " + form.CCCompletedFormToEmail + ", "; } }
                if (form.ModifiedByUser != null) { if (form.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified By:</b> " + form.ModifiedByUser + ", "; } }
                if (form.SubmittedByUser != null) { if (form.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted By:</b> " + form.SubmittedByUser + ", "; } }
                if (form.Customer != null) { if (form.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.Customer + ", "; } }
                if (form.ExpediteCode != null)
                {
                    if (form.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code:</b> " + form.ExpediteCode.Code + ", "; }
                    if (form.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code Description:</b> " + form.ExpediteCode.Description + ", "; }
                }
                if (form.ContactName != null) { if (form.ContactName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Contact Name:</b> " + form.ContactName + ", "; } }
                if (form.PhoneNumber != null) { if (form.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Phone Number:</b> " + form.PhoneNumber + ", "; } }
                if (form.MaterialSku != null) { if (form.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Material Sku #:</b> " + form.MaterialSku + ", "; } }
                if (form.QuantityOrdered != null) { if (form.QuantityOrdered.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity Ordered:</b> " + form.QuantityOrdered + ", "; } }
                if (form.AccountNumber != null) { if (form.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.AccountNumber + ", "; } }
                if (form.PurchaseOrderNumber != null) { if (form.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Purchase Order #:</b> " + form.PurchaseOrderNumber + ", "; } }
                if (form.OowOrderNumber != null) { if (form.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>OOW Order Number:</b> " + form.OowOrderNumber + ", "; } }
                if (form.SM != null) { if (form.SM.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>S/M:</b> " + form.SM + ", "; } }
                if (form.ShipVia != null) { if (form.ShipVia.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship Via:</b> " + form.ShipVia + ", "; } }
                if (form.Reserve != null) { if (form.Reserve.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Reserve:</b> " + form.Reserve + ", "; } }
                if (form.ShipToName != null) { if (form.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Name:</b> " + form.ShipToName + ", "; } }
                if (form.ShipToAddress != null) { if (form.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Address:</b> " + form.ShipToAddress + ", "; } }
                if (form.ShipToCity != null) { if (form.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To City:</b> " + form.ShipToCity + ", "; } }
                if (form.ShipToState != null) { if (form.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To State:</b> " + form.ShipToState + ", "; } }
                if (form.ShipToZip != null) { if (form.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Zip:</b> " + form.ShipToZip + ", "; } }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportDirectOrder.aspx?formId=" + form.RecordId;

                string matchHtml = "<div style=\"padding-left:18px\"><b>Submitted On:</b> " + form.Timestamp.ToShortDateString() + "     <b>By:</b> " + form.SubmittedByUser
                    + "     <b>Status:</b> " + form.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.RecordId.ToString(),
                    FormName = "Direct Order",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.Status.StatusText,
                    SubmittedBy = form.SubmittedByUser,
                    SubmittedDate = form.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                searchResults.Add(thisResult);
                resultCount++;
            }
        }

        private void FindExpeditedOrderForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderForms)
        {
            foreach (ExpeditedOrderForm form in expeditedOrderForms)
            {
                string textMatches = "";

                if (form.Company != null) { if (form.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.Company + ", "; } }
                if (form.Customer != null) { if (form.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.Customer + ", "; } }
                if (form.ExpediteCode != null)
                {
                    if (form.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code:</b> " + form.ExpediteCode.Code + ", "; }
                    if (form.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code Description:</b> " + form.ExpediteCode.Description + ", "; }
                }
                if (form.ContactName != null) { if (form.ContactName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Contact Name:</b> " + form.ContactName + ", "; } }
                if (form.PhoneNumber != null) { if (form.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Phone Number:</b> " + form.PhoneNumber + ", "; } }
                if (form.MaterialSku != null) { if (form.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Material Sku #:</b> " + form.MaterialSku + ", "; } }
                if (form.QuantityOrdered != null) { if (form.QuantityOrdered.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity Ordered:</b> " + form.QuantityOrdered + ", "; } }
                if (form.AccountNumber != null) { if (form.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.AccountNumber + ", "; } }
                if (form.PurchaseOrderNumber != null) { if (form.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Purchase Order #:</b> " + form.PurchaseOrderNumber + ", "; } }
                if (form.OowOrderNumber != null) { if (form.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>OOW Order Number:</b> " + form.OowOrderNumber + ", "; } }
                if (form.SM != null) { if (form.SM.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>S/M:</b> " + form.SM + ", "; } }
                if (form.ShipToName != null) { if (form.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Name:</b> " + form.ShipToName + ", "; } }
                if (form.ShipToAddress != null) { if (form.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Address:</b> " + form.ShipToAddress + ", "; } }
                if (form.ShipToCity != null) { if (form.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To City:</b> " + form.ShipToCity + ", "; } }
                if (form.ShipToState != null) { if (form.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To State:</b> " + form.ShipToState + ", "; } }
                if (form.ShipToZip != null) { if (form.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Zip:</b> " + form.ShipToZip + ", "; } }
                if (form.AdditionalInfo != null) { if (form.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Additional Info:</b> " + form.AdditionalInfo + ", "; } }
                if (form.CCFormToEmail != null) { if (form.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Form To:</b> " + form.CCFormToEmail + ", "; } }
                if (form.Status != null) { if (form.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.Status.StatusText + ", "; } }
                if (form.RequestHandler != null) { if (form.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expeditor Handler:</b> " + form.RequestHandler + ", "; } }
                if (form.CompletedNotes != null) { if (form.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Completion Notes:</b> " + form.CompletedNotes + ", "; } }
                if (form.CCCompletedFormToEmail != null) { if (form.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Receipt To:</b> " + form.CCCompletedFormToEmail + ", "; } }
                if (form.ModifiedByUser != null) { if (form.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified By:</b> " + form.ModifiedByUser + ", "; } }
                if (form.SubmittedByUser != null) { if (form.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted By:</b> " + form.SubmittedByUser + ", "; } }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportExpeditedOrder.aspx?formId=" + form.RecordId;

                string matchHtml = "<div style=\"padding-left:18px\"><b>Submitted On:</b> " + form.Timestamp.ToShortDateString() + "     <b>By:</b> " + form.SubmittedByUser
                    + "     <b>Status:</b> " + form.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.RecordId.ToString(),
                    FormName = "Expedited Order",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.Status.StatusText,
                    SubmittedBy = form.SubmittedByUser,
                    SubmittedDate = form.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                searchResults.Add(thisResult);

                resultCount++;
            }
        }

        private void FindHotRushForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushForms)
        {
            foreach (Tingle_WebForms.Models.HotRushForm form in hotRushForms)
            {
                string textMatches = "";

                if (form.Company != null) { if (form.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.Company + ", "; } }
                if (form.AdditionalInfo != null) { if (form.AdditionalInfo.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Additional Info:</b> " + form.AdditionalInfo + ", "; } }
                if (form.CCFormToEmail != null) { if (form.CCFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Form To:</b> " + form.CCFormToEmail + ", "; } }
                if (form.Status != null) { if (form.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.Status.StatusText + ", "; } }
                if (form.RequestHandler != null) { if (form.RequestHandler.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expeditor Handler:</b> " + form.RequestHandler + ", "; } }
                if (form.CompletedNotes != null) { if (form.CompletedNotes.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Completion Notes:</b> " + form.CompletedNotes + ", "; } }
                if (form.CCCompletedFormToEmail != null) { if (form.CCCompletedFormToEmail.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>CC Receipt To:</b> " + form.CCCompletedFormToEmail + ", "; } }
                if (form.ModifiedByUser != null) { if (form.ModifiedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified By:</b> " + form.ModifiedByUser + ", "; } }
                if (form.SubmittedByUser != null) { if (form.SubmittedByUser.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted By:</b> " + form.SubmittedByUser + ", "; } }
                if (form.Customer != null) { if (form.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.Customer + ", "; } }
                if (form.EntireOrderOrLineNumber != null) { if (form.EntireOrderOrLineNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Entire Order OR Line #:</b> " + form.EntireOrderOrLineNumber + ", "; } }
                if (form.OrderNumber != null) { if (form.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.OrderNumber + ", "; } }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportHotRush.aspx?formId=" + form.RecordId;

                string matchHtml = "<div style=\"padding-left:18px\"><b>Submitted On:</b> " + form.Timestamp.ToShortDateString() + "     <b>By:</b> " + form.SubmittedByUser
                    + "     <b>Status:</b> " + form.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.RecordId.ToString(),
                    FormName = "Hot Rush",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.Status.StatusText,
                    SubmittedBy = form.SubmittedByUser,
                    SubmittedDate = form.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                searchResults.Add(thisResult);

                resultCount++;
            }
        }

        private void CreatePagingControl()
        {
            int buttonCount = RowCount % 10 == 0 ? RowCount / 10 : (RowCount / 10) + 1;

            for (int i = 0; i < buttonCount; i++)
            {
                LinkButton lnk = new LinkButton();
                lnk.Click += new EventHandler(lbl_Click);
                lnk.ID = "lnkPage" + (i + 1).ToString();
                lnk.Text = (i + 1).ToString();
                plcPaging.Controls.Add(lnk);
                Label spacer = new Label();
                spacer.Text = "&nbsp;";
                plcPaging.Controls.Add(spacer);
            }
        }

        void lbl_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int currentPage = int.Parse(lnk.Text);
            int take = currentPage * 10;
            int skip = currentPage == 1 ? 0 : take - 10;
            SearchForText(take, skip);
        }
        
    }
}