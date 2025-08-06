using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Const
{
    static public class DataTables
    {

        public static DataTable GetCustomDatesTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Date", typeof(DateTime));

            return table;
        }

        public static DataTable GetWeekDaysTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Day", typeof(byte));

            return table;
        }

		public static DataTable GetBookedSeatsTable()
		{
			DataTable table = new DataTable();


	    	table.Columns.Add("SeatNumber", typeof(int));
			table.Columns.Add("PersonId", typeof(string));
			table.Columns.Add("PersonName", typeof(string));
			table.Columns.Add("PersonGender", typeof(int));

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
