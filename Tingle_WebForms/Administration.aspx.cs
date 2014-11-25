using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using Tingle_WebForms.Forms;
using Tingle_WebForms;
using System.Text;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Data.Objects;
using System.Web.ModelBinding;
using System.Data;
using System.Globalization;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;

namespace Tingle_WebForms
{
    public partial class Administration : System.Web.UI.Page
    {
        FormContext _ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                if (id != 0)
                {
                    using (FormContext ctx = new FormContext())
                    {
                        if (ctx.TForms.Any(t => t.FormID == id))
                        {
                            var thisForm = ctx.TForms.FirstOrDefault(f => f.FormID == id);

                            lblFormId.Text = thisForm.FormID.ToString();
                            lblFormName.Text = thisForm.FormName;
                            lblFormCreator.Text = thisForm.FormCreator;
                            lblTimestamp.Text = thisForm.Timestamp.ToString();
                            lblNotes.Text = thisForm.Notes;

                            gvEmailList.DataBind();

                            switch (ddlTab.SelectedValue)
                            {
                                case "FormSummary":
                                    pnlFormPopup.Visible = true;
                                    pnlEmailList.Visible = false;
                                    break;
                                case "EmailList":
                                    pnlFormPopup.Visible = false;
                                    pnlEmailList.Visible = true;
                                    break;
                                default:
                                    pnlFormPopup.Visible = true;
                                    pnlEmailList.Visible = false;
                                    break;
                            }

                            lblUserMessage.Text = "";
                        }
                        else
                        {
                            lblUserMessage.Text = "Form Not Found.";
                        }
                    }
                }
                else
                {
                    pnlFormPopup.Visible = false;
                    pnlEmailList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unabled to load form summary.  Please contact your system administrator.";
            }

        }

        public IEnumerable<TForm> SelectForms()
        {
            try
            {
                var FormsList = _ctx.TForms.Where(f => f.Status == 1).OrderBy(f => f.FormName).ToList();

                lblUserMessage.Text = "";

                return FormsList;
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to load forms.  Please contact your system administrator.";
                return null;
            }
        }

        protected void lbManageEmailList_Click(object sender, EventArgs e)
        {
            pnlFormPopup.Visible = false;
            pnlEmailList.Visible = true;

        }

        public IQueryable<Tingle_WebForms.Models.EmailAddress> gvEmailList_GetData()
        {
            try
            {
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                if (id != 0)
                {
                    IQueryable<Tingle_WebForms.Models.EmailAddress> EmailAddresses = _ctx.EmailAddresses.Where(ea => ea.TForm.FormID == id);

                    lblUserMessage.Text = "";

                    return EmailAddresses;
                }
                else
                {
                    lblEmailMessage.Text = "Unable to load Email List.  Please contact your system administrator.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to load Email List.  Please contact your system administrator.";
                return null;
            }
            
        }

        public Int32 GetListID(TForm tForm)
        {
            if (!String.IsNullOrEmpty(tForm.FormID.ToString()))
            {
            }

            return 0;
        }

        public void fvEmailInsert_InsertItem()
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvEmailInsert.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvEmailInsert.FindControl("txtAddressInsert");
                RadioButtonList rblCompanyInsert = (RadioButtonList)fvEmailInsert.FindControl("rblCompanyInsert");
                RadioButtonList rblStatusInsert = (RadioButtonList)fvEmailInsert.FindControl("rblStatusInsert");
                Int16 status = Convert.ToInt16(rblStatusInsert.SelectedValue);
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    var tForm = ctx.TForms.Where(f => f.FormID == id).FirstOrDefault();

                    EmailAddress newEmail = new EmailAddress();

                    newEmail.Name = txtNameInsert.Text;
                    newEmail.Address = txtAddressInsert.Text;
                    newEmail.Company = rblCompanyInsert.SelectedValue;
                    newEmail.Status = status;
                    newEmail.TForm = tForm;
                    newEmail.Timestamp = DateTime.Now;

                    ctx.EmailAddresses.Add(newEmail);

                    if (ModelState.IsValid)
                    {
                        ctx.SaveChanges();
                        gvEmailList.DataBind();
                    }

                    lblEmailMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to insert new Email Address.  Please contact your system administrator.";
            }
        }

        protected void btnClearForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvEmailInsert.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvEmailInsert.FindControl("txtAddressInsert");
                RadioButtonList rblStatusInsert = (RadioButtonList)fvEmailInsert.FindControl("rblStatusInsert");
                int status = Convert.ToInt32(rblStatusInsert.SelectedValue);

                txtNameInsert.Text = "";
                txtAddressInsert.Text = "";
                rblStatusInsert.SelectedValue = "1";

                lblUserMessage.Text = "";
            }
            catch(Exception ex)
            {
                lblEmailMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        public void gvEmailList_DeleteItem(int EmailAddressID)
        {
            try
            {
                EmailAddress EmailAddressToDelete = _ctx.EmailAddresses.Where(ea => ea.EmailAddressID == EmailAddressID).FirstOrDefault();

                if (EmailAddressToDelete != null)
                {
                    _ctx.EmailAddresses.Remove(EmailAddressToDelete);
                    _ctx.SaveChanges();
                }

                lblUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to delete Email Address.  Please contact your system administrator.";
            }
        }

        public void gvEmailList_UpdateItem(int EmailAddressID)
        {

        }

        protected void gvEmailList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblEmailAddressID = (Label)gvr.FindControl("lblEmailAddressIDEdit");
                int id = Convert.ToInt32(lblEmailAddressID.Text);

                using (FormContext ctx = new FormContext())
                {
                    var emailAddress = ctx.EmailAddresses.Where(ea => ea.EmailAddressID == id).FirstOrDefault();

                    emailAddress.Name = ((TextBox)gvr.FindControl("txtNameEdit")).Text;
                    emailAddress.Address = ((TextBox)gvr.FindControl("txtAddressEdit")).Text;
                    emailAddress.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblStatusEdit")).SelectedValue);
                    emailAddress.Company = ((DropDownList)gvr.FindControl("ddlCompanyEdit")).SelectedValue;

                    ctx.EmailAddresses.Attach(emailAddress);
                    ctx.Entry(emailAddress).State = EntityState.Modified;

                    ctx.SaveChanges();
                    gvEmailList.DataBind();

                    lblEmailMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to update Email Address.  Please contact your system administrator.";
            }
        }

        protected void rblStatusEdit_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormName.SelectedValue != "0")
            {
                switch (ddlTab.SelectedValue)
                {
                    case "FormSummary":
                        pnlFormPopup.Visible = true;
                        pnlEmailList.Visible = false;
                        break;
                    case "EmailList":
                        pnlFormPopup.Visible = false;
                        pnlEmailList.Visible = true;
                        break;
                    default:
                        pnlFormPopup.Visible = true;
                        pnlEmailList.Visible = false;
                        break;
                }
            }
        }

        public IQueryable<Tingle_WebForms.Models.SystemUsers> gvUsers_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.SystemUsers> SystemUsers = _ctx.SystemUsers;

                if (SystemUsers != null)
                {
                    lblUserMessage.Text = "";
                    return SystemUsers;
                }
                else
                {
                    lblUserMessage.Text = "Unable to load User List.  Please contact your system administrator.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to load User List.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvUsers_DeleteItem(int SystemUserID)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    SystemUsers UserToDelete = ctx.SystemUsers.Where(su => su.SystemUserID == SystemUserID).FirstOrDefault();

                    if (UserToDelete != null)
                    {
                        ctx.SystemUsers.Remove(UserToDelete);
                        ctx.SaveChanges();
                    }

                    lblUserMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to delete User.  Please contact your system administrator.";
            }
        }

        public void gvUsers_UpdateItem(int SystemUserID)
        {

        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblSystemUserID = (Label)gvr.FindControl("lblSystemUserIDEdit");
                int id = Convert.ToInt32(lblSystemUserID.Text);
                int userRoleID = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlUserRoleEdit")).SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    var systemUser = ctx.SystemUsers.Where(su => su.SystemUserID == id).FirstOrDefault();

                    systemUser.DisplayName = ((TextBox)gvr.FindControl("txtDisplayNameEdit")).Text;
                    systemUser.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblUserStatusEdit")).SelectedValue);
                    systemUser.UserRole = ctx.UserRoles.Where(ur => ur.UserRoleId == userRoleID).FirstOrDefault();

                    ctx.SystemUsers.Attach(systemUser);
                    ctx.Entry(systemUser).State = EntityState.Modified;

                    ctx.SaveChanges();
                    gvUsers.DataBind();
                    lblUserMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to update user.  Please contact your system administrator.";
            }

        }

        protected void rblUserStatusEdit_DataBound(object sender, EventArgs e)
        {

        }

        public void fvUsers_InsertItem()
        {

        }

        protected void btnClearUserForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtUserNameInsert = (TextBox)fvUserInsert.FindControl("txtUserNameInsert");
                TextBox txtDisplayNameInsert = (TextBox)fvUserInsert.FindControl("txtDisplayNameInsert");
                RadioButtonList rblUserStatusInsert = (RadioButtonList)fvUserInsert.FindControl("rblUserStatusInsert");
                DropDownList ddlUserRoleInsert = (DropDownList)fvUserInsert.FindControl("ddlUserRoleInsert");
                int status = Convert.ToInt32(rblUserStatusInsert.SelectedValue);

                txtUserNameInsert.Text = "";
                txtDisplayNameInsert.Text = "";
                rblUserStatusInsert.SelectedValue = "1";
                ddlUserRoleInsert.SelectedIndex = 0;
                lblUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        public void fvUserInsert_InsertItem()
        {
            try
            {
                TextBox txtUserNameInsert = (TextBox)fvUserInsert.FindControl("txtUserNameInsert");
                TextBox txtDisplayNameInsert = (TextBox)fvUserInsert.FindControl("txtDisplayNameInsert");
                RadioButtonList rblUserStatusInsert = (RadioButtonList)fvUserInsert.FindControl("rblUserStatusInsert");
                int roleId = Convert.ToInt32(((DropDownList)fvUserInsert.FindControl("ddlUserRoleInsert")).SelectedValue);
                Int16 status = Convert.ToInt16(rblUserStatusInsert.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.SystemUsers.Where(su => su.UserName == txtUserNameInsert.Text).FirstOrDefault() == null)
                    {
                        SystemUsers newUser = new SystemUsers();
                        UserRoles newRole = ctx.UserRoles.Where(ur => ur.UserRoleId == roleId).FirstOrDefault();

                        newUser.UserName = txtUserNameInsert.Text;
                        newUser.DisplayName = txtDisplayNameInsert.Text;
                        newUser.Status = status;
                        newUser.UserRole = newRole;

                        ctx.SystemUsers.Add(newUser);

                        if (ModelState.IsValid)
                        {
                            ctx.SaveChanges();
                            gvUsers.DataBind();
                        }
                        lblUserMessage.Text = "";
                    }
                    else
                    {
                        lblUserMessage.Text = "A User with the username: " + txtUserNameInsert.Text + " already exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to add new User.  Please contact your system administrator.";
            }
        }

        protected void ddlUserRoleEdit_DataBound(object sender, EventArgs e)
        {
        }

        public IEnumerable<UserRoles> GetUserRoles()
        {
            try
            {
                var UserRoleList = _ctx.UserRoles.OrderBy(ur => ur.UserRoleId).ToList();

                lblUserMessage.Text = "";

                return UserRoleList;
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to load User Roles.  Please contact your system administrator.";
                return null;
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlUserRoleEdit = (DropDownList)e.Row.FindControl("ddlUserRoleEdit");
                    int userId = Convert.ToInt32(((Label)e.Row.FindControl("lblSystemUserIDEdit")).Text);
                    SystemUsers sUser = _ctx.SystemUsers.Where(su => su.SystemUserID == userId).FirstOrDefault();

                    ddlUserRoleEdit.SelectedValue = sUser.UserRole.UserRoleId.ToString();

                }
            }
        }

        protected void gvEmailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && gvEmailList.EditIndex == e.Row.RowIndex)
                {
                    DropDownList ddlCompanyEdit = (DropDownList)e.Row.FindControl("ddlCompanyEdit");
                    ddlCompanyEdit.Items.FindByValue((e.Row.FindControl("lblCompanyEdit") as Label).Text).Selected = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}