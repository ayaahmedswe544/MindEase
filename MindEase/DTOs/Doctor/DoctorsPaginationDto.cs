using MindEase.Models;

namespace MindEase.DTOs.Doctor
{
    public class DoctorsPaginationDto
    {
public List<DoctorDto> Doctors { get; set; }
        public int TotalCount { get; set; }

    }
}
