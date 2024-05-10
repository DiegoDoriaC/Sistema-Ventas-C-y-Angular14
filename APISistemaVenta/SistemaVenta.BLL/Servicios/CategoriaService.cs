using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class CategoriaService : ICategoriaService
    {

        private readonly IGenericRepository<Categoria> _repository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> ropository, IMapper mapper)
        {
            _repository = ropository;
            _mapper = mapper;
        }
        public async Task<List<CategoriaDTO>> lista()
        {
            try
            {
                var listaCategorias = await _repository.Consultar();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
