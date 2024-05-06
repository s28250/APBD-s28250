using WebApplication4.Models;

namespace WebApplication4.Services;

public interface IPrescriptionsService
{ 
    IEnumerable<Prescription> GetPrescriptions();
    IEnumerable<Prescription>  GetPrescriptionsByDoctorId(int idDoctor);
    int DeletePrescription(int idDoctor);
}