<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicileTab.aspx.cs" Inherits="CarDealerWebApp.VehicileTab"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="Div1" style="height: 414px; width: 927px;" runat="server">
        <div style="margin: 30px 30px 30px 30px;">
            <asp:RadioButtonList ID="vehicileTypeRadio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="VehicilesTabPrivateRadioCheckedChanged"
                RepeatDirection="Vertical">
                <asp:ListItem Value="Trucks"></asp:ListItem>
                <asp:ListItem Value="Small Cars"></asp:ListItem>
                <asp:ListItem Value="Big Cars"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div id="Div2" runat="server">
            <div id="Div3" style="float: left; width: 100px; margin: 0px 10px 10px 10px;" runat="server">
                <div>
                    <asp:ListBox ID="vehicilesListBox" runat="server" Height="200px" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="VehicilesListBoxSelectionChanged" />
                </div>
                <div align="center">
                    <asp:Button ID="Button2" Width="100px" runat="server" Text="Add new" OnClick="AddNewVehicileButtonClick" />
                    <asp:Button ID="Button3" Width="100px" runat="server" Text="Remove selected" OnClick="RemoveSelectedVehicileButtonClick"
                        Style="margin-top: 10px; margin-bottom: 10px" />
                </div>
                <div>
                </div>
            </div>
            <div id="Div4" style="float: left; width: 417px; height: 197px; margin: 0px 10px 10px 10px;"
                align="left" runat="server">
                <div id="Div5" class="left" runat="server">
                    <asp:Label runat="server" Text="VehicileId:"></asp:Label>
                </div>
                <div id="Div6" class="right" runat="server">
                    <asp:TextBox ID="VehicileIdTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div7" class="left" runat="server">
                    <asp:Label ID="registerNumberLabel" runat="server" Text="Register Number:"></asp:Label>
                </div>
                <div id="Div8" class="right" runat="server">
                    <asp:TextBox ID="RegisterNumberTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div23" class="left" runat="server">
                    <asp:Label ID="Label4" runat="server" Text="Brand"></asp:Label>
                </div>
                <div id="Div24" class="right" runat="server">
                    <asp:TextBox ID="BrandTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div9" class="left" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="model"></asp:Label>
                </div>
                <div id="Div10" class="right" runat="server">
                    <asp:TextBox ID="ModelTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div18" class="left" runat="server">
                    <asp:Label runat="server" Text="Color"></asp:Label>
                </div>
                <div id="Div19" class="right" runat="server">
                    <asp:TextBox ID="ColorTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div20" class="left" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Price"></asp:Label>
                </div>
                <div id="Div21" class="right" runat="server">
                    <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="Div22" class="left" runat="server">
                    <asp:Label ID="Label3" runat="server" Text="VehicileState"></asp:Label>
                </div>
                <div class="right">
                    <asp:RadioButtonList ID="VehicileStateRadioList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Free"></asp:ListItem>
                        <asp:ListItem Value="Commision"></asp:ListItem>
                        <asp:ListItem Value="Leased"></asp:ListItem>
                        <asp:ListItem Value="Sold"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div id="Div25" class="left" runat="server">
                    <asp:Label ID="Label5" runat="server" Text="Rented/Leased by:"></asp:Label>
                </div>
                <div id="Div26" class="right" runat="server">
                    <asp:TextBox ID="RentetOrLeasedTextBox" runat="server"></asp:TextBox>
                </div>
                <div id="capacityPartDiv" runat="server">
                    <div id="Div11" class="left" runat="server">
                        <asp:Label ID="Label6" runat="server" Text="Capacity:"></asp:Label>
                    </div>
                    <div id="Div12" class="right" runat="server">
                        <asp:TextBox ID="CapacityTextBox2" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div id="TruckPartDiv" runat="server">
                    <div id="Div13" class="left" runat="server">
                        <asp:Label ID="Label7" runat="server" Text="Length:"></asp:Label>
                    </div>
                    <div id="Div14" class="right" runat="server">
                        <asp:TextBox ID="LengthTextBox" runat="server"></asp:TextBox>
                    </div>
                    <div id="Div15" class="left" runat="server">
                        <asp:Label ID="Label8" runat="server" Text="Weight:"></asp:Label>
                    </div>
                    <div id="Div16" class="right" runat="server">
                        <asp:TextBox ID="WeightTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div align="center">
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitVehicule" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
