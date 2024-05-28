using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model;


namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.RolDescripcion, options => options.MapFrom(origen => origen.IdRolNavigation.Nombre))
                .ForMember(destino => destino.EsActivo, options => options.MapFrom(origen => origen.EsActivo == true ? 1 : 0));
            #endregion Usuario

            #region SesionDTO
            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino => destino.RolDescripcion, options => options.MapFrom(origen => origen.IdRolNavigation.Nombre));
            #endregion SesionDTO

            #region UsuarioDTO
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino => destino.EsActivo, options => options.MapFrom(origen => origen.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.IdRolNavigation, options => options.Ignore());
            #endregion UsuarioDTO

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Menu

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.DescripcionCatergoria, options => options.MapFrom(origen => origen.IdCategoriaNavigation.Nombre))
                .ForMember(destino => destino.Precio, options => options.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.EsActivo, options => options.MapFrom(origen => origen.EsActivo == true ? 1 : 0));
            #endregion Producto

            #region Producto
            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino => destino.IdCategoriaNavigation, options => options.Ignore())
                .ForMember(destino => destino.Precio, options => options.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.EsActivo, options => options.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            #endregion Producto

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino => destino.TotalTexto, options => options.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.FechaRegistro, options => options.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy")));
            #endregion Venta

            #region VentaDTO
            CreateMap<VentaDTO, Venta>()
                .ForMember(destino => destino.Total, options => options.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE"))));
            #endregion VentaDTO

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino => destino.DescripcionProducto, options => options.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                .ForMember(destino => destino.PrecioTexto, options => options.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.TotalTexto, options => options.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE"))));
            #endregion DetalleVenta

            #region DetalleVentaDTO
            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino => destino.IdProductoNavigation, options => options.Ignore())
                .ForMember(destino => destino.Precio, options => options.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto)))
                .ForMember(destino => destino.Total, options => options.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto)));
            #endregion DetalleVentaDTO

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro, options => options.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
                .ForMember(destino => destino.TotalVenta, options => options.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Producto, options => options.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                .ForMember(destino => destino.NumeroDocumento, options => options.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento))
                .ForMember(destino => destino.TipoPago, options => options.MapFrom(origen => origen.IdVentaNavigation.TipoPago))
                .ForMember(destino => destino.Precio, options => options.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Precio.Value, new CultureInfo("es-PE"))));
            #endregion Reporte
        }

    }
}
