using Microsoft.EntityFrameworkCore;
using Models;
using ProyectoFinal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
  
#nullable disable // Para quitar el aviso de nulls

namespace ProyectoFinal.BLL
{
    public class ArticuloBLL // BLL Para Articulo
    {
        private ApplicationDbContext contexto;

        public ArticuloBLL(ApplicationDbContext _contexto)
        {
            contexto = _contexto;
        }

        private bool Existe(int id)
        {
            bool existe = false;

            try
            {
                existe = contexto.Articulo.Any(c => c.ArticuloId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return existe;
        }

         public Articulo ExisteNombre(string Nombre)
        {
            Articulo existe;

            try
            {
                existe = contexto.Articulo                
                .Where( p => p.Nombre
                .ToLower() == Nombre.ToLower())
                .AsNoTracking()
                .SingleOrDefault();

            }catch
            {
                throw;
            }
            return existe;
        }

        

        public bool Guardar(Articulo articulo)
        {
             
            if (!Existe(articulo.ArticuloId))
                return  Insertar(articulo);
            else
                return  Modificar(articulo);
        }

        

        private bool Insertar(Articulo articulo)
        {
            bool Insertado = false;

            try
            {
                if (contexto.Articulo.Add(articulo) != null)
                {
                    Insertado =  contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        private bool Modificar(Articulo articulo)
        {
            bool Insertado = false;

            try
            {
                contexto.Entry(articulo).State = EntityState.Modified;
                Insertado = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            return Insertado;
        }

        public Articulo Buscar(int id)
        {
            Articulo articulo = new Articulo();

            try
            {
                articulo =  contexto.Articulo.Find(id);

                
            }
            catch (Exception)
            {
                throw;
            }
            return articulo;
        }


 
        public async Task<bool> Eliminar(int id)
        {
            bool Eliminado = false;

            try
            {
                var articulo = await contexto.Articulo.FindAsync(id);

                if (articulo!= null)
                {
                    contexto.Articulo.Remove(articulo);
                    Eliminado = (await contexto.SaveChangesAsync() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Eliminado;
        }

        public List<Articulo> GetList(Expression<Func<Articulo, bool>> articulo)
        {
            List<Articulo> Lista = new List<Articulo>();
            try
            {
                Lista = contexto.Articulo
                .Where(articulo)
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
