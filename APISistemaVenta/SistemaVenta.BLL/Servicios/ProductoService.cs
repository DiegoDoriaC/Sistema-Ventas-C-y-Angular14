using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {

        private readonly IGenericRepository<Producto> _repository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _repository.Consultar();
                var listaProsuctos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductoDTO>>(listaProsuctos.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoMapeado = _mapper.Map<Producto>(modelo);
                var productoCreado = await _repository.Crear(productoMapeado);
                if (productoCreado.IdProducto == 0) throw new TaskCanceledException("No se pudo crear el producto");
                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoMappeado = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _repository.Obtener(u => u.IdProducto == productoMappeado.IdProducto);
                if (productoEncontrado == null) throw new TaskCanceledException("El producto no existe");
                productoEncontrado.Nombre = productoMappeado.Nombre;
                productoEncontrado.IdCategoria = productoMappeado.IdCategoria;
                productoEncontrado.Stock = productoMappeado.Stock;
                productoEncontrado.Precio = productoMappeado.Precio;
                productoEncontrado.EsActivo = productoMappeado.EsActivo;
                bool respuesta = await _repository.Editar(productoEncontrado);
                if (!respuesta) throw new TaskCanceledException("No se pudo editar");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEliminar = await _repository.Obtener(u => u.IdProducto == id);
                if(productoEliminar == null) throw new TaskCanceledException("No se encontro el producto");
                bool respuesta = await _repository.Eliminar(productoEliminar);
                if(!respuesta) throw new TaskCanceledException("No se pudo eliminar el producto");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}
