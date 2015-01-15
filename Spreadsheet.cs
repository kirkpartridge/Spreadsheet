using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

// Kirk Partridge
namespace SS
{
    /// <summary>
    /// 
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        //Track cell names to cells.
        private Dictionary<String, Cell> sheet;
        //Track cell dependencies
        private DependencyGraph graph;
        private bool changed;



        /// <summary>
        /// Creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet()
            : base(s => true, s => s, "default")
        {
            sheet = new Dictionary<String, Cell>();
            graph = new DependencyGraph();
            changed = false;

        }

        /// <summary>
        /// Creates an empty spreadsheet.
        /// </summary>
        public Spreadsheet(Func<String, bool> isValid, Func<String, String> Normalize, String Version)
            : base(isValid, Normalize, Version)
        {
            sheet = new Dictionary<String, Cell>();
            graph = new DependencyGraph();
            changed = false;
        }

        /// <summary>
        /// Reads in a spreadsheet from a previously saved version.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="isValid"></param>
        /// <param name="Normalize"></param>
        /// <param name="Version"></param>
        public Spreadsheet(String filename, Func<String, bool> isValid, Func<String, String> Normalize, String Version)
            : base(isValid, Normalize, Version)
        {
            sheet = new Dictionary<String, Cell>();
            graph = new DependencyGraph();

            String version = "";
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    reader.Read();
                    reader.Read();
                    version = reader.GetAttribute("version");
                    if (!(version.Equals(Version)))
                        throw new SpreadsheetReadWriteException("Versions do not match");
                    while (reader.Read())
                    {
                        reader.Read();
                        reader.Read();
                        String cellName = reader.Value;
                        if (cellName.Equals(""))
                            break;
                        reader.Read();
                        reader.Read();
                        reader.Read();
                        String cellContents = reader.Value;
                        reader.Read();
                        reader.Read();
                        SetContentsOfCell(cellName, cellContents);
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("V"))
                    throw new SpreadsheetReadWriteException("New Version (" + Version + ") does not match the old Version (" + version + ")");
                if (e is InvalidNameException)
                    throw new SpreadsheetReadWriteException("The new IsValid Function caught an invald cell name");
                if (e is CircularException)
                    throw new SpreadsheetReadWriteException("Circular Exception caught");
                throw new SpreadsheetReadWriteException("There was an error reading in " + filename);
            }
            changed = false;
        }
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            HashSet<String> nonEmptyCells = new HashSet<String>();
            foreach (String s in sheet.Keys)
            {
                Cell current = sheet[s];

                nonEmptyCells.Add(s);
            }
            return nonEmptyCells;
        }


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        ///</summary>
        public override object GetCellContents(String name)
        {

            if (String.IsNullOrEmpty(name))
                throw new InvalidNameException();
            //Formula.isVariable wont work.  Formula Constructor will
            //throw the error if the varible check fails.
            try
            {
                Formula test = new Formula(name, base.Normalize, base.IsValid);
            }
            catch (Exception)
            {
                throw new InvalidNameException();
            }
            if (sheet.Keys.Contains(base.Normalize(name)))
            {
                Cell current = sheet[base.Normalize(name)];
                return current.ToString();
            }
            return "";
        }

        /// <summary>
        /// Sets the contents of a cell.  Determines if the contents are a string, double, or a formula
        /// and calls the apropriate set function.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            changed = true;

            if (content == null)
                throw new ArgumentNullException();
            // If content is an empty string, don't add the cell.
            if (content.Equals(""))
            {
                sheet.Remove(base.Normalize(name));
                HashSet<String> toBeReturned = new HashSet<string>();
                toBeReturned.Add(name);
                return toBeReturned;
            }
            double i = 0;
            String contents = content.Trim();
            if (Double.TryParse(content, out i))
            {
                return SetCellContents(base.Normalize(name), i);
            }
            else if (contents.StartsWith("="))
            {
                String trimmedContents = contents.Trim().ToUpper();
                if (trimmedContents.StartsWith("=SUM"))
                {
                    try
                    {

                        //Get the length of the each cell name, 2 or 3
                        int length1 = trimmedContents.IndexOf(":") - trimmedContents.IndexOf("(") - 1;
                        int length2 = trimmedContents.IndexOf(")") - trimmedContents.IndexOf(":") - 1;
                        //Get the cell names themselves
                        String cell1 = trimmedContents.Substring(5, length1);
                        String cell2 = trimmedContents.Substring(6 + length1, length2);
                        //Get the Column names A-Z
                        String col1 = cell1.Substring(0, 1);
                        String col2 = cell2.Substring(0, 1);
                        //If they arent in the same column the formula is not valid.
                        if (!(col1.Equals(col2)))
                        {
                            throw new InvalidNameException();
                        }
                        else
                        {
                            //Create the new formula String
                            contents = "=";
                            double row1, row2;
                            double.TryParse(cell1.Substring(1, cell1.Length - 1), out row1);
                            double.TryParse(cell2.Substring(1, cell2.Length - 1), out row2);
                            while (row1 <= row2)
                            {
                                if (row1 == row2)
                                {
                                    contents += col1 + row1;
                                    break;
                                }
                                contents += col1 + row1 + "+";
                                row1++;
                            }
                        }
                    }

                    catch (Exception)
                    {
                        throw new InvalidNameException();
                    }
                }


                contents = contents.Substring(1);
                return SetCellContents(base.Normalize(name), new Formula(contents, base.Normalize, base.IsValid));

            }
            else
            {
                return SetCellContents(base.Normalize(name), content);
            }
        }


        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, double number)
        {
            if (String.IsNullOrEmpty(name))
                throw new InvalidNameException();
            //Formula.isVariable wont work.  Formula Constructor will
            //throw the error if the varible check fails.
            try
            {
                Formula test = new Formula(name, base.Normalize, base.IsValid);
            }
            catch (Exception)
            {
                changed = false;
                throw new InvalidNameException();

            }
            if (sheet.Keys.Contains(name))
            {
                sheet.Remove(name);
            }

            Cell current = new Cell(name, number);
            sheet.Add(name, current);
            HashSet<String> toBeReturned = new HashSet<String>();
            foreach (String s in GetCellsToRecalculate(name))
            {
                Cell toRecalculate = sheet[s];
                toRecalculate.Recalculate(CellLookup);
                toBeReturned.Add(s);
            }


            return toBeReturned;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, String text)
        {

            if (String.IsNullOrEmpty(name))
                throw new InvalidNameException();
            if (text == null)
                throw new ArgumentNullException();
            //Formula.isVariable wont work.  Formula Constructor will
            //throw the error if the varible check fails.
            try
            {
                Formula test = new Formula(name, base.Normalize, base.IsValid);
            }
            catch (Exception)
            {
                changed = false;
                throw new InvalidNameException();
            }
            //If the cell already contains something, remove it.
            if (sheet.Keys.Contains(name))
            {
                sheet.Remove(name);
            }
            Cell current = new Cell(name, text);
            sheet.Add(name, current);
            HashSet<String> toBeReturned = new HashSet<String>();
            foreach (String s in GetCellsToRecalculate(name))
            {
                current.Recalculate(CellLookup);
                toBeReturned.Add(s);
            }


            return toBeReturned;

        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(name))
                throw new InvalidNameException();
            try
            {
                Formula test = new Formula(name, base.Normalize, base.IsValid);
            }
            catch (FormulaFormatException)
            {
                changed = false;
                throw new InvalidNameException();
            }
            //Save current cell contents if any.
            Object CurrentCellContents = GetCellContents(name);
            if (sheet.Keys.Contains(name))
            {
                sheet.Remove(name);
            }

            Cell current = new Cell(name, formula);
            sheet.Add(name, current);

            foreach (String t in formula.GetVariables())
            {
                graph.AddDependency(t, name);
            }
            HashSet<String> toBeReturned = new HashSet<String>();
            try
            {
                foreach (String s in GetCellsToRecalculate(name))
                {
                    current.Recalculate(CellLookup);
                    toBeReturned.Add(s);
                }
            }
            // If circular dependency thrown, revert cell contents
            // to original conentents.
            catch (CircularException)
            {
                changed = false;
                HashSet<String> dependees = new HashSet<String>();
                //Remove dependees added by the circular exception cell
                graph.ReplaceDependees(name, dependees);
                //Re input cell that was there before.
                if (CurrentCellContents is Formula)
                    SetCellContents(name, (Formula)CurrentCellContents);
                if (CurrentCellContents is String)
                    SetCellContents(name, (String)CurrentCellContents);
                if (CurrentCellContents is double)
                    SetCellContents(name, (double)CurrentCellContents);
                throw new CircularException();

            }
            return toBeReturned;
        }


        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new InvalidNameException();
            try
            {
                Formula test = new Formula(name);
            }
            catch (Exception)
            {
                throw new InvalidNameException();
            }
            return graph.GetDependents(name);
        }

        /// <summary>
        /// Lookup function for a cell
        /// returns the cells value that was calculated
        /// in the cell class method "Recalculate"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected double CellLookup(String name)
        {
            return (double)sheet[name].getValue();
        }



        /// <summary>
        /// Returns whether or not the spreadsheet has been changed
        /// since last save.
        /// </summary>
        public override bool Changed
        {
            get
            {
                return changed;
            }
            protected set
            {

            }
        }

        /// <summary>
        /// Reads the version information from a saved spreadsheet.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    reader.Read();
                    reader.Read();
                    return reader.GetAttribute("version");
                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("The filename " + filename + " is not valid");
            }
        }

        /// <summary>
        /// Saves the spreadsheet to an xml file to be used later.
        /// throws a SpreadSheetReadAndWrite error if an exception is encountered. 
        /// </summary>
        /// <param name="filename"></param>
        public override void Save(String filename)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(filename))
                {
                    writer.WriteStartElement("spreedsheet");
                    writer.WriteAttributeString("version", base.Version);
                    foreach (Cell current in sheet.Values)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteStartElement("name");
                        writer.WriteString(current.getName());
                        writer.WriteEndElement();
                        writer.WriteStartElement("contents");
                        writer.WriteString(current.ToString());
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndDocument();
                }
            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("The filename " + filename + " is not valid");
            }

        }

        /// <summary>
        /// Returns the value of a cell.  Is either a String, double, or FormulaError
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override object GetCellValue(string name)
        {
            String normalizedName = base.Normalize(name);
            if (!(sheet.ContainsKey(normalizedName)))
                return "";
            Cell current = sheet[normalizedName];
            current.Recalculate(CellLookup);
            if (current.getValue() is FormulaError)
            {
                FormulaError error = (FormulaError)current.getValue();
                return error.Reason;
            }
            return current.getValue();
        }
        /// <summary>
        /// Removes the cell from the spreadhsset.
        /// </summary>
        /// <param name="cellName"></param>
        public void removeCell(String cellName)
        {
            sheet.Remove(cellName);
        }





        /// <summary>
        /// Private class that represents a cell.
        /// Has a name, contents, and a value
        /// name can be a String, Double, or a Formula
        /// 
        /// </summary>
        private class Cell
        {

            String name;
            object contents;
            object value;

            /// <summary>
            /// Creates a cell
            /// </summary>
            /// <param name="name"></param>
            /// <param name="contents"></param>
            public Cell(String name, object contents)
            {
                this.contents = contents;
                this.name = name;
            }
            /// <summary>
            /// Returns the name of a cell.
            /// </summary>
            /// <returns></returns>
            public String getName()
            {
                return this.name;
            }
            /// <summary>
            /// Returns the contents of the cell.
            /// </summary>
            /// <returns></returns>
            public object getContents()
            {
                return contents;
            }
            /// <summary>
            /// Sets the value of a cell.
            /// </summary>
            /// <param name="value"></param>
            public void setValue(Object value)
            {
                this.value = value;
            }
            /// <summary>
            /// Returns the value of a cell.
            /// </summary>
            /// <returns></returns>
            public Object getValue()
            {
                return this.value;
            }
            /// <summary>
            /// Returns a string representation of a cells contents
            /// String - returns the string
            /// double - returns the double
            /// Formula - adds an '=' then the formula
            /// </summary>
            /// <returns></returns>
            public override String ToString()
            {
                if (this.contents is double)
                {
                    return contents.ToString();
                }
                if (this.contents is String)
                {
                    return this.contents.ToString();
                }
                if (this.value is FormulaError)
                {
                    return this.contents.ToString().Insert(0, "=");
                }
                else
                {
                    Formula current = (Formula)this.contents;
                    return current.ToString().Insert(0, "=");
                }
            }
            /// <summary>
            /// Sets the value of the cell to either a String, double, or formulaError
            /// </summary>
            /// <param name="lookup"></param>
            public void Recalculate(Func<String, double> lookup)
            {
                if (this.contents is double)
                {
                    this.value = this.contents;
                    return;
                }
                if (this.contents is String)
                {
                    this.value = this.contents;
                    return;
                }
                else
                {

                    Formula current = (Formula)this.contents;
                    this.value = current.Evaluate(lookup);
                }
            }




        }
    }
}

