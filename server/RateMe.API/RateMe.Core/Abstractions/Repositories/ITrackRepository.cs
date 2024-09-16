using RateMe.Core.Models;

namespace RateMe.Core.Abstractions;

public interface ITrackRepository : IRepository<Track>
{
   Task<List<Track>> GetByPage(int page, int size);
}