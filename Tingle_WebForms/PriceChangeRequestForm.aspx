<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceChangeRequestForm.aspx.cs" Inherits="Tingle_WebForms.PriceChangeRequestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function BindEvents() {
            $(document).ready(function () {
                $("#MainContent_txtInstallDate").datepicker();
            });
        }
    </script>
    <asp:Panel ID="pnlCompleted" runat="server" Visible="false">
        <div style="text-align: center; width: 100%">
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="22px"></asp:Label><br />
            <br />
            <asp:LinkButton ID="lbStartOver" runat="server" Text="Click Here to Submit Another Form" OnClick="lbStartOver_Click" Font-Size="22px"></asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlForm" runat="server" Visible="true">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(BindEvents);
                </script>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
                <br />
                <br />
                <table>
                    <tr class="trheader">
                        <td colspan="4" class="tdheader">
                            <div style="float: left; width: 25%">
                                <asp:DropDownList runat="server" ID="ddlCompany"
                                    Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                    <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                    <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 50%">
                                Price Change Request Form
                            </div>
                            <div style="float: left; width: 25%">&nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required." ForeColor="Black" Font-Size="16px"
                                ControlToValidate="txtCustomer" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox><br />
                        </td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Line # field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtLineNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Line #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtLineNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Number field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtAccountNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Account Number:</td>
                        <td class="tdleft"><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999."
                                ControlToValidate="txtAccountNumber" MinimumValue="0" MaximumValue="999999" Font-Size="12px"
                                SetFocusOnError="true" Display="None"></asp:RangeValidator>
                            <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox></td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Quantity field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtQuantity" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Quantity:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Sales Rep field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtSalesRep" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Sales Rep:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtSalesRep" runat="server"></asp:TextBox></td>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Product field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtProduct" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Product:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Order # field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtOrderNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtOrderNumber" runat="server" MaxLength="6"></asp:TextBox></td>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Price field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtPrice" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Price:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Price field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtCrossRefOldOrderNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Cross Reference Old Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtCrossRefOldOrderNumber" runat="server"></asp:TextBox></td>
                        <td class="tdright"></td>
                        <td class="tdleft"></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdcenter">Additional Info - Notes - Comments:</td>
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
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdcenter">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Form" OnClick="btnSubmit_Click" CssClass="normalButton" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </asp:Panel>
</asp:Content>
