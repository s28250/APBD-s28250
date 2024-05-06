using WebApplication4.Models;

namespace WebApplication4.Services;

public interface IDoctorsService
{
    IEnumerable<Doctor> GetDoctors();
    Doctor GetDoctor(int idDoctor);
    int DeleteDoctor(int idDoctor);
    int UpdateDoctor(Doctor doctor);
}