using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;
using BusinessObjects;
using Sevices;
using System.Windows;

namespace WpfApp.ViewModels
{
    public class AdminReportViewModel : INotifyPropertyChanged
    {
        private readonly IBookingReservationService _bookingReservationService;
        private SeriesCollection _seriesCollection;
        private string[] _labels;
        private Func<decimal, string> _formatter;
        private DateOnly? _startDate;
        private DateOnly? _endDate;

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }

        public Func<decimal, string> Formatter
        {
            get => _formatter;
            set
            {
                _formatter = value;
                OnPropertyChanged();
            }
        }

        public AdminReportViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
            LoadChartData();
        }

        private void LoadChartData()
        {
            var bookings = _bookingReservationService.GetAll();
            var groupedBookings = bookings
                .OrderBy(b => b.BookingDate)
                .ToList();

            var values = new ChartValues<decimal>();
            var labels = new List<string>();

            foreach (var group in groupedBookings)
            {
                values.Add(group.TotalPrice ?? 0);
                labels.Add(group.BookingDate.ToString());
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Price",
                    Values = values
                }
            };

            Labels = labels.ToArray();
            Formatter = value => value.ToString("C"); // Currency format
        }

        public DateOnly? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateOnly? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public void FilterReportByDateRange(DateOnly StartDate, DateOnly EndDate)
        {
            if (StartDate >= EndDate)
            {
                bool? Result = new MessageBoxCustom("Start Date must before End Date", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }

            var bookings = _bookingReservationService.GetAll();
            var filteredBookings = bookings
                .Where(b => b.BookingDate >= StartDate && b.BookingDate <= EndDate)
                .OrderBy(b => b.BookingDate)
                .ToList();
            if(filteredBookings.Count == 0)
            {
                bool? Result = new MessageBoxCustom("No data found", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            UpdateChart(filteredBookings);
        }

        private void UpdateChart(List<BookingReservation> bookings)
        {
            var values = new ChartValues<decimal>();
            var labels = new List<string>();

            foreach (var booking in bookings)
            {
                values.Add(booking.TotalPrice ?? 0);
                labels.Add(booking.BookingDate.ToString()); // Using short date format
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Price",
                    Values = values
                }
            };

            Labels = labels.ToArray();
            Formatter = value => value.ToString("C"); // Currency format
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}