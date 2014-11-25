<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepAdvArchivePriceChangeRequest.aspx.cs" Inherits="Tingle_WebForms.RepAdvArchivePriceChangeRequest" %>
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
                            <td colspan="4" class="tdheader">Advanced Archive Price Change Request Report</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <a href="RepArchivePriceChangeRequest.aspx">Click for Basic Search</a></td>
                        </tr>
                        <tr>
                            <td class="tdcenter" colspan="2">Submission Date:</td>
                            <td colspan="2" class="tdleft">From:
                                <asp:TextBox ID="txtFromDate" runat="server" Width="100" />
                                To:
                                <asp:TextBox ID="txtToDate" runat="server" Width="100" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdheader" colspan="4"></td>
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
                            <td class="tdleft">
                                <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox></td>
                            <td class="tdright">Line #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtLineNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright">Account Number:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox></td>
                            <td class="tdright">Quantity:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Sales Rep:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtSalesRep" runat="server"></asp:TextBox></td>
                            <td class="tdright">Product:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Order #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox></td>
                            <td class="tdright">Price:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Cross Reference Old Order #:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtCrossRefOldOrderNumber" runat="server"></asp:TextBox></td>
                            <td class="tdright">Additional Info:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtAdditionalInfo" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Request Handler:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtRequestHandler" runat="server"></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">
                                <asp:Button ID="btnSubmit" runat="server" Text="Refresh Report" OnClick="btnSubmit_Click" CssClass="normalButton" />
                                <br />
                                <a href="RepAdvArchivePriceChangeRequest.aspx">Clear Report</a>
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
                                        <asp:TemplateField HeaderText="Account #" SortExpression="AccountNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNumber" runat="server" Text='<%#: Item.AccountNumber %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #" SortExpression="OrderNumber">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderNumber" runat="server" Text='<%#: Item.OrderNumber %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
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
                        BackColor="White" BorderStyle="None" DataKeyNames="RecordId" DefaultMode="Edit" UpdateMethod="UpdateForm" OnDataBound="fvReport_DataBound"
                        OnItemUpdating="fvReport_ItemUpdating">
                        <EditItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            <br />
                            <br />
                            <table>
                                <tr class="trheader">
                                    <td colspan="4" class="tdheader">Price Change Request Form Report Details</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Form ID:
                                        <asp:Label ID="lblRecordId" runat="server" Text='<%#: Eval("RecordId") %>' Enabled="false"></asp:Label></td>
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
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required."
                                            ControlToValidate="txtCustomerEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtCustomerEdit" runat="server" Text='<%# Bind("Customer") %>' Enabled="false"></asp:TextBox><br />
                                    </td>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Line # field is required."
                                            ControlToValidate="txtLineNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Line #:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtLineNumberEdit" runat="server" Text='<%# Bind("LineNumber") %>' Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Numberfield is required."
                                            ControlToValidate="txtAccountNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999."
                                            ControlToValidate="txtAccountNumberEdit" Font-Size="12px" ValidationExpression="^\d+$"
                                            SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>Account Number:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtAccountNumberEdit" runat="server" Text='<%# Bind("AccountNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity field is required."
                                            ControlToValidate="txtQuantityEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Phone Number:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtQuantityEdit" runat="server" Text='<%# Bind("Quantity") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Sales Rep field is required."
                                            ControlToValidate="txtSalesRepEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Sales Rep:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtSalesRepEdit" runat="server" Text='<%# Bind("SalesRep") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Product field is required."
                                            ControlToValidate="txtProductEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Product:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtProductEdit" runat="server" Text='<%# Bind("Product") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Order # field is required."
                                            ControlToValidate="txtOrderNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Order #:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtOrderNumberEdit" runat="server" Text='<%# Bind("OrderNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Price field is required."
                                            ControlToValidate="txtPriceEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Price:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtPriceEdit" runat="server" Text='<%# Bind("Price") %>' Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Cross Reference Old Order #:</td>
                                    <td class="tdleft">
                                        <asp:TextBox ID="txtCrossRefOldOrderNumberEdit" runat="server" Text='<%# Bind("CrossReferenceOldOrderNumber") %>' Enabled="false"></asp:TextBox></td>
                                    <td class="tdright"></td>
                                    <td class="tdleft"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Additional Info - Notes - Comments:</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">
                                        <asp:TextBox ID="txtAdditionalInfoEdit" runat="server" TextMode="MultiLine" Rows="5" Width="700"
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
                                            Style="border-style: inset;" OnDataBound="ddlStatusEdit_DataBound" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Request Handler:</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">
                                        <asp:TextBox ID="txtRequestHandlerEdit" runat="server" Text='<%# Bind("RequestHandler") %>' Enabled="false"></asp:TextBox></td>
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
