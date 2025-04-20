using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Const
{
    static public class DataTables
    {

        public static DataTable GetDatesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Date", typeof(DateTime));

            return table;
        }

        public static DataTable GetDaysTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Day", typeof(byte));

            return table;
        }



        //private static  DataTable? _dates;
        //public static DataTable Dates => _dates ??= _initialDatesTable();



        //private static DataTable _initialDatesTable()
        //{
        //    DataTable table=new DataTable();

        //    table.Columns.Add("Date", typeof(DateTime));

        //    return table;
        //}
    }
}
