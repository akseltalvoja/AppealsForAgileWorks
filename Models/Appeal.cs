using System.ComponentModel.DataAnnotations;

namespace AppealsProject.Models
{
    public class Appeal
    {
        private static int _lastAssignedId = 0;

        public int AppealId { get; set; }

        [Required(ErrorMessage = "Palun lisa pöördumisele nimi.")]
        public string AppealName { get; set; }
        [Required(ErrorMessage = "Palun lisa pöördumisele kirjeldus.")]
        public string AppealDescription { get; set; }

        [Required(ErrorMessage = "Palun lisa pöördumisele tähtaeg.")]
        public DateTime DueDateTime { get; set; }
        public DateTime AddedAt { get; }
        public static int GetLastAssignedId()
        {
            return _lastAssignedId;
        }
        public static void ResetLastAssignedId()
        {
            _lastAssignedId = 0;
        }
        public static void SetLastAssignedId(int value)
        {
            _lastAssignedId = value;
        }

        public Appeal()
        {
            var newId = Interlocked.Increment(ref _lastAssignedId);

            AppealId = newId;

            AddedAt = DateTime.Now;
        }
    }
}
