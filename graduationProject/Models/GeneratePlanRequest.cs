namespace graduationProject.Models
{
    public class GeneratePlanRequest
    {
        public int UserId { get; set; }
    }
}
//  pass the UserId when making a request to generate workout or diet plans. This model will be sent in the POST request.