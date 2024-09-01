using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
    class QuarterlyIncomeReport
    {
        static void Main(string[] args)
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();

            // call the GenerateSalesData method
            SalesData[] salesData = report.GenerateSalesData();
            
            // call the QuarterlySalesReport method
            report.QuarterlySalesReport(salesData);
        }

        /* public struct SalesData includes the following fields: date sold, department name, product ID, quantity sold, unit price */
        public struct SalesData
        {
            public DateOnly dateSold;
            public string departmentName;
            public string productID;
            public int quantitySold;
            public double unitPrice;
            public double baseCost;
            public int volumeDiscount;
        }
        
        /// <summary>
        /// Represents the product departments.
        /// </summary>
        public struct ProdDepartments
        {
            /// <summary>
            /// Gets the names of the departments.
            /// </summary>
            public static string[] departmentNames = { "Menswear", "Womenswear", "Childrenswear", "Footwear", "Accessories", "Sportswear", "Outerwear", "Intimates" };

            /// <summary>
            /// Gets the abbreviations of the departments.
            /// </summary>
            public static string[] departmentAbbreviations = { "MENS", "WMNS", "CHLD", "FOOT", "ACCS", "SPORT", "OUTR", "INTM" };
        }

        public struct ManufacturingSites
        {
            public static string[] manufacturingSites = { "US1", "US2", "UK1", "UK2", "FR1", "FR2", "DE1", "DE2", "JP1", "JP2" };
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[1000];
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                salesData[i].dateSold = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = ProdDepartments.departmentNames[random.Next(0, ProdDepartments.departmentNames.Length)];

                int indexOfDept = Array.IndexOf(ProdDepartments.departmentNames, salesData[i].departmentName);
                string deptAbb = ProdDepartments.departmentAbbreviations[indexOfDept];
                string firstDigit = (indexOfDept + 1).ToString();
                string nextTwoDigits = random.Next(1, 100).ToString("D2");
                string sizeCode = new string[] { "XS", "S", "M", "L", "XL" }[random.Next(0, 5)];
                string colorCode = new string[] { "BK", "BL", "GR", "RD", "YL", "OR", "WT", "GY" }[random.Next(0, 8)];
                string manufacturingSite = ManufacturingSites.manufacturingSites[random.Next(0, ManufacturingSites.manufacturingSites.Length)];

                salesData[i].productID = $"{deptAbb}-{firstDigit}{nextTwoDigits}-{sizeCode}-{colorCode}-{manufacturingSite}";
                salesData[i].quantitySold = random.Next(1, 101);
                salesData[i].unitPrice = random.Next(25, 300) + random.NextDouble();
                salesData[i].baseCost = salesData[i].unitPrice * (1 - (random.Next(5, 21) / 100.0));
                salesData[i].volumeDiscount = (int)(salesData[i].quantitySold * 0.1);

            }

            return salesData;
        }

        public void QuarterlySalesReport(SalesData[] salesData)
        {
            // create a dictionary to store the quarterly sales data
            Dictionary<string, double> quarterlySales = new Dictionary<string, double>();
            Dictionary<string, double> quarterlyProfit = new Dictionary<string, double>();
            Dictionary<string, double> quarterlyProfitPercentage = new Dictionary<string, double>();

            // create a dictionary to store the quarterly sales data by department
            Dictionary<string, Dictionary<string, double>> departmentQuarterlySales = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, double>> departmentQuarterlyProfit = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, double>> departmentQuarterlyProfitPercentage = new Dictionary<string, Dictionary<string, double>>();

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
            // calculate the total sales for each quarter
            string quarter = GetQuarter(data.dateSold.Month);
            double totalSales = data.quantitySold * data.unitPrice;
            double totalCost = data.quantitySold * data.baseCost;
            double profit = totalSales - totalCost;
            double profitPercentage = (profit / totalSales) * 100;

            // calculate the total sales, profit, and profit percentage by department
            if (departmentQuarterlySales.ContainsKey(data.departmentName))
            {
                if (departmentQuarterlySales[data.departmentName].ContainsKey(quarter))
                {
                departmentQuarterlySales[data.departmentName][quarter] += totalSales;
                departmentQuarterlyProfit[data.departmentName][quarter] += profit;
                }
                else
                {
                departmentQuarterlySales[data.departmentName].Add(quarter, totalSales);
                departmentQuarterlyProfit[data.departmentName].Add(quarter, profit);
                }
            }
            else
            {
                departmentQuarterlySales.Add(data.departmentName, new Dictionary<string, double> { { quarter, totalSales } });
                departmentQuarterlyProfit.Add(data.departmentName, new Dictionary<string, double> { { quarter, profit } });
            }

            if (!departmentQuarterlyProfitPercentage.ContainsKey(data.departmentName))
            {
                departmentQuarterlyProfitPercentage.Add(data.departmentName, new Dictionary<string, double> { { quarter, profitPercentage } });
            }
            else if (!departmentQuarterlyProfitPercentage[data.departmentName].ContainsKey(quarter))
            {
                departmentQuarterlyProfitPercentage[data.departmentName].Add(quarter, profitPercentage);
            }

            // calculate the total sales, profit, and profit percentage for all departments
            if (quarterlySales.ContainsKey(quarter))
            {
                quarterlySales[quarter] += totalSales;
                quarterlyProfit[quarter] += profit;
            }
            else
            {
                quarterlySales.Add(quarter, totalSales);
                quarterlyProfit.Add(quarter, profit);
            }

            if (!quarterlyProfitPercentage.ContainsKey(quarter))
            {
                quarterlyProfitPercentage.Add(quarter, profitPercentage);
            }
            }

            // display the quarterly sales report by department
            Console.WriteLine("Quarterly Sales Report by Department");
            Console.WriteLine("------------------------------------");
            foreach (KeyValuePair<string, Dictionary<string, double>> department in departmentQuarterlySales.OrderBy(d => d.Key))
            {
            Console.WriteLine("Department: {0}", department.Key);
            foreach (KeyValuePair<string, double> quarter in department.Value.OrderBy(q => q.Key))
            {
                Console.WriteLine("{0}: Sales - {1}, Profit - {2}, Profit Percentage - {3}%", quarter.Key, quarter.Value.ToString("C"), departmentQuarterlyProfit[department.Key][quarter.Key].ToString("C"), departmentQuarterlyProfitPercentage[department.Key][quarter.Key].ToString("F2"));
            }
            Console.WriteLine();
            }

            // display the overall quarterly sales report
            Console.WriteLine("Overall Quarterly Sales Report");
            Console.WriteLine("------------------------------");
            foreach (KeyValuePair<string, double> quarter in quarterlySales.OrderBy(q => q.Key))
            {
            Console.WriteLine("{0}: Sales - {1}, Profit - {2}, Profit Percentage - {3}%", quarter.Key, quarter.Value.ToString("C"), quarterlyProfit[quarter.Key].ToString("C"), quarterlyProfitPercentage[quarter.Key].ToString("F2"));
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
    }
}
