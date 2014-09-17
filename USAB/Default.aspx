<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container">
        <div id="topcontainer">
            <div id="slides">
                <div class="slides_container">
                  <%--  <div>
                        <img src="Images/MikePanUSABWarriors.jpg" height="300" alt="Slide 1" width="1077" />
                    </div>
                    <div>
                        <img src="Images/USABWarriorsTeamA.jpg" width="1077" height="300" alt="Slide 2" />
                    </div>
                    <div>
                        <img src="Images/USABwarriorsU18.jpg" width="1077" height="300" alt="Slide 3" />
                    </div>
                    <div>
                        <img src="Images/USABwarriorsU18.jpg" width="1077" height="300"
                            alt="Slide 6" />
                    </div>
                    <div>
                        <img src="Images/USABwarriorsU18.jpg" width="1077" height="300"
                            alt="Slide 7" />
                    </div>--%>
                </div>
            </div>
            <div id="headlinesNews" style="margin-top: 50px">
                <div id="espnHeadlinesNBA">

                    <div class="modal"><!-- Place at bottom of page --></div>
                </div>
               
            </div>
        </div>
        <div style="clear: both;"></div>
        <div id="bodycontent">
            <div class="leftcontent">
                <div class="News_Content">
                    <div id="News_Header">
                    </div>
                    <%--<div id="accordion">
                        <h3>Section 1</h3>
                        <div>
                            <p>
                                Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer
    ut neque. Vivamus nisi metus, molestie vel, gravida in, condimentum sit
    amet, nunc. Nam a nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut
    odio. Curabitur malesuada. Vestibulum a velit eu ante scelerisque vulputate.
                            </p>
                        </div>
                        <h3>Section 2</h3>
                        <div>
                            <p>
                                Sed non urna. Donec et ante. Phasellus eu ligula. Vestibulum sit amet
    purus. Vivamus hendrerit, dolor at aliquet laoreet, mauris turpis porttitor
    velit, faucibus interdum tellus libero ac justo. Vivamus non quam. In
    suscipit faucibus urna.
                            </p>
                        </div>
                        <h3>Section 3</h3>
                        <div>
                            <p>
                                Nam enim risus, molestie et, porta ac, aliquam ac, risus. Quisque lobortis.
    Phasellus pellentesque purus in massa. Aenean in pede. Phasellus ac libero
    ac tellus pellentesque semper. Sed ac felis. Sed commodo, magna quis
    lacinia ornare, quam ante aliquam nisi, eu iaculis leo purus venenatis dui.
                            </p>
                            <ul>
                                <li>List item one</li>
                                <li>List item two</li>
                                <li>List item three</li>
                            </ul>
                        </div>
                        <h3>Section 4</h3>
                        <div>
                            <p>
                                Cras dictum. Pellentesque habitant morbi tristique senectus et netus
    et malesuada fames ac turpis egestas. Vestibulum ante ipsum primis in
    faucibus orci luctus et ultrices posuere cubilia Curae; Aenean lacinia
    mauris vel est.
                            </p>
                            <p>
                                Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus.
    Class aptent taciti sociosqu ad litora torquent per conubia nostra, per
    inceptos himenaeos.
                            </p>
                        </div>
                    </div>--%>


                </div>
            </div>
            <div class="rightcontent">
                <div class="calendarcontent">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Calendar ID="Calendar1" runat="server" CellPadding="1" DayNameFormat="Shortest"
                                BackColor="#FFFFFF" Height="200px" Width="390px" ForeColor="#000000" Font-Names="Verdana">
                                <DayHeaderStyle ForeColor="#FFFFFF" Height="1px" BackColor="#000000" />
                                <NextPrevStyle ForeColor="#CCCCFF" Font-Size="8pt" />
                                <OtherMonthDayStyle ForeColor="#666666" />
                                <SelectedDayStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="Silver" ForeColor="White" />
                                <TitleStyle CssClass="TitleStye" BackColor="#2E4D7B" BorderColor="White" BorderWidth="1px"
                                    Font-Size="10pt" ForeColor="White" Height="25px" />
                                <TodayDayStyle BackColor="Silver" ForeColor="White" />
                            </asp:Calendar>
                            <asp:FormView ID="FormView1" runat="server">
                            </asp:FormView>
                            <div class="ResultCalendar">
                                <asp:Label runat="server" ID="selectedDatelabel" CssClass="SelectedTodayCSS"></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="NoGameLabel" CssClass="SelectDateDBCSS"></asp:Label>
                                <br />
                                <asp:Repeater ID="RepeatCalendar" runat="server">
                                    <ItemTemplate>
                                        <br />
                                        <label class="SelectDateDBCSS">
                                            <%#Eval("game_practice") %>
                                        </label>
                                        <br />
                                        <label class="SelectDateDBCSS">
                                            <%#Eval("team_level")%>
                                        </label>
                                        <br />
                                        <label class="SelectDateDBCSS">
                                            <%#Eval("Time")%>
                                        </label>
                                        <br />
                                        <label class="SelectDateDBCSS">
                                            <%#Eval("Location")%></label>
                                        <br />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblEmpty" CssClass="SelectDateDBCSS" runat="server" Visible='<%#bool.Parse((RepeatCalendar.Items.Count==0).ToString())%>'>
                                    No Games and Practices
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- end .container -->
    </div>
</asp:Content>

