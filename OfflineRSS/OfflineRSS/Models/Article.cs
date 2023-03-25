using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfflineRSS.Models
{
    public sealed class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Feed Feed { get; set; }
    }
}