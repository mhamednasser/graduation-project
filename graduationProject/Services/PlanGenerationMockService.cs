using graduationProject.Interfaces;
using graduationProject.Models;
using System.Runtime.CompilerServices;

namespace graduationProject.Services
{
    public class PlanGenerationMockService : IPlanGenerationService
    {
        private readonly string[] _mockedDietResponses = [
            """
{
    "saturday": {
        "breakfast": {
            "main": {
                "name": "Foul Medames",
                "portion": "1 bowl",
                "calories": 350,
                "macronutrients": {
                    "carbs": 45,
                    "protein": 15,
                    "fat": 10
                }
            },
            "alternatives": [
                {
                    "name": "Taameya Sandwich",
                    "portion": "1 pita sandwich",
                    "calories": 290,
                    "macronutrients": {
                        "carbs": 40,
                        "protein": 10,
                        "fat": 8
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Grilled Chicken with Rice",
                "portion": "1 chicken breast with 1 cup of rice",
                "calories": 500,
                "macronutrients": {
                    "carbs": 60,
                    "protein": 35,
                    "fat": 8
                }
            },
            "alternatives": [
                {
                    "name": "Koshari",
                    "portion": "1 serving",
                    "calories": 600,
                    "macronutrients": {
                        "carbs": 95,
                        "protein": 20,
                        "fat": 10
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Lentil Soup",
                "portion": "1 bowl",
                "calories": 250,
                "macronutrients": {
                    "carbs": 30,
                    "protein": 15,
                    "fat": 5
                }
            },
            "alternatives": [
                {
                    "name": "Molokhia with Beef",
                    "portion": "1 serving",
                    "calories": 400,
                    "macronutrients": {
                        "carbs": 20,
                        "protein": 35,
                        "fat": 15
                    }
                }
            ]
        }
    },
    "sunday": {
        "breakfast": {
            "main": {
                "name": "Cheese with Tomatoes and Cucumbers",
                "portion": "1 plate",
                "calories": 280,
                "macronutrients": {
                    "carbs": 15,
                    "protein": 12,
                    "fat": 18
                }
            },
            "alternatives": [
                {
                    "name": "Oats with Honey",
                    "portion": "1 bowl",
                    "calories": 320,
                    "macronutrients": {
                        "carbs": 60,
                        "protein": 10,
                        "fat": 6
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Stuffed Bell Peppers",
                "portion": "2 peppers",
                "calories": 400,
                "macronutrients": {
                    "carbs": 50,
                    "protein": 20,
                    "fat": 10
                }
            },
            "alternatives": [
                {
                    "name": "Fattah",
                    "portion": "1 serving",
                    "calories": 550,
                    "macronutrients": {
                        "carbs": 70,
                        "protein": 20,
                        "fat": 20
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Roasted Vegetables with Quinoa",
                "portion": "1 plate",
                "calories": 300,
                "macronutrients": {
                    "carbs": 50,
                    "protein": 10,
                    "fat": 5
                }
            },
            "alternatives": [
                {
                    "name": "Shakshuka",
                    "portion": "1 skillet",
                    "calories": 350,
                    "macronutrients": {
                        "carbs": 25,
                        "protein": 20,
                        "fat": 15
                    }
                }
            ]
        }
    },
    "monday": {
        "breakfast": {
            "main": {
                "name": "Ful and Egg Sandwich",
                "portion": "1 pita sandwich",
                "calories": 320,
                "macronutrients": {
                    "carbs": 40,
                    "protein": 18,
                    "fat": 10
                }
            },
            "alternatives": [
                {
                    "name": "Hummus with Pita",
                    "portion": "1 serving",
                    "calories": 280,
                    "macronutrients": {
                        "carbs": 45,
                        "protein": 10,
                        "fat": 8
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Chicken Kabsa",
                "portion": "1 plate",
                "calories": 600,
                "macronutrients": {
                    "carbs": 70,
                    "protein": 40,
                    "fat": 15
                }
            },
            "alternatives": [
                {
                    "name": "Egyptian Moussaka",
                    "portion": "1 serving",
                    "calories": 450,
                    "macronutrients": {
                        "carbs": 40,
                        "protein": 15,
                        "fat": 25
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Tuna Salad with Vegetables",
                "portion": "1 bowl",
                "calories": 250,
                "macronutrients": {
                    "carbs": 25,
                    "protein": 30,
                    "fat": 8
                }
            },
            "alternatives": [
                {
                    "name": "Eggplant Tahini Salad",
                    "portion": "1 bowl",
                    "calories": 280,
                    "macronutrients": {
                        "carbs": 20,
                        "protein": 10,
                        "fat": 20
                    }
                }
            ]
        }
    },
    "tuesday": {
        "breakfast": {
            "main": {
                "name": "Egyptian Omelette",
                "portion": "2 eggs",
                "calories": 250,
                "macronutrients": {
                    "carbs": 10,
                    "protein": 18,
                    "fat": 15
                }
            },
            "alternatives": [
                {
                    "name": "Feta Cheese and Olives",
                    "portion": "1 plate",
                    "calories": 300,
                    "macronutrients": {
                        "carbs": 5,
                        "protein": 12,
                        "fat": 25
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Beef Shawarma with Salad",
                "portion": "1 wrap",
                "calories": 500,
                "macronutrients": {
                    "carbs": 55,
                    "protein": 30,
                    "fat": 18
                }
            },
            "alternatives": [
                {
                    "name": "Vegetable Moussaka",
                    "portion": "1 serving",
                    "calories": 380,
                    "macronutrients": {
                        "carbs": 40,
                        "protein": 14,
                        "fat": 15
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Basmati Rice with Chickpeas",
                "portion": "1 cup",
                "calories": 300,
                "macronutrients": {
                    "carbs": 55,
                    "protein": 10,
                    "fat": 5
                }
            },
            "alternatives": [
                {
                    "name": "Kofta with a Side Salad",
                    "portion": "2 pieces with salad",
                    "calories": 450,
                    "macronutrients": {
                        "carbs": 30,
                        "protein": 35,
                        "fat": 20
                    }
                }
            ]
        }
    },
    "wednesday": {
        "breakfast": {
            "main": {
                "name": "Tahini Yogurt with Dates",
                "portion": "1 bowl",
                "calories": 300,
                "macronutrients": {
                    "carbs": 45,
                    "protein": 8,
                    "fat": 11
                }
            },
            "alternatives": [
                {
                    "name": "Fruit Salad with Nuts",
                    "portion": "1 bowl",
                    "calories": 270,
                    "macronutrients": {
                        "carbs": 50,
                        "protein": 7,
                        "fat": 10
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Grilled Fish with Tomato Rice",
                "portion": "1 fillet with 1 cup rice",
                "calories": 480,
                "macronutrients": {
                    "carbs": 60,
                    "protein": 35,
                    "fat": 8
                }
            },
            "alternatives": [
                {
                    "name": "Falafel Platter",
                    "portion": "1 plate",
                    "calories": 400,
                    "macronutrients": {
                        "carbs": 50,
                        "protein": 20,
                        "fat": 20
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Vegetable Stew with Lentils",
                "portion": "1 bowl",
                "calories": 320,
                "macronutrients": {
                    "carbs": 50,
                    "protein": 20,
                    "fat": 7
                }
            },
            "alternatives": [
                {
                    "name": "Couscous with Harissa",
                    "portion": "1 cup",
                    "calories": 380,
                    "macronutrients": {
                        "carbs": 70,
                        "protein": 15,
                        "fat": 5
                    }
                }
            ]
        }
    },
    "thursday": {
        "breakfast": {
            "main": {
                "name": "Pancakes with Honey",
                "portion": "2 pancakes",
                "calories": 290,
                "macronutrients": {
                    "carbs": 50,
                    "protein": 8,
                    "fat": 8
                }
            },
            "alternatives": [
                {
                    "name": "Ricotta Toast with Apple",
                    "portion": "2 slices",
                    "calories": 330,
                    "macronutrients": {
                        "carbs": 55,
                        "protein": 12,
                        "fat": 10
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Chicken Tagine",
                "portion": "1 serving",
                "calories": 540,
                "macronutrients": {
                    "carbs": 48,
                    "protein": 35,
                    "fat": 20
                }
            },
            "alternatives": [
                {
                    "name": "Sesame Noodles with Vegetables",
                    "portion": "1 plate",
                    "calories": 400,
                    "macronutrients": {
                        "carbs": 70,
                        "protein": 10,
                        "fat": 12
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Zucchini and Feta Frittata",
                "portion": "1 slice",
                "calories": 280,
                "macronutrients": {
                    "carbs": 12,
                    "protein": 18,
                    "fat": 15
                }
            },
            "alternatives": [
                {
                    "name": "Spinach and Paneer Curry",
                    "portion": "1 serving",
                    "calories": 350,
                    "macronutrients": {
                        "carbs": 20,
                        "protein": 25,
                        "fat": 20
                    }
                }
            ]
        }
    },
    "friday": {
        "breakfast": {
            "main": {
                "name": "Halawa with Bread",
                "portion": "1 serving",
                "calories": 360,
                "macronutrients": {
                    "carbs": 60,
                    "protein": 10,
                    "fat": 15
                }
            },
            "alternatives": [
                {
                    "name": "Fruit and Yogurt Parfait",
                    "portion": "1 bowl",
                    "calories": 300,
                    "macronutrients": {
                        "carbs": 50,
                        "protein": 12,
                        "fat": 8
                    }
                }
            ]
        },
        "lunch": {
            "main": {
                "name": "Beef and Okra Stew",
                "portion": "1 bowl",
                "calories": 450,
                "macronutrients": {
                    "carbs": 35,
                    "protein": 30,
                    "fat": 20
                }
            },
            "alternatives": [
                {
                    "name": "Stuffed Eggplant Rolls",
                    "portion": "2 pieces",
                    "calories": 400,
                    "macronutrients": {
                        "carbs": 30,
                        "protein": 20,
                        "fat": 25
                    }
                }
            ]
        },
        "dinner": {
            "main": {
                "name": "Mushroom and Rice Pilaf",
                "portion": "1 cup",
                "calories": 320,
                "macronutrients": {
                    "carbs": 60,
                    "protein": 10,
                    "fat": 8
                }
            },
            "alternatives": [
                {
                    "name": "Lentil Burgers with Salad",
                    "portion": "2 patties",
                    "calories": 280,
                    "macronutrients": {
                        "carbs": 40,
                        "protein": 15,
                        "fat": 10
                    }
                }
            ]
        }
    }
}
"""
            ];






        private readonly string[] _mockedWorkoutResponses = [
            """
{
  "duration": "1 week",
  "goal": "Gaining Muscle",
  "daily_plans": [
    {
      "day": "Monday",
      "focus": "Upper Body Strength",
      "exercises": [
        {
          "name": "Bench Press",
          "muscle_group": "Chest",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep feet flat on the ground",
            "Maintain a controlled movement"
          ]
        },
        {
          "name": "Bent Over Row",
          "muscle_group": "Back",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Maintain a flat back",
            "Pull towards your lower ribcage"
          ]
        },
        {
          "name": "Shoulder Press",
          "muscle_group": "Shoulders",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Start with dumbbells at shoulder height",
            "Press upward while maintaining core stability"
          ]
        },
        {
          "name": "Lat Pulldown",
          "muscle_group": "Back",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Focus on squeezing shoulder blades together",
            "Control the weight on the way up"
          ]
        },
        {
          "name": "Incline Dumbbell Fly",
          "muscle_group": "Chest",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Use a bench set to an incline",
            "Don't overstretch your arms"
          ]
        },
        {
          "name": "Tricep Dips",
          "muscle_group": "Triceps",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Lower down until elbows are at 90 degrees",
            "Keep your body close to the bench"
          ]
        }
      ]
    },
    {
      "day": "Tuesday",
      "focus": "Lower Body Strength",
      "exercises": [
        {
          "name": "Squats",
          "muscle_group": "Legs",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your chest up",
            "Ensure knees don’t go past toes"
          ]
        },
        {
          "name": "Leg Press",
          "muscle_group": "Legs",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your back flat against the seat",
            "Push through your heels"
          ]
        },
        {
          "name": "Lunges",
          "muscle_group": "Legs",
          "sets": 3,
          "reps": "10-12 (each leg)",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Step far enough to lower your knee to just above the ground",
            "Alternate legs for each set"
          ]
        },
        {
          "name": "Leg Curl Machine",
          "muscle_group": "Hamstrings",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Adjust the machine to fit your height",
            "Control the weight very slowly"
          ]
        },
        {
          "name": "Calf Raises",
          "muscle_group": "Calves",
          "sets": 3,
          "reps": "12-15",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Perform these on a step for increased range of motion",
            "Hold onto something for balance if needed"
          ]
        },
        {
          "name": "Plank",
          "muscle_group": "Core",
          "sets": 3,
          "reps": "30 seconds",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your body flat like a board",
            "Engage your core throughout"
          ]
        }
      ]
    },
    {
      "day": "Wednesday",
      "focus": "Rest Day",
      "exercises": []
    },
    {
      "day": "Thursday",
      "focus": "Full Body Strength",
      "exercises": [
        {
          "name": "Dumbbell Deadlift",
          "muscle_group": "Full Body",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep dumbbells close to your body",
            "Hinge at your hips as you lower"
          ]
        },
        {
          "name": "Push-Ups",
          "muscle_group": "Chest",
          "sets": 3,
          "reps": "8-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your body straight throughout the movement",
            "Go as low as you can comfortably"
          ]
        },
        {
          "name": "Seated Row Machine",
          "muscle_group": "Back",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Squeeze your shoulder blades together at the end",
            "Do not lean back too much"
          ]
        },
        {
          "name": "Leg Extensions",
          "muscle_group": "Quads",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Adjust the seat to fit your height",
            "Control both the upward and downward motion"
          ]
        },
        {
          "name": "Cable Lateral Raise",
          "muscle_group": "Shoulders",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep a slight bend in your elbows",
            "Control the weight as you raise it"
          ]
        },
        {
          "name": "Bicycle Crunches",
          "muscle_group": "Core",
          "sets": 3,
          "reps": "15-20",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your elbows wide",
            "Focus on a slow and controlled movement"
          ]
        }
      ]
    },
    {
      "day": "Friday",
      "focus": "Cardio and Flexibility",
      "exercises": [
        {
          "name": "Treadmill Walking/Jogging",
          "muscle_group": "Cardio",
          "sets": 1,
          "reps": "30 minutes",
          "rest_between_sets": "N/A",
          "intensity": "Moderate",
          "notes": [
            "Warm-up for 5 minutes",
            "Increase speed gradually"
          ]
        },
        {
          "name": "Stretching Routine",
          "muscle_group": "Flexibility",
          "sets": 1,
          "reps": "15-20 minutes",
          "rest_between_sets": "N/A",
          "intensity": "N/A",
          "notes": [
            "Focus on hamstrings, quadriceps, and upper body",
            "Hold each stretch for at least 15-30 seconds"
          ]
        }
      ]
    },
    {
      "day": "Saturday",
      "focus": "Upper Body Strength",
      "exercises": [
        {
          "name": "Incline Bench Press",
          "muscle_group": "Chest",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Focus on control and full range of motion",
            "Engage your core during movement"
          ]
        },
        {
          "name": "Pull-Ups (assisted if necessary)",
          "muscle_group": "Back",
          "sets": 3,
          "reps": "5-8",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Use an assisted machine if needed",
            "Maintain control while lowering"
          ]
        },
        {
          "name": "Barbell Shoulder Press",
          "muscle_group": "Shoulders",
          "sets": 3,
          "reps": "8-10",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Stand with feet shoulder-width apart",
            "Push straight up without arching your back"
          ]
        },
        {
          "name": "Dumbbell Chest Fly",
          "muscle_group": "Chest",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Focus on a slow and controlled movement",
            "Maintain a slight bend in your elbows"
          ]
        },
        {
          "name": "Tricep Extension",
          "muscle_group": "Triceps",
          "sets": 3,
          "reps": "10-12",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Keep your upper arms stationary",
            "Avoid swinging the weights"
          ]
        },
        {
          "name": "Russian Twists",
          "muscle_group": "Core",
          "sets": 3,
          "reps": "15 (each side)",
          "rest_between_sets": "60 seconds",
          "intensity": "Moderate",
          "notes": [
            "Maintain a strong core throughout",
            "Control the movement side to side"
          ]
        }
      ]
    },
    {
      "day": "Sunday",
      "focus": "Rest Day",
      "exercises": []
    }
  ],
  "rest_days": [
    "Wednesday",
    "Sunday"
  ],
  "notes": [
    "Always warm up for 5-10 minutes before starting your workout.",
    "Stay hydrated during workouts.",
    "Ensure proper form and technique to avoid injury."
  ]
}
"""
        ];











        private readonly Random random = new Random();

        public Task<StructuredDietPlan> GenerateDietPlanAsync(User user)
        {
            var js = _mockedDietResponses[random.Next(_mockedDietResponses.Length)];

            return Task.FromResult(new StructuredDietPlan
            {
                UserId = user.Id,
                PlanJson = js
            });
        }

        public Task<StructuredWorkoutPlan> GenerateWorkoutPlanAsync(User user)
        {
            var js = _mockedWorkoutResponses[random.Next(_mockedWorkoutResponses.Length)];

            return Task.FromResult(new StructuredWorkoutPlan
            {
                UserId = user.Id,
                PlanJson = js
            });
        }
    }
}
