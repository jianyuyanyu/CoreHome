﻿using System.ComponentModel.DataAnnotations;

namespace CoreHome.Data.Models
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
