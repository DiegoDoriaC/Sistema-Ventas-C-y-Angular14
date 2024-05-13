using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;

using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using SistemaVenta.DAL.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGenericRepository<Usuario> _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _repository.Consultar();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            } 
            catch
            {
                throw;
            }
        }
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _repository.Consultar(u => u.Correo == correo && u.Clave == clave);
                if(queryUsuario.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }
                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch
            {
                throw;
            }
        }
        public async Task <UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioMappeado = _mapper.Map<Usuario>(modelo);
                var usuarioCreado = await _repository.Crear(usuarioMappeado);
                if (usuarioCreado.IdUsuario == 0) 
                    throw new TaskCanceledException("No se pudo crear el usuario");

                var query = await _repository.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();
                var convertido = _mapper.Map<UsuarioDTO>(usuarioCreado);
                return convertido;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _repository.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("Usuario no existe");
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;
                bool respuesta = await _repository.Editar(usuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo actualizar el usuario");
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
                var usuarioEncontrado = await _repository.Obtener(u => u.IdUsuario == id);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");
                bool respuesta = await _repository.Eliminar(usuarioEncontrado);
                if (!respuesta) 
                    throw new TaskCanceledException("Usuario no pudo ser eliminado");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
