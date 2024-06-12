namespace ProyectoTFG_League.Models
{
    public class HabilidadModelo
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string DescripicionH { get; set; }
        public CampeonModelo CampeonID { get; set; }

    }
}
