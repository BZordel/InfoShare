<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HotRushForm.aspx.cs" Inherits="Tingle_WebForms.HotRushForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
<script>
    function BindEvents() {
        $(document).ready(function () {
            $("#<%= txtInstallDate.ClientID %>").datepicker({ dateFormat: "yy-mm-dd" });
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
                                Hot Rush Form
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Entire Order OR Line # field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtEntireOrderOrLineNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Entire Order OR Line #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtEntireOrderOrLineNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Order Number field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtOrderNumber" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox></td>
                        <td class="tdright"></td>
                        <td class="tdleft"><asp:CheckBox runat="server" ID="cbCreditRelease" Text="Credit Release" /></td>
                    </tr>
                    <tr>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Install Date field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtInstallDate" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Install Date:</td>
                        <td class="tdleft">
                            <input type="text" id="txtInstallDate" runat="server" /></td>
                        <td class="tdright"></td>
                        <td class="tdleft"><asp:CheckBox runat="server" ID="cbOrderAcknowledgement" Text="Order Acknowledgement" /></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdcenter">Additional Info - Notes:</td>
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
