using Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace LetsTestATP.PageLib
{
    class ExcelLib
    {
        private static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return
            return resultTable;
        }

        static List<Datacollection> dataCol;//= new List<Datacollection>();
        public static int numberOfRows;
        public static int numberOfColumns;
        public static int numberOfRows2;
        public static int numberOfColumns2;

        public static int counter = 0;
        public static void PopulateInCollection(string fileName)
        {
            DataTable table = ExcelToDataTable(fileName);
            dataCol = new List<Datacollection>();

            //Iterate through the rows and columns of the Table
            try
            {
                numberOfRows = table.Rows.Count;
                numberOfColumns = table.Columns.Count;
                for (int row = 1; row <= numberOfRows; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = table.Columns[col].ColumnName,
                            colValue = table.Rows[row - 1][col].ToString()
                        };
                        //Add all the details for each row
                        dataCol.Add(dtTable);
                    }
                }
            }
            catch (Exception e) { }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void lastcolumn()
        {

        }
        public static void WriteRefNoToExcel(int rowIndex, int colIndex, string strData, string ExcelPath)
        {
            String strDataSheetPath = ConfigurationManager.AppSettings["DataSheetPath"];
            Excel1.Application excelApp = new Excel1.Application();
            string myPath = strDataSheetPath + ExcelPath;
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Open(myPath);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
            //int rowIndex = 1; int colIndex = 1;
            Microsoft.Office.Interop.Excel.Range range = sheet.UsedRange;
            int lastColumn = range.Columns.Count;
            excelApp.Cells[rowIndex, colIndex] = strData;
            //excelApp.Cells[rowIndex, colIndex + 1] = strStatus;
            excelApp.ActiveWorkbook.Save();
            excelApp.Workbooks.Close();
            excelApp.Quit();
        }


        //top write values indata driven excel sheet
        public static void WriteRefNoToExcel(int rowIndex, string lastcolumn, string strData, string ExcelPath)
        {
            String strDataSheetPath = ConfigurationManager.AppSettings["DataSheetPath"];
            Excel1.Application excelApp = new Excel1.Application();
            string myPath = strDataSheetPath + ExcelPath;
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Open(myPath);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range = sheet.UsedRange;
            int lastColumnNumber = range.Columns.Count;
            int WriteException = lastColumnNumber;
            int WritePassFail = lastColumnNumber - 2;
            int WriteStringRef = lastColumnNumber - 1;
            if (lastcolumn == "Exception")
            {
                excelApp.Cells[rowIndex, WriteException] = strData;
                excelApp.Cells[rowIndex, WritePassFail] = strData;

            }

            else if (lastcolumn == "refNum")
            {
                excelApp.Cells[rowIndex, WriteStringRef] = strData;
            }
            else
            {
                excelApp.Cells[rowIndex, WritePassFail] = strData;
            }
            excelApp.ActiveWorkbook.Save();
            excelApp.Workbooks.Close();
            excelApp.Quit();
        }

        public static void WriteToResultFile(string Result, string Errormsg, string Description, string strPrefix, string OemPbm, string referenceNo)
        {
            String strDataSheetPath = ConfigurationManager.AppSettings["ResultDataSheetPath"];

            string myPath = strDataSheetPath + strPrefix + "_Result.xlsx";

            // DataTable table2 = ExcelToDataTable2(myPath);
            ExcelLib.PopulateInCollection(@"" + myPath);

            string count = ReadData(1, "No. of times TC's Executed");

            /*   counter = Int16.Parse(count);
               counter++;*/

            Excel1.Application excelApp = new Excel1.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Open(myPath);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range = sheet.UsedRange;


            counter = Int16.Parse(count);
            counter++;
            int lastRowNumber = range.Rows.Count;
            int lastColumnNumber = range.Columns.Count;
            int lastrowindex = lastRowNumber + 1;

            excelApp.Cells[lastrowindex, 1] = Description;
            excelApp.Cells[lastrowindex, 2] = Result;
            excelApp.Cells[lastrowindex, 3] = Errormsg;
            excelApp.Cells[lastrowindex, 4] = DateTime.Now.ToShortDateString();
            excelApp.Cells[lastrowindex, 5] = DateTime.Now.ToString("HH:mm:ss");
            excelApp.Cells[lastrowindex, 6] = referenceNo;
            excelApp.Cells[2, 7] = counter;



            excelApp.ActiveWorkbook.Save();
            excelApp.Workbooks.Close();
            excelApp.Quit();


        }


    }



    public class Datacollection
    {
        public int rowNumber { get; set; }
        public string colName { get; set; }
        public string colValue { get; set; }
    }
    public class Datacollection2
    {
        public int rowNumber2 { get; set; }
        public string colName2 { get; set; }
        public string colValue2 { get; set; }
    }
}
