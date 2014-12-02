<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Tingle_WebForms.Reports.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
                <div style="float: left; width: 100%; padding-bottom: 20px;">
                    <table>
                        <tr class="trheader">
                            <td colspan="4" class="tdheader">
                                <div style="float: left; width: 25%">
                                    <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                        Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                        <asp:ListItem Text="Any Company" Value="Any"></asp:ListItem>
                                        <asp:ListItem Text="Tingle" Value="Tingle"></asp:ListItem>
                                        <asp:ListItem Text="Summit" Value="Summit"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 50%">
                                    Reporting Overview
                                </div>
                                <div style="float: left; width: 25%">
                                    <asp:DropDownList runat="server" ID="ddlGlobalStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlGlobalStatus_SelectedIndexChanged"
                                        Style="border-style: inset; height: 27px; width: 75%; text-align: center; background-color: #bc4445; color: white; font-weight: bold">
                                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                        <asp:ListItem Text="Archive" Value="Archive"></asp:ListItem>
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom:1px solid black">Order Forms</td>
                        </tr>
                        <tr>
                            <td class="tdcenterSmall" style="width: 25%">Expedited Order:
                                <asp:LinkButton runat="server" ID="lbExpeditedOrderCount" PostBackUrl="~/ReportExpeditedOrder.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">Direct Order:
                                <asp:LinkButton runat="server" ID="lbDirectOrderCount" PostBackUrl="~/ReportDirectOrder.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">Hot Rush:
                                <asp:LinkButton runat="server" ID="lbHotRushCount" PostBackUrl="~/ReportHotRush.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">Order Cancellation:
                                <asp:LinkButton runat="server" ID="lbOrderCancellationCount" PostBackUrl="~/ReportOrderCancellation.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdcenterSmall" style="width: 25%">Must Include:
                                <asp:LinkButton runat="server" ID="lbMustIncludeCount" PostBackUrl="~/ReportMustInclude.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom:1px solid black">Inventory Forms</td>
                        </tr>
                        <tr>
                            <td class="tdcenterSmall" style="width: 25%">Sample Request:
                                <asp:LinkButton runat="server" ID="lbSampleRequestCount" PostBackUrl="~/ReportSampleRequest.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">Low Inventory:
                                <asp:LinkButton runat="server" ID="lbLowInventoryCount" PostBackUrl="~/ReportLowInventory.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="4"><br /></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom:1px solid black">Price Forms</td>
                        </tr>
                        <tr>
                            <td class="tdcenterSmall" style="width: 25%">Price Change Request:
                                <asp:LinkButton runat="server" ID="lbPriceChangeRequestCount" PostBackUrl="~/ReportPriceChangeRequest.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">Request For Check:
                                <asp:LinkButton runat="server" ID="lbRequestForCheckCount" PostBackUrl="~/ReportRequestForCheck.aspx" CssClass="ReportLinkButton"></asp:LinkButton>
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdcenter" style="border-bottom:1px solid black">Setup Forms</td>
                        </tr>
                        <tr>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                            <td class="tdcenterSmall" style="width: 25%">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="accordion1" style="float: left; width: 100%">
                    <h3>Order Forms</h3>
                    <div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Expedited Order</div>
                            <a href="ReportExpeditedOrder.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Direct Order</div>
                            <a href="ReportDirectOrder.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Hot Rush</div>
                            <a href="ReportHotRush.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText2">Order<br />
                                Cancellation</div>
                            <a href="ReportOrderCancellation.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Must Include</div>
                            <a href="ReportMustInclude.aspx"></a>
                        </div>
                    </div>
                    <h3>Inventory Forms</h3>
                    <div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Sample Request</div>
                            <a href="ReportSampleRequest.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText">Low Inventory</div>
                            <a href="ReportLowInventory.aspx"></a>
                        </div>
                    </div>
                    <h3>Pricing Forms</h3>
                    <div>
                        <div class="FormButton">
                            <div class="FavFormButtonText2">Price Change<br />
                                Request</div>
                            <a href="ReportPriceChangeRequest.aspx"></a>
                        </div>
                        <div class="FormButton">
                            <div class="FavFormButtonText2">Request For<br />
                                Check</div>
                            <a href="ReportRequestForCheck.aspx"></a>
                        </div>
                    </div>
                    <h3>Setup Forms</h3>
                    <div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_initializeRequest(InitializeRequest);
                    prm.add_endRequest(EndRequest);
                    // on page ready first init of your accordion
                    $("#accordion1").accordion({
                        heightStyle: "content"
                    });
                });


                function InitializeRequest(sender, args) {
                }

                function EndRequest(sender, args) {
                    // after the UpdatePanel finish the render from ajax call
                    //  and the DOM is ready, re-initilize the accordion
                    $("#accordion1").accordion({
                        heightStyle: "content"
                    });
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
