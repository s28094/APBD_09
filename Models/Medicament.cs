namespace APBD_09.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
