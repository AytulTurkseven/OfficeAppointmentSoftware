namespace OfficeAppointmentSoftware.DAL.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using OfficeAppointmentSoftware.Entities.Entities;

    public partial class AppointmentDBContext : DbContext
    {
        public AppointmentDBContext()
        {
            Database.Connection.ConnectionString = @"server=BETUL-WINDOWS;Database=AppointmentDB;UID=sa; PWD=12345";
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Expert> Expert { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<Works> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Appointment)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Contact)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Expert)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.PhoneNumber1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.PhoneNumber2)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Expert>()
                .Property(e => e.Profession)
                .IsUnicode(false);

            modelBuilder.Entity<Expert>()
                .HasMany(e => e.Contact)
                .WithOptional(e => e.Expert)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Plans>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Plans>()
                .HasMany(e => e.Company)
                .WithRequired(e => e.Plans)
                .WillCascadeOnDelete(false);
        }
    }
}
