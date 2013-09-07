using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
namespace LSD
{
    static class Scores_RW
    {
        private static string file_name = "scores.csv", file_address = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LSD\";
        private static SortedDictionary<short, string> scores_list = new SortedDictionary<short, string>();
        public static void existance()
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
        public static SortedDictionary<short, string> read()
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
        public static void write(Triangle trin) {
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
