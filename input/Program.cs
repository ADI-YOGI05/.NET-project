using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class InputData
{
    [Name("date")]
    public DateOnly Date { get; set; }

    [Name("grain")]
    public string Grain { get; set; }

    [Name("beerproduction")]
    public decimal BeerProduction { get; set; }

    [Name("factoryManagerName")]
    public string Manager { get; set; }
}

public class OutputData
{

   

    [Name("Grain")]
    public string Grain { get; set; }

    [Name("Date")]
    public string Date { get; set; }


    [Name("Manager")]
    public string Manager { get; set; }

    [Name("BeerProduction")]
    public string BeerProduction { get; set; }


   
    [Name("YearMean")]
    public string YearMean { get; set; }

    [Name("YearMedian")]
    public string YearMedian { get; set; }

    [Name("BeerProductionGreaterThanYearMean")]
    public string BeerProductionGreaterThanYearMean { get; set; }
}

class Program
{
    static void Main()
    {
        // Read input CSV
        List<InputData> inputData;
        using (var reader = new StreamReader("Input_v1.0.csv", Encoding.UTF8))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null, // Disable header validation
            MissingFieldFound = null // Disable handling missing fields
        }))
        {
            inputData = csv.GetRecords<InputData>().ToList();
        }

        // Process data
        List<OutputData> processedData = ProcessData(inputData);

        // Write output CSV
        using (var writer = new StreamWriter("Output.csv", false, new UTF8Encoding(true)))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(processedData);
        }
    }

    static List<OutputData> ProcessData(List<InputData> inputData)
    {
        var processedData = inputData.Select(input =>
            new OutputData
            {
                Grain = input.Grain,
                Date = input.Date.ToString("yyyy/dd/MM"),
                Manager = input.Manager,
                BeerProduction = input.BeerProduction.ToString("F2"),
                YearMean = GetYearMean(inputData, input.Date.Year).ToString("F2"),
                YearMedian = CalculateMedian(inputData.Where(x => x.Date.Year == input.Date.Year).Select(x => x.BeerProduction)),
                // BeerProductionGreaterThanYearMean = (input.BeerProduction > GetYearMean(inputData, input.Date.Year)).ToString()
                BeerProductionGreaterThanYearMean = (input.BeerProduction > GetYearMean(inputData, input.Date.Year)) ? "yes" : "no"


            }).ToList();

        return processedData;
    }

    static decimal GetYearMean(List<InputData> yearMeanData, int year)
    {
        return yearMeanData.Where(x => x.Date.Year == year).Average(x => x.BeerProduction);
    }

    static string CalculateMedian(IEnumerable<decimal> values)
    {
        var orderedValues = values.OrderBy(x => x).ToList();
        int count = orderedValues.Count;
        int mid = count / 2;

        return count % 2 == 0
            ? ((orderedValues[mid - 1] + orderedValues[mid]) / 2).ToString("F2")
            : orderedValues[mid].ToString("F2");
    }
}
