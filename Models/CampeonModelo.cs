namespace ProyectoTFG_League.Models
{
    public class CampeonModelo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public RolModelo NombreRol { get; set; }
        public string Posicion { get; set; }
        public string Recurso { get; set; }
        public string Gama { get; set; }
        public string Fecha { get; set; }
        public int CosteRP { get; set; }
        public int CosteAzul { get; set; }

        public byte[] Imagen { get; set; }
    }
}
