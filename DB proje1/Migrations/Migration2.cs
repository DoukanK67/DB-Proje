using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DB_proje1.Models;

#nullable disable

namespace DB_proje1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Advisor", b =>
            {
                b.Property<int>("AdvisorID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvisorID"));

                b.Property<string>("Department")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("FullName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("AdvisorID");

                b.ToTable("Advisors");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Course", b =>
            {
                b.Property<int>("CourseID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"));

                b.Property<string>("CourseCode")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("CourseName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Credit")
                    .HasColumnType("int");

                b.Property<string>("Department")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("IsMandatory")
                    .HasColumnType("bit");

                b.Property<int?>("Quota")
                    .HasColumnType("int");

                b.HasKey("CourseID");

                b.ToTable("Courses");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Student", b =>
            {
                b.Property<int>("StudentID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                b.Property<int?>("AdvisorID")
                    .HasColumnType("int");

                b.Property<string>("Department")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("EnrollmentDate")
                    .HasColumnType("datetime2");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("StudentID");

                b.HasIndex("AdvisorID");

                b.ToTable("Students");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.StudentCourseSelection", b =>
            {
                b.Property<int>("SelectionID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SelectionID"));

                b.Property<int>("CourseID")
                    .HasColumnType("int");

                b.Property<bool>("IsApproved")
                    .HasColumnType("bit");

                b.Property<DateTime>("SelectionDate")
                    .HasColumnType("datetime2");

                b.Property<int>("StudentID")
                    .HasColumnType("int");

                b.HasKey("SelectionID");

                b.HasIndex("CourseID");

                b.HasIndex("StudentID");

                b.ToTable("StudentCourseSelections");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Transcript", b =>
            {
                b.Property<int>("TranscriptID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TranscriptID"));

                b.Property<int>("CourseID")
                    .HasColumnType("int");

                b.Property<string>("Grade")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Semester")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("StudentID")
                    .HasColumnType("int");

                b.HasKey("TranscriptID");

                b.HasIndex("CourseID");

                b.HasIndex("StudentID");

                b.ToTable("Transcripts");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.User", b =>
            {
                b.Property<int>("UserID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int?>("RelatedID")
                    .HasColumnType("int");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserID");

                b.ToTable("Users");
            });

            modelBuilder.Entity("StudentIMS.Models.CourseQuota", b =>
            {
                b.Property<int>("CourseId")
                    .HasColumnType("int");

                b.Property<int>("Quota")
                    .HasColumnType("int");

                b.Property<int>("RemainingQuota")
                    .HasColumnType("int");

                b.HasKey("CourseId");

                b.ToTable("CourseQuotas");
            });

            modelBuilder.Entity("StudentIMS.Models.UnapprovedSelections", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                b.Property<int>("CourseID")
                    .HasColumnType("int");

                b.Property<int>("StudentID")
                    .HasColumnType("int");

                b.HasKey("ID");

                b.HasIndex("CourseID");

                b.HasIndex("StudentID");

                b.ToTable("UnapprovedSelections", (string)null);
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Student", b =>
            {
                b.HasOne("SmartCourseSelectorWeb.Models.Advisor", "Advisor")
                    .WithMany("Students")
                    .HasForeignKey("AdvisorID")
                    .OnDelete(DeleteBehavior.Restrict);

                b.Navigation("Advisor");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.StudentCourseSelection", b =>
            {
                b.HasOne("SmartCourseSelectorWeb.Models.Course", "Course")
                    .WithMany("StudentCourseSelections")
                    .HasForeignKey("CourseID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SmartCourseSelectorWeb.Models.Student", "Student")
                    .WithMany("StudentCourseSelections")
                    .HasForeignKey("StudentID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Course");

                b.Navigation("Student");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Transcript", b =>
            {
                b.HasOne("SmartCourseSelectorWeb.Models.Course", "Course")
                    .WithMany()
                    .HasForeignKey("CourseID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SmartCourseSelectorWeb.Models.Student", "Student")
                    .WithMany()
                    .HasForeignKey("StudentID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Course");

                b.Navigation("Student");
            });

            modelBuilder.Entity("StudentIMS.Models.CourseQuota", b =>
            {
                b.HasOne("SmartCourseSelectorWeb.Models.Course", "Course")
                    .WithOne()
                    .HasForeignKey("StudentIMS.Models.CourseQuota", "CourseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Course");
            });

            modelBuilder.Entity("StudentIMS.Models.UnapprovedSelections", b =>
            {
                b.HasOne("SmartCourseSelectorWeb.Models.Course", "Course")
                    .WithMany("UnapprovedSelections")
                    .HasForeignKey("CourseID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SmartCourseSelectorWeb.Models.Student", "Student")
                    .WithMany("UnapprovedSelections")
                    .HasForeignKey("StudentID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Course");

                b.Navigation("Student");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Advisor", b =>
            {
                b.Navigation("Students");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Course", b =>
            {
                b.Navigation("StudentCourseSelections");

                b.Navigation("UnapprovedSelections");
            });

            modelBuilder.Entity("SmartCourseSelectorWeb.Models.Student", b =>
            {
                b.Navigation("StudentCourseSelections");

                b.Navigation("UnapprovedSelections");
            });
#pragma warning restore 612, 618
        }
    }
}