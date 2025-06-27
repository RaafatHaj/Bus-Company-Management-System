using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Recurrings
{
    public class RecurringServcie : IRecurringServcie
    {


        private readonly IUnitOfWork _unitOfWork;

        public RecurringServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //public async Task<IEnumerable<TripPattern>> GetRouteTripPattern(int routeId)
        //{
        //    return await _unitOfWork.Recurrings.GetQueryable()
        //                  .Where(r => r.RouteId == routeId)
        //                  .ToListAsync();
        //}


        //public async Task<bool> SetRecurringPattern(IList<SetRecurringDTO> patterns)
        //{

        //    await _unitOfWork.BeginTransactionAsync();

        //    try
        //    {
        //        foreach (var pattern in patterns)
        //        {
        //            var startingDate = await _unitOfWork.Recurrings.GetQueryable()
        //                                     .Where(r => r.RouteId == pattern.RouteId && r.Time == pattern.Time)
        //                                     .Select(r => r.StartDate)
        //                                     .SingleOrDefaultAsync();

        //            if (startingDate == null)
        //                startingDate = pattern.StartDate;



        //            var trips = await _unitOfWork.Trips.GetQueryable()
        //                              .Where(t=> t.RouteId==pattern.RouteId && t.Time==pattern.Time && t.Date >= startingDate)
        //                              .ToListAsync();

        //            var tripDateSet = new HashSet<DateTime>(trips.Select(d => d.Date.Date));

        //            DateTime minDate = tripDateSet.Min();
        //            DateTime maxDate = tripDateSet.Max();


        //            // _tripRepository.Add(trip) etc.
        //        }

        //        await _unitOfWork.CommitTransactionAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        await _unitOfWork.RollbackTransactionAsync();
        //        return false;
        //        throw;
        //    }


        //    //using var transaction = await _dbContext.Database.BeginTransactionAsync();
        //    //try
        //    //{
        //    //    foreach (var trip in trips)
        //    //    {
        //    //        // Set up recurring based on trip
        //    //        // _dbContext.Recurrings.Add(new Recurring { ... });
        //    //    }

        //    //    await _dbContext.SaveChangesAsync();
        //    //    await transaction.CommitAsync();
        //    //    return true;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    await transaction.RollbackAsync();
        //    //    return false;
        //    //}
        //}

        class Program
        {
            static void Main(string[] args)
            {
                // Simulate loading trip dates from database
                var tripDates = LoadTripDates();

                // Normalize trip dates to Date only
                var tripDateSet = new HashSet<DateTime>(tripDates.Select(d => d.Date));

                // Find range
                DateTime minDate = tripDateSet.Min();
                DateTime maxDate = tripDateSet.Max();

                // Start from the first day of the first week
                DateTime currentWeekStart = FirstDayOfWeek(minDate);
                DateTime currentWeekEnd = currentWeekStart.AddDays(6);

                var weekPatterns = new List<WeekPattern>();

                // Process each week
                while (currentWeekStart <= maxDate)
                {
                    var daysWithTrips = new HashSet<DayOfWeek>();

                    for (var day = currentWeekStart; day <= currentWeekEnd && day <= maxDate; day = day.AddDays(1))
                    {
                        if (tripDateSet.Contains(day))
                        {
                            daysWithTrips.Add(day.DayOfWeek);
                        }
                    }

                    var pattern = (daysWithTrips.Count == 7) ? "Daily" : "Weekly";

                    weekPatterns.Add(new WeekPattern
                    {
                        WeekNumber = ISOWeek.GetWeekOfYear(currentWeekStart),
                        StartDate = currentWeekStart,
                        EndDate = currentWeekEnd > maxDate ? maxDate : currentWeekEnd,
                        Pattern = pattern,
                        DaysWithTrips = daysWithTrips
                    });

                    currentWeekStart = currentWeekStart.AddDays(7);
                    currentWeekEnd = currentWeekStart.AddDays(6);
                }

                // Group by Days Pattern
                var grouped = weekPatterns
                    .GroupBy(w => GetDaysSignature(w.DaysWithTrips))
                    .Select(g => new
                    {
                        DaysPattern = g.Key,
                        Pattern = g.First().Pattern,
                        WeeksCount = g.Count()
                    })
                    .ToList();

                // Print the result
                foreach (var group in grouped)
                {
                    Console.WriteLine($"{group.Pattern} ({group.DaysPattern}) => {group.WeeksCount} Week(s)");
                }
            }

            static List<DateTime> LoadTripDates()
            {
                // 🔥 This simulates your database trips
                var trips = new List<DateTime>();

                var random = new Random();
                var startDate = new DateTime(2025, 1, 1);
                var endDate = new DateTime(2025, 6, 30);

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    // Randomly choose days that have trips
                    if (random.NextDouble() < 0.5) // 50% chance there is a trip that day
                    {
                        trips.Add(date);
                    }
                }

                return trips;
            }

            static DateTime FirstDayOfWeek(DateTime date)
            {
                var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
                return date.AddDays(-1 * diff).Date;
            }

            static string GetDaysSignature(HashSet<DayOfWeek> days)
            {
                return string.Join(",", days.OrderBy(d => d)); // Standard order
            }
        }

        class WeekPattern
        {
            public int WeekNumber { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Pattern { get; set; }
            public HashSet<DayOfWeek> DaysWithTrips { get; set; }
        }




    }
}
