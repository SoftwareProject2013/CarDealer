<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractsManagement.aspx.cs" Inherits="CarDealerWebApp.ContractsManagement"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="Div1" style="height: 250px; width: 934px;" runat="server">
        <div id="Div2" runat="server">
            <div id="Div3" style="float: left; width: 150px; margin: 10px 20px 10px 30px;" runat="server">
                <asp:Label ID="Label1" runat="server" Text="1 - Select customer type"></asp:Label>
                <div style="margin: 10px 10px 10px 10px;">
                    <asp:RadioButtonList ID="clientTypeRadio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustomersTabPrivateRadioCheckedChanged"
                        RepeatDirection="Vertical">
                        <asp:ListItem Value="Private"></asp:ListItem>
                        <asp:ListItem Value="Business"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div>
                    <asp:ListBox ID="customersListBox" runat="server" Height="150px" Width="150px" AutoPostBack="True"
                        OnSelectedIndexChanged="customersListBox_SelectedIndexChanged" />
                </div>
            </div>
            <div id="Div4" style=" float: left; width: 150px; margin: 10px 10px 20px 10px;" runat="server">
            <div id="checkboxHider" runat="server">
                <asp:Label ID="Label2" runat="server" Text="2 - Select a vehicule"></asp:Label>
                    <div style="margin: 10px 10px 18px 10px;">
                        <asp:CheckBox ID="SmallCarCheck" runat="server" Text="Small car" style="margin-bottom:6px;" 
                            AutoPostBack="True" Checked="True" 
                            oncheckedchanged="CustomersTabPrivateRadioCheckedChanged"  /></br>
                        <asp:CheckBox ID="LargeCarCheck" runat="server" style="margin-top:6px;" text="Large car"
                            AutoPostBack="True" Checked="True" 
                            oncheckedchanged="CustomersTabPrivateRadioCheckedChanged" />
                  </div>
                  </div>
                <div>
                    <asp:ListBox ID="vehicilesListBox" runat="server" Height="150px" Width="150px" AutoPostBack="True"
                        />
                </div>
            </div>
            
                <asp:Label style=" float: left;" ID="Label5" runat="server" Text="3 - Pick dates"  ></asp:Label><br />
                <div style=" float: left;margin: 0px 0px0px0px;"  runat="server">
                    <asp:Label ID="Label3" runat="server" Text="From" ></asp:Label>
                    <asp:Calendar ID="CalendarFrom" runat="server" Height="100px" Width="130px" style="margin: 10px 20px 0px 10px;"></asp:Calendar>
                </div>
                            <div runat="server" style=" float: left;">
                    <asp:Label ID="Label4" runat="server" Text="To" ></asp:Label>
                    <asp:Calendar ID="CalendarTo" runat="server" Height="100px" Width="130px" style="margin: 10px 20px 0px 10px;"></asp:Calendar>

                </div>

        </div>
            <asp:Button ID="Button1" runat="server" Text="Submit" onclick="SubmitContract" />
        <div>

        </div>

    </div>
        <div>
        <asp:Label ID="Label6" runat="server" Text="Contracts list:"></asp:Label>
            <div style="margin: 60px 20px20px20px;" align="center" >
            
                                        <asp:GridView ID="GridView1" runat="server" 
                                    onselectedindexchanged="GridView1_SelectedIndexChanged" Width="685px">
                                </asp:GridView>
        </div>
        </div>
</asp:Content>
