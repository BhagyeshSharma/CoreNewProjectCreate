using System;
using System.Collections.Generic;
using Entity.Modal;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class UserMgMtContext : DbContext
{
    public UserMgMtContext()
    {
    }

    public UserMgMtContext(DbContextOptions<UserMgMtContext> options)
        : base(options)
    {
    }
    public virtual DbSet<State> State { get; set; }
    public virtual DbSet<EmployeeReg> EmployeeReg { get; set; }
    public virtual DbSet<StudentReg> StudentReg { get; set; }
    public virtual DbSet<StudentEdu_Details> StudentEdu_Details { get; set; }
    public virtual DbSet<tbl_Subject> Tbl_Subject { get; set; }
    public virtual DbSet<tblDivision> tblDivision { get; set; }
    public virtual DbSet<tblDistrict> tblDistrict { get; set; }
    public virtual DbSet<tblBlock> tblBlock { get; set; }
    public virtual DbSet<Trn_DivisonDistrictBlock> Trn_DivisonDistrictBlocks { get; set; }
    public virtual DbSet<Tbl_Attendance> Tbl_Attendance { get; set; }
    public virtual DbSet<TblQuestion> TblQuestion { get; set; }
    public virtual DbSet<TblExamSessions> TblExamSessions { get; set; }
    public virtual DbSet<TblUserActivityLogs> TblUserActivityLogs { get; set; }
    public virtual DbSet<Tbl_StudentVerification> Tbl_StudentVerifications { get; set; }    
    public virtual DbSet<Tbl_Books> Tbl_Books { get; set; } 
    public virtual DbSet<Tbl_BookTransaction> Tbl_BookTransaction { get; set; }
    public virtual DbSet<Tbl_User> Tbl_User { get; set; }   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-2RDEOIE;Initial Catalog=UserMgMt;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
