namespace SistemaGestion.Models
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }

        public ProductoVendido()
        {
            Id = 0;
            IdProducto = 0;
            Stock = 0;
            IdVenta = 0;
        }

        public ProductoVendido(int id, int idProducto, int stock, int idVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;
        }
    }
}
