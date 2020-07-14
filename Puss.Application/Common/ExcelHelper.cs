using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Puss.Application.Common
{
    public class ExcelHelper
    {
        public static List<T> ExportToList<T>(ISheet sheet, string[] fields) where T : class, new()
        {
            List<T> list = new List<T>();

            //遍历每一行数据
            for (int i = sheet.FirstRowNum + 1, len = sheet.LastRowNum + 1; i < len; i++)
            {
                T t = new T();
                IRow row = sheet.GetRow(i);

                for (int j = 0, len2 = fields.Length; j < len2; j++)
                {
                    Type propertyType = typeof(T).GetProperty(fields[j]).PropertyType; //获取当前属性的类型
                    ICell cell = row.GetCell(j);
                    object cellValue = null;

                    if (cell == null)
                    {
                        continue;
                    }

                    if (propertyType == typeof(string) | cell.CellType == CellType.Blank)
                    {
                        cell.SetCellType(CellType.String);
                        cellValue = cell.StringCellValue;
                    }
                    if (propertyType == typeof(int) && cell.CellType != CellType.Blank)
                    {
                        cell.SetCellType(CellType.Numeric);
                        cellValue = Convert.ToInt32(cell.NumericCellValue); //Double转int
                    }
                    if (propertyType == typeof(bool))
                    {
                        cell.SetCellType(CellType.Boolean);
                        cellValue = cell.BooleanCellValue;
                    }

                    typeof(T).GetProperty(fields[j]).SetValue(t, cellValue, null);
                }
                list.Add(t);
            }
            return list;
        }
    }
}
