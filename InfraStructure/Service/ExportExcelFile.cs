using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace InfraStructure.Service
{
    public class ExportExcelFile<T>
    {

        IXLWorkbook wb;
        IXLWorksheet ws;
        public ExportExcelFile()
        {
            if (wb == null)
            {
                wb = new XLWorkbook();
                // ws = wb.Worksheets.Add("test");

            }
        }


        public void CreateWorkSheet(List<T> data, string sheetName = "Sheet", string[] header = null, string[] footer = null)
        {
            ws = wb.Worksheets.Add(sheetName);

            ws.RightToLeft = true;

            if (header != null)
            {
                CreateHeader(header);

            }
            if (data.Any())
            {
                CreateTableContent(data);
            }
            if (footer != null)
            {
                CreateFooter(footer);

            }

        }


        private void CreateHeader(string[] header)
        {

            for (int i = 0; i < header.Length; i++)
            {
                ws.Cell(1, i + 1).Value = header[i];

                ws.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(1, i + 1).Style.Font.Bold = true;
                ws.Cell(1, i + 1).Style.Font.FontSize = 14;
                ws.Cell(1, i + 1).Style.Fill.SetBackgroundColor(XLColor.Silver);

                ws.Cell(1, i + 1).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }
        }
        private void CreateFooter(string[] footer)
        {

            int lastRow = ws.LastRowUsed().RowNumber() + 1;
            for (int i = 0; i < footer.Length; i++)
            {
                ws.Cell(lastRow, i + 1).Value = footer[i];

                ws.Cell(lastRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(lastRow, i + 1).Style.Font.Bold = true;
                ws.Cell(lastRow, i + 1).Style.Font.FontSize = 14;
                ws.Cell(lastRow, i + 1).Style.Fill.SetBackgroundColor(XLColor.Silver);

                ws.Cell(lastRow, i + 1).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }
        }
        private void CreateTableContent(List<T> data)
        {



            for (int i = 0; i < data.Count; i++)
            {
                var type = data[i].GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
                var col = 1;

                foreach (PropertyInfo prop in props)
                {


                    object propValue = prop.GetValue(data[i], null);
                    if (prop.PropertyType == typeof(string))
                    {
                        ws.Cell(i + 2, col).SetDataType(XLDataType.Text);
                    }
                    if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(float))
                    {
                        ws.Cell(i + 2, col).SetDataType(XLDataType.Number);
                    }
                    if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateOnly) || prop.PropertyType == typeof(Nullable<DateTime>))
                    {
                        ws.Cell(i + 2, col).SetDataType(XLDataType.DateTime);
                    }
                    if (prop.PropertyType == typeof(bool))
                    {
                        ws.Cell(i + 2, col).SetDataType(XLDataType.Boolean);
                    }
                    ws.Cell(i + 2, col).Value = propValue;

                    ws.Cell(i + 2, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(i + 2, col).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);

                    col++;
                }
            }
        }


        public string SaveAs()
        {



            var filePath = Path.GetTempPath() + "/test.xlsx";
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);



            ws.Columns().AdjustToContents();
            wb.SaveAs(filePath);

            return filePath;
        }
    }
}