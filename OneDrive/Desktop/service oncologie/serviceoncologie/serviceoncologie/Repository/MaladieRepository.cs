using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public class MaladieRepository : IMaladieRepository
    {
        private readonly AppDbContext _context;

        public MaladieRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Maladie> GetAll()
        {
            return _context.Maladies.ToList();
        }

        public Maladie? GetById(int id)
        {
            return _context.Maladies.Find(id);
        }

        public void Add(Maladie maladie)
        {
            _context.Maladies.Add(maladie);
            Save();
        }

        public void Update(Maladie maladie)
        {
            _context.Maladies.Update(maladie);
            Save();
        }

        public void Delete(int id)
        {
            var maladie = _context.Maladies.Find(id);
            if (maladie != null)
            {
                _context.Maladies.Remove(maladie);
                Save();
            }
        }
        public async Task<IEnumerable<Maladie>> GetMaladiesByMedecinIdAsync(int medecinId)
        {
            return await _context.Maladies
                                 .Where(m => m.MedecinId == medecinId)
                                 .ToListAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
