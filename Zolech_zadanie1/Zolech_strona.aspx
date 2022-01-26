<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zolech_strona.aspx.cs" Inherits="Zolech_zadanie1.Zolech_strona" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link rel="stylesheet" href="StyleZolech.css" />
    <title></title>

    <style type="text/css">
        .central {}
    </style>

</head>
<body >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:Panel runat="server" ID="MZ_LoginPanel" class="LoginPanel" CssClass="central" Width="308px">
            <h1>Podaj imię i nazwisko</h1>
            <asp:TextBox CssClass="textbox" ID="MZ_Identyfikator" runat="server" placeholder="Imie Nazwisko" AutoPostBack="true" onTextChanged="walidacja" Width="297px"/>
            <asp:CustomValidator class="MZ_LoginContent" runat="server" ControlToValidate="MZ_Identyfikator" Text="*" ErrorMessage="Użytkownik już zalogowany!" Display="Dynamic" OnServerValidate="czyZalogowany"/>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="MZ_Identyfikator" Text="*" ErrorMessage="Nie podano identyfikatora" Display="Dynamic"/>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="MZ_Identyfikator" ValidationExpression="^([A-Z]|[ÓĄĘŚĆŻŹŃŁ])([a-z]|[óąęśćżźńł]){2,} ([A-Z]|[ÓĄĘŚĆŻŹŃŁ])([a-z]|[óąęśćżźńł]){2,}$" Text="*" ErrorMessage="Identyfikator nie jest zgodny ze wzorcem" Display="Dynamic"/>  <br />
            <asp:Button ID="MZ_Loginutton" runat="server" Text="Zarejestruj" OnClick="Zarejestruj" Width="96px"/><br />
            <asp:ValidationSummary runat="server" DisplayMode="SingleParagraph"/>
            
        </asp:Panel>
       
    <asp:Panel runat="server" ID="MZ_Content" Visible="false" Height="100%">
    <!-- MAIN MODUL Z LEWEJ  -->
    <div id="MainContainer" class="data_container" >
        <div class="Main_Upper_Square"> 
            <asp:ImageButton ID="ImageButton1"  CssClass="Visibility" Visible="false"  ImageUrl="smile.jpg" runat="server"  /></div>

        <!-- Srodek pasek -->
        <div class="Main_Middle_Square"> 
            <!-- Kwadraty w srodku paska -->
            <div class="Main_Middle_Middle_Square" > 
                <asp:ImageButton ID="ImageButton2"  CssClass="Visibility" Visible="false"  ImageUrl="smile.jpg" runat="server"  />
            </div>
            <div class="Main_Middle_Right_Square"> 

                <asp:ImageButton ID="ImageButton3"  CssClass="Visibility" Visible="false"  ImageUrl="smile.jpg" runat="server" />
            </div>
            <div class="Main_Middle_Left_Square"> 
                <asp:ImageButton ID="ImageButton4"  CssClass="showimg100" Visible="true"  ImageUrl="smile.jpg" runat="server"  />
            </div>
        </div>
       
        <div class="Main_Down_Square"> 
            <asp:ImageButton ID="ImageButton5"  CssClass="Visibility" Visible="false"  ImageUrl="smile.jpg" runat="server" />
        </div>


    </div>
    <!-- PANEL STERUJACY -->
    <div id="SteerContainer" class="data-container">
        <asp:TextBox ID="TextBox1" runat="server" BackColor="#FF66CC" BorderColor="Black" Width="358px">Określ rozmiar rysunku</asp:TextBox>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="radiolist" AutoPostBack="true" OnSelectedIndexChanged="ZmianaRozmiaru">
                                <asp:ListItem id="MZ_radio_maly" runat="server" Selected="True" Text="Mały"/>
                                <asp:ListItem id="MZ_radio_sredni" runat="server" Text="Średni"/>
                                <asp:ListItem id="MZ_radio_duzy" runat="server" Text="Duży"/>
                            </asp:RadioButtonList>
        <asp:TextBox ID="TextBox2" runat="server" BackColor="#66FF66"  Width="354px">Ilosc przelaczen rysunkow</asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="0" Width="496px"></asp:Label>
         <asp:TextBox ID="TextBox3" runat="server" BackColor="#CC9900" Width="343px">Ilosc zmian rozmiarow rysunku</asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="0" Width="497px"></asp:Label>
        <div style="height: 342px" class="DataDown"> 
            <div class="controls">
                                <asp:Timer runat="server" Enabled="true" Interval="5000" ID="timer" onTick="update"/>
                                <asp:Label runat="server" ID="MZ_zalogowany_uzytkownik" CssClass="logged_label"/><br />
                                <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="timer" EventName="Tick" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DetailsView ID="zalogowany" runat="server" DataSourceID="MZ_SQLDataSourceLogged"/>
                                        <asp:SqlDataSource ID="MZ_SQLDataSourceLogged" runat="server" CancelSelectOnNullParameter="true"
                                            ConnectionString="<%$ ConnectionStrings:MZ_DB %>"
                                            SelectCommand="SELECT  
                                                            format(czas_rejestracji, 'dd.MM.yyyy') as 'Data rejestracji', 
                                                            format(czas_rejestracji, 'HH:mm:ss') as 'Czas rejestracji', 
                                                            zmiany_rozmiaru as 'Zmiany rozmiaru', 
                                                            zmiany_polozenia as 'Zmiany położenia', 
                                                            (SELECT COUNT(*) from users) AS 'Liczba zalogowanych',
                                                            (SELECT COUNT(*) from users where zalogowany = 1) AS 'Liczba aplikacji'
                                                            FROM users where Id = @MZ_id">
                                            <SelectParameters>
                                                <asp:ControlParameter Name="MZ_id" ControlID="MZ_zalogowany_uzytkownik" PropertyName="Text" /> 
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        
                                        
                                        <asp:DropDownList ID="MZ_lista" runat="server" AutoPostBack="true" OnSelectedIndexChanged="wybierzUzytkownika"/>
                                        
                                        
                                        <asp:DetailsView ID="MZ_DaneUzytkownika" runat="server" DataSourceID="MZ_SQLDataSource"/>
                                        <asp:SqlDataSource ID="MZ_SQLDataSource" runat="server" CancelSelectOnNullParameter="true"
                                            ConnectionString="<%$ ConnectionStrings:MZ_DB %>"
                                            SelectCommand="SELECT 
                                                            format(czas_rejestracji, 'dd.MM.yyyy') as 'Data rejestracji', 
                                                            format(czas_rejestracji, 'HH:mm:ss') as 'Czas rejestracji', 
                                                            zmiany_rozmiaru as 'Zmiany rozmiaru', 
                                                            zmiany_polozenia as 'Zmiany położenia', 
                                                            (SELECT COUNT(*) from users) AS 'Liczba zalogowanych',
                                                             (SELECT COUNT(*) from users where zalogowany = 1) AS 'Liczba aplikacji'
                                                            FROM users where Id = @MZ_id AND Id != @MZ_zal">
                                            <SelectParameters>
                                                <asp:ControlParameter Name="MZ_id" ControlID="MZ_lista" PropertyName="SelectedValue" /> 
                                                <asp:ControlParameter Name="MZ_zal" ControlID="MZ_zalogowany_uzytkownik" PropertyName="Text" /> 
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>              
                                <br/><br/>
                                <asp:Button runat="server" ID="MZ_usun" OnClick="usuwanie" Text="WYLOGUJ"/>
            </div>
           
        </div>
         
        
        
        </div>
        
         </asp:Panel>
    </form>
</body>
</html>
