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
            public static string[] DepartmentNames = { "Menswear", "Womenswear", "Childrenswear", "Footwear", "Accessories", "Sportswear", "Outerwear", "Intimates" };

            /// <summary>
            /// Gets the abbreviations of the departments.
            /// </summary>
            public static string[] DepartmentAbbreviations = { "MENS", "WMNS", "CHLD", "FOOT", "ACCS", "SPORT", "OUTR", "INTM" };
        }

        public struct ManufacturingSites
        {
            public static string[] manSites = { "US1", "US2", "UK1", "UK2", "FR1", "FR2", "DE1", "DE2", "JP1", "JP2" };
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[1000];
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                salesData[i].dateSold = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = ProdDepartments.DepartmentNames[random.Next(0, ProdDepartments.DepartmentNames.Length)];

                int indexOfDept = Array.IndexOf(ProdDepartments.DepartmentNames, salesData[i].departmentName);
                string deptAbb = ProdDepartments.DepartmentAbbreviations[indexOfDept];
                string firstDigit = (indexOfDept + 1).ToString();
                string nextTwoDigits = random.Next(1, 100).ToString("D2");
                sizeCode = new string[] { "XS", "S", "M", "L", "XL" }[random.Next(0, 5)];
                string[] colorCodes = { "BK", "BL", "GR", "RD", "YL", "OR", "WT", "GY" };
                string colorCode = colorCodes[random.Next(0, colorCodes.Length)];
                string[] manufacturingSites = ManufacturingSites.manSites;
                int randomIndex = random.Next(0, manufacturingSites.Length);
                string manufacturingSite = manufacturingSites[randomIndex];

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

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
                // calculate the total sales for each quarter
                string quarter = GetQuarter(data.dateSold.Month);
                double totalSales = data.quantitySold * data.unitPrice;

                if (quarterlySales.ContainsKey(quarter))
                {
                    quarterlySales[quarter] += totalSales;
                }
                else
                {
                    quarterlySales.Add(quarter, totalSales);
                }
            }

            // display the quarterly sales report
            Console.WriteLine("Quarterly Sales Report");
            Console.WriteLine("----------------------");
            foreach (KeyValuePair<string, double> quarter in quarterlySales)
            {
                Console.WriteLine("{0}: ${1}", quarter.Key, quarter.Value);
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
