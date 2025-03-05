using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class Program
{

    /*
    Demonstrates how to use threads to make multiple HTTP requests concurrently,
    in order to improve performance.

    Performs a performance comparison between using threads and tasks to make
    multiple HTTP requests concurrently.
    */
    public static async Task Main(string[] args)
    {

        var pageURLs = new List<string> {
            "https://www.scrapingcourse.com/ecommerce/page/1/",
            "https://www.scrapingcourse.com/ecommerce/page/2/",
            "https://www.scrapingcourse.com/ecommerce/page/3/",
            "https://www.scrapingcourse.com/ecommerce/page4/",
            "https://www.scrapingcourse.com/ecommerce/page5/"
        };

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
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
        double elapsedTimeSecondsThreads = stopwatch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine($"Total time taken for thread version: {elapsedTimeSecondsThreads} seconds\n");

        stopwatch.Reset();
        stopwatch.Start();

        client = new HttpClient();

        List<Task> tasks = new List<Task>();

        foreach (var pageURL in pageURLs)
        {
            tasks.Add(Task.Run(() =>
                {
                    ProcessRequest(client, pageURL);
                }));
        }
        
        // wait for all the tasks to complete
        await Task.WhenAll(tasks);

        Console.WriteLine("All tasks completed");
        
        // dispose of client
        client.Dispose();

        // get the elapsed time in seconds
        stopwatch.Stop();
        double elapsedTimeSecondsTask = stopwatch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine($"Total time taken for task version: {elapsedTimeSecondsTask} seconds\n");

        /// Performing an analysis without concurrency to illustrate the speed difference
        /// between the concurrent and non-concurrent versions
        /// 
        stopwatch.Reset();
        stopwatch.Start();

        client = new HttpClient();

        foreach (var pageURL in pageURLs)
        {
            ProcessRequest(client, pageURL);
        }
        Console.WriteLine("All requests completed");
        // dispose of client
        client.Dispose();   

        // get the elapsed time in seconds
        stopwatch.Stop();
        double elapsedTimeSecondsNoConcurrency = stopwatch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine($"Total time taken for non-concurrent version: {elapsedTimeSecondsNoConcurrency} seconds\n");

        Console.WriteLine($"Speedup of using threads over non-concurrent version: {(elapsedTimeSecondsNoConcurrency / elapsedTimeSecondsThreads * 100):F2}%");
        Console.WriteLine($"Speedup of using tasks over non-concurrent version: {(elapsedTimeSecondsNoConcurrency / elapsedTimeSecondsTask * 100):F2}%");
        
    }

    private static void ProcessRequest(HttpClient client, string pageURL)
    {
        var response = client.GetAsync(pageURL).Result;
        Console.WriteLine($"URL: {pageURL} => Request Completed with Status code: {response.StatusCode}");
    }
}