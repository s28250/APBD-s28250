using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services;

public class PrescriptionsService : IPrescriptionsService
{
    private IPrescriptionRepository _prescriptionRepository;

    public PrescriptionsService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    public IEnumerable<Prescription> GetPrescriptions()
    {
        return _prescriptionRepository.GetPrescriptions();
    }

    public IEnumerable<Prescription>  GetPrescriptionsByDoctorId(int idDoctor)
    {
        return _prescriptionRepository.GetPrescriptionsByDoctorId(idDoctor);
    }

    public int DeletePrescription(int idDoctor)
    {
        throw new NotImplementedException();
    }
}