<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomersTab.aspx.cs" Inherits="CarDealerWebApp.CustomersTab"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div >
    <div style="height: 414px; width: 927px;" runat="server">
        <div style="margin: 30px 30px 30px 30px;">
            <asp:RadioButtonList ID="clientTypeRadio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustomersTabPrivateRadioCheckedChanged"
                RepeatDirection="Horizontal">
                <asp:ListItem Value="Private"></asp:ListItem>
                <asp:ListItem Value="Business"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div runat="server" >
            <div style="float: left; width: 100px; margin: 0px 10px 10px 10px;" runat="server">
                <div>
                    <asp:ListBox ID="customersListBox" runat="server" Height="200px" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="customersListBox_SelectedIndexChanged" />
                </div>
                <div align="center">
                    <asp:Button ID="Button1" runat="server" Width="100px" Text="Edit customer" Style="margin-top: 10px;
                        margin-bottom: 10px" OnClick="EditCustomerButtonClick" />
                    <asp:Button ID="Button2" Width="100px" runat="server" Text="Add new" OnClick="AddNewCustomerButtonClick" />
                    <asp:Button ID="Button3" Width="100px" runat="server" Text="Remove selected" OnClick="RemoveSelectedCustomerButtonClick"
                        Style="margin-top: 10px; margin-bottom: 10px" />
                </div>
            </div>
            <div style="float: left; width: 345px; height: 197px; margin: 0px 10px 10px 10px;"
                align="left" runat="server">
                <div class="left" runat="server">
                    <asp:Label ID="customerNameLabel" runat="server" Text="Name:"></asp:Label>
                </div>
                <div class="right" runat="server">
                    <asp:TextBox ID="CustomerNameTextBox" runat="server"></asp:TextBox>
                </div>
                <div class="left" runat="server">
                    <asp:Label ID="CustomerPhoneLabel" runat="server" Text="Phone Number:"></asp:Label>
                </div>
                <div class="right" runat="server">
                    <asp:TextBox ID="CustomerPhoneTextBox" runat="server"></asp:TextBox>
                </div>
                <div class="left" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="Address"></asp:Label>
                </div>
                <div class="right" runat="server">
                    <asp:TextBox ID="AddressTextBox" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div id="businessPartDiv" runat="server">
                    <div class="left" runat="server">
                        <asp:Label ID="SEorSexLabel" runat="server" Text="SE-no"></asp:Label>
                    </div>
                    <div class="right" runat="server">
                        <asp:TextBox ID="SENoTextBox" runat="server"></asp:TextBox>
                    </div>
                    <div class="left" runat="server">
                        <asp:Label ID="FaxLabel" runat="server" Text="Fax"></asp:Label>
                    </div>
                    <div class="right" runat="server">
                        <asp:TextBox ID="FaxTextBox" runat="server"></asp:TextBox>
                    </div>
                    <div class="left" runat="server">
                        <asp:Label ID="ContactPersonLabel" runat="server" Text="Contact person:"></asp:Label>
                    </div>
                    <div class="right" runat="server">
                        <asp:TextBox ID="ContactPersonTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div id="privatePartDiv" runat="server">
                    <div class="left" runat="server">
                        <asp:Label ID="SexLabel" runat="server" Text="Sex"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:RadioButtonList ID="SexRadioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Male"></asp:ListItem>
                            <asp:ListItem Value="Female"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="left">
                        <asp:Label ID="AgeLabel2" runat="server" Text="Age"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:TextBox ID="AgeTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div align="center">
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitClientButton" />
                </div>
            </div>
        </div>
        <div>
        </div>
    </div>
    </div>
</asp:Content>
