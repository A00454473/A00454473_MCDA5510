using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Assignment1.ProgAssign1
{
    public class DirWalker
    {

        public List<string> walk(string path)
        {
            SimpleCSVParser parser = new SimpleCSVParser();

            string[] list = Directory.GetDirectories(path);
            List<string> csv_files = new List<string>();


            if (list == null) return null;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                }
            }
            string[] files = Directory.GetFiles(path);


            foreach (string filepath in files)
            {
                //Console.WriteLine(filepath);
                if (filepath.Contains(".csv"))
                {
                    //csv_files.Add(filepath);
                    Console.WriteLine("Parsing: " + filepath);
                    parser.parse(filepath);

                }
            }
            //Console.WriteLine(csv_files);\\
            Global.valid_records += parser.valid_records;
            Global.skipped_records += parser.skipped_records;

            return csv_files;
        }

        public static void Main(String[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (FileStream fs = File.Open("..\\..\\..\\..\\Output\\csv_merged.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(@"First Name,Last Name,Street Number,Street,City,Province,Postal Code,Country,Phone Number,email Address,Date");
                }
            }

            DirWalker dw = new DirWalker();

            List<string> fileList = dw.walk("..\\..\\..\\Sample Data");

            Console.WriteLine(Global.valid_records);
            Console.WriteLine(Global.skipped_records);

            stopwatch.Stop();

            LoggerClass.Log("Valid Records: " + Global.valid_records.ToString());
            LoggerClass.Log("Skipped Records: " + Global.skipped_records.ToString());
            LoggerClass.Log("Total Time: " + stopwatch.Elapsed.ToString());
            LoggerClass.Log("----------------------------------");
        }

    }
}
