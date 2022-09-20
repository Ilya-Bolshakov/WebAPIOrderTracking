using System;
using System.Collections.Generic;

namespace WebAPIOrderTracking.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Userpassword { get; set; } = null!;
    }
}
