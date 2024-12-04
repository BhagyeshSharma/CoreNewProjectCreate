using Entity.Modal;
using Entity.ViewModal;


namespace InfraStucture.Contract
{
    public interface IStudentRegRepository
    {
        public void InsertMultipleRows(List<StudentEdu_Details> people);
       public void InsertSingleEntry(string StudentRollNo, string StudentName, string StudentEmail,string StudentMobNo);
    }
}
