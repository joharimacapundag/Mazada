using Mazada.Services;
using System;

namespace Mazada.Model
{
    [Table("users")]
    class User 
    {
        [Column("user_id", IsPrimaryKey = true, AutoIncrement = true)]
        public int? UserId { get; private set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("created_at", AutoIncrement = true)]
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

    }
}
