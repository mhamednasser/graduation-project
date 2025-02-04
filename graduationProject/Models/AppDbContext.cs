using graduationProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<nExercise> nExercises { get; set; }
    public DbSet<MuscleGroup> MuscleGroups { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }  // اممممممممممممممممممم
        public DbSet<GeneratedDietPlan> GeneratedDietPlans { get; set; }
        public DbSet<GeneratedWorkoutPlan> GeneratedWorkoutPlans { get; set; }

        // 
        public DbSet<StructuredDietPlan> StructuredDietPlans { get; set; }
        public DbSet<StructuredWorkoutPlan> StructuredWorkoutPlans { get; set; }

        public DbSet<NutritionalInfo> NutritionalInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



      

        // Configure relationships
        modelBuilder.Entity<StructuredDietPlan>()
                .HasOne(sdp => sdp.User)
                .WithMany(u => u.StructuredDietPlans)
                .HasForeignKey(sdp => sdp.UserId);

            modelBuilder.Entity<StructuredWorkoutPlan>()
                .HasOne(swp => swp.User)
                .WithMany(u => u.StructuredWorkoutPlans)
                .HasForeignKey(swp => swp.UserId);

       


        modelBuilder.Entity<MuscleGroup>().HasData(
               new MuscleGroup { Id = 1, Name = "ARM" },
               new MuscleGroup { Id = 2, Name = "Chest" },
               new MuscleGroup { Id = 3, Name = "ABS" },
               new MuscleGroup { Id = 4, Name = "LEG" },
               new MuscleGroup { Id = 5, Name = "Back & Shoulder" },
               new MuscleGroup { Id = 6, Name = "Stretches" }
           );



        modelBuilder.Entity<nExercise>().HasData(
               new nExercise { Id = 1, Name = "Overhead Extension", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/3DH2fwV5u1k", Icon = "https://img.youtube.com/vi/3DH2fwV5u1k/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 2, Name = "Dumbbell Press", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/SNFj4cBJ6ds", Icon = "https://img.youtube.com/vi/SNFj4cBJ6ds/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 3, Name = "Tricep Dips", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/tPAkjMLWvCY", Icon = "https://img.youtube.com/vi/tPAkjMLWvCY/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 4, Name = "Dumbbell Pullover", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/sz_BVxduUWE", Icon = "https://img.youtube.com/vi/sz_BVxduUWE/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 5, Name = "Concentration Curl", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/dsRNsZHuUkw", Icon = "https://img.youtube.com/vi/dsRNsZHuUkw/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 6, Name = "Cable Reverse", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/BneGVmsYBTY", Icon = "https://img.youtube.com/vi/BneGVmsYBTY/maxresdefault.jpg", MuscleGroupId = 1 },
              new nExercise { Id = 7, Name = "Cable Crossovers", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/KVT2n9CpqRU", Icon = "https://img.youtube.com/vi/KVT2n9CpqRU/maxresdefault.jpg", MuscleGroupId = 2 },
              new nExercise { Id = 8, Name = "Chest Press", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/W1qukYP2FLA", Icon = "https://img.youtube.com/vi/W1qukYP2FLA/maxresdefault.jpg", MuscleGroupId = 2 },
              new nExercise { Id = 9, Name = "Dumbbell Chest", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/WU_AUjGaHnM", Icon = "https://img.youtube.com/vi/WU_AUjGaHnM/maxresdefault.jpg", MuscleGroupId = 2 },
              new nExercise { Id = 10, Name = "Dumbbell Fly", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/OAps0BeSd7Y", Icon = "https://img.youtube.com/vi/OAps0BeSd7Y/maxresdefault.jpg", MuscleGroupId = 2 },
              new nExercise { Id = 11, Name = "Band Bench Press", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/mATfjUjXr60", Icon = "https://img.youtube.com/vi/mATfjUjXr60/maxresdefault.jpg", MuscleGroupId = 2 },
              new nExercise { Id = 12, Name = "Weighted Crunch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/WfFWsolPlP8", Icon = "https://img.youtube.com/vi/WfFWsolPlP8/maxresdefault.jpg", MuscleGroupId = 3 },
              new nExercise { Id = 13, Name = "Rotating Stomach", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/cK2VIjqK4PY?si=-JIDMS5GRjBU_2PI", Icon = "https://img.youtube.com/vi/cK2VIjqK4PY/maxresdefault.jpg", MuscleGroupId = 3 },
                new nExercise { Id = 14, Name = "Cable Crunch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/iZOAVk8qjjU", Icon = "https://img.youtube.com/vi/iZOAVk8qjjU/maxresdefault.jpg", MuscleGroupId = 3 },
              new nExercise { Id = 15, Name = "Leg Reverse Crunch", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/jZYon72vEtU", Icon = "https://img.youtube.com/vi/jZYon72vEtU/maxresdefault.jpg", MuscleGroupId = 3 },
              new nExercise { Id = 16, Name = " Seated Crunch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/-uQO2gG8sjk", Icon = "https://img.youtube.com/vi/-uQO2gG8sjk/maxresdefault.jpg", MuscleGroupId = 3 },
              new nExercise { Id = 17, Name = "Leg Curl", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/1SaYpphpQ5w", Icon = "https://img.youtube.com/vi/1SaYpphpQ5w/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 18, Name = "Low Bar Squat", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/weVucBtUl7I", Icon = "https://img.youtube.com/vi/weVucBtUl7I/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 19, Name = "Smith Deadlift", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/aL3VFTD_4ew", Icon = "https://img.youtube.com/vi/aL3VFTD_4ew/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 20, Name = "Leg Press", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/myNurJqfgDc", Icon = "https://img.youtube.com/vi/myNurJqfgDc/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 21, Name = "Standing Calf Raises", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/qvCOYW80akQ", Icon = "https://img.youtube.com/vi/qvCOYW80akQ/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 22, Name = "Hip Abduction", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/33sUKgU3yjI", Icon = "https://img.youtube.com/vi/33sUKgU3yjI/maxresdefault.jpg", MuscleGroupId = 4 },
               new nExercise { Id = 23, Name = "Hip Thrusts", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/hghkuX7ejVo", Icon = "https://img.youtube.com/vi/hghkuX7ejVo/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 24, Name = "Barbell Lunge", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/2Zbv0h5OA3I", Icon = "https://img.youtube.com/vi/2Zbv0h5OA3I/maxresdefault.jpg", MuscleGroupId = 4 },
              new nExercise { Id = 25, Name = "Cable Elevated Row", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/EQZvNNjCCPM", Icon = "https://img.youtube.com/vi/EQZvNNjCCPM/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 26, Name = "Cable Arm Pulldown", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/T-wcu8iRSW4", Icon = "https://img.youtube.com/vi/T-wcu8iRSW4/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 27, Name = "Reverse Fly", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/XsN8glqhebU", Icon = "https://img.youtube.com/vi/XsN8glqhebU/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 28, Name = "Lying T Bar Back", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/LGjNc2nKYsk", Icon = "https://img.youtube.com/vi/LGjNc2nKYsk/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 29, Name = "Cable Pulldown", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/imkOFyqxNUc", Icon = "https://img.youtube.com/vi/imkOFyqxNUc/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 30, Name = "Dumbbell Press", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/anxZfYAXmsY", Icon = "https://img.youtube.com/vi/anxZfYAXmsY/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 31, Name = "Cuban Press", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/U79gWoPFNMU", Icon = "https://img.youtube.com/vi/U79gWoPFNMU/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 32, Name = "Raise Plate", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/BEazZDwwns0", Icon = "https://img.youtube.com/vi/BEazZDwwns0/maxresdefault.jpg", MuscleGroupId = 5 },
              new nExercise { Id = 33, Name = "Lying Cross Over", Sets = "3*12", VideoUrl = "https://www.youtube.com/embed/YkvgWqV9SuU", Icon = "https://img.youtube.com/vi/YkvgWqV9SuU/maxresdefault.jpg", MuscleGroupId = 6 },
               new nExercise { Id = 34, Name = "Crouching Heel Stretch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/UzLl2wWApDM", Icon = "https://img.youtube.com/vi/UzLl2wWApDM/maxresdefault.jpg", MuscleGroupId = 6 },
              new nExercise { Id = 35, Name = "Kneeling Toe Up Stretch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/1qVP0Sz-jhE", Icon = "https://img.youtube.com/vi/1qVP0Sz-jhE/maxresdefault.jpg", MuscleGroupId = 6 },
              new nExercise { Id = 36, Name = "Single Lean Back Stretch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/sJ7FLxLCqe8", Icon = "https://img.youtube.com/vi/sJ7FLxLCqe8/maxresdefault.jpg", MuscleGroupId = 6 },
              new nExercise { Id = 37, Name = "Back Pec Stretch", Sets = "3*10", VideoUrl = "https://www.youtube.com/embed/W-4JfmbiEl8", Icon = "https://img.youtube.com/vi/W-4JfmbiEl8/maxresdefault.jpg", MuscleGroupId = 6 }

            );
    }

    }


