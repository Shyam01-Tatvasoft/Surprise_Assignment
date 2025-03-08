using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ImportCSV.Models;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ImportCSV.Controllers
{
    public class StudentController : Controller
    {
        private readonly Assignment4Context _context;

        public StudentController(Assignment4Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UploadCsv()
        {
            return View(new UploadCsvViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(UploadCsvViewModel model)
        {
            if (model.File != null)
            {
                try
                {
                    using (var reader = new StreamReader(model.File.OpenReadStream()))
                    using (var csvr = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) {Delimiter = ","}))
                    {
                        var students = csvr.GetRecords<Student>().ToList();

                        foreach (var student in students)
                        {
                            if (string.IsNullOrWhiteSpace(student.Email)) continue;

                            if (!student.MobileNumber.StartsWith("+91"))
                            {
                                student.MobileNumber = "+91" + student.MobileNumber;
                            }
                            
                            if (student.Pincode.Length > 6) continue;

                            var alreadyStudent = await _context.Students
                                .FirstOrDefaultAsync(s => s.RollNumber == student.RollNumber);

                            if (alreadyStudent != null)
                            {
                                alreadyStudent.Name = student.Name;
                                alreadyStudent.MobileNumber = student.MobileNumber;
                                alreadyStudent.City = student.City;
                                alreadyStudent.Address = student.Address;
                                alreadyStudent.Email = student.Email;
                                alreadyStudent.Pincode = student.Pincode;
                            }
                            else
                            {
                                _context.Students.Add(student);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    Console.WriteLine("CSV uploaded and processed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CSV file is not Processed. Exception: " + ex.Message);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                }
            }
            else
            {
                Console.WriteLine("No file uploaded.");
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}