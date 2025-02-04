using System.Text.Json.Serialization;

namespace graduationProject.Models
{
    public class NutritionalInfo
    {

        public int Id { get; set; }
        public string JsonData { get; set; } // Store raw JSON data as a string

        public int UserId { get; set; }     // Link to User

    }
}