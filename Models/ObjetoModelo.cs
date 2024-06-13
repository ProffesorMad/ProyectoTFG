namespace ProyectoTFG_League.Models
{
    public class ObjetoModelo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<string> Modo { get; set; } = new List<string>();
        public int Coste { get; set; }
        public string Estadisticas { get; set; }
        public byte[] Imagen { get; set; }

        public string ModoString
        {
            get => string.Join(",", Modo);
            set => Modo = value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
