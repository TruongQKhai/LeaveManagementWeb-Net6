namespace LeaveManagement.Web.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCrated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
