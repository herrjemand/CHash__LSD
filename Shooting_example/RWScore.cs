using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
namespace LSD
{
    static class Scores_RW//scores read write
    {
        private static string file_name = "scores.csv", 
            file_address = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LSD\";//file name, and location
        private static SortedDictionary<short, string> scores_list = new SortedDictionary<short, string>();
        public static void existance() //check existance, and if not create folder and file in MyDocuments folder
        {
            if (!Directory.Exists(file_address))
            {
                Directory.CreateDirectory(file_address);
                File.Create(file_address + file_name);
            }
            else
            {
                if (!File.Exists(file_address + file_name))
                {
                    File.Create(file_address + file_name);
                }
            }
        }
        public static SortedDictionary<short, string> read() //read file and return sorted dictionary
        {
            existance();
            scores_list.Clear();
            if (new FileInfo(file_address + file_name).Length != 0)
            {
                StreamReader sr = new StreamReader(File.OpenRead(file_address + file_name));
                string[] row = new string[2];
                while (!sr.EndOfStream)
                {
                    row = sr.ReadLine().Split(',');
                    scores_list.Add(Convert.ToInt16(row[0]), row[1]);
                }
                sr.Close();
            }
            return scores_list;
        }
        public static void write(Triangle trin) {//if user score is not 0, hten itr writes all scores to file in CSV format
            if (trin.score != 0)
            {
                SortedDictionary<short, string> local_scores = read();
                string x = "";
                if (new FileInfo(file_address + file_name).Length != 0)
                {
                    foreach (KeyValuePair<short, string> item in local_scores)
                    {
                        x += item.ToString().Replace(" ", "").Replace("[", "").Replace("]", "") + "\n";
                    }
                }
                x += trin.score + "," + trin.name.Replace(" ", "") + "\n";
                File.WriteAllText(file_address + file_name, x, Encoding.UTF8);
            }
        }
    }
}
