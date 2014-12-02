<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestForCheckForm.aspx.cs" Inherits="Tingle_WebForms.RequestForCheckForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
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
                                Request For Check Form
                            </div>
                            <div style="float: left; width: 25%">&nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Payable To field is required." ForeColor="Black" Font-Size="16px"
                                ControlToValidate="txtPayableTo" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Payable To:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtPayableTo" runat="server"></asp:TextBox><br />
                        </td>
                        <td class="tdright" rowspan="2">Charge To:</td>
                        <td class="tdleft">
                            <asp:RadioButtonList runat="server" ID="rblChargeTo" RepeatDirection="Horizontal" BorderStyle="None">
                                <asp:ListItem Text="Account #" Value="A" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Amount field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtAmount" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Amount:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox></td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtChargeTo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdright">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="For field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtFor" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>For:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtFor" runat="server"></asp:TextBox></td>
                        <td class="tdright"></td>
                        <td class="tdleft"></td>
                    </tr>
                    <tr>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Requested By field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtRequestedBy" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Requested By:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtRequestedBy" runat="server" MaxLength="6"></asp:TextBox></td>
                        <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Approved By field is required." ForeColor="Red" Font-Size="12px"
                                ControlToValidate="txtApprovedBy" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>Approved By:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtApprovedBy" runat="server" MaxLength="6"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdsectionheader" colspan="4">
                            <div style="float: left; width: 25%">&nbsp;</div>
                            <div style="float: left; width: 50%; text-align: center">Ship To (If different than default)</div>
                            <div style="float: left; width: 25%; text-align: right">
                                <asp:CheckBox runat="server" ID="cbShipToSection" AutoPostBack="true" OnCheckedChanged="cbShipToSection_CheckedChanged" Checked="false" />
                            </div>
                        </td>
                    </tr>
                    <tr runat="server" id="tr1" visible="false">
                        <td class="tdright">Name:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToName" runat="server"></asp:TextBox></td>
                        <td class="tdright">Street Address:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="tr2" visible="false">
                        <td class="tdright">City:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToCity" runat="server"></asp:TextBox></td>
                        <td class="tdright">State:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToState" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="tr3" visible="false">
                        <td class="tdright">Zip:</td>
                        <td class="tdleft">
                            <asp:TextBox ID="txtShipToZip" runat="server"></asp:TextBox></td>
                        <td class="tdright"></td>
                        <td class="tdleft"></td>
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
