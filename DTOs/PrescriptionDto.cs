namespace APBD_09.DTOs
{
    public class PrescriptionDto
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdDoctor { get; set; }
        public List<int> MedicamentIds { get; set; }
    }
}
