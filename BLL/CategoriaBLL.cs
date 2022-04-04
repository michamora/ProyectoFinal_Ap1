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

namespace ProyectoFinal.BLL // BLL Para la categoria del articulo
{
    public class CategoriaBLL
    {
        private ApplicationDbContext contexto;

        public CategoriaBLL(ApplicationDbContext _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> Guardar(Categoria categoria)
        {
            if (!Existe(categoria.CategoriaId))
                return await Insertar(categoria);
            else
                return await Modificar(categoria);
        }

        private bool Existe(int id)
        {
            bool existe = false;

            try
            {
                existe = contexto.Categoria.Any(t =>t.CategoriaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return existe;
        }

        private async Task<bool> Insertar(Categoria categoria)
        {
            bool Insertado = false;

            try
            {
                contexto.Categoria.Add(categoria);
                Insertado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        private async Task<bool> Modificar(Categoria categoria)
        {
            bool Modificado = false;

            try
            {
                contexto.Entry(categoria).State = EntityState.Modified;
                Modificado = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return Modificado;
        }

        public Categoria Buscar(int id)
        {
            Categoria categoria = new Categoria();

            try
            {
                categoria = contexto.Categoria.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return categoria;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool Eliminado = false;

            try
            {
                var categoria = await contexto.Categoria.FindAsync(id);

                if (categoria != null)
                {
                    contexto.Categoria.Remove(categoria);
                    Eliminado = (await contexto.SaveChangesAsync() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Eliminado;
        }

        public async Task<List<Categoria>> GetList(Expression<Func<Categoria, bool>> categoria)
        {
            List<Categoria> Lista = new List<Categoria>();
            try
            {
                Lista = await contexto.Categoria
                .Where(categoria)
                .AsNoTracking()
                .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }
    }
}
