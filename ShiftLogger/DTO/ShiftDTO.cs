namespace ShiftLogger.DTO
{
    public class ShiftDTO
    {
        public Guid ShiftId { set; get; }

        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }

        public string ShiftDuration { set; get; }
    }
}
