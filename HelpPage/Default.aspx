<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelpPage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Spreadsheet Help</h2>
    <h3>Provides help with how to use the Spreadsheet Application</h3>
    <p>
    <asp:Table runat="server" CellPadding="10" GridLines="Both" >
            <asp:TableHeaderRow id="Table1HeaderRow" 
            BackColor="LightBlue"
            runat="server">
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Functions" />
            <asp:TableHeaderCell  
                Scope="Column" 
                Text="Description" />
            <asp:TableHeaderCell 
                Scope="Column" 
                Text="Examples" />
                <asp:TableHeaderCell 
                Scope="Column" 
                Text="Images" />
        </asp:TableHeaderRow>    
            <asp:TableRow>
                <asp:TableCell>
                    Set Cell Contents to a Number: 
                </asp:TableCell>
                <asp:TableCell>
                    To set a cells contents to a Number, click on the cell whose contents you would like to change.  Once the desired cell
                    is bolded, click in the "Set Contents" box above.  Type in the desired number for the cell and hit enter when finished.
                    If Enter is not pressed, nothing is done.
                </asp:TableCell>
                <asp:TableCell>
                     '12', '1', '-1', '1234.56'
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Image runat="server" ImageUrl="~/Images/setNumber.PNG" />
                </asp:TableCell>
                </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Set Cell Contents to a String:
                </asp:TableCell>
                <asp:TableCell>
                    To set a cells contents to a String, click on the cell whose contents you would like to change.  Once the desired cell
                    is bolded, click in the "Set Contents" box above.  Type in the desired string for the cell and hit enter when finished.
                    If Enter is not pressed, nothing is done.
                </asp:TableCell>
                <asp:TableCell>
                    'Hello', 'hello', 'This is a cell'
                </asp:TableCell>
                <asp:TableCell>
                   <asp:Image runat="server" ImageUrl="~/Images/setString.PNG" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Set Cell Contents to a Formula:
                </asp:TableCell>
                <asp:TableCell>
                    To set a cells contents to a Formula, click on the cell whose contents you would like to change.  Once the desired cell
                    is bolded, click in the "Set Contents" box above.  Type in the desired Formula for the cell with an '=' sign at the start and hit enter when finished.
                    If Enter is not pressed, nothing is done.
                </asp:TableCell>
                <asp:TableCell>
                     '=A1', '=A1 + B1', '=A1 - 2', 'C1 * 50'
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Image runat="server" ImageUrl="~/Images/setFormulas.PNG" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Sum a Series of Cells:
                </asp:TableCell>
                <asp:TableCell>
                    To Sum a series of cells, select the cell where you would like the sum amount to be displayed.  Once this cell is highlighted, use the "Set Contents" box to input
                    "=SUM(Starting cell:Ending cell)" The cells must be in the same column. 
                </asp:TableCell>
                <asp:TableCell>
                     '=SUM(A1:A3)', '=SUM(B5:B7)', '=SUM(Z12:Z45)'
                </asp:TableCell>
                <asp:TableCell>
                   <asp:Image runat="server" ImageUrl="~/Images/setSums.PNG" />
                </asp:TableCell>
                </asp:TableRow>
        <asp:TableRow>
                <asp:TableCell>
                    Create a Bar Graph:
                </asp:TableCell>
                <asp:TableCell>
                   To create a bar graph, place the data in the columns A and B starting at A1 and B1 (No other columns will populate the graph).
                    Place the data as pairs, i.e. the data in A1 will be compared to the data in B1.
                    Be sure to place the same amount of data in each column.  When completed,
                    press the "Graph BarGraph" button and a bar graph will be created.

                    To close the bar graph, just click the "Close Bar Graph" button at the top.
                </asp:TableCell>
                <asp:TableCell>
                    A1 = 100, A2 = 150, A3 = 175, A4 = 200
                    B1 = 200, B2 = 175, B3 = 150, B4 = 100
                </asp:TableCell>
                <asp:TableCell>
                   <asp:Image runat="server" ImageUrl="~/Images/setGraph.PNG" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </p>
</asp:Content>


