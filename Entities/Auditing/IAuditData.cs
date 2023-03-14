namespace BasicsOfWebApi.Entities.Auditing
{
    public interface IAuditData
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
