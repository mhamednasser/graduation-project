using graduationProject.DTOs;
using graduationProject.Models;
using System.Threading.Tasks;

namespace graduationProject.Interfaces
{
    public interface IPlanGenerationService
    {
        Task<StructuredDietPlan> GenerateDietPlanAsync(User user); // Updated return type

        Task<StructuredWorkoutPlan> GenerateWorkoutPlanAsync(User user); // updated tooooooooooo
    }
}