using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfflineRSS.Models
{
    public sealed class Feed
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime LastSynced { get; set; }
        public int ArticleId { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}