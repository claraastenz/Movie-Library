using System;
using System.IO;
using NLog;

class Program
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    private const string moviesFilePath = "movies.csv";
    private const string linksFilePath = "links.csv";
    private const string tagsFilePath = "tags.csv";
    private const string ratingsFilePath = "ratings.csv";

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Movie Library!");

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. View all movies");
            Console.WriteLine("2. View all links");
            Console.WriteLine("3. View all tags");
            Console.WriteLine("4. View all ratings");
            Console.WriteLine("5. Add a new movie");
            Console.WriteLine("6. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllMovies();
                    break;
                case "2":
                    ViewAllLinks();
                    break;
                case "3":
                    ViewAllTags();
                    break;
                case "4":
                    ViewAllRatings();
                    break;
                case "5":
                    AddNewMovie();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void ViewAllMovies()
    {
        ViewAllEntitiesFromFile(moviesFilePath);
    }

    static void ViewAllLinks()
    {
        ViewAllEntitiesFromFile(linksFilePath);
    }

    static void ViewAllTags()
    {
        ViewAllEntitiesFromFile(tagsFilePath);
    }

    static void ViewAllRatings()
    {
        ViewAllEntitiesFromFile(ratingsFilePath);
    }

    static void ViewAllEntitiesFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"No data found in {Path.GetFileName(filePath)}.");
                logger.Warn($"No data found in {Path.GetFileName(filePath)}.");
                return;
            }

            Console.WriteLine($"All {Path.GetFileNameWithoutExtension(filePath)}:");
            foreach (string line in File.ReadAllLines(filePath))
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            logger.Error(ex, $"An error occurred while reading {Path.GetFileName(filePath)}.");
        }
    }

    static void AddNewMovie()
    {
        try
        {
            Console.WriteLine("Enter movie details in the format: movieId,title,genres");
            string newMovie = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newMovie))
            {
                // Append the new movie details to the file
                using (StreamWriter writer = File.AppendText(moviesFilePath))
                {
                    writer.WriteLine(newMovie);
                }
                Console.WriteLine("Movie added successfully.");
                logger.Info("New movie added: " + newMovie);
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            logger.Error(ex, "An error occurred while adding a new movie.");
        }
    }
}
