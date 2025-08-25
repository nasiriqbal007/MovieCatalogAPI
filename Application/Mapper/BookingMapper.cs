using MovieCatalogAPI.Application.DTOs;
using MovieCatalogAPI.Domain;

public static class BookingMapper
{
    public static Booking ToEntity(this BookingRequestDto dto, ShowTime showTime, int userId)
    {
        return new Booking
        {
            ShowTimeId = dto.ShowTimeId,
            UserId = userId,
            NumberOfSeats = dto.NumberOfSeats,
            TotalPrice = dto.NumberOfSeats * showTime.TicketPrice,
            ShowTime = showTime,

        };
    }

    public static BookingResponseDto ToDto(this Booking booking)
    {
        return new BookingResponseDto
        {
            Id = booking.Id,
            ShowTimeId = booking.ShowTimeId,
            MovieTitle = booking.ShowTime.Movie.Title,
            ShowTime = booking.ShowTime.StartTime,
            UserId = booking.UserId,
            NumberOfSeats = booking.NumberOfSeats,
            TotalPrice = booking.TotalPrice,
            BookedAt = booking.BookedAt
        };
    }
}
