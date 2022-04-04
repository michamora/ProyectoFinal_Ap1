using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Models
{
    public class VentasDetalle // Detalles de la venta
    {
        [Key]
       
        public int Id { get; set; }
    
        public int VentaId { get; set; }
     
        public int ClienteId { get; set; }
            
        public int ArticuloId { get; set; }
        
        public float Cantidad { get; set; }

        public double PrecioArticulo { get; set; }
        

        //-------------------------------------------------------------------------------------


         public VentasDetalle()
        {
            Id = 0;
            VentaId = 0;
            ClienteId = 0;
            ArticuloId = 0;
            Cantidad = 0;    
            PrecioArticulo = 0;     
        }

        public VentasDetalle(int id, int ventaId, int clienteId, int articuloId, int cantidad, double precioArticulo)
        {
            Id = id;
            VentaId = ventaId;
            ClienteId = clienteId;
            ArticuloId = articuloId;
            Cantidad = cantidad;
            PrecioArticulo = precioArticulo;
           
        }

    }
}