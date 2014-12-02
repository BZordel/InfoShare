using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Text;

namespace Tingle_WebForms.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSummary();
            }
        }

        private void BindSummary()
        {
            try
            {
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                using (FormContext ctx = new FormContext())
                {
                    IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderList = ctx.ExpeditedOrderForms;
                    IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderList = ctx.DirectOrderForms;
                    IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushList = ctx.HotRushForms;
                    IQueryable<Tingle_WebForms.Models.OrderCancellationForm> orderCancellationList = ctx.OrderCancellationForms;
                    IQueryable<Tingle_WebForms.Models.MustIncludeForm> mustIncludeList = ctx.MustIncludeForms;
                    IQueryable<Tingle_WebForms.Models.SampleRequestForm> sampleRequestList = ctx.SampleRequestForms;
                    IQueryable<Tingle_WebForms.Models.LowInventoryForm> lowInventoryList = ctx.LowInventoryForms;
                    IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> priceChangeRequestList = ctx.PriceChangeRequestForms;
                    IQueryable<Tingle_WebForms.Models.RequestForCheckForm> requestForCheckList = ctx.RequestForCheckForms;

                    if (Session["Company"] != null)
                    {
                        ddlCompany.SelectedValue = Session["Company"].ToString();

                        if (Session["Company"].ToString() != "Any")
                        {
                            String sessionCompany = Session["Company"].ToString();

                            expeditedOrderList = expeditedOrderList.Where(f => f.Company == sessionCompany);
                            directOrderList = directOrderList.Where(f => f.Company == sessionCompany);
                            hotRushList = hotRushList.Where(f => f.Company == sessionCompany);
                            orderCancellationList = orderCancellationList.Where(f => f.Company == sessionCompany);
                            mustIncludeList = mustIncludeList.Where(f => f.Company == sessionCompany);
                            sampleRequestList = sampleRequestList.Where(f => f.Company == sessionCompany);
                            lowInventoryList = lowInventoryList.Where(f => f.Company == sessionCompany);
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Company == sessionCompany);
                            requestForCheckList = requestForCheckList.Where(f => f.Company == sessionCompany);
                        }
                    }
                    else
                    {
                        Session["Company"] = "Any";
                        ddlCompany.SelectedValue = "Any";
                    }

                    if (Session["GlobalStatus"] != null)
                    {
                        ddlGlobalStatus.SelectedValue = Session["GlobalStatus"].ToString();

                        if (Session["GlobalStatus"].ToString() == "Active")
                        {
                            expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            directOrderList = directOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            hotRushList = hotRushList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        }
                        else if (Session["GlobalStatus"].ToString() == "Archive")
                        {
                            expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "Completed");
                            directOrderList = directOrderList.Where(f => f.Status.StatusText == "Completed");
                            hotRushList = hotRushList.Where(f => f.Status.StatusText == "Completed");
                            orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "Completed");
                            mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "Completed");
                            sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "Completed");
                            lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "Completed");
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "Completed");
                            requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "Completed");
                        }
                    }
                    else
                    {
                        Session["GlobalStatus"] = "Active";
                        ddlGlobalStatus.SelectedValue = "Active";

                        expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        directOrderList = directOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        hotRushList = hotRushList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                    }

                    if (currentUser.UserRole.RoleName == "ReportsUser")
                    {
                        expeditedOrderList = expeditedOrderList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        directOrderList = directOrderList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        hotRushList = hotRushList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        orderCancellationList = orderCancellationList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        mustIncludeList = mustIncludeList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        sampleRequestList = sampleRequestList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        lowInventoryList = lowInventoryList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.SubmittedByUser == currentUser.UserName);
                        requestForCheckList = requestForCheckList.Where(f => f.SubmittedByUser == currentUser.UserName);
                    }

                    if (expeditedOrderList.Count() != 1) { lbExpeditedOrderCount.Text = expeditedOrderList.Count().ToString() + " Forms"; }
                    else { lbExpeditedOrderCount.Text = expeditedOrderList.Count().ToString() + " Form"; }

                    if (directOrderList.Count() != 1) { lbDirectOrderCount.Text = directOrderList.Count().ToString() + " Forms"; }
                    else { lbDirectOrderCount.Text = directOrderList.Count().ToString() + " Form"; }

                    if (hotRushList.Count() != 1) { lbHotRushCount.Text = hotRushList.Count().ToString() + " Forms"; }
                    else { lbHotRushCount.Text = hotRushList.Count().ToString() + " Form"; }

                    if (orderCancellationList.Count() != 1) { lbOrderCancellationCount.Text = orderCancellationList.Count().ToString() + " Forms"; }
                    else { lbOrderCancellationCount.Text = orderCancellationList.Count().ToString() + " Form"; }

                    if (mustIncludeList.Count() != 1) { lbMustIncludeCount.Text = mustIncludeList.Count().ToString() + " Forms"; }
                    else { lbMustIncludeCount.Text = mustIncludeList.Count().ToString() + " Form"; }

                    if (sampleRequestList.Count() != 1) { lbSampleRequestCount.Text = sampleRequestList.Count().ToString() + " Forms"; }
                    else { lbSampleRequestCount.Text = sampleRequestList.Count().ToString() + " Form"; }

                    if (lowInventoryList.Count() != 1) { lbLowInventoryCount.Text = lowInventoryList.Count().ToString() + " Forms"; }
                    else { lbLowInventoryCount.Text = lowInventoryList.Count().ToString() + " Form"; }

                    if (priceChangeRequestList.Count() != 1) { lbPriceChangeRequestCount.Text = priceChangeRequestList.Count().ToString() + " Forms"; }
                    else { lbPriceChangeRequestCount.Text = priceChangeRequestList.Count().ToString() + " Form"; }

                    if (requestForCheckList.Count() != 1) { lbRequestForCheckCount.Text = requestForCheckList.Count().ToString() + " Forms"; }
                    else { lbRequestForCheckCount.Text = requestForCheckList.Count().ToString() + " Form"; }

                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Company"] = ddlCompany.SelectedValue;
            BindSummary();
            Response.Redirect(Request.RawUrl);
        }

        protected void ddlGlobalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["GlobalStatus"] = ddlGlobalStatus.SelectedValue;
            BindSummary();
            Response.Redirect(Request.RawUrl);
        }

    }
}