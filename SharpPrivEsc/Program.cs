using System;
using System.Diagnostics;

namespace AddUserToLocalAdminGroup
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the domain and user to add
            string domain = "global";
            string user = "pok05094";
            string domainUser = $"{domain}\\{user}";

            // Specify the local Administrators group
            string localGroup = "Administrators";

            Console.WriteLine("Starting process to add user to local administrators group.");
            Console.WriteLine($"Target user: {domainUser}");
            Console.WriteLine($"Target group: {localGroup}");

            try
            {
                // Build the command
                string command = "net";
                string arguments = $"localgroup {localGroup} {user} /add";

                Console.WriteLine($"Command to execute: {command} {arguments}");

                // Start the process to run the command
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = command;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    Console.WriteLine("Starting process to execute the command...");

                    // Start the process and wait for it to complete
                    process.Start();

                    // Read the output
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    // Display the results
                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("User successfully added to the local Administrators group.");
                        Console.WriteLine("Command output:\n" + output);
                    }
                    else
                    {
                        Console.WriteLine("Failed to add user to the local Administrators group.");
                        Console.WriteLine("Command output:\n" + output);
                        Console.WriteLine("Error output:\n" + error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during the process.");
                Console.WriteLine($"Exception message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}
