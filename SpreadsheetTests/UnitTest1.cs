using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using System.Collections.Generic;
using SpreadsheetUtilities;

// Kirk Partridge
namespace GradingTests
{

    //TESTS 1-50 TAKEN FROM PS4 GRADING TESTS


    /// <summary>
    ///This is a test class for SpreadsheetTest and is intended
    ///to contain all SpreadsheetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpreadsheetTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        // EMPTY SPREADSHEETS
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test1()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test2()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("1AA");
        }

        [TestMethod()]
        public void Test3()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("A2"));
        }

        // SETTING CELL TO A DOUBLE
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test4()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "1.5");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1A1A", "1.5");
        }

        [TestMethod()]
        public void Test6()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "1.5");
            Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        }

        // SETTING CELL TO A STRING
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test7()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", (string)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test8()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "hello");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test9()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", "hello");
        }

        [TestMethod()]
        public void Test10()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "hello");
            Assert.AreEqual("hello", s.GetCellContents("Z7"));
        }

        // SETTING CELL TO A FORMULA
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test11()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", (String)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test12()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, new Formula("2").ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test13()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", new Formula("2").ToString());
        }

        [TestMethod()]
        public void Test14()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "=3");
            Formula f = (Formula)s.GetCellContents("Z7");
            Assert.AreEqual(new Formula("3"), f);
            Assert.AreNotEqual(new Formula("2"), f);
        }

        // CIRCULAR FORMULA DETECTION
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test15()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2");
            s.SetContentsOfCell("A2", "=A1");
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test16()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A3", "=A4+A5");
            s.SetContentsOfCell("A5", "=A6+A7");
            s.SetContentsOfCell("A7", "=A1+A1");
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void Test17()
        {
            Spreadsheet s = new Spreadsheet();
            try
            {
                s.SetContentsOfCell("A1", "=A2+A3");
                s.SetContentsOfCell("A2", "15");
                s.SetContentsOfCell("A3", "30");
                s.SetContentsOfCell("A2", "=A3*A1");
            }
            catch (CircularException e)
            {
                Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                throw e;
            }
        }

        // NONEMPTY CELLS
        [TestMethod()]
        public void Test18()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void Test19()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "");
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void Test20()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test21()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "52.25");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test22()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "=3.5");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test23()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "hello");
            s.SetContentsOfCell("B1", "=3.5");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        }

        // RETURN VALUE OF SET CELL CONTENTS
        [TestMethod()]
        public void Test24()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            s.SetContentsOfCell("C1", "=5");
            Assert.IsTrue(s.SetContentsOfCell("A1", "17.2").SetEquals(new HashSet<string>() { "A1" }));
        }

        [TestMethod()]
        public void Test25()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "=5");
            Assert.IsTrue(s.SetContentsOfCell("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void Test26()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(s.SetContentsOfCell("C1", "=5").SetEquals(new HashSet<string>() { "C1" }));
        }

        [TestMethod()]
        public void Test27()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A2", " 6");
            s.SetContentsOfCell("A3", "=A2+A4");
            s.SetContentsOfCell("A4", "=A2+A5");
            Assert.IsTrue(s.SetContentsOfCell("A5", "82.5").SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
        }

        // CHANGING CELLS
        [TestMethod()]
        public void Test28()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A1", "2.5");
            Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        }

        [TestMethod()]
        public void Test29()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A1", "Hello");
            Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        }

        [TestMethod()]
        public void Test30()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Hello");
            s.SetContentsOfCell("A1", "=23");
            Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
            Assert.AreNotEqual(new Formula("24"), (Formula)s.GetCellContents("A1"));
        }

        // STRESS TESTS
        [TestMethod()]
        public void Test31()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=B1+B2");
            s.SetContentsOfCell("B1", "=C1-C2");
            s.SetContentsOfCell("B2", "=C3*C4");
            s.SetContentsOfCell("C1", "=D1*D2");
            s.SetContentsOfCell("C2", "=D3*D4");
            s.SetContentsOfCell("C3", "=D5*D6");
            s.SetContentsOfCell("C4", "=D7*D8");
            s.SetContentsOfCell("D1", "=E1");
            s.SetContentsOfCell("D2", "=E1");
            s.SetContentsOfCell("D3", "=E1");
            s.SetContentsOfCell("D4", "=E1");
            s.SetContentsOfCell("D5", "=E1");
            s.SetContentsOfCell("D6", "=E1");
            s.SetContentsOfCell("D7", "=E1");
            s.SetContentsOfCell("D8", "=E1");
            ISet<String> cells = s.SetContentsOfCell("E1", "0");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }
        [TestMethod()]
        public void Test32()
        {
            Test31();
        }
        [TestMethod()]
        public void Test33()
        {
            Test31();
        }
        [TestMethod()]
        public void Test34()
        {
            Test31();
        }

        [TestMethod()]
        public void Test35()
        {
            Spreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
            }
        }
        [TestMethod()]
        public void Test36()
        {
            Test35();
        }
        [TestMethod()]
        public void Test37()
        {
            Test35();
        }
        [TestMethod()]
        public void Test38()
        {
            Test35();
        }
        [TestMethod()]
        public void Test39()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }
            try
            {
                s.SetContentsOfCell("A150", "=A50");
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }
        [TestMethod()]
        public void Test40()
        {
            Test39();
        }
        [TestMethod()]
        public void Test41()
        {
            Test39();
        }
        [TestMethod()]
        public void Test42()
        {
            Test39();
        }

        [TestMethod()]
        public void Test47()
        {
            RunRandomizedTest(47, 2519);
        }
        [TestMethod()]
        public void Test48()
        {
            RunRandomizedTest(48, 2521);
        }
        [TestMethod()]
        public void Test49()
        {
            RunRandomizedTest(49, 2526);
        }
        [TestMethod()]
        public void Test50()
        {
            RunRandomizedTest(50, 2521);
        }

        [TestMethod()]
        public void Test51()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            Assert.AreEqual(15.0, (double)s.GetCellValue("C1"), 1e-9);
        }

        [TestMethod()]
        public void Test52()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");
            Assert.AreEqual(45.0, (double)s.GetCellValue("C2"), 1e-9);
        }

        [TestMethod()]
        public void Test53()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save("Test52.xml");
            Assert.AreEqual("default", s.GetSavedVersion("Test52.xml"));
        }

        [TestMethod()]
        public void Test54()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save("Test52.xml");
            Assert.AreEqual("default", s.GetSavedVersion("Test52.xml"));
            Spreadsheet d = new Spreadsheet("Test52.xml", x => true, x => x, "default");
            Assert.IsTrue(new HashSet<String>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<String>(d.GetNamesOfAllNonemptyCells())));
            d.Save("Test52d.xml");
            Assert.IsTrue("default".Equals(d.GetSavedVersion("Test52d.xml")));
        }

        [TestMethod()]
        public void Test55()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");
            Assert.AreEqual(true, s.Changed);
            s.Save("Test52.xml");
            Assert.AreEqual("15", s.GetSavedVersion("Test52.xml"));
            Spreadsheet d = new Spreadsheet("Test52.xml", x => true, x => x, "15");
            Assert.IsTrue(new HashSet<String>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<String>(d.GetNamesOfAllNonemptyCells())));
            d.Save("Test52d.xml");
            Assert.IsTrue("15".Equals(d.GetSavedVersion("Test52d.xml")));
            Assert.AreEqual(false, d.Changed);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test56()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("_A1", "5");



        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test57()
        {
            Spreadsheet s = new Spreadsheet(isValid, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C23", "=C1*3");


        }


        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test58()
        {
            Spreadsheet s = new Spreadsheet(isValid, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save(null);
        }


        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test59()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save("Test52.xml");
            Assert.AreEqual("15", s.GetSavedVersion("Test52.xml"));
            Spreadsheet d = new Spreadsheet("Test52.xml", x => true, x => x, "2.5");

        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test60()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save("");
        }
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test61()
        {
            Spreadsheet s = new Spreadsheet(isValid, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");

            s.Save("test.xml");
            s.GetSavedVersion("123");

        }
        [TestMethod()]

        public void Test62()
        {
            Spreadsheet s = new Spreadsheet(isValid, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C2", "=C1*3");
            s.SetContentsOfCell("C4", "=C2*4");

            Assert.AreEqual(180, (double)s.GetCellValue("C4"), 1e-9);

        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test63()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C23", "=C1*3");

            s.Save("test.xml");
            Spreadsheet d = new Spreadsheet("test.xml", isValid, x => x, "15");


        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void Test64()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A1", "5");
            s.SetContentsOfCell("C1", "=A1*3");
            s.SetContentsOfCell("C23", "=C1*3");

            s.Save("test.xml");
            Spreadsheet d = new Spreadsheet("", isValid, x => x, "15");


        }
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test65()
        {
            Spreadsheet s = new Spreadsheet(x => true, x => x, "15");
            s.SetContentsOfCell("A", "5");


        }

        /// <summary> 
        /// 
        /// Tests the contents are apprpriately set
        /// </summary>
        [TestMethod]
        public void ContentsTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X1", "5.0");
            Assert.AreEqual(5.0, sheet.GetCellContents("X1"));
            Assert.AreNotEqual(1, sheet.GetCellContents("X1"));
        }
        [TestMethod]
        public void ContentsTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X1", "OK");
            Assert.IsTrue("OK".Equals(sheet.GetCellContents("X1")));
        }

        /// <summary>
        /// Tests that dependents are assigned correctly
        /// </summary>
        [TestMethod]
        public void DependentsTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<String> answer = new HashSet<String>();
            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            HashSet<String> test = new HashSet<String>();
            test = (HashSet<String>)sheet.SetContentsOfCell("A1", "5.0");
            answer.Add("A1");
            answer.Add("B1");
            answer.Add("C1");

            Assert.IsTrue(answer.SetEquals(test));
            Assert.IsTrue(answer.SetEquals(sheet.GetNamesOfAllNonemptyCells()));

        }


        /// <summary>
        /// Tests various null/ empty specifications
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidCellNameTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("2X1", "5.0");

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidCellNameTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("2&X1", "OK");

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalideCellNameTest5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X&^", "=X3+4");

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void NullCellNameTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "5.0");

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void NullCellNameTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "OK");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void NullCellNameTest3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "=2+3");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullCellContentsTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A12", (String)null);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullCellContentsTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A12", (String)null);

        }


        /// <summary>
        /// Tests that a circular dependecy is detected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependentsTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "4");
            sheet.SetContentsOfCell("A1", "=B1+3");
        }

        /// <summary>
        /// 
        /// Tests that cell contents are properly overwritten
        /// </summary>
        [TestMethod]

        public void OverwritingCellTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "4");
            sheet.SetContentsOfCell("A1", "5");
            Assert.AreEqual(5.0, (double)sheet.GetCellContents("A1"));
        }

        [TestMethod]

        public void OverwritingCellTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "4");

            try
            {
                sheet.SetContentsOfCell("A1", "=B1");
            }
            catch (CircularException)
            {

            }


            Assert.AreEqual(4.0, sheet.GetCellContents("A1"));

        }
        [TestMethod]

        public void OverwritingCellTest3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "CS");

            try
            {
                sheet.SetContentsOfCell("A1", "=B1");
            }
            catch (CircularException)
            {

            }


            Assert.IsTrue("CS".Equals(sheet.GetCellContents("A1")));

        }

        [TestMethod]

        public void OverwritingCellTest4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "=3+4");

            try
            {
                sheet.SetContentsOfCell("A1", "=B1");
            }
            catch (CircularException)
            {

            }


            Assert.IsTrue((new Formula("3+4")) == (Formula)sheet.GetCellContents("A1"));

        }

        [TestMethod]

        public void OverwritingCellTest5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetContentsOfCell("B1", "=A1*2");
            sheet.SetContentsOfCell("C1", "=B1+A1");
            sheet.SetContentsOfCell("A1", "14.0");
            sheet.SetContentsOfCell("A1", "14.9");




            Assert.AreEqual(14.9, (double)sheet.GetCellContents("A1"));

        }

        /// <summary>
        /// Tests that ("") is considered an empty cell
        /// </summary>
        [TestMethod]

        public void NullCellTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Assert.AreEqual("", sheet.GetCellContents("X1"));

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void NullCellTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("&");
        }


        /// <summary>
        /// Tests that non empty cells are properly reported
        /// </summary>
        [TestMethod]

        public void GetNonEmptyTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X1", "");
            HashSet<String> test = new HashSet<String>();
            test = (HashSet<String>)sheet.GetNamesOfAllNonemptyCells();
            Assert.IsTrue(test.Count == 0);

        }

        [TestMethod]

        public void GetNonEmptyTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X1", "");
            sheet.SetContentsOfCell("X2", "4.0");
            HashSet<String> test = new HashSet<String>();
            test = (HashSet<String>)sheet.GetNamesOfAllNonemptyCells();
            Assert.IsTrue(test.Count == 1);

        }
        [TestMethod]

        public void GetNonEmptyTest3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("X1", "");
            sheet.SetContentsOfCell("X4", "=X2 + 5");
            HashSet<String> test = new HashSet<String>();
            test = (HashSet<String>)sheet.GetNamesOfAllNonemptyCells();
            Assert.IsTrue(test.Count == 1);

        }








        //PRIVATE FUNCTION TESTS

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetDirectDependentsTest1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            PrivateObject test = new PrivateObject(sheet);
            test.Invoke("GetDirectDependents", new String[1] { null });

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetDirectDependentsTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            PrivateObject test = new PrivateObject(sheet);
            test.Invoke("GetDirectDependents", new String[1] { "%%" });

        }


        public bool isValid(String name)
        {
            if (name.Length != 2)
                return false;
            return true;
        }
        public void RunRandomizedTest(int seed, int size)
        {
            Spreadsheet s = new Spreadsheet();
            Random rand = new Random(seed);
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            s.SetContentsOfCell(randomName(rand), "3.14");
                            break;
                        case 1:
                            s.SetContentsOfCell(randomName(rand), "hello");
                            break;
                        case 2:
                            s.SetContentsOfCell(randomName(rand), randomFormula(rand).ToString());
                            break;
                    }
                }
                catch (CircularException)
                {
                }
            }
            ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(size, set.Count);
        }

        private String randomName(Random rand)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        }

        private String randomFormula(Random rand)
        {
            String f = randomName(rand);
            for (int i = 0; i < 10; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        f += "+";
                        break;
                    case 1:
                        f += "-";
                        break;
                    case 2:
                        f += "*";
                        break;
                    case 3:
                        f += "/";
                        break;
                }
                switch (rand.Next(2))
                {
                    case 0:
                        f += 7.2;
                        break;
                    case 1:
                        f += randomName(rand);
                        break;
                }
            }
            return f;
        }

    }
}


