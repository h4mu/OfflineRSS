using System.ComponentModel.DataAnnotations.Schema;

namespace OfflineRSS.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}