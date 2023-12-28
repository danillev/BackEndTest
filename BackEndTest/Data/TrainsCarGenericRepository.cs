using BackEndTest.Interfaces;
using BackEndTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndTest.Data
{
    public class TrainsCarGenericRepository: GenericRepository<TrainsCar>
    {
        public TrainsCarGenericRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<TrainsCar> GetTrainsCar(int trainNumber, int carNumber)
        {
            return await _context.Set<TrainsCar>().FirstOrDefaultAsync(x => x.traintNumber == trainNumber && x.carNumber == carNumber);
        }
        
        public async ValueTask<IEnumerable<TrainsCar>> GetListById(int id)
        {
            return await _context.Set<TrainsCar>().Where(x => x.traintNumber == id).ToListAsync();
        }
    }
}
