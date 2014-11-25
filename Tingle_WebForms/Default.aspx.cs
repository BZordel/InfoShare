using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;

namespace Tingle_WebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadFavoriteForms();
            }
        }

        private void LoadFavoriteForms()
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID))
                        {
                            phNoFavorites.Visible = false;

                            var favForms = ctx.FavoriteForms.Where(f => f.User.SystemUserID == currentUser.SystemUserID).ToList();

                            if (favForms.Any(f => f.Form.FormName == "Expedited Order"))
                            {
                                phFavoriteExpeditedOrder.Visible = true;
                                cbFavoriteExpeditedOrder.Checked = true;
                            }
                            else
                            {
                                phFavoriteExpeditedOrder.Visible = false;
                                cbFavoriteExpeditedOrder.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Price Change Request"))
                            {
                                phFavoritePriceChangeRequest.Visible = true;
                                cbFavoritePriceChangeRequest.Checked = true;
                            }
                            else
                            {
                                phFavoritePriceChangeRequest.Visible = false;
                                cbFavoritePriceChangeRequest.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Order Cancellation"))
                            {
                                phFavoriteOrderCancellation.Visible = true;
                                cbFavoriteOrderCancellation.Checked = true;
                            }
                            else
                            {
                                phFavoriteOrderCancellation.Visible = false;
                                cbFavoriteOrderCancellation.Checked = false;
                            }
                        }
                        else
                        {
                            phNoFavorites.Visible = true;
                            phFavoriteExpeditedOrder.Visible = false;
                            phFavoritePriceChangeRequest.Visible = false;
                        }
                    }
                    upFavorites.Update();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void FormRedirect(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.CommandArgument != null)
                {
                    Response.Redirect(btn.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteExpeditedOrder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteExpeditedOrder.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Expedited Order"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Expedited Order" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Expedited Order");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }

                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Expedited Order"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Expedited Order");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoritePriceChangeRequest_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoritePriceChangeRequest.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Price Change Request"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Price Change Request" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Price Change Request");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }

                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Price Change Request"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Price Change Request");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteOrderCancellation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteOrderCancellation.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Order Cancellation"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Order Cancellation" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Order Cancellation");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Order Cancellation"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Order Cancellation");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}