using System;
using System.IO;
using NLog.Web;

namespace ticketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            // log sample messages

            string file = "movies.csv";
            string choice;
            do
            {
                // ask user a question
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Add data to file.");
                Console.WriteLine("Enter any other key to exit.");
                // input response

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    // read data from file
                    if (File.Exists(file))
                    {

                        // read data from file
                        StreamReader sr = new StreamReader(file);
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] splitLine = line.Split(',');                       
                            string MovieID = splitLine[0];
                            string MovieName = splitLine[1];
                            string MovieGenres = splitLine[2];

                            Console.WriteLine($"Movie ID: {MovieID} \nMovie Name: {MovieName}\nMovie Genres: {MovieGenres}\n\n");
                            
                        }
                        sr.Close();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                }
                else if (choice == "2")
                {
                    StreamWriter sw = new StreamWriter("movies.csv", true);
                    int movieID;
                    string movieNameAndYear = "";
                    string movieGenres = "";
                    bool inputExists = false;

                        Console.WriteLine("Enter Movie ID (Numbers)");

                        
                            while(!int.TryParse(Console.ReadLine(), out movieID)) {
                            Console.WriteLine("Input invalid, try again.");
                            }

                            //inputExists = File.ReadAllText(@"movies.csv").Contains(movieID);
                            if(inputExists){
                                Console.WriteLine("Input already exists, try again.");
                                inputExists = false;
                                break;
                            }


                        Console.WriteLine("Enter Movie Name (Include Year in Parenthises)");
                        
                        try {
                            movieNameAndYear = Console.ReadLine();
                            //inputExists = File.ReadAllText(@"movies.csv").Contains(movieNameAndYear);
                            if(inputExists) {
                                Console.WriteLine("Input already exists, try again.");
                                inputExists = false;
                                break;
                            }
                        } catch(Exception ex) {
                            Console.WriteLine("Input invalid.");
                            logger.Error(ex.Message);
                        }

                        Console.WriteLine("Enter Movie Genres (Split Multiple Genres by |'s.");

                        try {
                            movieGenres = Console.ReadLine();
                        } catch(Exception ex) {
                            Console.WriteLine("Input invalid.");
                            logger.Error(ex.Message);
                        }
                        
                        

                    sw.WriteLine($"{movieID},{movieNameAndYear},{movieGenres}");
                    sw.Close();
                }
            } while (choice == "1" || choice == "2");
        }
    }
}
