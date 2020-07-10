using System;
using System.Collections.Generic;
using System.Linq;
using DotNETProject;
using System.Text;

class DataAggregator
{

    public static string collectAndSortData(Dictionary<string, List<Person>> dict1, Dictionary<string, float> dict2)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string country in dict2.Keys)
        {
            sb.Append(country);
            sb.Append("\n" + "#Average Score ").Append(String.Format("{0:F2}", dict2[country])).Append("\n");
            sb.Append("#Median score " + getMedianScore(dict1, country)).Append("\n");
            float maxScore = float.NegativeInfinity;
            string maxScorePerson = "";
            float minScore = float.PositiveInfinity;
            string minScorePerson = "";

            foreach (Person person in dict1[country])
            {
                if (person.getScore() > maxScore)
                {
                    maxScore = person.getScore();
                    maxScorePerson = person.getFirstName() + " " + person.getLastName();
                }
                if (person.getScore() < minScore)
                {
                    minScore = person.getScore();
                    minScorePerson = person.getFirstName() + " " + person.getLastName();
                }
            }
            sb.Append("#Max score ").Append(maxScore.ToString()).Append("\n");
            sb.Append("#Max score person ").Append(maxScorePerson).Append("\n");
            sb.Append("#Min score ").Append(minScore.ToString()).Append("\n");
            sb.Append("#Min score person ").Append(minScorePerson).Append("\n");
            sb.Append("#Record count ").Append(dict1[country].Count).Append("\n").Append("\n");



        }

        return sb.ToString();


    }

    private static string getMedianScore(Dictionary<string, List<Person>> dict1, string country)
    {
        float medianScore;
        if (dict1[country].Count >= 2)
        {
            if (dict1[country].Count % 2 == 0)
            {
                int medianId = (int)dict1[country].Count / 2;
                medianScore = dict1[country].ElementAt(medianId - 1).getScore();
                medianScore += dict1[country].ElementAt(medianId).getScore();
                medianScore /= 2;
            }
            else
            {
                int medianId = ((int)dict1[country].Count / 2) + 1;
                medianScore = dict1[country].ElementAt(medianId - 1).getScore();
            }
        }else {
            medianScore = dict1[country].ElementAt(0).getScore();
        }

        return medianScore.ToString();
    }

}