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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
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

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.Idinventario)
                    .HasName("PK__Inventar__D69EA8807F29B91C");

                entity.Property(e => e.Idinventario).HasColumnName("IDInventario");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventari__IDPro__32E0915F");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.Idmarca)
                    .HasName("PK__Marca__CEC375E783858E8D");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaximosMinimos>(entity =>
            {
                entity.HasKey(e => e.IdmaxMin)
                    .HasName("PK__MaximosM__4BF9ABB1A5F29B48");

                entity.Property(e => e.IdmaxMin).HasColumnName("IDMaxMin");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.MaximosMinimos)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MaximosMi__IDPro__35BCFE0A");
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

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.Idmarca).HasColumnName("IDMarca");

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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
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

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
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
