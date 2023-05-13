using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using appfunko.Models;
using appfunko.Data;

namespace appfunko.Service
{
    public class ProductoService
    {
        private readonly ILogger<ProductoService> _logger;
        private readonly ApplicationDbContext _context;

        public ProductoService(ILogger<ProductoService> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Producto> CreateOrUpdate(Producto p){
            //Regla de Negocio 1
            if(p.Precio < 1){
                throw new SystemException("No se puede ingresar datos con precio menor 1 sol");
            }
            //Regla de Negocio 2
            
            _context.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public async Task<List<Producto>?> GetAll(){
            if(_context.DataProductos == null )
                return null;
            return await _context.DataProductos.ToListAsync();
        }

        public async Task<Producto?> Get(int? id){
            if (id == null || _context.DataProductos == null)
            {
                return null;
            }

            var producto = await _context.DataProductos.FindAsync(id);
            if (producto == null)
            {
                return null;
            }
            return producto;
        }

        public async Task<Producto?> FirstOrDefault(int? id){
            var producto = await _context.DataProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return null;
            }
            return producto;
        }

        public async Task Delete(int? id){
            var producto = await _context.DataProductos.FindAsync(id);
            if (producto != null)
            {
                _context.DataProductos.Remove(producto);
            }
            await _context.SaveChangesAsync();
        }

        public bool ProductoExists(int id)
        {
          return (_context.DataProductos?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}