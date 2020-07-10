using System;
using System.IO;
using System.Text;

class FileReaderWriter
{

    public static string[] readFromFile(string filePath)
    {
        try
        {
            string fileContent = System.IO.File.ReadAllText(filePath);

            string[] lines = fileContent.Split("\n");

            string[] tokens = new string[lines.Length - 1];

            for (int i = 1; i < lines.Length; i++)
            {
                tokens[i - 1] = lines[i];
            }

            return tokens;

        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine("Wrong filepath ! Check parameters and contact your IT support.");
            throw new System.IO.FileNotFoundException();
        }

    }

    public static string writeAggregatedDataToFile(string aggregatedData)
    {

        string currentDate = DateTime.UtcNow.ToString("D");

        string reportsDirectory = Directory.GetCurrentDirectory() + String.Format("\\Reports ({0})", currentDate);
        if(!Directory.Exists(reportsDirectory)){
            Directory.CreateDirectory(reportsDirectory);
        }        

        StringBuilder sb = new StringBuilder();

        int counter = 0;

        while (counter++ <= 100)
        {
            sb.Append(reportsDirectory).Append(String.Format("\\ReportByCountry ({0}).csv", counter));
            try
            {
                System.IO.File.ReadAllText(sb.ToString());
            }
            catch (System.IO.FileNotFoundException)
            {
                File.AppendAllText(sb.ToString(), aggregatedData);
                return sb.ToString();
            }

            sb.Clear();
        }

        Console.WriteLine("You did too many reports for the day. Take a rest or a day off. :)");
        throw new InvalidOperationException();

    }
}