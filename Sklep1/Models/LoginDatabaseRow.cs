using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep1.Models
{
    public class LoginDatabaseRow
    {
        public LoginDatabaseRow(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public String Token { get; set; }
    }
}