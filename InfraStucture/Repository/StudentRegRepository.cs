using InfraStucture.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entity.Modal;
using Entity.ViewModal;

namespace InfraStucture.Repository
{
    public class StudentRegRepository : IStudentRegRepository
    {
        private readonly string _connection;

        public StudentRegRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection.ConnectionString;
        }

        // Method to insert multiple rows into the StudentEdu_Details table
        public void InsertMultipleRows(List<StudentEdu_Details> studentEdu_Details)
        {
            // Initialize a DataTable to hold the student education details
            DataTable table = new DataTable();
            table.Columns.Add("StudentRollNo", typeof(string));
            table.Columns.Add("StudentName", typeof(string));
            table.Columns.Add("StudentEmail", typeof(string));
            table.Columns.Add("StudentMobNo", typeof(string));

            // Populate the DataTable with student education data
            foreach (var studentEduReg in studentEdu_Details)
            {
                //table.Rows.Add(studentEduReg.StudentRollNo, studentEduReg.StudentName, studentEduReg.StudentEmail, studentEduReg.StudentMobNo);
            }

            // Use SqlBulkCopy to insert data in bulk
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = "StudentEdu_Details"; // Ensure the table name is correct
                    bulkCopy.ColumnMappings.Add("StudentRollNo", "StudentRollNo");
                    bulkCopy.ColumnMappings.Add("StudentName", "StudentName");
                    bulkCopy.ColumnMappings.Add("StudentEmail", "StudentEmail");
                    bulkCopy.ColumnMappings.Add("StudentMobNo", "StudentMobNo");
                    bulkCopy.WriteToServer(table);
                }
            }
        }

        // Method to insert a single student record in the StudentReg table
        public void InsertSingleEntry(string StudentRollNo, string StudentName, string StudentEmail, string StudentMobNo)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                conn.Open();
                string query = "INSERT INTO StudentReg (StudentRollNo, StudentName, StudentEmail, StudentMobNo) " +
                               "VALUES (@StudentRollNo, @StudentName, @StudentEmail, @StudentMobNo)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentRollNo", StudentRollNo);
                    cmd.Parameters.AddWithValue("@StudentName", StudentName);
                    cmd.Parameters.AddWithValue("@StudentEmail", StudentEmail);
                    cmd.Parameters.AddWithValue("@StudentMobNo", StudentMobNo);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
