namespace ProyectoTFG_League.Models
{
    public class AspectoModelo
    {
        public int ID { set; get; }
        public string Nombre { get; set; }
        public int PrecioRP { get; set; }
        public string Fecha { get; set; }
        public byte[] Imagen { get; set; }
        public CampeonModelo CampeonNombre { get; set; }

    }
}
