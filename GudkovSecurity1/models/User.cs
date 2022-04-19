using System;

namespace GudkovSecurity1.models
{
    public class User
    {
        public int Id { get; set; }

        public bool isBlocked { get; set; }

        public string Login { get; set; }

        public bool isAdmin { get; set; }

        public DateTime? LastBlockTime { get; set; }
    }
}
