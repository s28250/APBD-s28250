using Microsoft.Data.SqlClient;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private IConfiguration _configuration;
    public DoctorsService(IDoctorsRepository doctorsRepository, IPrescriptionRepository prescriptionRepository, IConfiguration configuration)
    {
        _configuration = configuration;
        _prescriptionRepository= prescriptionRepository;
        _doctorsRepository = doctorsRepository;
    }
    public IEnumerable<Doctor> GetDoctors()
    {
        return _doctorsRepository.GetDoctors();
    }

    public Doctor GetDoctor(int idDoctor)
    {
        return _doctorsRepository.GetDoctor(idDoctor);
    }

    public int DeleteDoctor(int idDoctor)
    {
        int count1 = 0;
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            var prescriptions = _prescriptionRepository.GetPrescriptionsByDoctorId(idDoctor);
            if (prescriptions.Any())
            {
                foreach (var prescription in prescriptions)
                {
                    _prescriptionRepository.DeletePrescription(prescription.IdPrescription, transaction);
                }
            }
            int affectedRows = _doctorsRepository.DeleteDoctor(idDoctor, transaction);
            if (affectedRows == 0)
            {
                throw new Exception("Doctor not found."); 
            }

            transaction.Commit();
            return affectedRows;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

    }

    public int UpdateDoctor(Doctor doctor)
    {
        return _doctorsRepository.UpdateDoctor(doctor);
    }
}