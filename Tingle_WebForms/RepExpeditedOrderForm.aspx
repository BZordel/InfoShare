<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepExpeditedOrderForm.aspx.cs" Inherits="Tingle_WebForms.Reports.ExpeditedOrderForm" EnableEventValidation="false" %>

<%@ Register Assembly="Trirand.Web" TagPrefix="trirand" Namespace="Trirand.Web.UI.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <script>
        function WireUpFormFields() {
            $("#MainContent_txtToDate").datepicker({ dateFormat: "yy-mm-dd" });
            $("#MainContent_txtFromDate").datepicker({ dateFormat: "yy-mm-dd" });
            $("#MainContent_fvReport_txtInstallDateEdit").datepicker({ dateFormat: "yy-mm-dd" });
        }

        $(document).ready(function () {
            WireUpFormFields();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divcenter">
                <asp:Panel ID="pnlReport" runat="server">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">Expedited Orders Report</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <a href="RepAdvExpeditedOrderForm.aspx">Click for Advanced Search</a><br /><br /></td>
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
                            <td class="tdright">Material SKU #:</td>
                            <td class="tdleft"><asp:TextBox ID="txtMaterialSku" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Expeditor Handling:</td>
                            <td class="tdleft"><asp:TextBox ID="txtExpeditorHandling" runat="server"></asp:TextBox></td>
                            <td class="tdright">Submission Date:</td>
                            <td class="tdleft">
                                From: <asp:TextBox ID="txtFromDate" runat="server" Style="width: 75px;" AutoPostBack="true" />
                                To: <asp:TextBox ID="txtToDate" runat="server" Style="width: 75px;" AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td class="tdright">Status:</td>
                            <td class="tdleft">                    
                                <asp:DropDownList ID="ddlStatus" runat="server" SelectMethod="GetStatuses"
                                    DataTextField="StatusText" DataValueField="StatusID" AppendDataBoundItems="true"
                                    AutoPostBack="true" Style="border-style: inset;">
                                    <asp:ListItem Value="0">Any Status</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /><br /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Refresh Report" OnClick="btnSubmit_Click" CssClass="normalButton" />
                                <br />
                                <a href="RepExpeditedOrderForm.aspx">Clear Report</a><br /><br />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" class="reportrow">
                                <asp:GridView ID="gvReport" runat="server" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    ItemType="Tingle_WebForms.Models.ExpeditedOrderForm" SelectMethod="GetExpeditedOrderForms" CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal"
                                    AllowPaging="True" DataKeyNames="ExpeditedOrderFormID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                    OnRowCommand="gvReport_RowCommand" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpeditedOrderFormID" runat="server" Text='<%#: Item.ExpeditedOrderFormID %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Material SKU" SortExpression="MaterialSku">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialSku" runat="server" Text='<%#: Item.MaterialSku %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" SortExpression="QuantityOrdered">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantityOrdered" runat="server" Text='<%#: Item.QuantityOrdered %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expeditor Handling" SortExpression="ExpeditorHandling">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpeditorHandling" runat="server" Text='<%#: Item.ExpeditorHandling %>' ToolTip='<%#: Item.ExpeditorHandling %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
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
                    <asp:FormView ID="fvReport" runat="server" ItemType="Tingle_WebForms.Models.ExpeditedOrderForm" SelectMethod="GetFormDetails"
                        BackColor="White" BorderStyle="None" DataKeyNames="ExpeditedOrderFormID" DefaultMode="Edit" UpdateMethod="UpdateForm"
                        OnItemUpdating="fvReport_ItemUpdating" OnDataBound="fvReport_DataBound">
                            <EditItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>                        
                            <br /><br />
                            <table>
                                <tr class="trheader">
                                    <td colspan="4" class="tdheader">Expedited Order Form Report Details</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Form ID: <asp:Label ID="lblExpeditedOrderFormID" runat="server" Text='<%#: Eval("ExpeditedOrderFormID") %>'></asp:Label></td>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer field is required." ControlToValidate="txtCustomerEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Customer:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtCustomerEdit" runat="server" Text='<%# Bind("Customer") %>'></asp:TextBox><br />
                                    </td>
                                    <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Expedite Code field is required." ControlToValidate="ddlExpediteCodeEdit" InitialValue="0" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Expedite Code:</td>
                                    <td class="tdleft">
                                        <asp:DropDownList ID="ddlExpediteCodeEdit" runat="server" SelectMethod="GetExpediteCodes"
                                            DataTextField="Code" DataValueField="ExpediteCodeID" AppendDataBoundItems="true"
                                            AutoPostBack="true" style="border-style:inset;" OnDataBinding="ddlExpediteCodeEdit_DataBinding">
                                            <asp:ListItem Text="Select Code" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdright">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Contact Name field is required." ControlToValidate="txtContactNameEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Contact Name:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtContactNameEdit" runat="server" Text='<%# Bind("ContactName") %>'></asp:TextBox></td>
                                    <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone Number field is required." ControlToValidate="txtPhoneNumberEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Phone Number:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtPhoneNumberEdit" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Material SKU # field is required." ControlToValidate="txtMaterialSkuEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Material SKU#:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtMaterialSkuEdit" runat="server" Text='<%# Bind("MaterialSku") %>'></asp:TextBox></td>
                                    <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Quantity Ordered field is required." ControlToValidate="txtQuantityOrderedEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Quantity Ordered:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtQuantityOrderedEdit" runat="server" Text='<%# Bind("QuantityOrdered") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Account Number must be a numeric value between 0 and 999999." 
                                        ControlToValidate="txtAccountNumberEdit" Font-Size="12px" ValidationExpression="^\d+$"
                                        SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>Account Number:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtAccountNumberEdit" runat="server" Text='<%# Bind("AccountNumber") %>'></asp:TextBox></td>
                                    <td class="tdright">Purchase Order #:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtPurchaseOrderNumberEdit" runat="server" Text='<%# Bind("PurchaseOrderNumber") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">OOW Order Number:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtOowOrderNumberEdit" runat="server" Text='<%# Bind("OowOrderNumber") %>'></asp:TextBox></td>
                                    <td class="tdright">S/M:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtSMEdit" runat="server" Text='<%# Bind("SM") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Install Date:</td>
                                    <td class="tdleft"><input type="text" id="txtInstallDateEdit" runat="server" value='<%# Bind("InstallDate") %>' /></td>
                                    <td class="tdright"></td>
                                    <td class="tdleft"></td>
                                </tr>
                                <tr>
                                    <td class="tdsectionheader" colspan="4">Ship To (If different than default)</td>
                                </tr>
                                <tr>
                                    <td class="tdright">Name:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtShipToNameEdit" runat="server" Text='<%# Bind("ShipToName") %>'></asp:TextBox></td>
                                    <td class="tdright">Sreet Address:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtShipToAddressEdit" runat="server" Text='<%# Bind("ShipToAddress") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">City:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtShipToCityEdit" runat="server" Text='<%# Bind("ShipToCity") %>'></asp:TextBox></td>
                                    <td class="tdright">State:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtShipToStateEdit" runat="server" Text='<%# Bind("ShipToState") %>'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="tdright">Zip:</td>
                                    <td class="tdleft"><asp:TextBox ID="txtShipToZipEdit" runat="server" Text='<%# Bind("ShipToZip") %>'></asp:TextBox></td>
                                    <td class="tdright"></td>
                                    <td class="tdleft"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Additional Info (Cross Reference to another order, specific dye lots, etc):</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter"><asp:TextBox ID="txtAdditionalInfoEdit" runat="server" TextMode="MultiLine" Rows="5" Width="700"  Text='<%# Bind("AdditionalInfo") %>'></asp:TextBox></td>
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
                                            style="border-style:inset;" OnDataBound="ddlStatusEdit_DataBound">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter">Expeditor Handler:</td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdcenter"><asp:TextBox ID="txtExpeditorHandlingEdit" runat="server" Text='<%# Bind("ExpeditorHandling") %>'></asp:TextBox></td>
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
                                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update" CssClass="normalButton" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" CssClass="normalButton" OnClick="btnCancel_Click" />
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
