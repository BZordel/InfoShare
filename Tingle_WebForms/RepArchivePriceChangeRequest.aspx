<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepArchivePriceChangeRequest.aspx.cs" Inherits="Tingle_WebForms.RepArchivePriceChangeRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script>
        function WireUpFormFields() {
            $("#MainContent_txtToDate").datepicker({ dateFormat: "yy-mm-dd" });
            $("#MainContent_txtFromDate").datepicker({ dateFormat: "yy-mm-dd" });
        }

        $(document).ready(function () {
            WireUpFormFields();
        });
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divcenter">
                <asp:Panel ID="pnlReport" runat="server">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">Archive Price Change Request Report</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <a href="RepAdvArchivePriceChangeRequest.aspx">Click for Advanced Search</a><br /><br /></td>
                        </tr>
                        <tr>
                            <td class="tdright">Company:</td>
                            <td class="tdleft">
                                <asp:DropDownList runat="server" ID="ddlCompany">
                                    <asp:ListItem Text="Any Company" Value="Any"></asp:ListItem>
                                    <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                    <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td class="tdright">Customer:</td>
                            <td class="tdleft"><asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox></td>
                            <td class="tdright">Order #:</td>
                            <td class="tdleft"><asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Request Handler:</td>
                            <td class="tdleft"><asp:TextBox ID="txtRequestHandler" runat="server"></asp:TextBox></td>
                            <td class="tdright">Submission Date:</td>
                            <td class="tdleft">
                                From: <asp:TextBox ID="txtFromDate" runat="server" Style="width: 75px;" AutoPostBack="true" />
                                To: <asp:TextBox ID="txtToDate" runat="server" Style="width: 75px;" AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /><br /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Refresh Report" OnClick="btnSubmit_Click" CssClass="normalButton" />
                                <br />
                                <a href="RepArchivePriceChangeRequest.aspx">Clear Report</a><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="reportrow">
                                <asp:GridView ID="gvReport" runat="server" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    ItemType="Tingle_WebForms.Models.PriceChangeRequestForm" SelectMethod="GetPriceChangeRequestForms" CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal"
                                    AllowPaging="True" DataKeyNames="RecordId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    OnRowCommand="gvReport_RowCommand" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecordId" runat="server" Text='<%#: Item.RecordId %>'></asp:Label>
                                            </ItemTemplate>
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
                                        <asp:TemplateField HeaderText="Account #" SortExpression="AccountNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNumber" runat="server" Text='<%#: Item.AccountNumber %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #" SortExpression="OrderNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderNumber" runat="server" Text='<%#: Item.OrderNumber %>'></asp:Label>
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
                </asp:Panel>
                <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" HeaderText="There are errors in the form:" Font-Size="14px" ForeColor="Red" />
                    <asp:FormView ID="fvReport" runat="server" ItemType="Tingle_WebForms.Models.PriceChangeRequestForm" SelectMethod="GetFormDetails"
                        BackColor="White" BorderStyle="None" DataKeyNames="RecordId" DefaultMode="Edit" UpdateMethod="UpdateForm"
                        OnItemUpdating="fvReport_ItemUpdating" OnDataBound="fvReport_DataBound">
                            <EditItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>                        
                            <br /><br />
                            <table>
                                <tr class="trheader">
                                    <td colspan="4" class="tdheader">Price Change Request Form Report Details</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Form ID: <asp:Label ID="lblRecordId" runat="server" Text='<%#: Eval("RecordId") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Company:</td>
                                    <td class="tdleft">
                                        <asp:Label runat="server" ID="lblCompanyEdit" Visible="false" Text='<%# Bind("Company") %>'></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlCompanyEdit" Enabled="false">
                                            <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                            <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdright"></td>
                                    <td class="tdleft"></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Customer:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtCustomerEdit" runat="server" Enabled="false" Text='<%# Bind("Customer") %>'></asp:TextBox><br />
                                    </td>
                                    <td class="tdright">Line #:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtLineNumberEdit" runat="server" Text='<%# Bind("LineNumber") %>' Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdright">Account Number:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtAccountNumberEdit" runat="server" Text='<%# Bind("AccountNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright">Quantity:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("Quantity") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Sales Rep:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtSalesRepEdit" runat="server" Text='<%# Bind("SalesRep") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright">Product:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtProductEdit" runat="server" Text='<%# Bind("Product") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Order #:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtOrderNumberEdit" runat="server" Text='<%# Bind("OrderNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright">Price:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtPriceEdit" runat="server" Text='<%# Bind("Price") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Cross Reference Old Order #:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtCrossRefOldOrderNumberEdit" runat="server" Text='<%# Bind("CrossReferenceOldOrderNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright"></td>
                                    <td class="tdleft"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Additional Info - Notes - Comments:</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter"><asp:TextBox ID="txtAdditionalInfoEdit" runat="server" TextMode="MultiLine" Rows="5" Width="700"
                                        Text='<%# Bind("AdditionalInfo") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">CC Form To:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtCCFormToEdit" runat="server" Enabled="false" Text='<%# Bind("CCFormToEmail") %>'></asp:TextBox>
                                    </td>
                                    <td class="tdright">Status:</td>
                                    <td class="tdleft">
                                        <asp:DropDownList ID="ddlStatusEdit" runat="server" SelectMethod="GetStatusesEdit"
                                            DataTextField="StatusText" DataValueField="StatusID" AppendDataBoundItems="true"
                                            style="border-style:inset;" OnDataBound="ddlStatusEdit_DataBound" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Request Handler:</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter"><asp:TextBox ID="txtRequestHandlerEdit" runat="server" Text='<%# Bind("RequestHandler") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Submitted By:</td>
                                    <td class="tdleft"><asp:TextBox ID="lblSubmittedByEdit" runat="server" Enabled="false" Text='<%#: Eval("SubmittedByUser") %>'></asp:TextBox></td>
                                    <td class="tdright">Modified By:</td>
                                    <td class="tdleft"><asp:TextBox ID="lblModifiedByEdit" runat="server" Enabled="false" Text='<%#: Eval("ModifiedByUser") %>'></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Back" CssClass="normalButton" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>

                            </table>
					    </EditItemTemplate>
                    </asp:FormView>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
<script type="text/javascript">
    // Create the event handler for PageRequestManager.endRequest
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(WireUpFormFields);
</script>
</asp:Content>
