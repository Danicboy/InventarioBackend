using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using InventarioBack.Models.DBModels;

namespace InventarioBack.Context
{
    public partial class InventarioContext : DbContext
    {
        public InventarioContext()
        {
        }

        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
        public virtual DbSet<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
        public virtual DbSet<Dimensiones> Dimensiones { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<MaximosMinimos> MaximosMinimos { get; set; }
        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }
        public virtual DbSet<OrdenVenta> OrdenVenta { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UnidadesDeMedida> UnidadesDeMedida { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=38.17.54.162,1433;Database=BodegaLaBendicion;user id=temp; password=Bodega123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__Categori__70E82E28F1E02C84");

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.CategoriaIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Categoria__IDUsu__5CD6CB2B");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.CategoriaIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Categoria__IDUsu__5BE2A6F2");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente)
                    .HasName("PK__Cliente__9B8553FC5732B1BF");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.ClienteIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Cliente__IDUsuar__5EBF139D");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.ClienteIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Cliente__Estado__5DCAEF64");
            });

            modelBuilder.Entity<DetalleOrdenCompra>(entity =>
            {
                entity.HasKey(e => e.IddetalleOc)
                    .HasName("PK__DetalleO__A712925BF97C56D5");

                entity.Property(e => e.IddetalleOc).HasColumnName("IDDetalleOC");

                entity.Property(e => e.Iddimension).HasColumnName("IDDimension");

                entity.Property(e => e.IdordenCompra).HasColumnName("IDOrdenCompra");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.IdunidadMedida).HasColumnName("IDUnidadMedida");

                entity.HasOne(d => d.IddimensionNavigation)
                    .WithMany(p => p.DetalleOrdenCompra)
                    .HasForeignKey(d => d.Iddimension)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDDim__4CA06362");

                entity.HasOne(d => d.IdordenCompraNavigation)
                    .WithMany(p => p.DetalleOrdenCompra)
                    .HasForeignKey(d => d.IdordenCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDOrd__4BAC3F29");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.DetalleOrdenCompra)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDPro__4E88ABD4");

                entity.HasOne(d => d.IdunidadMedidaNavigation)
                    .WithMany(p => p.DetalleOrdenCompra)
                    .HasForeignKey(d => d.IdunidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDUni__4D94879B");
            });

            modelBuilder.Entity<DetalleOrdenVenta>(entity =>
            {
                entity.HasKey(e => e.IddetalleOv)
                    .HasName("PK__DetalleO__A7129228A29D8CB0");

                entity.Property(e => e.IddetalleOv).HasColumnName("IDDetalleOV");

                entity.Property(e => e.Iddimension).HasColumnName("IDDimension");

                entity.Property(e => e.IdordenVenta).HasColumnName("IDOrdenVenta");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.IdunidadMedida).HasColumnName("IDUnidadMedida");

                entity.HasOne(d => d.IddimensionNavigation)
                    .WithMany(p => p.DetalleOrdenVenta)
                    .HasForeignKey(d => d.Iddimension)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDDim__412EB0B6");

                entity.HasOne(d => d.IdordenVentaNavigation)
                    .WithMany(p => p.DetalleOrdenVenta)
                    .HasForeignKey(d => d.IdordenVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDOrd__403A8C7D");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.DetalleOrdenVenta)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDPro__4316F928");

                entity.HasOne(d => d.IdunidadMedidaNavigation)
                    .WithMany(p => p.DetalleOrdenVenta)
                    .HasForeignKey(d => d.IdunidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleOr__IDUni__4222D4EF");
            });

            modelBuilder.Entity<Dimensiones>(entity =>
            {
                entity.HasKey(e => e.Iddimension)
                    .HasName("PK__Dimensio__889B54181B7650E4");

                entity.Property(e => e.Iddimension).HasColumnName("IDDimension");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.DimensionesIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Dimension__IDUsu__60A75C0F");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.DimensionesIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Dimension__Estad__5FB337D6");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.Idinventario)
                    .HasName("PK__Inventar__D69EA8807F29B91C");

                entity.Property(e => e.Idinventario).HasColumnName("IDInventario");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventari__IDPro__32E0915F");

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.InventarioIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Inventari__IDUsu__628FA481");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.InventarioIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Inventari__Estad__619B8048");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.Idmarca)
                    .HasName("PK__Marca__CEC375E783858E8D");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.MarcaIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Marca__IDUsuario__6477ECF3");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.MarcaIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Marca__Estado__6383C8BA");
            });

            modelBuilder.Entity<MaximosMinimos>(entity =>
            {
                entity.HasKey(e => e.IdmaxMin)
                    .HasName("PK__MaximosM__4BF9ABB1A5F29B48");

                entity.Property(e => e.IdmaxMin).HasColumnName("IDMaxMin");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.MaximosMinimos)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaximosMi__IDPro__35BCFE0A");

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.MaximosMinimosIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__MaximosMi__IDUsu__66603565");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.MaximosMinimosIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__MaximosMi__Estad__656C112C");
            });

            modelBuilder.Entity<OrdenCompra>(entity =>
            {
                entity.HasKey(e => e.IdordenCompra)
                    .HasName("PK__OrdenCom__D3C512400D663193");

                entity.Property(e => e.IdordenCompra).HasColumnName("IDOrdenCompra");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEspectativa).HasColumnType("datetime");

                entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");

                entity.Property(e => e.UserCreatedId).HasColumnName("UserCreatedID");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenComp__IDPro__48CFD27E");

                entity.HasOne(d => d.UserCreated)
                    .WithMany(p => p.OrdenCompra)
                    .HasForeignKey(d => d.UserCreatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenComp__UserC__47DBAE45");
            });

            modelBuilder.Entity<OrdenVenta>(entity =>
            {
                entity.HasKey(e => e.IdordenVenta)
                    .HasName("PK__OrdenVen__25D2EE31564C7CB1");

                entity.Property(e => e.IdordenVenta).HasColumnName("IDOrdenVenta");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaSalida).HasColumnType("datetime");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.UserCreatedId).HasColumnName("UserCreatedID");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.OrdenVenta)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenVent__IDCli__3D5E1FD2");

                entity.HasOne(d => d.UserCreated)
                    .WithMany(p => p.OrdenVenta)
                    .HasForeignKey(d => d.UserCreatedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdenVent__UserC__3C69FB99");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("PK__Producto__ABDAF2B400F04BD1");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__IDCate__300424B4");

                entity.HasOne(d => d.IdmarcaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Idmarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__IDMarc__2F10007B");

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.ProductoIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Producto__IDUsua__68487DD7");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.ProductoIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Producto__Estado__6754599E");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Idproveedor)
                    .HasName("PK__Proveedo__4CD73240A80BF3C4");

                entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.ProveedorIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__Proveedor__IDUsu__6A30C649");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.ProveedorIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__Proveedor__Estad__693CA210");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.Idrole)
                    .HasName("PK__Roles__A1BA16C420A1A8E9");

                entity.Property(e => e.Idrole).HasColumnName("IDRole");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnidadesDeMedida>(entity =>
            {
                entity.HasKey(e => e.IdunidadMedida)
                    .HasName("PK__Unidades__1DB90804E89C561F");

                entity.Property(e => e.IdunidadMedida).HasColumnName("IDUnidadMedida");

                entity.Property(e => e.FechaActualizado).HasColumnType("datetime");

                entity.Property(e => e.FechaCreado).HasColumnType("datetime");

                entity.Property(e => e.IdusuarioActualizo).HasColumnName("IDUsuarioActualizo");

                entity.Property(e => e.IdusuarioCreado).HasColumnName("IDUsuarioCreado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioActualizoNavigation)
                    .WithMany(p => p.UnidadesDeMedidaIdusuarioActualizoNavigation)
                    .HasForeignKey(d => d.IdusuarioActualizo)
                    .HasConstraintName("FK__UnidadesD__IDUsu__6C190EBB");

                entity.HasOne(d => d.IdusuarioCreadoNavigation)
                    .WithMany(p => p.UnidadesDeMedidaIdusuarioCreadoNavigation)
                    .HasForeignKey(d => d.IdusuarioCreado)
                    .HasConstraintName("FK__UnidadesD__Estad__6B24EA82");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__52311169858BC5A2");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Idrole).HasColumnName("IDRole");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Idrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__IDRole__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
