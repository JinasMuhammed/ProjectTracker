using System;
using System.Collections.Generic;

namespace ProjectTracker.Domain.Entities
{ 
    public class User
    {
      
        public Guid Id { get; set; }

     
        public string Username { get; set; } = null!;

    
        public string PasswordHash { get; set; } = null!;

     
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
