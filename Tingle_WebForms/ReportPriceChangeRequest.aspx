﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPriceChangeRequest.aspx.cs" Inherits="Tingle_WebForms.ReportPriceChangeRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        WireUpFormFields();

    });

    function WireUpFormFields() {
        $("#MainContent_txtToDate").datepicker({ dateFormat: "yy-mm-dd" });
        $("#MainContent_txtFromDate").datepicker({ dateFormat: "yy-mm-dd" });

        $("#<%= txtCustomer.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtCustomer.ClientID %>', '');
        });
        $("#<%= txtLineNumber.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtLineNumber.ClientID %>', '');
        });
        $("#<%= txtAccountNumber.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtAccountNumber.ClientID %>', '');
        });
        $("#<%= txtQuantity.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtQuantity.ClientID %>', '');
        });
        $("#<%= txtSalesRep.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtSalesRep.ClientID %>', '');
        });
        $("#<%= txtProduct.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtProduct.ClientID %>', '');
        });
        $("#<%= txtOrderNumber.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtOrderNumber.ClientID %>', '');
        });
        $("#<%= txtPrice.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtPrice.ClientID %>', '');
        });
        $("#<%= txtCrossRefOldOrderNumber.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtCrossRefOldOrderNumber.ClientID %>', '');
        });
        $("#<%= txtAdditionalInfo.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtAdditionalInfo.ClientID %>', '');
        });
        $("#<%= txtRequestHandler.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtRequestHandler.ClientID %>', '');
        });
    }

</script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="divcenter">
        <asp:Panel ID="pnlFilter" runat="server">
            <table>
                <tr class="trheader">
                    <td colspan="4" class="tdheader">
                        <div style="float: left; width: 25%">
                            <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true"
                                Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                <asp:ListItem Text="Any Company" Value="Any"></asp:ListItem>
                                <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 50%">
                            Price Change Request Report
                        </div>
                        <div style="float: left; width: 25%">
                            <asp:DropDownList runat="server" ID="ddlGlobalStatus" AutoPostBack="true"
                                Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem Text="Archive" Value="Archive"></asp:ListItem>
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:LinkButton runat="server" ID="btnAdvancedSearch" Text="Click for Advanced Search" OnClick="btnAdvancedSearch_Click"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnBasicSearch" Text="Click for Basic Search" Visible="false" OnClick="btnBasicSearch_Click"></asp:LinkButton>
                </tr>
                <tr>
                    <td class="tdcenter" colspan="2">Submission Date Range:</td>
                    <td colspan="2" class="tdleft">From:
                                <asp:TextBox ID="txtFromDate" runat="server" Width="100" AutoPostBack="true" />
                        To:
                                <asp:TextBox ID="txtToDate" runat="server" Width="100" AutoPostBack="true" /></td>
                </tr>

                <tr>
                    <td class="tdheader" colspan="4"></td>
                </tr>
                <tr>
                    <td class="tdright">Customer:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtCustomer" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="tdright">Order #:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtOrderNumber" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdright">Request Handler:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtRequestHandler" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="tdright">Status:</td>
                    <td class="tdleft">
                        <asp:DropDownList ID="ddlStatus" runat="server" SelectMethod="GetStatuses"
                            DataTextField="StatusText" DataValueField="StatusID" AppendDataBoundItems="true"
                            AutoPostBack="true" Style="border-style: inset;">
                            <asp:ListItem Value="0">Any Status</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="tr1" visible="false">
                    <td class="tdright">Account Number:</td>
                    <td class="tdleft"><asp:TextBox ID="txtAccountNumber" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="tdright">Line #:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtLineNumber" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr2" visible="false">
                    <td class="tdright">Sales Rep:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtSalesRep" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright">Quantity:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr3" visible="false">
                    <td class="tdright">Product:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtProduct" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright">Price:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr4" visible="false">
                    <td class="tdright">Cross Reference Old Order #:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtCrossRefOldOrderNumber" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright"></td>
                    <td class="tdleft"></td>
                </tr>
                <tr runat="server" id="tr5" visible="false">
                    <td colspan="4" class="tdcenter">Additional Info:</td>
                </tr>
                <tr runat="server" id="tr6" visible="false">
                    <td colspan="4" class="tdcenter">
                        <asp:TextBox ID="txtAdditionalInfo" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" class="tdcenter"><br /></td>
                </tr>
                <tr>
                    <td colspan="4" class="tdcenter">
                        <asp:LinkButton runat="server" ID="btnResetFilters" PostBackUrl="~/ReportPriceChangeRequest.aspx" CssClass="normalButton" Text="Reset Filters" AutoPostBack="true"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReport">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCustomer" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtLineNumber" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtAccountNumber" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtSalesRep" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtProduct" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtPrice" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtCrossRefOldOrderNumber" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtAdditionalInfo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtRequestHandler" EventName="TextChanged" />
                    <asp:PostBackTrigger ControlID="gvReport" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gvReport" runat="server" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    ItemType="Tingle_WebForms.Models.PriceChangeRequestForm" SelectMethod="GetPriceChangeRequestForms" CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal"
                                    AllowPaging="True" DataKeyNames="RecordId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    OnRowCommand="gvReport_RowCommand" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecordId" runat="server" Text='<%#: Item.RecordId %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Timestamp" ItemStyle-Width="15%" HeaderText="Submission Date" SortExpression="Timestamp" DataFormatString="{0:d}" HtmlEncode="false" />
                                        <asp:TemplateField HeaderText="Submitted By" SortExpression="SubmittedByUser">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubmittedByUser" runat="server" Text='<%#: Item.SubmittedByUser %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer" SortExpression="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%#: Item.Customer %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #" SortExpression="OrderNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderNumber" runat="server" Text='<%#: Item.OrderNumber %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line #" SortExpression="LineNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLineNumber" runat="server" Text='<%#: Item.LineNumber %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Request Handler" SortExpression="RequestHandler">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestHandler" runat="server" Text='<%#: Item.RequestHandler %>' ToolTip='<%#: Item.RequestHandler %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#: Item.Status.StatusText %>' ToolTip='<%#: Item.Status.StatusText %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" SelectText="Details" ShowHeader="True" HeaderText="Details"></asp:CommandField>
                                    </Columns>
                                    <EmptyDataTemplate>No results were found matching your selection.</EmptyDataTemplate>
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
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
            <asp:FormView ID="fvReport" runat="server" ItemType="Tingle_WebForms.Models.PriceChangeRequestForm" SelectMethod="GetFormDetails"
                BackColor="White" BorderStyle="None" DataKeyNames="RecordId" DefaultMode="Edit" UpdateMethod="UpdateForm" OnDataBound="fvReport_DataBound"
                OnItemUpdating="fvReport_ItemUpdating">
                <EditItemTemplate>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:PlaceHolder runat="server" ID="phFVDetails">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">Price Change Request Report Details</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">Form ID:
                                <asp:Label ID="lblRecordId" runat="server" Text='<%#: Eval("RecordId") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdright">Company:</td>
                            <td class="tdleft">
                                <asp:Label runat="server" ID="lblCompanyEdit" Visible="false" Text='<%# Bind("Company") %>'></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlCompanyEdit">
                                    <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                    <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required." 
                                    ControlToValidate="txtCustomerEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtCustomerEdit" runat="server" Text='<%# Bind("Customer") %>'></asp:TextBox><br />
                            </td>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Line # field is required." 
                                    ControlToValidate="txtLineNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Line #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtLineNumberEdit" runat="server" Text='<%# Bind("LineNumber") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Number field is required." 
                                    ControlToValidate="txtAccountNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999."
                                    ControlToValidate="txtAccountNumberEdit" Font-Size="12px" ValidationExpression="^\d+$"
                                    SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>Account Number:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtAccountNumberEdit" runat="server" Text='<%# Bind("AccountNumber") %>'></asp:TextBox></td>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity field is required." 
                                    ControlToValidate="txtQuantityEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Quantity:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Sales Rep field is required." 
                                    ControlToValidate="txtSalesRepEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Sales Rep:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSalesRepEdit" runat="server" Text='<%# Bind("SalesRep") %>'></asp:TextBox></td>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Product field is required." 
                                    ControlToValidate="txtProductEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Product:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtProductEdit" runat="server" Text='<%# Bind("Product") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Order # field is required." 
                                    ControlToValidate="txtOrderNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtOrderNumberEdit" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:TextBox></td>
                            <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Price field is required." 
                                    ControlToValidate="txtPriceEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Price:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtPriceEdit" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Cross Reference Old Order # field is required." 
                                    ControlToValidate="txtCrossRefOldOrderNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Cross Reference Old Order #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtCrossRefOldOrderNumberEdit" runat="server" Text='<%# Bind("CrossReferenceOldOrderNumber") %>'></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">Additional Info - Notes - Comments:</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <asp:TextBox ID="txtAdditionalInfoEdit" runat="server" TextMode="MultiLine" Rows="5" Width="700" Text='<%# Bind("AdditionalInfo") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">CC Form To:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtCCFormTo" runat="server" Enabled="false" Text='<%# Bind("CCFormToEmail") %>'></asp:TextBox>
                            </td>
                            <td class="tdright">Status:</td>
                            <td class="tdleft">
                                <asp:DropDownList ID="ddlStatusEdit" runat="server" SelectMethod="GetStatusesEdit"
                                    DataTextField="StatusText" DataValueField="StatusID" AppendDataBoundItems="true"
                                    Style="border-style: inset;" OnDataBound="ddlStatusEdit_DataBound" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">Request Handler:</td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <asp:TextBox ID="txtRequestHandlerEdit" runat="server" Text='<%# Bind("RequestHandler") %>'></asp:TextBox></td>
                        </tr>
                        <tr runat="server" id="trCompleted1" visible="false">
                            <td colspan="4" class="tdcenter">Completion Notes:</td>
                        </tr>
                        <tr runat="server" id="trCompleted2" visible="false">
                            <td colspan="4" class="tdcenter">
                                <asp:TextBox ID="txtCompletionNotes" runat="server" TextMode="MultiLine" Rows="5" Width="700" Text='<%# Bind("CompletedNotes") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trCompleted3" visible="false">
                            <td colspan="4" class="tdcenter">CC Receipt To: 
                                <asp:TextBox ID="txtCCCompletedFormToEmail" runat="server" Text='<%# Bind("CCCompletedFormToEmail") %>'></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="regexCCFormTo" runat="server" ErrorMessage="Invalid Email Address" ForeColor="Red" Font-Size="12px" ControlToValidate="txtCCCompletedFormToEmail"
                                    ValidationExpression=".+\@.+\..+" Display="None"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright">Submitted By:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="lblSubmittedByEdit" Enabled="false" runat="server" Text='<%#: Eval("SubmittedByUser") %>'></asp:TextBox></td>
                            <td class="tdright">Modified By:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="lblModifiedByEdit" Enabled="false" runat="server" Text='<%#: Eval("ModifiedByUser") %>'></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update" CssClass="normalButton" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" CssClass="normalButton" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                            
                    </asp:PlaceHolder>
                </EditItemTemplate>
            </asp:FormView>

        </asp:Panel>
    </div>
</asp:Content>
