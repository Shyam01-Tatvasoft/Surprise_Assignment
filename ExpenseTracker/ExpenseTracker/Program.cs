// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpenseTracker;
public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }

    public Expense(int id, string description, double amount, string category)
    {
        Id = id;
        Description = description;
        Amount = amount;
        Category = category;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Description: {Description}, Amount: {Amount}, Category: {Category}";
    }
}

public class Tracker
{
    public List<Expense> expenses = new List<Expense>();

    public void AddExpense()
    {
        string description;
        string amount;
        string category;

        Console.Write("Enter description: ");
        description = Console.ReadLine();

        Console.Write("Enter amount: ");
        amount = Console.ReadLine();
        // while (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        // {
        //     Console.Write("Invalid amount. Enter a valid amount: ");
        // }

        Console.Write("Enter category: ");
        category = Console.ReadLine();

        bool isValid = validateData(null, description, amount, category);
        while (!isValid)
        {
            AddExpense();
        }

        int id = expenses.Count > 0 ? expenses.Select(s => s.Id).Max() : 0;

        expenses.Add(new Expense(++id, description, double.Parse(amount), category));
        Console.WriteLine("Expense added successfully!");
        Console.ReadKey();
    }

    public bool validateData(string id = null, string descreption = null, string amount = null, string category = null)
    {
        List<string> errors = new List<string>();

        if (!string.IsNullOrEmpty(descreption) && descreption.Length > 50)
        {
            errors.Add("Invalid descreption. Please enter a descreption value less than 50 charechters.");
        }

        if (!string.IsNullOrEmpty(id) && (!int.TryParse(id, out int parsedId) || parsedId <= 0))
        {
            errors.Add("Invalid id. Please enter a numeric and postive value.");
        }

        if (!string.IsNullOrEmpty(amount) && (!double.TryParse(amount, out double parsedAmount) || parsedAmount <= 0))
        {
            errors.Add("Invalid amount. Please enter a numeric and postive value.");
        }

        if (string.IsNullOrEmpty(category))
        {
            Console.WriteLine("cat = " + category);
            errors.Add("Please enter a category.");
        }

        if (errors.Count > 0)
        {
            // throw new Exception(string.Join("\n", errors));
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return false;
        }
        else
        {
            return true;
        }

    }

    public void ViewAllExpenses()
    {
        Console.WriteLine("=================================== All Expenses ===================================");
        foreach (var expense in expenses)
        {
            Console.WriteLine(expense);
        }
        Console.ReadKey();
    }

    public void ExportExpenses()
    {
        Console.WriteLine("Export Expenses:");
        Console.WriteLine("1. Text File");
        Console.WriteLine("2. CSV File");
        Console.WriteLine("3. Excel File");
        Console.Write("Select an option: ");

        string filePath = "";
        switch (Console.ReadLine())
        {
            case "1":
                filePath = "expenses.txt";
                ExportToText(filePath);
                break;
            case "2":
                filePath = "expenses.csv";
                ExportToCsv(filePath);
                break;
            case "3":
                filePath = "expenses.xlsx";
                ExportToExcel(filePath);
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
        Console.ReadKey();
    }

    private void ExportToText(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var expense in expenses)
            {
                writer.WriteLine(expense);
            }
        }
        Console.WriteLine($"Expenses exported to {filePath}");
    }

    private void ExportToCsv(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Description,Amount,Category");
            foreach (var expense in expenses)
            {
                writer.WriteLine($"{expense.Description},{expense.Amount},{expense.Category}");
            }
        }
        Console.WriteLine($"Expenses exported to {filePath}");
    }

    private void ExportToExcel(string filePath)
    {
        // For simplicity, we'll just create a text file with a .xlsx extension
        ExportToText(filePath);
        Console.WriteLine($"Expenses exported to {filePath}");
    }

    public void CheckTotalSpentAmount()
    {
        double total = expenses.Sum(expense => expense.Amount);
        Console.WriteLine($"Total amount spent: {total}");
        Console.ReadKey();
    }

    public void CheckExpensesByCategory()
    {
        var categories = expenses.Select(expense => expense.Category).Distinct();
        Console.WriteLine("Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine(category);
        }

        Console.Write("Select a category: ");
        string selectedCategory = Console.ReadLine();

        var filteredExpenses = expenses.Where(expense => expense.Category.ToLower() == selectedCategory.ToLower()).ToList();
        Console.WriteLine($"Expenses in category {selectedCategory}:");
        foreach (var expense in filteredExpenses)
        {
            Console.WriteLine(expense);
        }

        double total = filteredExpenses.Sum(expense => expense.Amount);
        Console.WriteLine($"Total amount spent in category {selectedCategory}: {total}");
        Console.ReadKey();
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Tracker tracker = new Tracker();
        bool exit = false;

        Console.WriteLine("=================================== Welcome to Expense ===================================");
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Expense Tracker");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View All Expenses");
            Console.WriteLine("3. Export Expenses");
            Console.WriteLine("4. Check Total Spent Amount");
            Console.WriteLine("5. Check All Expenses by Category");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    tracker.AddExpense();
                    break;
                case "2":
                    tracker.ViewAllExpenses();
                    break;
                case "3":
                    tracker.ExportExpenses();
                    break;
                case "4":
                    tracker.CheckTotalSpentAmount();
                    break;
                case "5":
                    tracker.CheckExpensesByCategory();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }
}

