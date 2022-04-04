using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ProyectoFinal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

#nullable disable // Para quitar el aviso de nulls

namespace ProyectoFinal.BLL
{
    
    public class ClientesBLL  // BLL Para Clientes
    {
        private ApplicationDbContext contexto;

        public ClientesBLL(ApplicationDbContext _contexto)
        {
            contexto = _contexto;
        }

          private bool Existe(int id)
        {
            bool existe = false;

            try
            {
                existe = contexto.Clientes.Any(c => c.ClienteId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return existe;
        }

        public async Task<bool> Guardar(Clientes clientes)
        {
            if (!Existe(clientes.ClienteId))
                return await Insertar(clientes);
            else
                return await Modificar(clientes);
        }

      

        private async Task<bool> Insertar(Clientes clientes)
        {
            bool Insertado = false;

            try
            {
                contexto.Clientes.Add(clientes);
                Insertado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        private async Task<bool> Modificar(Clientes clientes)
        {
            bool Insertado = false;

            try
            {
                contexto.Entry(clientes).State = EntityState.Modified;
                Insertado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        public Clientes Buscar(int id)
        {
            Clientes cliente = new Clientes();

            try
            {
                cliente = contexto.Clientes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return cliente;
        }
 
        public async Task<bool> Eliminar(int id)
        {
            bool Eliminado = false;

            try
            {
                var cliente = await contexto.Clientes.FindAsync(id);

                if (cliente != null)
                {
                    contexto.Clientes.Remove(cliente);
                    Eliminado = (await contexto.SaveChangesAsync() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Eliminado;
        }

        public List<Clientes> GetList(Expression<Func<Clientes, bool>> cliente)
        {
            List<Clientes> Lista = new List<Clientes>();
            try
            {
                Lista = contexto.Clientes
                .Where(cliente)
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
