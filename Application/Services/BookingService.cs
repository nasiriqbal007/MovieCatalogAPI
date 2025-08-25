using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.DTOs;

using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class BookingService
    {
        private readonly AppDbContext _appDb;

        public BookingService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto dto, int userId)
        {
            var showTime = await _appDb.ShowTimes
                .Include(st => st.Movie)
                .FirstOrDefaultAsync(st => st.Id == dto.ShowTimeId);

            if (showTime == null)
                throw new Exception("ShowTime not found");

            if (dto.NumberOfSeats <= 0)
                throw new Exception("Number of seats must be greater than 0");

            if (dto.NumberOfSeats > showTime.AvailableSeats)
                throw new Exception("Not enough seats available");


            var booking = dto.ToEntity(showTime, userId);


            showTime.AvailableSeats -= dto.NumberOfSeats;

            _appDb.Bookings.Add(booking);
            await _appDb.SaveChangesAsync();

            return booking.ToDto();
        }

        public async Task<List<BookingResponseDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _appDb.Bookings
                .Include(b => b.ShowTime).ThenInclude(st => st.Movie)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookings.Select(b => b.ToDto()).ToList();
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id, int userId)
        {
            var booking = await _appDb.Bookings
                .Include(b => b.ShowTime).ThenInclude(st => st.Movie)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            return booking?.ToDto();
        }

    }
}
