using System.ComponentModel.DataAnnotations;

namespace vscode.Models
{
    public class TrafficSample
    {
     
       // [Required]
       // public string id { get; set; } = $"sample:{Guid.NewGuid()}";
        [Required]
        public string EagleBotid { get; set; } = String.Empty; 
        [Required]
        public string Longitude  { get; set; }= String.Empty; 
        
        [Required]
        public string Latitude { get; set; }  = String.Empty; 
        [Required]
        public DateTime Timestamp {get;set;} = DateTime.UtcNow; 
        [Required]
        public string RoadName { get; set; }= String.Empty;

        public string DirOfTrafficFlow { get; set; }= String.Empty;

        public decimal? RateOfTrafficFlow { get; set; }
        public decimal? AverageVehicleSpeed{ get; set; }

    }
}