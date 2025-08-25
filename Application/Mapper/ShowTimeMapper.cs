using MovieCatalogAPI.Application.DTOs.ShowTime;
using MovieCatalogAPI.Application.DTOs.ShowTimes;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class ShowTimeMapper
    {
        public static ShowTime ToEnity(ShowTimeCreateDto dto, int totalSeats, DateTime dayOffset)
        {
            return new ShowTime
            {
                MovieId = dto.MovieId,
                CinemaId = dto.CinemaId,
                StartTime = dayOffset.Date + dto.StartTime,
                EndTime = dayOffset.Date + dto.EndTime,
                TicketPrice = dto.TicketPrice,
                AvailableSeats = totalSeats,
            };
        }

        public static void MapUpdate(ShowTime entity, ShowTimeUpdateDto dto)
        {
            if (dto.StartTime.HasValue && dto.StartDate.HasValue)
                entity.StartTime = dto.StartDate.Value.Date + dto.StartTime.Value;

            if (dto.EndTime.HasValue && dto.StartDate.HasValue)
                entity.EndTime = dto.StartDate.Value.Date + dto.EndTime.Value;

            if (dto.TicketPrice.HasValue)
                entity.TicketPrice = dto.TicketPrice.Value;

            if (dto.CinemaId.HasValue)
                entity.CinemaId = dto.CinemaId.Value;

            if (dto.MovieId.HasValue)
                entity.MovieId = dto.MovieId.Value;
        }

        public static ShowTimeResponseDto ToResponseDto(this ShowTime entity)
        {
            return new ShowTimeResponseDto
            {
                Id = entity.Id,
                MovieId = entity.MovieId,
                MovieTitle = entity.Movie.Title,
                CinemaId = entity.CinemaId,
                CinemaName = entity.Cinema.Name,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                TicketPrice = entity.TicketPrice,
                AvailableSeats = entity.AvailableSeats
            };
        }
    }
}
