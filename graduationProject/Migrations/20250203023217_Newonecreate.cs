using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace graduationProject.Migrations
{
    /// <inheritdoc />
    public partial class Newonecreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionalInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    FitnessLevel = table.Column<int>(type: "int", nullable: false),
                    WeeklyWorkoutDays = table.Column<int>(type: "int", nullable: false),
                    WorkoutDuration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    DietaryRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredDiet = table.Column<int>(type: "int", nullable: false),
                    MedicalConditions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sets = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nExercises_MuscleGroups_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalTable: "MuscleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedDietPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MealPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedDietPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedDietPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedWorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedWorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedWorkoutPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructuredDietPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuredDietPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructuredDietPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructuredWorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructuredWorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructuredWorkoutPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuscleGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MuscleGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ARM" },
                    { 2, "Chest" },
                    { 3, "ABS" },
                    { 4, "LEG" },
                    { 5, "Back & Shoulder" },
                    { 6, "Stretches" }
                });

            migrationBuilder.InsertData(
                table: "nExercises",
                columns: new[] { "Id", "Icon", "MuscleGroupId", "Name", "Sets", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "https://img.youtube.com/vi/3DH2fwV5u1k/maxresdefault.jpg", 1, "Overhead Extension", "3*12", "https://www.youtube.com/embed/3DH2fwV5u1k" },
                    { 2, "https://img.youtube.com/vi/SNFj4cBJ6ds/maxresdefault.jpg", 1, "Dumbbell Press", "3*10", "https://www.youtube.com/embed/SNFj4cBJ6ds" },
                    { 3, "https://img.youtube.com/vi/tPAkjMLWvCY/maxresdefault.jpg", 1, "Tricep Dips", "3*10", "https://www.youtube.com/embed/tPAkjMLWvCY" },
                    { 4, "https://img.youtube.com/vi/sz_BVxduUWE/maxresdefault.jpg", 1, "Dumbbell Pullover", "3*12", "https://www.youtube.com/embed/sz_BVxduUWE" },
                    { 5, "https://img.youtube.com/vi/dsRNsZHuUkw/maxresdefault.jpg", 1, "Concentration Curl", "3*12", "https://www.youtube.com/embed/dsRNsZHuUkw" },
                    { 6, "https://img.youtube.com/vi/BneGVmsYBTY/maxresdefault.jpg", 1, "Cable Reverse", "3*10", "https://www.youtube.com/embed/BneGVmsYBTY" },
                    { 7, "https://img.youtube.com/vi/KVT2n9CpqRU/maxresdefault.jpg", 2, "Cable Crossovers", "3*12", "https://www.youtube.com/embed/KVT2n9CpqRU" },
                    { 8, "https://img.youtube.com/vi/W1qukYP2FLA/maxresdefault.jpg", 2, "Chest Press", "3*10", "https://www.youtube.com/embed/W1qukYP2FLA" },
                    { 9, "https://img.youtube.com/vi/WU_AUjGaHnM/maxresdefault.jpg", 2, "Dumbbell Chest", "3*10", "https://www.youtube.com/embed/WU_AUjGaHnM" },
                    { 10, "https://img.youtube.com/vi/OAps0BeSd7Y/maxresdefault.jpg", 2, "Dumbbell Fly", "3*12", "https://www.youtube.com/embed/OAps0BeSd7Y" },
                    { 11, "https://img.youtube.com/vi/mATfjUjXr60/maxresdefault.jpg", 2, "Band Bench Press", "3*12", "https://www.youtube.com/embed/mATfjUjXr60" },
                    { 12, "https://img.youtube.com/vi/WfFWsolPlP8/maxresdefault.jpg", 3, "Weighted Crunch", "3*10", "https://www.youtube.com/embed/WfFWsolPlP8" },
                    { 13, "https://img.youtube.com/vi/cK2VIjqK4PY/maxresdefault.jpg", 3, "Rotating Stomach", "3*12", "https://www.youtube.com/embed/cK2VIjqK4PY?si=-JIDMS5GRjBU_2PI" },
                    { 14, "https://img.youtube.com/vi/iZOAVk8qjjU/maxresdefault.jpg", 3, "Cable Crunch", "3*10", "https://www.youtube.com/embed/iZOAVk8qjjU" },
                    { 15, "https://img.youtube.com/vi/jZYon72vEtU/maxresdefault.jpg", 3, "Leg Reverse Crunch", "3*12", "https://www.youtube.com/embed/jZYon72vEtU" },
                    { 16, "https://img.youtube.com/vi/-uQO2gG8sjk/maxresdefault.jpg", 3, " Seated Crunch", "3*10", "https://www.youtube.com/embed/-uQO2gG8sjk" },
                    { 17, "https://img.youtube.com/vi/1SaYpphpQ5w/maxresdefault.jpg", 4, "Leg Curl", "3*10", "https://www.youtube.com/embed/1SaYpphpQ5w" },
                    { 18, "https://img.youtube.com/vi/weVucBtUl7I/maxresdefault.jpg", 4, "Low Bar Squat", "3*12", "https://www.youtube.com/embed/weVucBtUl7I" },
                    { 19, "https://img.youtube.com/vi/aL3VFTD_4ew/maxresdefault.jpg", 4, "Smith Deadlift", "3*12", "https://www.youtube.com/embed/aL3VFTD_4ew" },
                    { 20, "https://img.youtube.com/vi/myNurJqfgDc/maxresdefault.jpg", 4, "Leg Press", "3*10", "https://www.youtube.com/embed/myNurJqfgDc" },
                    { 21, "https://img.youtube.com/vi/qvCOYW80akQ/maxresdefault.jpg", 4, "Standing Calf Raises", "3*10", "https://www.youtube.com/embed/qvCOYW80akQ" },
                    { 22, "https://img.youtube.com/vi/33sUKgU3yjI/maxresdefault.jpg", 4, "Hip Abduction", "3*12", "https://www.youtube.com/embed/33sUKgU3yjI" },
                    { 23, "https://img.youtube.com/vi/hghkuX7ejVo/maxresdefault.jpg", 4, "Hip Thrusts", "3*10", "https://www.youtube.com/embed/hghkuX7ejVo" },
                    { 24, "https://img.youtube.com/vi/2Zbv0h5OA3I/maxresdefault.jpg", 4, "Barbell Lunge", "3*10", "https://www.youtube.com/embed/2Zbv0h5OA3I" },
                    { 25, "https://img.youtube.com/vi/EQZvNNjCCPM/maxresdefault.jpg", 5, "Cable Elevated Row", "3*12", "https://www.youtube.com/embed/EQZvNNjCCPM" },
                    { 26, "https://img.youtube.com/vi/T-wcu8iRSW4/maxresdefault.jpg", 5, "Cable Arm Pulldown", "3*10", "https://www.youtube.com/embed/T-wcu8iRSW4" },
                    { 27, "https://img.youtube.com/vi/XsN8glqhebU/maxresdefault.jpg", 5, "Reverse Fly", "3*12", "https://www.youtube.com/embed/XsN8glqhebU" },
                    { 28, "https://img.youtube.com/vi/LGjNc2nKYsk/maxresdefault.jpg", 5, "Lying T Bar Back", "3*10", "https://www.youtube.com/embed/LGjNc2nKYsk" },
                    { 29, "https://img.youtube.com/vi/imkOFyqxNUc/maxresdefault.jpg", 5, "Cable Pulldown", "3*12", "https://www.youtube.com/embed/imkOFyqxNUc" },
                    { 30, "https://img.youtube.com/vi/anxZfYAXmsY/maxresdefault.jpg", 5, "Dumbbell Press", "3*10", "https://www.youtube.com/embed/anxZfYAXmsY" },
                    { 31, "https://img.youtube.com/vi/U79gWoPFNMU/maxresdefault.jpg", 5, "Cuban Press", "3*12", "https://www.youtube.com/embed/U79gWoPFNMU" },
                    { 32, "https://img.youtube.com/vi/BEazZDwwns0/maxresdefault.jpg", 5, "Raise Plate", "3*10", "https://www.youtube.com/embed/BEazZDwwns0" },
                    { 33, "https://img.youtube.com/vi/YkvgWqV9SuU/maxresdefault.jpg", 6, "Lying Cross Over", "3*12", "https://www.youtube.com/embed/YkvgWqV9SuU" },
                    { 34, "https://img.youtube.com/vi/UzLl2wWApDM/maxresdefault.jpg", 6, "Crouching Heel Stretch", "3*10", "https://www.youtube.com/embed/UzLl2wWApDM" },
                    { 35, "https://img.youtube.com/vi/1qVP0Sz-jhE/maxresdefault.jpg", 6, "Kneeling Toe Up Stretch", "3*10", "https://www.youtube.com/embed/1qVP0Sz-jhE" },
                    { 36, "https://img.youtube.com/vi/sJ7FLxLCqe8/maxresdefault.jpg", 6, "Single Lean Back Stretch", "3*10", "https://www.youtube.com/embed/sJ7FLxLCqe8" },
                    { 37, "https://img.youtube.com/vi/W-4JfmbiEl8/maxresdefault.jpg", 6, "Back Pec Stretch", "3*10", "https://www.youtube.com/embed/W-4JfmbiEl8" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutPlanId",
                table: "Exercises",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedDietPlans_UserId",
                table: "GeneratedDietPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedWorkoutPlans_UserId",
                table: "GeneratedWorkoutPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_nExercises_MuscleGroupId",
                table: "nExercises",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StructuredDietPlans_UserId",
                table: "StructuredDietPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StructuredWorkoutPlans_UserId",
                table: "StructuredWorkoutPlans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DietPlans");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "GeneratedDietPlans");

            migrationBuilder.DropTable(
                name: "GeneratedWorkoutPlans");

            migrationBuilder.DropTable(
                name: "nExercises");

            migrationBuilder.DropTable(
                name: "NutritionalInfos");

            migrationBuilder.DropTable(
                name: "StructuredDietPlans");

            migrationBuilder.DropTable(
                name: "StructuredWorkoutPlans");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
