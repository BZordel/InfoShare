<%@ Page Title="WCTingle - Expedited Order Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpeditedOrderForm.aspx.cs" Inherits="Tingle_WebForms.Forms.ExpeditedOrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("#MainContent_txtInstallDate").datepicker();
        });
    </script>
    <asp:Panel ID="pnlCompleted" runat="server" Visible="false">
        <div style="text-align: center; width: 100%">
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="22px"></asp:Label><br />
            <br />
            <asp:LinkButton ID="lbStartOver" runat="server" Text="Click Here to Submit Another Form" OnClick="lbStartOver_Click" Font-Size="22px"></asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlForm" runat="server" Visible="true">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
        <br />
        <br />
        <table>
            <tr class="trheader">
                <td colspan="4" class="tdheader">Expedited Order Form</td>
            </tr>
            <tr>
                <td class="tdright">
                    Company:
                </td>
                <td class="tdleft">
                    <asp:DropDownList runat="server" ID="ddlCompany" Style="border-style:inset;">
                        <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                        <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdright"></td>
                <td class="tdleft"></td>
            </tr>
            <tr>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required." ForeColor="Black" Font-Size="16px" 
                        ControlToValidate="txtCustomer" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox><br />
                </td>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Expedite Code field is required." ForeColor="Red" Font-Size="12px" 
                        ControlToValidate="ddlExpediteCode" InitialValue="0" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Expedite Code:</td>
                <td class="tdleft">
                    <asp:DropDownList ID="ddlExpediteCode" runat="server" SelectMethod="GetExpediteCodes"
                        DataTextField="Code" DataValueField="ExpediteCodeID" AppendDataBoundItems="true"
                        Style="border-style: inset;">
                        <asp:ListItem Text="Select Code" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Contact Name field is required." ForeColor="Red" Font-Size="12px" 
                        ControlToValidate="txtContactName" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Contact Name:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox></td>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Phone Number field is required." ForeColor="Red" Font-Size="12px" 
                        ControlToValidate="txtPhoneNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Phone Number:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Material SKU # field is required." ForeColor="Red" Font-Size="12px" 
                        ControlToValidate="txtMaterialSku" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Material SKU#:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtMaterialSku" runat="server"></asp:TextBox></td>
                <td class="tdright">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Quantity Ordered field is required." ForeColor="Red" Font-Size="12px" 
                        ControlToValidate="txtQuantityOrdered" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Quantity Ordered:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtQuantityOrdered" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999."
                        ControlToValidate="txtAccountNumber" MinimumValue="0" MaximumValue="999999" Font-Size="12px"
                        SetFocusOnError="true" Display="None"></asp:RangeValidator>Account Number:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtAccountNumber" runat="server" MaxLength="6"></asp:TextBox></td>
                <td class="tdright">Purchase Order #:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtPurchaseOrderNumber" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">OOW Order #:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtOowOrderNumber" runat="server"></asp:TextBox></td>
                <td class="tdright">S/M:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtSM" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">Install Date:</td>
                <td class="tdleft">
                    <input type="text" id="txtInstallDate" runat="server" /></td>
                <td class="tdright"></td>
                <td class="tdleft">
                    
                </td>
            </tr>
            <tr>
                <td class="tdsectionheader" colspan="4">Ship To (If different than default)</td>
            </tr>
            <tr>
                <td class="tdright">Name:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtShipToName" runat="server"></asp:TextBox></td>
                <td class="tdright">Street Address:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtShipToAddress" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">City:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtShipToCity" runat="server"></asp:TextBox></td>
                <td class="tdright">State:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtShipToState" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">Zip:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtShipToZip" runat="server"></asp:TextBox></td>
                <td class="tdright"></td>
                <td class="tdleft"></td>
            </tr>
            <tr>
                <td colspan="4" class="tdcenter">Additional Info (Cross Reference to another order, specific dye lots, etc):</td>
            </tr>
            <tr>
                <td colspan="4" class="tdcenter">
                    <asp:TextBox ID="txtAdditionalInfo" runat="server" TextMode="MultiLine" Rows="5" Width="700"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdright">CC Form To:</td>
                <td class="tdleft">
                    <asp:TextBox ID="txtCCFormTo" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexCCFormTo" runat="server" ErrorMessage="Invalid Email Address" ForeColor="Red" Font-Size="12px" ControlToValidate="txtCCFormTo"
                        ValidationExpression=".+\@.+\..+" Display="None"></asp:RegularExpressionValidator>
                </td>
                <td class="tdright">Status:</td>
                <td class="tdleft">
                    <asp:DropDownList ID="ddlStatus" runat="server" SelectMethod="GetStatuses"
                        DataTextField="StatusText" DataValueField="StatusID"
                        AutoPostBack="true" Style="border-style: inset;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
        </table>
        <br /><br />
        <div style="width:100%; text-align:center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit Form" OnClick="btnSubmit_Click" CssClass="normalButton" />
        </div>
    </asp:Panel>


</asp:Content>
