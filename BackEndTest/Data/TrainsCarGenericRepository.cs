using BackEndTest.Interfaces;
using BackEndTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndTest.Data
{
    public class TrainsCarGenericRepository: GenericRepository<trainsCar>
    {
        public TrainsCarGenericRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<trainsCar> GetTrainsCar(int trainNumber, int carNumber)
        {
            return await _context.Set<trainsCar>().FirstOrDefaultAsync(x => x.traintNumber == trainNumber && x.carNumber == carNumber);
        }
        
        public async ValueTask<IEnumerable<trainsCar>> GetListById(int id)
        {
            return await _context.Set<trainsCar>().Where(x => x.traintNumber == id).ToListAsync();
        }
    }
}
