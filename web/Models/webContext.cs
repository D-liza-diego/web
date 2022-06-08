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

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Salesdetail> Salesdetails { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

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
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("dnicustomer");

                entity.Property(e => e.Lastnamecustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastnamecustomer");

                entity.Property(e => e.Namecustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("namecustomer");

                entity.Property(e => e.Phonecustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phonecustomer");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct)
                    .HasName("PK__products__8D507424618A05E4");

                entity.ToTable("products");

                entity.Property(e => e.Idproduct).HasColumnName("idproduct");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Nameproduct)
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

                entity.Property(e => e.Visibilidad).HasColumnName("visibilidad");

                entity.Property(e => e.Comprobante)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comprobante");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Idcustomer).HasColumnName("idcustomer");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__usuario__778B892125CB24BB");

                entity.ToTable("usuario");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Dni).HasColumnName("dni");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
