using System;
using System.Collections.Generic;
using System.Linq;
using DotNETProject;

class DictionaryFactory
{

    public static Dictionary<string, List<Person>> createCountryPersonDictionary(string[] lines)
    {
        var countryPersonPair = new Dictionary<string, List<Person>>();

        Person person;
        for (int i = 0; i < lines.Length - 1; i++)
        {
            string firstName = lines[i].Split(";")[0];
            string lastName = lines[i].Split(";")[1];
            string country = lines[i].Split(";")[2];
            string town = lines[i].Split(";")[3];
            try
            {
                int score = Int32.Parse(lines[i].Split(";")[4]);
                person = new Person(firstName, lastName, town, score);

                if (!countryPersonPair.ContainsKey(country))
                {
                    countryPersonPair.Add(country, new List<Person>());
                    countryPersonPair[country].Add(person);
                }
                else
                {
                    countryPersonPair[country].Add(person);
                }
            }catch(FormatException){
                Console.WriteLine(String.Format("You have a wrong input data at line {0}. Fix it and start application again.", i + 2));
                Console.Write("Text to fix: ");
                Console.WriteLine(lines[i]);
                Environment.Exit(1);
            }



            }

        return countryPersonPair;
    }

    public static Dictionary<string, float> sortCountriesByAverageScore(Dictionary<string, List<Person>> kvp)
    {

        var dic = new Dictionary<string, float>();

        foreach (string entry in kvp.Keys)
        {
            float totalScore = 0f;
            foreach (Person p in kvp[entry])
            {
                totalScore += (float)(p.getScore());
            }

            float averageScore = totalScore / (float)(kvp[entry].Count);
            dic.Add(entry, averageScore);
        }

        var dictionaryToReturn = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> item in dic.OrderByDescending(key => key.Value))
        {
            dictionaryToReturn.Add(item.Key, item.Value);
        }

        return dictionaryToReturn;
    }
}