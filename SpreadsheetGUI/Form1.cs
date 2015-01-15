using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//KIRK PARTRIDGE
namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        //Spreadsheet for the GUI
        private AbstractSpreadsheet spreadsheet;
        //Maps Column numbers to Letters
        private Dictionary<int, char> cellNames;
        //Maps column Letters to numbers.
        private Dictionary<char, int> reversedCellNames;

        /// <summary>
        /// Populates the dictionaries that map
        /// numbers to letters and vice versa.
        /// </summary>
        private void PopulateDictionary()
        {
            cellNames = new Dictionary<int, char>();
            int i = 0;
            for (char c = 'A'; c <= 'Z'; ++c)
            {
                cellNames[i++] = c;
            }
            reversedCellNames = cellNames.ToDictionary(x => x.Value, x => x.Key);
        }

        /// <summary>
        /// Constructor for the Spreadsheet
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // This an example of registering a method so that it is notified when
            // an event happens.  The SelectionChanged event is declared with a
            // delegate that specifies that all methods that register with it must
            // take a SpreadsheetPanel as its parameter and return nothing.  So we
            // register the displaySelection method below.

            // This could also be done graphically in the designer, as has been
            // demonstrated in class.
            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(0, 0);
            spreadsheet = new Spreadsheet(isValid, x => x.ToUpper(), "ps6");
            PopulateDictionary();
            displaySelection(spreadsheetPanel1);
            this.cellContentsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);
        }


        /// <summary>
        /// Check that the cell name is a valid cell name.
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        private bool isValid(String cellName)
        {
            if (cellName.Length > 3)
                return false;
            String subString = cellName.Substring(1);
            double cellRow;
            if (double.TryParse(subString, out cellRow))
                return (cellRow < 100 && cellRow > 0);
            return false;
        }


        /// <summary>
        /// Every time the selection changes, this method is called with the
        //  Spreadsheet as its parameter.  
        /// </summary>
        /// <param name="ss"></param>
        private void displaySelection(SpreadsheetPanel ss)
        {
            int row, col;

            ss.GetSelection(out col, out row);
            int newRow = row + 1;
            String cellName = cellNames[col] + newRow.ToString();
            //Set the contents box to the current cell contents
            cellContentsBox.Text = spreadsheet.GetCellContents(cellName).ToString();
            //Display the current cell selections value
            cellValueBox.Text = spreadsheet.GetCellValue(cellName).ToString();


            ss.SetValue(col, row, spreadsheet.GetCellValue(cellName).ToString());
            //Set the cellname box to selected cell name
            cellNameBox.Text = cellName;
        }

        /// <summary>
        /// Sets the cell selection to the next row
        /// Allows for faster data input.
        /// </summary>
        /// <param name="ss"></param>
        private void nextCell(SpreadsheetPanel ss)
        {
            int row, col;

            ss.GetSelection(out col, out row);
            ss.SetSelection(col, row + 1);

        }
        /// <summary>
        /// Converts the selected cell to be
        /// formatted into A-Z, 1-100
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void cellNameToNumbers(String cellName, out int row, out int col)
        {
            char[] name = cellName.ToCharArray();
            col = reversedCellNames[name[0]];
            String rowString = "";
            for (int i = 1; i < name.Length; i++)
            {

                rowString += name[i];

            }
            int.TryParse(rowString, out row);
            row = row - 1;
        }

        /// <summary>
        /// Updates the displays of the value of the cells.
        /// Used primarily with setContentsOfCell, which returns
        /// the affected cells.
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="cells"></param>
        private void UpdateCellDisplays(SpreadsheetPanel ss, IEnumerable<String> cells)
        {
            foreach (String cellName in cells)
            {
                int row;
                int col;
                cellNameToNumbers(cellName, out row, out col);
                ss.SetValue(col, row, spreadsheet.GetCellValue(cellName).ToString());
            }
        }


        /// <summary>
        /// Event handler for the close option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            if (spreadsheet.Changed == true)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                result = MessageBox.Show("Would you like to save before closing?", "Spreadsheet has been modified", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                    SaveFileDialog1.Filter = "Spreadsheet Files (.sprd)|*.sprd|All Files (*.*)|*.*";
                    SaveFileDialog1.CreatePrompt = true;
                    SaveFileDialog1.OverwritePrompt = true;
                    SaveFileDialog1.ShowDialog();
                    String filename = SaveFileDialog1.FileName;
                    spreadsheet.Save(filename);
                    Close();
                }
                else if (result == DialogResult.No)
                {
                    Close();
                }
                else
                    return;
            }
            Close();
        }

        /// <summary>
        /// Creates another window with a spreadsheet in it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Tell the application context to run the form on the same
            // thread as the other forms.
            SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }

        /// <summary>
        /// Checks the key that was pressed when the Cell Contents
        /// box is active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //If it is the enter key...
            if (e.KeyChar == (char)13)
            {
                //Save the current contents.
                object currentContents = spreadsheet.GetCellContents(cellNameBox.Text);
                String newContents = cellContentsBox.Text;
                String trimmedNewContents = newContents.Trim().ToUpper();
                try
                {
                    //Update the cells that are effected by this change.
                    UpdateCellDisplays(spreadsheetPanel1, spreadsheet.SetContentsOfCell(cellNameBox.Text, newContents));
                }
                catch (Exception)
                {
                    MessageBox.Show("The formula " + newContents + " is not a valid formula", "Formula Error");
                    UpdateCellDisplays(spreadsheetPanel1, spreadsheet.SetContentsOfCell(cellNameBox.Text, currentContents.ToString()));
                }

                cellValueBox.Text = spreadsheet.GetCellValue(cellNameBox.Text).ToString();
                UpdateCellDisplays(spreadsheetPanel1, spreadsheet.GetNamesOfAllNonemptyCells());
                //Move the spreadsheetPanel to the next cell
                nextCell(spreadsheetPanel1);
                displaySelection(spreadsheetPanel1);
            }
        }



        /// <summary>
        /// Event handler for the save option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.Filter = "Spreadsheet Files (.sprd)|*.sprd|All Files (*.*)|*.*";
                SaveFileDialog1.CreatePrompt = true;
                SaveFileDialog1.OverwritePrompt = true;
                SaveFileDialog1.ShowDialog();
                String filename = SaveFileDialog1.FileName;
                spreadsheet.Save(filename);
            }
            catch (Exception)
            {
                MessageBox.Show("The file could not be saved", "Save Error");
                return;
            }
            Close();
        }

        /// <summary>
        /// Event handler for the open item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "Spreadsheet Files (.sprd)|*.sprd|All Files (*.*)|*.*";

            OpenFileDialog1.ShowDialog();
            String filename = OpenFileDialog1.FileName;
            try
            {
                //Update the member variable spreadsheet to the newly created one from the file.
                spreadsheet = new Spreadsheet(filename, isValid, s => s.ToUpper(), "ps6");
                //Update the cells to show the new values.
                UpdateCellDisplays(spreadsheetPanel1, spreadsheet.GetNamesOfAllNonemptyCells());
                //Display the current selection upon opening.
                displaySelection(spreadsheetPanel1);
            }
            catch (SpreadsheetReadWriteException)
            {
                MessageBox.Show("The file " + filename + " could not be opened", "File Error");
                return;
            }

        }

        /// <summary>
        /// Help dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String helpMessage = "- Click on a cell to select it." +
                "\n- Type in the “Cell Contents” field at the top of the window to enter data into the selected cell." +
                "\n- Begin all formulas with “=” (e.g. =2*8, =D1*8)." +
                "\n- To use data from another cell in a formula, type the cell’s address, letter first (e.g. =D1*8)." +
                "\n- For SUM and GRAPHING functions, see the webpage." +
                "\n\n Would you like more Information?";

            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(helpMessage, "Help", buttons1, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                //Opens the help webpage.
                Process.Start("http://localhost:1291/");
            }

        }


        /// <summary>
        /// Evant Handler for the graph.  Creates a bar graph with the data in the A and B columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphButton_Click(object sender, EventArgs e)
        {
            //If the graph is already there, change the button to say close graph.
            if (graph.Visible == true)
            {
                graph.Visible = false;
                graphButton.Text = "Create BarGraph";
                return;
            }
            try
            {
                //Get the non empty cells in the spreadsheet.
                HashSet<String> nonEmptyCells = new HashSet<string>(spreadsheet.GetNamesOfAllNonemptyCells());
                Dictionary<double, double> graphMap = new Dictionary<double, double>();
                double i = 0;

                foreach (String s in nonEmptyCells)
                {
                    //If the cell starts with A, map it to its B counterpart.
                    if (s.StartsWith("A"))
                    {
                        int value;

                        int.TryParse(s.Substring(1), out value);
                        //If the same key already exists, change it slightly by adding i.
                        if (graphMap.Keys.Contains((double)spreadsheet.GetCellValue(s)))
                        {
                            i += .001;
                            graphMap.Add((double)spreadsheet.GetCellValue(s) + i, (double)spreadsheet.GetCellValue("B" + value));

                            continue;
                        }
                        graphMap.Add((double)spreadsheet.GetCellValue(s), (double)spreadsheet.GetCellValue("B" + value));

                    }
                }
                //Clear the series if there is any
                graph.Series.Clear();
                graph.DataBindTable(graphMap);
                //Show the graph.
                graph.Visible = true;
                graphButton.Text = "Close Graph";
            }
            catch (Exception)
            {
                MessageBoxButtons buttons2 = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("The Data was not inputted correctly.  Would you like to visit the Help page to see a graphing example?", "Input Error", buttons2);
                if (result == DialogResult.Yes)
                {
                    //Opens help webpage.
                    Process.Start("http://localhost:1291/");
                }

            }

        }

    }
}

