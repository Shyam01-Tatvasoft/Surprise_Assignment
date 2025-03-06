using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Npgsql;

namespace CsvToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "D:/QuizzApp/CsvToSql/data.csv";
            string connectionString = "Host=localhost;Port=5432;Database=assignment_csv_to_sql;Username=postgres;Password=Tatva@123";

            List<Student> students = ReadCsvFile(csvFilePath);
            SaveStudentsToDatabase(students, connectionString);

            Console.WriteLine("Process completed.");
        }

        static List<Student> ReadCsvFile(string filePath)
        {
            return File.ReadAllLines(filePath)
                       .Skip(1)
                       .Select(line => line.Split(','))
                       .Where(values => values.Length == 7 && !string.IsNullOrEmpty(values[5]) && values[2].Length == 10 && values[6].Length == 6)
                       .Select(values => new Student
                       {
                           RollNumber = values[0],
                           Name = values[1],
                           MobileNumber = values[2].StartsWith("+91") ? values[2] : "+91" + values[2],
                           City = values[3],
                           Address = values[4],
                           Email = values[5],
                           Pincode = values[6]
                       })
                       .ToList();
        }

        static void SaveStudentsToDatabase(List<Student> students, string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                foreach (var student in students)
                {
                    string query = @"
                        INSERT INTO Students (RollNumber, Name, MobileNumber, City, Address, Email, Pincode)
                        VALUES (@RollNumber, @Name, @MobileNumber, @City, @Address, @Email, @Pincode)
                        ON CONFLICT (RollNumber) DO UPDATE
                        SET Name = EXCLUDED.Name,
                            MobileNumber = EXCLUDED.MobileNumber,
                            City = EXCLUDED.City,
                            Address = EXCLUDED.Address,
                            Email = EXCLUDED.Email,
                            Pincode = EXCLUDED.Pincode";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                        command.Parameters.AddWithValue("@Name", student.Name);
                        command.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
                        command.Parameters.AddWithValue("@City", student.City);
                        command.Parameters.AddWithValue("@Address", student.Address);
                        command.Parameters.AddWithValue("@Email", student.Email);
                        command.Parameters.AddWithValue("@Pincode", student.Pincode);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    class Student
    {
        public string RollNumber { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
    }
}
