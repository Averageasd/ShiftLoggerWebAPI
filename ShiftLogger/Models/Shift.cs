using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Models
{
    public class Shift
    {
        public Guid? ShiftId { set; get; }

        [Required(ErrorMessage = "start time is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-mm hh:mm:ss}")]
        public DateTime StartTime { set; get; }
        [Required(ErrorMessage = "end time is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-mm hh:mm:ss}")]
        public DateTime EndTime { set; get; }
    }
}
