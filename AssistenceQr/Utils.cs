using System.Data;
using System.Reflection;

namespace AssistanceQr
{
    public static class Utils
    {

        public static string[] headers = new string[] { "DNI", "A_PATERNO", "A_MATERNO", "NOMBRES", "E_MAIL", "TELEFONO","T.PARTICIPANTE" };
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
    public class AssistanceLoad
    {
        public string DNI { get; set; } = string.Empty;
        public string A_PATERNO { get; set; } = string.Empty;
        public string A_MATERNO { get; set; } = string.Empty;
        public string NOMBRES { get; set; } = string.Empty;
        public string E_MAIL { get; set; } = string.Empty;
        public string TELEFONO { get; set; } = string.Empty;
        public Models.ParticipantType T_PARTICIPANT { get; set; }


    }


}
