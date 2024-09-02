using System;
using System.Collections.Generic;

/*
Copilot translated/generated this code (from the Python version) and this C# code will not run, hmmm

$ dotnet run
Quarterly Sales Report
----------------------
Unhandled exception. System.Collections.Generic.KeyNotFoundException: The given key 'Q1' was not present in the dictionary.
   at System.Collections.Generic.Dictionary`2.get_Item(TKey key)
   at QuarterlyIncomeReportApp.QuarterlyIncomeReport.QuarterlySalesReport(List`1 salesData) in /workspaces/mslearn-SampleApps/APL2007M3B/Program.cs:line 161
   at QuarterlyIncomeReportApp.QuarterlyIncomeReport.Main() in /workspaces/mslearn-SampleApps/APL2007M3B/Program.cs:line 17
   at QuarterlyIncomeReportApp.Program.Main() in /workspaces/mslearn-SampleApps/APL2007M3B/Program.cs:line 253
*/

namespace QuarterlyIncomeReportApp
{
    public class QuarterlyIncomeReport
    {
        public void Main()
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();

            // call the GenerateSalesData method
            List<SalesData> salesData = report.GenerateSalesData();

            // call the QuarterlySalesReport method
            report.QuarterlySalesReport(salesData);
        }

        // public struct SalesData includes the following fields: date sold, department name, product ID, quantity sold, unit price
        public struct SalesData
        {
            public DateTime DateSold { get; set; }
            public string DepartmentName { get; set; }
            public string ProductId { get; set; }
            public int QuantitySold { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal BaseCost { get; set; }
            public int VolumeDiscount { get; set; }
        }

        public List<SalesData> GenerateSalesData()
        {
            List<SalesData> salesData = new List<SalesData>();
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                DateTime dateSold = new DateTime(2023, random.Next(1, 13), random.Next(1, 29));
                string departmentName = ProdDepartments.DepartmentNames[random.Next(0, ProdDepartments.DepartmentNames.Length)];
                string departmentAbbreviation = ProdDepartments.DepartmentAbbreviations[Array.IndexOf(ProdDepartments.DepartmentNames, departmentName)];
                int departmentIndex = Array.IndexOf(ProdDepartments.DepartmentNames, departmentName) + 1;
                string firstDigit = departmentIndex.ToString();
                string nextTwoDigits = random.Next(1, 100).ToString().PadLeft(2, '0');
                string sizeCode = new string[] { "XS", "S", "M", "L", "XL" }[random.Next(0, 5)];
                string colorCode = new string[] { "BK", "BL", "GR", "RD", "YL", "OR", "WT", "GY" }[random.Next(0, 8)];
                string manufacturingSite = ManufacturingSites.Sites[random.Next(0, ManufacturingSites.Sites.Length)];
                string productId = $"{departmentAbbreviation}-{firstDigit}{nextTwoDigits}-{sizeCode}-{colorCode}-{manufacturingSite}";
                int quantitySold = random.Next(1, 101);
                decimal unitPrice = random.Next(25, 300) + random.Next(0, 100) / 100m;
                decimal baseCost = unitPrice * (1 - random.Next(5, 21) / 100m);
                int volumeDiscount = (int)(quantitySold * 0.1);
                SalesData salesDataItem = new SalesData
                {
                    DateSold = dateSold,
                    DepartmentName = departmentName,
                    ProductId = productId,
                    QuantitySold = quantitySold,
                    UnitPrice = unitPrice,
                    BaseCost = baseCost,
                    VolumeDiscount = volumeDiscount
                };
                salesData.Add(salesDataItem);
            }

            return salesData;
        }

        public void QuarterlySalesReport(List<SalesData> salesData)
        {
            // create a dictionary to store the quarterly sales data
            Dictionary<string, decimal> quarterlySales = new Dictionary<string, decimal>();
            Dictionary<string, decimal> quarterlyProfit = new Dictionary<string, decimal>();
            Dictionary<string, decimal> quarterlyProfitPercentage = new Dictionary<string, decimal>();

            // create a dictionary to store the quarterly sales data by department
            Dictionary<string, Dictionary<string, decimal>> quarterlySalesByDepartment = new Dictionary<string, Dictionary<string, decimal>>();
            Dictionary<string, Dictionary<string, decimal>> quarterlyProfitByDepartment = new Dictionary<string, Dictionary<string, decimal>>();
            Dictionary<string, Dictionary<string, decimal>> quarterlyProfitPercentageByDepartment = new Dictionary<string, Dictionary<string, decimal>>();

            // create a dictionary to store the top 3 sales orders by quarter
            Dictionary<string, List<SalesData>> top3SalesOrdersByQuarter = new Dictionary<string, List<SalesData>>();

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
                // calculate the total sales for each quarter
                string quarter = GetQuarter(data.DateSold.Month);
                decimal totalSales = data.QuantitySold * data.UnitPrice;
                decimal totalCost = data.QuantitySold * data.BaseCost;
                decimal profit = totalSales - totalCost;
                decimal profitPercentage = (profit / totalSales) * 100;

                // calculate the total sales, profit, and profit percentage by department
                if (!quarterlySalesByDepartment.ContainsKey(quarter))
                {
                    quarterlySalesByDepartment[quarter] = new Dictionary<string, decimal>();
                    quarterlyProfitByDepartment[quarter] = new Dictionary<string, decimal>();
                    quarterlyProfitPercentageByDepartment[quarter] = new Dictionary<string, decimal>();
                }

                if (!quarterlySalesByDepartment[quarter].ContainsKey(data.DepartmentName))
                {
                    quarterlySalesByDepartment[quarter][data.DepartmentName] = 0;
                    quarterlyProfitByDepartment[quarter][data.DepartmentName] = 0;
                    quarterlyProfitPercentageByDepartment[quarter][data.DepartmentName] = 0;
                }

                quarterlySalesByDepartment[quarter][data.DepartmentName] += totalSales;
                quarterlyProfitByDepartment[quarter][data.DepartmentName] += profit;
                quarterlyProfitPercentageByDepartment[quarter][data.DepartmentName] = profitPercentage;

                // calculate the total sales and profit for each quarter
                if (!quarterlySales.ContainsKey(quarter))
                {
                    quarterlySales[quarter] = 0;
                    quarterlyProfit[quarter] = 0;
                }

                quarterlySales[quarter] += totalSales;
                quarterlyProfit[quarter] += profit;

                // add the sales data to the top 3 sales orders by quarter
                if (!top3SalesOrdersByQuarter.ContainsKey(quarter))
                {
                    top3SalesOrdersByQuarter[quarter] = new List<SalesData>();
                }

                top3SalesOrdersByQuarter[quarter].Add(data);
            }

            // sort the top 3 sales orders by profit in descending order
            foreach (string quarter in top3SalesOrdersByQuarter.Keys)
            {
                top3SalesOrdersByQuarter[quarter].Sort((order1, order2) =>
                {
                    decimal profit1 = (order1.QuantitySold * order1.UnitPrice) - (order1.QuantitySold * order1.BaseCost);
                    decimal profit2 = (order2.QuantitySold * order2.UnitPrice) - (order2.QuantitySold * order2.BaseCost);
                    return profit2.CompareTo(profit1);
                });

                top3SalesOrdersByQuarter[quarter] = top3SalesOrdersByQuarter[quarter].GetRange(0, Math.Min(top3SalesOrdersByQuarter[quarter].Count, 3));
            }

            // display the quarterly sales report
            Console.WriteLine("Quarterly Sales Report");
            Console.WriteLine("----------------------");

            // sort the quarterly sales by key (quarter)
            List<KeyValuePair<string, decimal>> sortedQuarterlySales = new List<KeyValuePair<string, decimal>>(quarterlySales);
            sortedQuarterlySales.Sort((item1, item2) => item1.Key.CompareTo(item2.Key));

            foreach (KeyValuePair<string, decimal> item in sortedQuarterlySales)
            {
                string quarter = item.Key;
                decimal salesAmount = item.Value;

                // format the sales amount as currency using regional settings
                string formattedSalesAmount = salesAmount.ToString("C");
                string formattedProfitAmount = quarterlyProfit[quarter].ToString("C");
                string formattedProfitPercentage = quarterlyProfitPercentage[quarter].ToString("F2");

                Console.WriteLine($"{quarter}: Sales: {formattedSalesAmount}, Profit: {formattedProfitAmount}, Profit Percentage: {formattedProfitPercentage}%");

                // display the quarterly sales, profit, and profit percentage by department
                Console.WriteLine("By Department:");

                List<KeyValuePair<string, decimal>> sortedQuarterlySalesByDepartment = new List<KeyValuePair<string, decimal>>(quarterlySalesByDepartment[quarter]);
                sortedQuarterlySalesByDepartment.Sort((item1, item2) => item1.Key.CompareTo(item2.Key));

                // Print table headers
                Console.WriteLine("┌───────────────────────┬───────────────────┬───────────────────┬───────────────────┐");
                Console.WriteLine("│      Department       │       Sales       │       Profit      │ Profit Percentage │");
                Console.WriteLine("├───────────────────────┼───────────────────┼───────────────────┼───────────────────┤");

                foreach (KeyValuePair<string, decimal> departmentItem in sortedQuarterlySalesByDepartment)
                {
                    string department = departmentItem.Key;
                    decimal departmentSalesAmount = departmentItem.Value;

                    string formattedDepartmentSalesAmount = departmentSalesAmount.ToString("C");
                    string formattedDepartmentProfitAmount = quarterlyProfitByDepartment[quarter][department].ToString("C");
                    string formattedDepartmentProfitPercentage = quarterlyProfitPercentageByDepartment[quarter][department].ToString("F2");

                    Console.WriteLine($"│ {department,-22}│ {formattedDepartmentSalesAmount,17} │ {formattedDepartmentProfitAmount,17} │ {formattedDepartmentProfitPercentage,17} │");
                }

                Console.WriteLine("└───────────────────────┴───────────────────┴───────────────────┴───────────────────┘");
                Console.WriteLine();

                // display the top 3 sales orders for the quarter
                Console.WriteLine("Top 3 Sales Orders:");

                List<SalesData> top3SalesOrders = top3SalesOrdersByQuarter[quarter];

                // Print table headers
                Console.WriteLine("┌───────────────────────┬───────────────────┬───────────────────┬───────────────────┬───────────────────┬───────────────────┐");
                Console.WriteLine("│      Product ID       │   Quantity Sold   │    Unit Price     │   Total Sales     │      Profit       │ Profit Percentage │");
                Console.WriteLine("├───────────────────────┼───────────────────┼───────────────────┼───────────────────┼───────────────────┼───────────────────┤");

                foreach (SalesData salesOrder in top3SalesOrders)
                {
                    decimal orderTotalSales = salesOrder.QuantitySold * salesOrder.UnitPrice;
                    decimal orderProfit = orderTotalSales - (salesOrder.QuantitySold * salesOrder.BaseCost);
                    decimal orderProfitPercentage = (orderProfit / orderTotalSales) * 100;

                    Console.WriteLine($"│ {salesOrder.ProductId,-22}│ {salesOrder.QuantitySold,17} │ {salesOrder.UnitPrice,17:F2} │ {orderTotalSales,17:F2} │ {orderProfit,17:F2} │ {orderProfitPercentage,17:F2} │");
                }

                Console.WriteLine("└───────────────────────┴───────────────────┴───────────────────┴───────────────────┴───────────────────┴───────────────────┘");
                Console.WriteLine();
            }
        }

        public string GetQuarter(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return "Q1";
            }
            else if (month >= 4 && month <= 6)
            {
                return "Q2";
            }
            else if (month >= 7 && month <= 9)
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }

        public static class ProdDepartments
        {
            public static string[] DepartmentNames = { "Men's Clothing", "Women's Clothing", "Children's Clothing", "Accessories", "Footwear", "Outerwear", "Sportswear", "Undergarments" };
            public static string[] DepartmentAbbreviations = { "MENS", "WOMN", "CHLD", "ACCS", "FOOT", "OUTR", "SPRT", "UNDR" };
        }

        public static class ManufacturingSites
        {
            public static string[] Sites = { "US1", "US2", "US3", "UK1", "UK2", "UK3", "JP1", "JP2", "JP3", "CA1" };
        }
    }

    public class Program
    {
        public static void Main()
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();
            report.Main();
        }
    }
}