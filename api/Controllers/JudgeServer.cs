using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController : ControllerBase
    {
        private readonly List<TestCase> testCases = new List<TestCase>
        {
            new TestCase { Input = "5\n1 2 3 4 5\n", ExpectedOutput = "5\n" },
            new TestCase { Input = "6\n6 7 8 9 10 11\n", ExpectedOutput = "11\n" },
            new TestCase { Input = "3\n999 1 1000\n", ExpectedOutput = "1000\n" },
            new TestCase { Input = "4\n10 20 30 40\n", ExpectedOutput = "40\n" },
            new TestCase { Input = "5\n15 25 35 45 55\n", ExpectedOutput = "55\n" },
            new TestCase { Input = "6\n123 456 789 1011 1213 1415\n", ExpectedOutput = "1415\n" },
            new TestCase { Input = "2\n0 0\n", ExpectedOutput = "0\n" },
            new TestCase { Input = "2\n-5 5\n", ExpectedOutput = "5\n" },
            new TestCase { Input = "3\n-10 -20 -30\n", ExpectedOutput = "-10\n" },
            new TestCase { Input = "3\n1000 2000 3000\n", ExpectedOutput = "3000\n" },
            new TestCase { Input = "2\n2147483647 1\n", ExpectedOutput = "2147483647\n" },
            new TestCase { Input = "2\n-2147483648 -1\n", ExpectedOutput = "-1\n" },
            new TestCase { Input = "2\n999999 1000000\n", ExpectedOutput = "1000000\n" }
        };

        [HttpPost]
        public IActionResult Post([FromBody] CodeSubmission submission)
        {
            string randomExePath = string.Empty;

            try
            {
                string sourceCode = submission.Code;
                string cppFilePath = Path.Combine(Directory.GetCurrentDirectory(), "temp.cpp");

                System.IO.File.WriteAllText(cppFilePath, sourceCode);

                randomExePath = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid()}.exe");

                string compileError = CompileCppCode(cppFilePath, randomExePath);
                if (!string.IsNullOrEmpty(compileError))
                {
                    string sanitizedError = SanitizeError(compileError);
                    return BadRequest(new { error = sanitizedError });
                }
                var result = RunAndCheckTestCases(randomExePath);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred: " + ex.Message });
            }
            finally
            {
                CleanupFiles("temp.cpp", randomExePath);
            }
        }

        private TestCaseResult RunAndCheckTestCases(string executablePath)
        {
            int count = 0;
            foreach (var testCase in testCases)
            {
                string programOutput = RunExecutableWithInput(executablePath, testCase.Input);

                if (programOutput.Trim() != testCase.ExpectedOutput.Trim())
                {
                    return new TestCaseResult
                    {
                        Passed = false,
                        count = count+1, 
                        FailedTestCase = testCase,
                        ActualOutput = programOutput
                    };
                }
                count++;
            }

            return new TestCaseResult
            {
                Passed = true,
                count = count,
                Message = "All test cases passed."
            };
        }


        private string RunExecutableWithInput(string executablePath, string input)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = executablePath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    if (process == null)
                    {
                        throw new Exception("Failed to start the program.");
                    }

                    using (var writer = process.StandardInput)
                    {
                        writer.Write(input);
                    }

                    process.WaitForExit();

                    using (var reader = process.StandardOutput)
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error running executable: {ex.Message}";
            }
        }

        private string CompileCppCode(string cppFilePath, string exeFilePath)
        {
            string compilerPath = "g++";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = compilerPath,
                Arguments = $"\"{cppFilePath}\" -o \"{exeFilePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processStartInfo))
            {
                if (process == null)
                {
                    return "Failed to start compiler.";
                }

                using (var reader = process.StandardError)
                {
                    string error = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(error))
                    {
                        return error;
                    }
                }
            }

            return null;
        }

    
        private void CleanupFiles(string cppFilePath, string exeFilePath)
        {
            try
            {
                if (System.IO.File.Exists(cppFilePath))
                    System.IO.File.Delete(cppFilePath);

                if (!string.IsNullOrEmpty(exeFilePath) && System.IO.File.Exists(exeFilePath))
                    System.IO.File.Delete(exeFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup failed: {ex.Message}");
            }
        }


        private string SanitizeError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return string.Empty;

            return Regex.Replace(error, @"[A-Za-z]:\\[^\s]*", string.Empty);
        }
    }

    public class CodeSubmission
    {
        public string Code { get; set; } = string.Empty;
    }

    public class TestCase
    {
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
    }

    public class TestCaseResult
    {
        public bool Passed { get; set; }

        public int count { get; set; }
        public TestCase FailedTestCase { get; set; }
        public string ActualOutput { get; set; }
        public string Message { get; set; }
    }
}
