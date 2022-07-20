using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web.Models
{
    public partial class webContext : DbContext
    {
        public webContext()
        {
        }

        public webContext(DbContextOptions<webContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Numeracion> Numeracions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Salesdetail> Salesdetails { get; set; }
        public virtual DbSet<TipoComprobante> TipoComprobantes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<empresa> Empresa { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QAG3240; Database=web; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__categori__140587C7A9FCEC19");

                entity.ToTable("categorias");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Catname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("catname");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Idcustomer)
                    .HasName("PK__customer__53EFD0A667F61416");

                entity.ToTable("customers");

                entity.Property(e => e.Idcustomer).HasColumnName("idcustomer");

                entity.Property(e => e.Dnicustomer)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("dnicustomer");

                entity.Property(e => e.Lastnamecustomer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastnamecustomer");

                entity.Property(e => e.Namecustomer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("namecustomer");

                entity.Property(e => e.Phonecustomer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phonecustomer");
            });

            modelBuilder.Entity<Numeracion>(entity =>
            {
                entity.ToTable("numeracion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdSale).HasColumnName("id_sale");

                entity.Property(e => e.IdTipoComprobante).HasColumnName("id_tipo_comprobante");

                entity.Property(e => e.NumeracionBoleta).HasColumnName("numeracion_boleta");

                entity.Property(e => e.NumeracionFactura).HasColumnName("numeracion_factura");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.Numeracions)
                    .HasForeignKey(d => d.IdSale)
                    .HasConstraintName("FK__numeracio__id_sa__0D7A0286");

                entity.HasOne(d => d.IdTipoComprobanteNavigation)
                    .WithMany(p => p.Numeracions)
                    .HasForeignKey(d => d.IdTipoComprobante)
                    .HasConstraintName("FK__numeracio__id_ti__0C85DE4D");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct)
                    .HasName("PK__products__8D507424618A05E4");

                entity.ToTable("products");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Nameproduct)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nameproduct");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_categorias");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.IdSale)
                    .HasName("PK__sales__D18B01570B1B49BF");

                entity.ToTable("sales");

                entity.Property(e => e.IdSale).HasColumnName("id_sale");

              
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdComprobante).HasColumnName("id_comprobante");

                entity.Property(e => e.Idcustomer).HasColumnName("idcustomer");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

                entity.HasOne(d => d.IdComprobanteNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IdComprobante)
                    .HasConstraintName("FK__sales__id_compro__07C12930");

                entity.HasOne(d => d.IdcustomerNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Idcustomer)
                    .HasConstraintName("FK__sales__idcustome__60A75C0F");
            });

            modelBuilder.Entity<Salesdetail>(entity =>
            {
                entity.HasKey(e => e.IdSaleDetail)
                    .HasName("PK__salesdet__33BFC625F175D666");

                entity.ToTable("salesdetail");

                entity.Property(e => e.IdSaleDetail).HasColumnName("id_sale_detail");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IdSale).HasColumnName("id_sale");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Salesdetails)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__salesdeta__id_pr__6477ECF3");

                entity.HasOne(d => d.IdSaleNavigation)
                    .WithMany(p => p.Salesdetails)
                    .HasForeignKey(d => d.IdSale)
                    .HasConstraintName("FK__salesdeta__id_sa__6383C8BA");
            });

            modelBuilder.Entity<TipoComprobante>(entity =>
            {
                entity.ToTable("tipo_comprobante");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });
            modelBuilder.Entity<empresa>(entity =>
            {
                entity.ToTable("empresa");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ruc)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ruc");

                entity.Property(e => e.razonSocial)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("razonSocial");

                entity.Property(e => e.nombreComercial)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("nombreComercial");

                entity.Property(e => e.direccion)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("direccion");

                entity.Property(e => e.provincia)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("provincia");

                entity.Property(e => e.departamento)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("departamento");

                entity.Property(e => e.distrito)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("distrito");

                entity.Property(e => e.ubigueo)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("ubigueo");

                entity.Property(e => e.token)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("token");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__usuario__778B892125CB24BB");

                entity.ToTable("usuario");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
