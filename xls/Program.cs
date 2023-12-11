using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

class Program
{
    static void Main()
    {
        string inputFilePath = "input.xlsx";
        string outputFilePath = "output.xlsx";

        // Read data from the input Excel file
        List<string> inputData = ReadDataFromExcel(inputFilePath);

        // Split the data into separate columns
        List<List<string>> columns = SplitData(inputData);

        // Write the columns to a new Excel file
        WriteDataToExcel(columns, outputFilePath);

        Console.WriteLine("Data split and written to output.xlsx successfully.");
    }

    static List<string> ReadDataFromExcel(string filePath)
    {
        List<string> data = new List<string>();

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];

            for (int row = 1; row <= worksheet.Dimension.Rows; row++)
            {
                var cellValue = worksheet.Cells[row, 1].Text;
                data.Add(cellValue);
            }
        }

        return data;
    }

    static List<List<string>> SplitData(List<string> inputData)
    {
        List<List<string>> columns = new List<List<string>>();
        List<string> currentColumn = new List<string>();

        foreach (var value in inputData)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                // Blank row, start a new column
                columns.Add(currentColumn);
                currentColumn = new List<string>();
            }
            else
            {
                // Add value to the current column
                currentColumn.Add(value);
            }
        }

        // Add the last column
        columns.Add(currentColumn);

        return columns;
    }

    static void WriteDataToExcel(List<List<string>> columns, string filePath)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            int row = 1;
            foreach (var column in columns)
            {
                int col = 1;
                foreach (var value in column)
                {
                    worksheet.Cells[row, col].Value = value;
                    col++;
                }
                row++;
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
