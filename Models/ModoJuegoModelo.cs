namespace ProyectoTFG_League.Models
{
    public class ModoJuegoModelo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string DescripcionM { get; set; }
        public static List<string> Tipos => new List<string> { "Especial", "Permanente" };
    }
}
