using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Assignment1
{
    public class SimpleCSVParser
    {
        public int valid_records = 0;
        public int skipped_records = 0;

        public void parse(String fileName)
        {
            try {

                string[] date_split = fileName.Split("\\");
                string date = date_split[4] + "/" + date_split[5] + "/" + date_split[6];

                using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                    int is_header = 1;

                    while (!parser.EndOfData)
                    {
                        int skip = 0;

                        //Process row
                        string[] record = parser.ReadFields();

                        foreach (string field in record)
                        {
                            if (field == "" || field == " ")
                            {
                                skip = 1;
                                break;
                            }

                        }

                        if (skip == 0 && is_header == 0)
                        {
                            using (FileStream fs = new FileStream("..\\..\\..\\..\\Output\\csv_merged.csv", FileMode.Append, FileAccess.Write))
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.WriteLine(String.Join(",", record) + "," + date);

                                valid_records++;
                            }
                        }
                        else
                        {
                            skipped_records++;
                        }

                        is_header = 0;
                        
                    }

            }
        
        }catch(IOException ioe){
                Console.WriteLine(ioe.StackTrace);
         }

    }


    }
}
