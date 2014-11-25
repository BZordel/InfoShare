<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="Tingle_WebForms.Administration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%; text-align:center">
        <asp:Label ID="lblEmailMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br /><br />
    </div>
    <table>
        <tr class="trheader">
            <td>                  
                <table>
                    <tr>
                        <td class="tdheader" style="width:25%; text-align:center;">
                            <asp:DropDownList ID="ddlFormName" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    ItemType="Tingle_WebForms.Models.TForm" SelectMethod="SelectForms" DataTextField="FormName" DataValueField="FormID"
                                    OnSelectedIndexChanged="ddlFormName_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">Select Form</asp:ListItem>
                                </asp:DropDownList></td>
                        <td class="tdheader" style="width:50%; text-align:center;">Form Administration</td>
                        <td class="tdheader" style="width:25%; text-align:center;">
                            <asp:DropDownList ID="ddlTab" runat="server" OnSelectedIndexChanged="ddlTab_SelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="FormSummary">Form Summary</asp:ListItem>
                                <asp:ListItem Value="EmailList">Email List</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                              
                </td>
        </tr>
        <asp:Panel ID="pnlAdministration" runat="server" Visible="true">
            <tr>
                <td class="tdcenter EmailLabels">
                    <table style="border-style: none;">
                    <asp:Panel ID="pnlFormPopup" Visible="false" runat="server">
                        <tr>
                            <td colspan="2">
                                <div style="width: 100%; text-align: center;">
                                    <br />
                                    <table style="border: 2px inset #000; width:50%; margin-left:auto; margin-right:auto;">
                                        <tr>
                                            <td>Form ID:</td>
                                            <td>
                                                <asp:Label ID="lblFormId" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Form Name:</td>
                                            <td>
                                                <asp:Label ID="lblFormName" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Created By:</td>
                                            <td>
                                                <asp:Label ID="lblFormCreator" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Created On:</td>
                                            <td>
                                                <asp:Label ID="lblTimestamp" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Notes:</td>
                                            <td>
                                                <asp:Label ID="lblNotes" runat="server" Text="" CssClass="EmailValues"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlEmailList" runat="server" Visible="false">
                        <tr>
                            <td class="tdcenter" colspan="2">
                                <asp:GridView ID="gvEmailList" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.EmailAddress"
                                    SelectMethod="gvEmailList_GetData" DataKeyNames="EmailAddressID" AutoGenerateColumns="false"
                                    CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC" 
                                    BorderStyle="None" BorderWidth="1px" DeleteMethod="gvEmailList_DeleteItem" UpdateMethod="gvEmailList_UpdateItem"
                                    OnRowUpdating="gvEmailList_RowUpdating" OnRowDataBound="gvEmailList_RowDataBound"
                                    >
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmailAddressID" Width="1px" runat="server" Visible="false" Text='<%#: Eval("EmailAddressID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEmailAddressIDEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("EmailAddressID") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblCompanyEdit" runat="server" Text='<%# Bind("Company") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlCompanyEdit" Style="border-style:inset;">
                                                    <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                                    <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButtonList ID="rblStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblStatusEdit_DataBound"
                                                    BorderStyle="None">
                                                    <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                    <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDeleteEmail" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this Email Address?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>Fill out the form below to add the first Email Address.</EmptyDataTemplate>
                                    <FooterStyle BackColor="white" ForeColor="Black" />
                                    <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <br /><br />
                                <asp:FormView ID="fvEmailInsert" runat="server" ItemType="Tingle_WebForms.Models.EmailAddress" 
                                    DataKeyNames="EmailAddressID" AutoGenerateColumns="false" InsertMethod="fvEmailInsert_InsertItem"
                                    DefaultMode="Insert" BorderStyle="None"
                                    >
                                    <InsertItemTemplate>
                                        <div style="width:100%; text-align:center;">
                                            <table style="border:2px inset #000;">
                                                <tr>
                                                    <td>Recipient's Name:</td>
                                                    <td><asp:TextBox ID="txtNameInsert" runat="server" Text='<%#: Bind("Name") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Email Address:</td>
                                                    <td><asp:TextBox ID="txtAddressInsert" runat="server" Text='<%#: Bind("Address") %>'></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Company:</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblCompanyInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                            <asp:ListItem Value="Tingle" Selected="True">Tingle</asp:ListItem>
                                                            <asp:ListItem Value="Summit">Summit</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Status:</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                            <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                            <asp:ListItem Value="0">Disabled</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br /><br />
                                            <asp:Button ID="btnInsertAddress" runat="server" Text="Add Email" CommandName="Insert" />
                                            <asp:Button ID="btnClearForm" runat="server" Text="Clear Form" OnClick="btnClearForm_Click" />
                                        </div>
                                    </InsertItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                    </asp:Panel>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        
    </table>
    <br /><br /><br />
    <div style="width:100%; text-align:center">
        <asp:Label ID="lblUserMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br /><br />
    </div>
    <table>
        <tr class="trheader">
            <td>                  
                <table>
                    <tr>
                        <td class="tdheader" style="width:100%; text-align:center;">User Administration</td>
                    </tr>
                </table>
                              
                </td>
        </tr>
        <tr>
            <td>
                    <asp:GridView ID="gvUsers" runat="server" AllowSorting="true" ItemType="Tingle_WebForms.Models.SystemUsers"
                        SelectMethod="gvUsers_GetData" DataKeyNames="SystemUserID" AutoGenerateColumns="false"
                        CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal" AllowPaging="true" PageSize="25" BackColor="White" BorderColor="#CCCCCC" 
                        BorderStyle="None" BorderWidth="1px" DeleteMethod="gvUsers_DeleteItem" UpdateMethod="gvUsers_UpdateItem" 
                        OnRowUpdating="gvUsers_RowUpdating" OnRowDataBound="gvUsers_RowDataBound"
                        >
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="1px">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemUserID" Width="1px" runat="server" Visible="false" Text='<%#: Eval("SystemUserID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblSystemUserIDEdit" Width="1px" runat="server" Visible="false" Text='<%#: Eval("SystemUserID") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserName" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblUserNameEdit" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DisplayName" SortExpression="DisplayName">
                                <ItemTemplate>
                                    <asp:Label ID="lblDisplayName" runat="server" Text='<%# Bind("DisplayName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDisplayNameEdit" runat="server" Text='<%# Bind("DisplayName") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Item.Status == 0 ? "Disabled" : "Enabled" %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rblUserStatusEdit" runat="server" RepeatDirection="Horizontal" OnDataBound="rblUserStatusEdit_DataBound"
                                        BorderStyle="None">
                                        <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                        <asp:ListItem Value="0">Disabled</asp:ListItem>
                                    </asp:RadioButtonList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserRole" runat="server" Text='<%# Item.UserRole.RoleName %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlUserRoleEdit" runat="server" SelectMethod="GetUserRoles"
                                        DataTextField="RoleName" DataValueField="UserRoleId" AppendDataBoundItems="true"
                                        Style="border-style: inset;" OnDataBound="ddlUserRoleEdit_DataBound">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" EditText="Edit" ShowHeader="True" HeaderText="Edit"></asp:CommandField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDeleteUser" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this User?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>Fill out the form below to add the first System User.</EmptyDataTemplate>
                        <FooterStyle BackColor="white" ForeColor="Black" />
                        <HeaderStyle BackColor="#bc4445" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="#bc4445" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="#FFFFFF" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                    <br /><br />
                    <asp:FormView ID="fvUserInsert" runat="server" ItemType="Tingle_WebForms.Models.SystemUsers" 
                        DataKeyNames="SysteUserID" AutoGenerateColumns="false" InsertMethod="fvUserInsert_InsertItem"
                        DefaultMode="Insert" BorderStyle="None"
                        >
                        <InsertItemTemplate>
                            <div style="width:100%; text-align:center;">
                                <table style="border:2px inset #000;">
                                    <tr>
                                        <td>UserName:</td>
                                        <td><asp:TextBox ID="txtUserNameInsert" runat="server" Text='<%#: Bind("UserName") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Display Name:</td>
                                        <td><asp:TextBox ID="txtDisplayNameInsert" runat="server" Text='<%#: Bind("DisplayName") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Status:</td>
                                        <td>
                                            <asp:RadioButtonList ID="rblUserStatusInsert" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                                <asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
                                                <asp:ListItem Value="0">Disabled</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Role:</td>
                                        <td>                                    
                                            <asp:DropDownList ID="ddlUserRoleInsert" runat="server" SelectMethod="GetUserRoles"
                                                DataTextField="RoleName" DataValueField="UserRoleId" AppendDataBoundItems="true"
                                                Style="border-style: inset;">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br /><br />
                                <asp:Button ID="btnInsertUser" runat="server" Text="Add User" CommandName="Insert" />
                                <asp:Button ID="btnClearUserForm" runat="server" Text="Clear Form" OnClick="btnClearUserForm_Click" />
                            </div>
                        </InsertItemTemplate>
                    </asp:FormView>
            </td>
        </tr>
    </table>

</asp:Content>
