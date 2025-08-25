using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.DTOs.ShowTime;
using MovieCatalogAPI.Application.DTOs.ShowTimes;
using MovieCatalogAPI.Application.Mapper;
using MovieCatalogAPI.Domain;
using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class ShowTimeService
    {
        private readonly AppDbContext _appDb;
        public ShowTimeService(AppDbContext appDb)
        {
            _appDb = appDb;
        }
        public async Task<List<ShowTimeResponseDto>> CreateShowTimeAsync(ShowTimeCreateDto dto)
        {
            var cinema = await _appDb.Cinemas.FindAsync(dto.CinemaId);
            if (cinema == null) throw new Exception("Cinema not found");
            var showTimes = new List<ShowTime>();
            for (int i = 0; i < dto.NumberOfDays; i++)
            {
                var date = dto.StartDate.AddDays(i);
                var showTime = new ShowTime
                {
                    MovieId = dto.MovieId,
                    CinemaId = dto.CinemaId,
                    StartTime = date.Date + dto.StartTime,
                    EndTime = date.Date + dto.EndTime,
                    TicketPrice = dto.TicketPrice,
                    AvailableSeats = cinema.TotalSeats
                };

                // Check overlapping per day
                var overlapping = await _appDb.ShowTimes.AnyAsync(st =>
                    st.CinemaId == dto.CinemaId &&
                    ((showTime.StartTime >= st.StartTime && showTime.StartTime < st.EndTime) ||
                     (showTime.EndTime > st.StartTime && showTime.EndTime <= st.EndTime))
                );
                if (overlapping) throw new Exception($"Overlapping show on {date:yyyy-MM-dd}");

                showTimes.Add(showTime);
            }

            // Add all days
            _appDb.ShowTimes.AddRange(showTimes);
            await _appDb.SaveChangesAsync();
            foreach (var st in showTimes)
            {
                await _appDb.Entry(st).Reference(s => s.Cinema).LoadAsync();
                await _appDb.Entry(st).Reference(s => s.Movie).LoadAsync();
            }

            return showTimes.Select(st => st.ToResponseDto()).ToList();
        }
        public async Task<ShowTimeResponseDto> UpdateShowTimeAsync(int id, ShowTimeUpdateDto dto)
        {
            var showTime = await _appDb.ShowTimes.FindAsync(id);
            if (showTime == null) throw new Exception("ShowTime not found");

            ShowTimeMapper.MapUpdate(showTime, dto);

            await _appDb.SaveChangesAsync();

            await _appDb.Entry(showTime).Reference(st => st.Cinema).LoadAsync();
            await _appDb.Entry(showTime).Reference(st => st.Movie).LoadAsync();

            return showTime.ToResponseDto();
        }

        public async Task<List<ShowTimeResponseDto>> GetAllShowTimesAsync()
        {
            var showTimes = await _appDb.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Cinema)
                .ToListAsync();

            return showTimes.Select(st => st.ToResponseDto()).ToList();
        }


        public async Task<ShowTimeResponseDto> GetShowTimeByIdAsync(int id)
        {
            var entity = await _appDb.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Cinema)
                .FirstOrDefaultAsync(st => st.Id == id);

            if (entity == null) throw new Exception("ShowTime not found");

            return entity.ToResponseDto();
        }

        public async Task DeleteShowTimeAsync(int id)
        {
            var entity = await _appDb.ShowTimes.FindAsync(id);
            if (entity == null) throw new Exception("ShowTime not found");

            _appDb.ShowTimes.Remove(entity);
            await _appDb.SaveChangesAsync();
        }
    }
}
