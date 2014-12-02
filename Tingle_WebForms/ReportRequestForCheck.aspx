<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportRequestForCheck.aspx.cs" Inherits="Tingle_WebForms.ReportRequestForCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        WireUpFormFields();

    });

    function WireUpFormFields() {
        $("#<%= txtToDate.ClientID %>").datepicker({ dateFormat: "yy-mm-dd" });
        $("#<%= txtFromDate.ClientID %>").datepicker({ dateFormat: "yy-mm-dd" });

        $("#<%= txtPayableTo.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtPayableTo.ClientID %>', '');
        });
        $("#<%= txtChargeTo.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtChargeTo.ClientID %>', '');
        });
        $("#<%= txtRequestHandler.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtRequestHandler.ClientID %>', '');
        });
        $("#<%= txtAmount.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtAmount.ClientID %>', '');
        });
        $("#<%= txtFor.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtFor.ClientID %>', '');
        });
        $("#<%= txtRequestedBy.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtRequestedBy.ClientID %>', '');
        });
        $("#<%= txtApprovedBy.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtApprovedBy.ClientID %>', '');
        });
        $("#<%= txtShipToName.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtShipToName.ClientID %>', '');
        });
        $("#<%= txtShipToAddress.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtShipToAddress.ClientID %>', '');
        });
        $("#<%= txtShipToCity.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtShipToCity.ClientID %>', '');
        });
        $("#<%= txtShipToState.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtShipToState.ClientID %>', '');
        });
        $("#<%= txtShipToZip.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtShipToZip.ClientID %>', '');
        });
        $("#<%= txtAdditionalInfo.ClientID %>").bind("keyup", function (e) {
            __doPostBack('<%= txtAdditionalInfo.ClientID %>', '');
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
                            Request For Check Report
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
                    <td class="tdright">Payable To:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtPayableTo" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="tdright">Charge To:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtChargeTo" runat="server" AutoPostBack="true"></asp:TextBox>
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
                    <td class="tdright">Amount:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="tdright">For:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtFor" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="tr2" visible="false">
                    <td class="tdright">Requested By:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtRequestedBy" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright">Approved By:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtApprovedBy" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr3" visible="false">
                    <td class="tdsectionheader" colspan="4">Ship To </td>
                </tr>
                <tr runat="server" id="tr4" visible="false">
                    <td class="tdright">Name:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtShipToName" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright">Street Address:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtShipToAddress" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr5" visible="false">
                    <td class="tdright">City:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtShipToCity" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright">State:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtShipToState" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr runat="server" id="tr6" visible="false">
                    <td class="tdright">Zip:</td>
                    <td class="tdleft">
                        <asp:TextBox ID="txtShipToZip" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    <td class="tdright"></td>
                    <td class="tdleft"></td>
                </tr>
                <tr runat="server" id="tr7" visible="false">
                    <td colspan="4" class="tdcenter">Additional Info:</td>
                </tr>
                <tr runat="server" id="tr8" visible="false">
                    <td colspan="4" class="tdcenter">
                        <asp:TextBox ID="txtAdditionalInfo" runat="server" AutoPostBack="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" class="tdcenter"><br /></td>
                </tr>
                <tr>
                    <td colspan="4" class="tdcenter">
                        <asp:LinkButton runat="server" ID="btnResetFilters" PostBackUrl="~/ReportRequestForCheck.aspx" CssClass="normalButton" Text="Reset Filters" AutoPostBack="true"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReport">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtPayableTo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtChargeTo" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtRequestHandler" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtAmount" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtFor" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtRequestedBy" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtApprovedBy" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtShipToName" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtShipToAddress" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtShipToCity" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtShipToState" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtShipToZip" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtAdditionalInfo" EventName="TextChanged" />
                    <asp:PostBackTrigger ControlID="gvReport" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gvReport" runat="server" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    ItemType="Tingle_WebForms.Models.RequestForCheckForm" SelectMethod="GetRequestForCheckForms" CellPadding="4" ForeColor="#BC4445" GridLines="Horizontal"
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
                                        <asp:TemplateField HeaderText="Payable To" SortExpression="PayableTo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayableTo" runat="server" Text='<%#: Item.PayableTo %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="For" SortExpression="For">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFor" runat="server" Text='<%#: Item.For %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested By" SortExpression="RequestedBy">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestedBy" runat="server" Text='<%#: Item.RequestedBy %>'></asp:Label>
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
            <asp:FormView ID="fvReport" runat="server" ItemType="Tingle_WebForms.Models.RequestForCheckForm" SelectMethod="GetFormDetails"
                BackColor="White" BorderStyle="None" DataKeyNames="RecordId" DefaultMode="Edit" UpdateMethod="UpdateForm" OnDataBound="fvReport_DataBound"
                OnItemUpdating="fvReport_ItemUpdating">
                <EditItemTemplate>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:PlaceHolder runat="server" ID="phFVDetails">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">Request For Check Report Details</td>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Payable To field is required." 
                                    ControlToValidate="txtPayableToEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Payable To:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtPayableToEdit" runat="server" Text='<%# Bind("PayableTo") %>'></asp:TextBox><br />

                            </td>
                            <td class="tdright" rowspan="2">
                                    Charge To:</td>
                            <td class="tdleft">
                                <asp:RadioButtonList runat="server" ID="rblChargeToEdit" BorderStyle="None" RepeatDirection="Horizontal" OnDataBound="rblChargeToEdit_DataBound">
                                    <asp:ListItem Text="Account #" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Amount field is required." 
                                    ControlToValidate="txtAmountEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Amount:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtAmountEdit" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox></td>
                            <td class="tdleft">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Charge To field is required." 
                                    ControlToValidate="txtChargeToEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtChargeToEdit" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="For field is required." 
                                    ControlToValidate="txtForEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>For:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtForEdit" runat="server" Text='<%# Bind("For") %>'></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td class="tdright">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Requested By field is required." 
                                    ControlToValidate="txtRequestedByEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Requested By:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtRequestedByEdit" runat="server" Text='<%# Bind("RequestedBy") %>'></asp:TextBox></td>
                            <td class="tdright"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Approved By field is required." 
                                    ControlToValidate="txtApprovedByEdit" ForeColor="Red" Display="None"></asp:RequiredFieldValidator>Approved By:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtApprovedByEdit" runat="server" Text='<%# Bind("ApprovedBy") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdsectionheader" colspan="4">Ship To (If different than default)</td>
                        </tr>
                        <tr>
                            <td class="tdright">Name:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipToNameEdit" runat="server" Text='<%# Bind("ShipToName") %>'></asp:TextBox></td>
                            <td class="tdright">Sreet Address:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipToAddressEdit" runat="server" Text='<%# Bind("ShipToAddress") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">City:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipToCityEdit" runat="server" Text='<%# Bind("ShipToCity") %>'></asp:TextBox></td>
                            <td class="tdright">State:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipToStateEdit" runat="server" Text='<%# Bind("ShipToState") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdright">Zip:</td>
                            <td class="tdleft">
                                <asp:TextBox ID="txtShipToZipEdit" runat="server" Text='<%# Bind("ShipToZip") %>'></asp:TextBox></td>
                            <td class="tdright"></td>
                            <td class="tdleft"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter">Additional Info - Notes:</td>
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
                                <asp:RegularExpressionValidator ID="regexCCFormTo" runat="server" ErrorMessage="Invalid Email Address" ForeColor="Red" Font-Size="12px" 
                                    ControlToValidate="txtCCCompletedFormToEmail" ValidationExpression=".+\@.+\..+" Display="None"></asp:RegularExpressionValidator>
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
