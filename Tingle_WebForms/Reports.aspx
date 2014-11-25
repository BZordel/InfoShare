<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Tingle_WebForms.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align:center">
    <div id="accordion1" style="float:left; width:100%">
        <h3>Order Forms</h3>
        <div>
            <div id="accordion2" style="float:left; width:100%">
                <h3>Expedited Order Form Reports</h3>
                <div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Standard Report</div>
                        <a href="RepExpeditedOrderForm.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Advanced Report</div>
                        <a href="RepAdvExpeditedOrderForm.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Archive Report</div>
                        <a href="RepArchiveExpeditedOrder.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText2">Advanced Archive<br />Report</div>
                        <a href="RepAdvArchiveExpeditedOrder.aspx"></a>
                    </div>
                </div>
                <h3>Order Cancellation Form Reports</h3>
                <div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Standard Report</div>
                        <a href="RepOrderCancellation.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Advanced Report</div>
                        <a href="RepAdvOrderCancellation.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Archive Report</div>
                        <a href="RepArchiveOrderCancellation.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText2">Advanced Archive<br />Report</div>
                        <a href="RepAdvArchiveOrderCancellation.aspx"></a>
                    </div>
                </div>
            </div>
        </div>
        <h3>Inventory Forms</h3>
        <div>
            <div id="accordion3" style="float:left; width:100%">

            </div>
        </div>
        <h3>Pricing Forms</h3>
        <div>
            <div id="accordion4" style="float:left; width:100%">
                <h3>Price Change Request Form Reports</h3>
                <div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Standard Report</div>
                        <a href="RepPriceChangeRequest.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Advanced Report</div>
                        <a href="RepAdvPriceChangeRequest.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText">Archive Report</div>
                        <a href="RepArchivePriceChangeRequest.aspx"></a>
                    </div>
                    <div class="FormButton">
                        <div class="FavFormButtonText2">Advanced Archive<br />Report</div>
                        <a href="RepAdvArchivePriceChangeRequest.aspx"></a>
                    </div>
                </div>  
            </div>
        </div>
        <h3>Setup Forms</h3>
        <div>
            <div id="accordion5" style="float:left; width:100%">

            </div>
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
        $("#accordion2").accordion({
            heightStyle: "content"
        });
        $("#accordion3").accordion({
            heightStyle: "content"
        });
        $("#accordion4").accordion({
            heightStyle: "content"
        });
        $("#accordion5").accordion({
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
        $("#accordion2").accordion({
            heightStyle: "content"
        });
        $("#accordion3").accordion({
            heightStyle: "content"
        });
        $("#accordion4").accordion({
            heightStyle: "content"
        });
        $("#accordion5").accordion({
            heightStyle: "content"
        });
    }

</script>
</asp:Content>
