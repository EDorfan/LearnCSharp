using System;
using System.Diagnostics;

public class Program
{

    /*
    Demonstrates how to use threads to make multiple HTTP requests concurrently,
    in order to improve performance.
    */
    public static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var pageURLs = new List<string> {
            "https://www.scrapingcourse.com/ecommerce/page/1/",
            "https://www.scrapingcourse.com/ecommerce/page/2/",
            "https://www.scrapingcourse.com/ecommerce/page/3/",
            "https://www.scrapingcourse.com/ecommerce/page4/",
            "https://www.scrapingcourse.com/ecommerce/page5/"
        };

        // initialize the common HTTP client to make
        // all the requests
        HttpClient client = new HttpClient();

        List<Thread> threads = new List<Thread>();

        foreach (var pageURL in pageURLs)
        {
            Thread thread = new Thread(() =>
                {
                    ProcessRequest(client, pageURL);
                });
            threads.Add(thread);
        }

        // start all the threads
        foreach (var thread in threads)
        {
            thread.Start();
        }

        // wait for all the threads to complete
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("All threads completed");

        // dispose the HTTP client
        client.Dispose();

        // get the elapsed time in seconds
        stopwatch.Stop();
        double elapsedTimeSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine($"Total time taken: {elapsedTimeSeconds} seconds");
    }

    private static void ProcessRequest(HttpClient client, string pageURL)
    {
        var response = client.GetAsync(pageURL).Result;
        Console.WriteLine($"URL: {pageURL} => Request Completed with Status code: {response.StatusCode}");
    }
}