using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IMedicamentRepository
{
    IEnumerable<Medicament> GetMedicaments();
    Medicament GetMedicament(int idMedicament);
    int DeleteMedicament(int idMedicament);
}