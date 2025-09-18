using System.Collections.Generic;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IMaladieRepository
    {
        IEnumerable<Maladie> GetAll();
        Maladie? GetById(int id);
        Task<IEnumerable<Maladie>> GetMaladiesByMedecinIdAsync(int medecinId);

        void Add(Maladie maladie);
        void Update(Maladie maladie);
        void Delete(int id);
        void Save();
    }
}
