namespace SistemaGestion.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }

        public Venta()
        {
            Id = 0;
            Comentarios = string.Empty;
        }

        public Venta(int id, string comentarios)
        {
            Id = id;
            Comentarios = comentarios;
        }
    }
}
