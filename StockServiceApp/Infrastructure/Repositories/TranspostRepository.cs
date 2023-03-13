using TransportServiceApp.Core.Models;
using TransportServiceApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using TransportServiceApp.Infrastructure.Data;

namespace TransportServiceApp.Infrastructure.Repositories
{
    public class TranspostRepository : ITranspostRepository
    {

        private readonly TranspostDBContext _context;

        public TranspostRepository(TranspostDBContext context)
        {
            _context = context;
        }

        public async Task<Transpost> Add(Transpost transpost)
        {
            try
            {
                await _context.Transposts.AddAsync(transpost);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return transpost;
        }

        public async Task<Transpost> Remove(Transpost transpost)
        {
            try
            {
                Transpost transpostFromDB = await _context.Transposts.FirstOrDefaultAsync(x => x.OrderId == transpost.OrderId);
                _context.Transposts.Remove(transpostFromDB);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return transpost;
        }
    }
}
