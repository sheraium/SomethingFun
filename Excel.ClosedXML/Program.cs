using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace Excel.ClosedXML
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var workbook = new XLWorkbook();
            //var worksheet = workbook.AddWorksheet("Test");
            //worksheet.Cell(1, 1).Value = "TEST";
            //worksheet.Cell(1, 2).DataType = XLDataType.Text;
            //worksheet.Cell(1, 2).SetValue("0001");
            //worksheet.Cell(1, 3).Value = "0xF001";

            //var cars = new List<Car>
            //{
            //    new Car(){Id = 1, Name = "111"},
            //    new Car(){Id = 2, Name = "222"}
            //};

            //worksheet.Cell(2, 1).Value = cars;
            //workbook.SaveAs("Test1.xlsx");

            //var test1 = new XLWorkbook("Test1.xlsx");
            //var test1Ws = test1.Worksheets.First();

            //var firstCell = test1Ws.FirstCellUsed();
            //var lastCell = test1Ws.LastCellUsed();

            //var data = test1Ws.Range(firstCell.Address, lastCell.Address);
            //foreach (var row in data.Rows())
            //{
            //    // 取值
            //    row.Cell(1).Value.ToString();
            //    row.Cell(2).Value.ToString();
            //    row.Cell(3).Value.ToString();
            //}

            var type = typeof(Car);
            var data = new List<Car>();
            using (var workbook = new XLWorkbook("Test1.xlsx"))
            {
                var index = 1;
                var columns = new Dictionary<int, string>();
                foreach (var propertyInfo in type.GetProperties())
                {
                    columns.Add(index, propertyInfo.Name);
                    index++;
                }

                var ws = workbook.Worksheets.First();
                var firstCell = ws.FirstCellUsed();
                var lastCell = ws.LastCellUsed();
                var range = ws.Range(firstCell.Address, lastCell.Address);

                foreach (var row in range.Rows().Skip(1))
                {
                    try
                    {
                        var instance = (Car)Activator.CreateInstance(typeof(Car));
                        foreach (var column in columns)
                        {
                            var propertyInfo = type.GetProperty(column.Value);
                            var value = Convert.ChangeType(row.Cell(column.Key).Value, propertyInfo.PropertyType);
                            propertyInfo?.SetValue(instance, value);
                        }
                        data.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}