namespace LearningApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Code { get; set; }


        public string? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }

        public string? EditBy { get; set; }
        public DateTime? EditAt { get; set; }

        public string? DeleteBy { get; set; }
        public DateTime? DeleteAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
