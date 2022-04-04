using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ProyectoFinal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


#nullable disable // Para quitar el aviso de nulls

namespace ProyectoFinal.BLL // BLL Para el metodo de pago
{
    public class PagoBLL
    {
        private ApplicationDbContext contexto;

        public PagoBLL(ApplicationDbContext _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> Guardar(Pago pago)
        {
            if (!Existe(pago.PagoId))
                return await Insertar(pago);
            else
                return await Modificar(pago);
        }

        private bool Existe(int id)
        {
            bool existe = false;

            try
            {
                existe = contexto.Pago.Any(t =>t.PagoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return existe;
        }

        private async Task<bool> Insertar(Pago pago)
        {
            bool Insertado = false;

            try
            {
                contexto.Pago.Add(pago);
                Insertado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        private async Task<bool> Modificar(Pago pago)
        {
            bool Modificado = false;

            try
            {
                contexto.Entry(pago).State = EntityState.Modified;
                Modificado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Modificado;
        }

        public async Task<Pago> Buscar(int id)
        {
            Pago pago = new Pago();

            try
            {
                pago = await contexto.Pago.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return pago;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool Eliminado = false;

            try
            {
                var pago = await contexto.Pago.FindAsync(id);

                if (pago != null)
                {
                    contexto.Pago.Remove(pago);
                    Eliminado = (await contexto.SaveChangesAsync() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Eliminado;
        }

        public List<Pago> GetList(Expression<Func<Pago, bool>> pago)
        {
            List<Pago> Lista = new List<Pago>();
            try
            {
                Lista = contexto.Pago
                .Where(pago)
                .AsNoTracking()
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }
    }
}
