using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ParallelAsyncExample
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client = new HttpClient { MaxResponseContentBufferSize = 1_000_000 };

        private readonly IEnumerable<string> _urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/xamarin",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/gaming"
        };

        /// <summary>
        /// Event handler for the Start button click event.
        /// Disables the Start button, clears the results text box, and starts the asynchronous task to calculate the sum of page sizes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            // Disable the Start button
            _startButton.IsEnabled = false;

            // Clear the results text box
            _resultsTextBox.Clear();

            // Start the asynchronous task to calculate the sum of page sizes
            Task.Run(() => StartSumPageSizesAsync());
        }

        /// <summary>
        /// Starts the asynchronous task to calculate the sum of page sizes.
        /// </summary>
        private async Task StartSumPageSizesAsync()
        {
            await SumPageSizesAsync();

            // Update UI after the task is completed
            await Dispatcher.BeginInvoke(() =>
            {
                // Add a message indicating that control has returned to OnStartButtonClick
                _resultsTextBox.Text += $"\nControl returned to {nameof(OnStartButtonClick)}.";

                // Enable the Start button
                _startButton.IsEnabled = true;
            });
        }

        /// <summary>
        /// Calculates the sum of page sizes asynchronously.
        /// </summary>
        private async Task SumPageSizesAsync()
        {
            // Start a stopwatch to measure the elapsed time
            var stopwatch = Stopwatch.StartNew();

            // Create an array of tasks to download the page sizes
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in _urlList
                select ProcessUrlAsync(url, _client);

            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            // Wait for all the download tasks to complete
            int[] lengths = await Task.WhenAll(downloadTasks);

            // Calculate the total sum of page sizes
            int total = lengths.Sum();

            // Update UI after the task is completed
            await Dispatcher.BeginInvoke(() =>
            {
                stopwatch.Stop();

                // Display the total bytes returned
                _resultsTextBox.Text += $"\nTotal bytes returned:  {total:#,#}";

                // Display the elapsed time
                _resultsTextBox.Text += $"\nElapsed time:          {stopwatch.Elapsed}\n";
            });
        }

        /// <summary>
        /// Processes a URL asynchronously and returns the size of the downloaded content.
        /// </summary>
        /// <param name="url">The URL to process.</param>
        /// <param name="client">The HttpClient instance to use for downloading.</param>
        /// <returns>The size of the downloaded content.</returns>
        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            try
            {
                // Download the content of the URL as a byte array
                byte[] byteArray = await client.GetByteArrayAsync(url);

                // Display the results asynchronously
                await DisplayResultsAsync(url, byteArray);

                // Return the size of the downloaded content
                return byteArray.Length;
            }
            catch (Exception ex)
            {
                // Display an error message if the download fails
                await Dispatcher.BeginInvoke(() =>
                {
                    _resultsTextBox.Text += $"\nError downloading {url}: {ex.Message}\n";
                });

                // Return 0 if the download fails
                return 0;
            }
        }

        /// <summary>
        /// Displays the results asynchronously.
        /// </summary>
        /// <param name="url">The URL of the downloaded content.</param>
        /// <param name="content">The downloaded content as a byte array.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private Task DisplayResultsAsync(string url, byte[] content) =>
            Dispatcher.BeginInvoke(() =>
                _resultsTextBox.Text += $"{url,-60} {content.Length,10:#,#}\n")
                      .Task;

        /// <summary>
        /// Overrides the OnClosed method to dispose the HttpClient instance.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnClosed(EventArgs e) => _client.Dispose();
    }
}
