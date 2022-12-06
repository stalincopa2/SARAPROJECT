using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SARAPROJECT.Models
{
    public partial class SARADBContext : DbContext
    {
        public SARADBContext()
        {
        }
        public SARADBContext(DbContextOptions<SARADBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bodega> Bodegas { get; set; } = null!;
        public virtual DbSet<BodegaProducto> BodegaProductos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClienteVentum> ClienteVenta { get; set; } = null!;
        public virtual DbSet<DetPago> DetPagos { get; set; } = null!;
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; } = null!;
       // public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<EstadoVentum> EstadoVenta { get; set; } = null!;
        public virtual DbSet<Existencium> Existencia { get; set; } = null!;
        public virtual DbSet<Mesa> Mesas { get; set; } = null!;
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; } = null!;
        public virtual DbSet<Modulo> Modulos { get; set; } = null!;
        public virtual DbSet<Operacion> Operacions { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductoUnidad> ProductoUnidads { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolOperacion> RolOperacions { get; set; } = null!;
        public virtual DbSet<Salon> Salons { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Sucursal> Sucursals { get; set; } = null!;
        public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-I1EL63V7;Initial Catalog=SARADB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");*/
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bodega>(entity =>
            {
                entity.HasKey(e => e.IdBodega);

                entity.ToTable("BODEGA");

                entity.HasIndex(e => e.CodBodega, "UQ__BODEGA__0AF2A38EE5AC3BB7")
                    .IsUnique();

                entity.Property(e => e.IdBodega).HasColumnName("ID_BODEGA");

                entity.Property(e => e.CodBodega)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COD_BODEGA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.NombreBodega)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_BODEGA");
            });

            modelBuilder.Entity<BodegaProducto>(entity =>
            {
                entity.HasKey(e => e.IdBodePro);

                entity.ToTable("BODEGA_PRODUCTO");

                entity.HasIndex(e => e.IdProducto, "RELATIONSHIP_12_FK");

                entity.HasIndex(e => e.IdBodega, "RELATIONSHIP_16_FK");

                entity.Property(e => e.IdBodePro).HasColumnName("ID_BODE_PRO");

                entity.Property(e => e.IdBodega).HasColumnName("ID_BODEGA");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.HasOne(d => d.IdBodegaNavigation)
                    .WithMany(p => p.BodegaProductos)
                    .HasForeignKey(d => d.IdBodega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BODEGA_P_RELATIONS_BODEGA");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.BodegaProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BODEGA_P_RELATIONS_PRODUCTO");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("CLIENTE");

                entity.HasIndex(e => e.RucCliente, "UQ__CLIENTE__09569D3715D3C498")
                    .IsUnique();

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.DireccionCliente)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_CLIENTE");

                entity.Property(e => e.EmailCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_CLIENTE");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CLIENTE");

                entity.Property(e => e.RucCliente)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("RUC_CLIENTE");

                entity.Property(e => e.TelefonoCliente)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_CLIENTE");
            });

            modelBuilder.Entity<ClienteVentum>(entity =>
            {
                entity.HasKey(e => e.IdClienvent);

                entity.ToTable("CLIENTE_VENTA");

                entity.HasIndex(e => e.IdCliente, "RELATIONSHIP_25_FK");

                entity.HasIndex(e => e.IdVenta, "RELATIONSHIP_26_FK");

                entity.Property(e => e.IdClienvent)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CLIENVENT");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteVenta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE__RELATIONS_CLIENTE");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.ClienteVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE__RELATIONS_VENTA");
            });

            modelBuilder.Entity<DetPago>(entity =>
            {
                entity.HasKey(e => e.IdDetpago);

                entity.ToTable("DET_PAGO");

                entity.HasIndex(e => e.IdVenta, "RELATIONSHIP_27_FK");

                entity.HasIndex(e => e.IdMetodo, "RELATIONSHIP_28_FK");

                entity.Property(e => e.IdDetpago).HasColumnName("ID_DETPAGO");

                entity.Property(e => e.IdMetodo).HasColumnName("ID_METODO");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("MONTO");

                entity.HasOne(d => d.IdMetodoNavigation)
                    .WithMany(p => p.DetPagos)
                    .HasForeignKey(d => d.IdMetodo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DET_PAGO_RELATIONS_METODO_P");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetPagos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DET_PAGO_RELATIONS_VENTA");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IdDventa);

                entity.ToTable("DETALLE_VENTA");

                entity.HasIndex(e => e.IdVenta, "RELATIONSHIP_4_FK");

                entity.HasIndex(e => e.IdProducto, "RELATIONSHIP_6_FK");

                entity.Property(e => e.IdDventa).HasColumnName("ID_DVENTA");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACION");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("PRECIO_UPRODUCT");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE__RELATIONS_PRODUCTO");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE__RELATIONS_VENTA");
            });

          /*  modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("ESTADO");

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.DescripcionEst)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_EST");

                entity.Property(e => e.NombreEstado)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ESTADO");
            });*/

            modelBuilder.Entity<EstadoVentum>(entity =>
            {
                entity.HasKey(e => e.IdEstventa);

                entity.ToTable("ESTADO_VENTA");

                entity.Property(e => e.IdEstventa).HasColumnName("ID_ESTVENTA");

                entity.Property(e => e.DescripcionEsv)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_ESV");

                entity.Property(e => e.NombreEstadov)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ESTADOV");
            });

            modelBuilder.Entity<Existencium>(entity =>
            {
                entity.HasKey(e => e.IdExistencia);

                entity.ToTable("EXISTENCIA");

                entity.HasIndex(e => e.IdProducto, "RELATIONSHIP_18_FK");

                entity.Property(e => e.IdExistencia).HasColumnName("ID_EXISTENCIA");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Existencia)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EXISTENC_RELATIONS_PRODUCTO");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.HasKey(e => e.IdMesa);

                entity.ToTable("MESA");

                entity.HasIndex(e => e.IdSalon, "RELATIONSHIP_16_FK");

                entity.HasIndex(e => e.CodMesa, "UQ__MESA__C9AB5FC76861DC1F")
                    .IsUnique();

                entity.Property(e => e.IdMesa).HasColumnName("ID_MESA");

                entity.Property(e => e.CodMesa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COD_MESA");

                entity.Property(e => e.IdSalon).HasColumnName("ID_SALON");

                entity.Property(e => e.NombreMesa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_MESA");

                entity.HasOne(d => d.IdSalonNavigation)
                    .WithMany(p => p.Mesas)
                    .HasForeignKey(d => d.IdSalon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MESA_RELATIONS_SALON");
            });

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodo);

                entity.ToTable("METODO_PAGO");

                entity.Property(e => e.IdMetodo).HasColumnName("ID_METODO");

                entity.Property(e => e.DetalleMpago)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DETALLE_MPAGO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.IdModulo);

                entity.ToTable("MODULO");

                entity.Property(e => e.IdModulo).HasColumnName("ID_MODULO");

                entity.Property(e => e.NombreModulo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_MODULO");
            });

            modelBuilder.Entity<Operacion>(entity =>
            {
                entity.HasKey(e => e.IdOperacion);

                entity.ToTable("OPERACION");

                entity.HasIndex(e => e.IdModulo, "RELATIONSHIP_11_FK");

                entity.Property(e => e.IdOperacion).HasColumnName("ID_OPERACION");

                entity.Property(e => e.IdModulo).HasColumnName("ID_MODULO");

                entity.Property(e => e.NombreOperacion)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_OPERACION");

                entity.HasOne(d => d.IdModuloNavigation)
                    .WithMany(p => p.Operacions)
                    .HasForeignKey(d => d.IdModulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OPERACIO_RELATIONS_MODULO");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("PRODUCTO");

                entity.HasIndex(e => e.IdCategoria, "RELATIONSHIP_1_FK");

                entity.HasIndex(e => e.CodProducto, "UQ__PRODUCTO__8DF7FD2C5EFCE6C8")
                    .IsUnique();

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.CodProducto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COD_PRODUCTO");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("COSTO");

                entity.Property(e => e.FotoProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FOTO_PRODUCTO");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.ImpuestoProd).HasColumnName("IMPUESTO_PROD");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_PRODUCTO");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("PRECIO");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTO_RELATIONS_CATEGORI");
            });

            modelBuilder.Entity<ProductoUnidad>(entity =>
            {
                entity.HasKey(e => e.IdProUni);

                entity.ToTable("PRODUCTO_UNIDAD");

                entity.HasIndex(e => e.IdProducto, "RELATIONSHIP_19_FK");

                entity.HasIndex(e => e.IdUnidad, "RELATIONSHIP_20_FK");

                entity.Property(e => e.IdProUni).HasColumnName("ID_PRO_UNI");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdUnidad).HasColumnName("ID_UNIDAD");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductoUnidads)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTO_RELATIONS_PRODUCTO");

                entity.HasOne(d => d.IdUnidadNavigation)
                    .WithMany(p => p.ProductoUnidads)
                    .HasForeignKey(d => d.IdUnidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTO_RELATIONS_UNIDAD_M");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ROL");
            });

            modelBuilder.Entity<RolOperacion>(entity =>
            {
                entity.HasKey(e => e.IdRope);

                entity.ToTable("ROL_OPERACION");

                entity.HasIndex(e => e.IdOperacion, "RELATIONSHIP_10_FK");

                entity.HasIndex(e => e.IdRol, "RELATIONSHIP_9_FK");

                entity.Property(e => e.IdRope).HasColumnName("ID_ROPE");

                entity.Property(e => e.IdOperacion).HasColumnName("ID_OPERACION");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.HasOne(d => d.IdOperacionNavigation)
                    .WithMany(p => p.RolOperacions)
                    .HasForeignKey(d => d.IdOperacion)
                    .HasConstraintName("FK_ROL_OPER_RELATIONS_OPERACIO");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolOperacions)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_ROL_OPER_RELATIONS_ROL");
            });

            modelBuilder.Entity<Salon>(entity =>
            {
                entity.HasKey(e => e.IdSalon);

                entity.ToTable("SALON");

                entity.Property(e => e.IdSalon).HasColumnName("ID_SALON");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.NombreSalon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_SALON");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("STOCK");

                entity.HasIndex(e => e.IdProducto, "RELATIONSHIP_23_FK");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdStock).HasColumnName("ID_STOCK");

                entity.Property(e => e.StockActual).HasColumnName("STOCK_ACTUAL");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithOne(p => p.Stock)
                    .HasForeignKey<Stock>(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STOCK_RELATIONS_PRODUCTO");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal);

                entity.ToTable("SUCURSAL");

                entity.HasIndex(e => e.Ruc, "UQ__SUCURSAL__CAF3326B56E19390")
                    .IsUnique();

                entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Ruc)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("RUC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<UnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.IdUnidad);

                entity.ToTable("UNIDAD_MEDIDA");

                entity.Property(e => e.IdUnidad).HasColumnName("ID_UNIDAD");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.NombreUnidad)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_UNIDAD");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.IdSucursal, "RELATIONSHIP_14_FK");

                entity.HasIndex(e => e.IdRol, "RELATIONSHIP_8_FK");

                entity.HasIndex(e => e.CedulaUsuario, "UQ__USUARIO__41ECEBB4F876B84A")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.CedulaUsuario)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("CEDULA_USUARIO");

                entity.Property(e => e.Contracenia)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("CONTRACENIA");

                entity.Property(e => e.EmailUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_USUARIO");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.IdSucursal).HasColumnName("ID_SUCURSAL");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_USUARIO");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEXO")
                    .IsFixedLength();

                entity.Property(e => e.SisUsuario)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("SIS_USUARIO");

               entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_RELATIONS_ROL");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_RELATIONS_SUCURSAL");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta);

                entity.ToTable("VENTA");

                entity.HasIndex(e => e.IdMesa, "RELATIONSHIP_13_FK");

                entity.HasIndex(e => e.IdUsuario, "RELATIONSHIP_15_FK");

              /*  entity.HasIndex(e => e.IdEstado, "RELATIONSHIP_17_FK");*/

                entity.HasIndex(e => e.CodVenta, "UQ__VENTA__BFAA53CD42088529")
                    .IsUnique();

                entity.Property(e => e.IdVenta)
                    .HasColumnName("ID_VENTA");

                entity.Property(e => e.ClaveAcceso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLAVE_ACCESO");

                entity.Property(e => e.CodVenta)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("COD_VENTA");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_VENTA");

                //entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdEstventa).HasColumnName("ID_ESTVENTA");

                entity.Property(e => e.IdMesa).HasColumnName("ID_MESA");

               // entity.Property(e => e.IdMetodo).HasColumnName("ID_METODO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

              /*  entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");*/

                entity.Property(e => e.NroFactura)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NRO_FACTURA");

                entity.Property(e => e.NroPedido).HasColumnName("NRO_PEDIDO");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("TOTAL");

                /*entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENTA_RELATIONS_ESTADO");*/

                entity.HasOne(d => d.IdEstventaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdEstventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENTA_RELATIONS_ESTADO_V");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENTA_RELATIONS_USUARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
