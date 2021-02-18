using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_3SemesterApp.Models
{
    public class ProductRate
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Column("Rate")]
        public short UserRate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
