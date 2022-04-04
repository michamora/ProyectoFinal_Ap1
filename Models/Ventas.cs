using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable // Para quitar el aviso de nulls
 
namespace Models
{
    public class Ventas // Entidad Ventas
    {
        [Key]
        public int VentaId { get; set; } 

        [DataType(DataType.Date)]  
        public DateTime Fecha { get; set; } 
        public float Total { get; set; }
        public float ITBIS { get; set; }
        public float SubTotal { get; set; }
        public float Existencia { get; set; }

        public float UnidadesVendidas { get; set;}

        [Required(ErrorMessage = "Ingrese el monto a pagar.")]
        public float PagoObtenido { get; set;}

        public float MontoRestante { get; set;}

        public float MetodoDePago { get; set;}

        //-------------------------------------------------------------------------------------

        
        [ForeignKey("VentaId")]
        public virtual List<VentasDetalle> ventasDetalle { get; set; } 

         public Ventas()
        {
            VentaId = 0;
            Fecha = DateTime.Now;
            ITBIS = 0;
            SubTotal = 0;
            Total = 0;
            Existencia = 0;

            ventasDetalle = new List<VentasDetalle>();
        }
   
    }
}