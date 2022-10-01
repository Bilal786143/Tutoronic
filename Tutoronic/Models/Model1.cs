using System.Data.Entity;

namespace Tutoronic.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public Model1(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment_reply> Comment_reply { get; set; }
        public virtual DbSet<Course_Student_Registration> Course_Student_Registration { get; set; }
        public virtual DbSet<Course_teacher_registration> Course_teacher_registration { get; set; }
        public virtual DbSet<Course_Video> Course_Video { get; set; }
        public virtual DbSet<Course_video_comment> Course_video_comment { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.cat_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment_reply>()
                .Property(e => e.reply)
                .IsFixedLength();

            modelBuilder.Entity<Course_Student_Registration>()
                .Property(e => e.course_price)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Course_teacher_registration>()
                .Property(e => e.course_price)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Course_Video>()
                .HasMany(e => e.Course_video_comment)
                .WithRequired(e => e.Course_Video)
                .HasForeignKey(e => e.course_vid_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course_video_comment>()
                .HasMany(e => e.Comment_reply)
                .WithRequired(e => e.Course_video_comment)
                .HasForeignKey(e => e.C_V_C_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.course_price)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Course_Student_Registration)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Course_teacher_registration)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Course_Video)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.order_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Course_Student_Registration)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.std_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Course_video_comment)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.std_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.Student_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubCategory>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.SubCategory)
                .HasForeignKey(e => e.Subcat_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasOptional(e => e.Comment_reply)
                .WithRequired(e => e.Teacher);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Course_teacher_registration)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.teacher_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Course_Video)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.teacher_fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.teacher_fid)
                .WillCascadeOnDelete(false);
        }
    }
}
