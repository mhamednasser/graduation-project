using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using graduationProject.DTOs;
using graduationProject.Interfaces;
using graduationProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;

namespace graduationProject.Services
{
    public class PlanGenerationService : IPlanGenerationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _openAiApiKey;
        private readonly ILogger<PlanGenerationService> _logger;
        private readonly ChatClient _chatClient;


        private static readonly ChatResponseFormat DietPlanResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
jsonSchemaIsStrict: true,
jsonSchemaFormatName: "diet_plan",
jsonSchema: BinaryData.FromBytes("""
{
    "$schema": "https://json-schema.org/draft/2019-09/schema",
    "title": "diet plan",
    "description": "description of a diet play for a week, three meals a day. with a main dish and alternatives, each with macronutrients",
    "type": "object",
    "additionalProperties": false,
    "required": [
        "saturday",
        "sunday",
        "monday",
        "tuesday",
        "wednesday",
        "thursday",
        "friday"
    ],
    "properties": {
        "saturday": {
            "$ref": "#/$defs/day"
        },
        "sunday": {
            "$ref": "#/$defs/day"
        },
        "monday": {
            "$ref": "#/$defs/day"
        },
        "tuesday": {
            "$ref": "#/$defs/day"
        },
        "wednesday": {
            "$ref": "#/$defs/day"
        },
        "thursday": {
            "$ref": "#/$defs/day"
        },
        "friday": {
            "$ref": "#/$defs/day"
        }
    },
    "$defs": {
        "day": {
            "type": "object",
            "additionalProperties": false,
            "required": [
                "breakfast",
                "lunch",
                "dinner"
            ],
            "properties": {
                "breakfast": {
                    "$ref": "#/$defs/meal"
                },
                "lunch": {
                    "$ref": "#/$defs/meal"
                },
                "dinner": {
                    "$ref": "#/$defs/meal"
                }
            }
        },
        "meal": {
            "description": "meal with a main dish and at least one alternative",
            "type": "object",
            "additionalProperties": false,
            "required": [
                "main",
                "alternatives"
            ],
            "properties": {
                "main": {
                    "$ref": "#/$defs/dish"
                },
                "alternatives": {
                    "description": "an array of alternative dishes, there must be at least one alternative and at most three",
                    "type": "array",
                    "items": {
                        "$ref": "#/$defs/dish"
                    }
                }
            }
        },
        "dish": {
            "description": "name of a dish with its portion and macronutrients",
            "type": "object",
            "additionalProperties": false,
            "required": [
                "name",
                "portion",
                "calories",
                "macronutrients"
            ],
            "properties": {
                "name": {
                    "description": "english name of the dish",
                    "type": "string"
                },
                "portion": {
                    "description": "the serving size of the dish in free text units, e.g. '1 slice' etc.",
                    "type": "string"
                },
                "calories": {
                    "description": "total calories in the meal in kcal",
                    "type": "number"
                },
                "macronutrients": {
                    "description": "macronutrients in grams",
                    "type": "object",
                    "additionalProperties": false,
                    "required": [
                        "carbs",
                        "protein",
                        "fat"
                    ],
                    "properties": {
                        "carbs": {
                            "description": "total carbohydrates in grams",
                            "type": "number"
                        },
                        "protein": {
                            "description": "total protein in grams",
                            "type": "number"
                        },
                        "fat": {
                            "description": "total fat in grams",
                            "type": "number"
                        }
                    }
                }
            }
        }
    }
}
"""u8.ToArray())
);

        //        // Function schema for the diet plan
        //        private static readonly string DietPlanFunctionSchema = @"
        //{
        //  ""name"": ""generate_diet_plan"",
        //  ""description"": ""Generates a structured diet plan based on user input."",
        //  ""parameters"": {
        //    ""type"": ""object"",
        //    ""properties"": {
        //      ""Duration"": {
        //        ""type"": ""string"",
        //        ""description"": ""The duration of the diet plan.""
        //      },
        //      ""DailyCalories"": {
        //        ""type"": ""integer"",
        //        ""description"": ""The total daily calorie target.""
        //      },
        //      ""MacronutrientDistribution"": {
        //        ""type"": ""string"",
        //        ""description"": ""The macronutrient distribution (e.g., 40% carbs, 30% protein, 30% fat).""
        //      },
        //      ""WeeklyPlans"": {
        //        ""type"": ""array"",
        //        ""items"": {
        //          ""type"": ""object"",
        //          ""properties"": {
        //            ""WeekNumber"": {
        //              ""type"": ""integer"",
        //              ""description"": ""The week number.""
        //            },
        //            ""DailyPlans"": {
        //              ""type"": ""array"",
        //              ""items"": {
        //                ""type"": ""object"",
        //                ""properties"": {
        //                  ""Day"": {
        //                    ""type"": ""string"",
        //                    ""description"": ""The day of the week (e.g., Monday).""
        //                  },
        //                  ""Meals"": {
        //                    ""type"": ""array"",
        //                    ""items"": {
        //                      ""type"": ""object"",
        //                      ""properties"": {
        //                        ""Name"": {
        //                          ""type"": ""string"",
        //                          ""description"": ""The name of the meal (e.g., Breakfast).""
        //                        },
        //                        ""Items"": {
        //                          ""type"": ""array"",
        //                          ""items"": {
        //                            ""type"": ""object"",
        //                            ""properties"": {
        //                              ""Name"": {
        //                                ""type"": ""string"",
        //                                ""description"": ""The name of the food item.""
        //                              },
        //                              ""PortionSize"": {
        //                                ""type"": ""string"",
        //                                ""description"": ""The portion size of the food item.""
        //                              },
        //                              ""Calories"": {
        //                                ""type"": ""integer"",
        //                                ""description"": ""The calorie count of the food item.""
        //                              },
        //                              ""Macronutrients"": {
        //                                ""type"": ""object"",
        //                                ""properties"": {
        //                                  ""Carbs"": {
        //                                    ""type"": ""string"",
        //                                    ""description"": ""The carbohydrate content.""
        //                                  },
        //                                  ""Protein"": {
        //                                    ""type"": ""string"",
        //                                    ""description"": ""The protein content.""
        //                                  },
        //                                  ""Fat"": {
        //                                    ""type"": ""string"",
        //                                    ""description"": ""The fat content.""
        //                                  }
        //                                }
        //                              },
        //                              ""Alternatives"": {
        //                                ""type"": ""array"",
        //                                ""items"": {
        //                                  ""type"": ""object"",
        //                                  ""properties"": {
        //                                    ""Name"": {
        //                                      ""type"": ""string"",
        //                                      ""description"": ""The name of the alternative food item.""
        //                                    },
        //                                    ""PortionSize"": {
        //                                      ""type"": ""string"",
        //                                      ""description"": ""The portion size of the alternative food item.""
        //                                    },
        //                                    ""Calories"": {
        //                                      ""type"": ""integer"",
        //                                      ""description"": ""The calorie count of the alternative food item.""
        //                                    },
        //                                    ""Macronutrients"": {
        //                                      ""type"": ""object"",
        //                                      ""properties"": {
        //                                        ""Carbs"": {
        //                                          ""type"": ""string"",
        //                                          ""description"": ""The carbohydrate content of the alternative food item.""
        //                                        },
        //                                        ""Protein"": {
        //                                          ""type"": ""string"",
        //                                          ""description"": ""The protein content of the alternative food item.""
        //                                        },
        //                                        ""Fat"": {
        //                                          ""type"": ""string"",
        //                                          ""description"": ""The fat content of the alternative food item.""
        //                                        }
        //                                      }
        //                                    }
        //                                  }
        //                                }
        //                              }
        //                            }
        //                          }
        //                        }
        //                      }
        //                    }
        //                  }
        //                }
        //              }
        //            }
        //          }
        //        }
        //      },
        //      ""ShoppingList"": {
        //        ""type"": ""array"",
        //        ""items"": {
        //          ""type"": ""object"",
        //          ""properties"": {
        //            ""Item"": {
        //              ""type"": ""string"",
        //              ""description"": ""The name of the shopping list item.""
        //            },
        //            ""Quantity"": {
        //              ""type"": ""string"",
        //              ""description"": ""The quantity of the shopping list item.""
        //            }
        //          }
        //        }
        //      },
        //      ""HydrationPlan"": {
        //        ""type"": ""array"",
        //        ""items"": {
        //          ""type"": ""object"",
        //          ""properties"": {
        //            ""Time"": {
        //              ""type"": ""string"",
        //              ""description"": ""The time for hydration (e.g., Morning).""
        //            },
        //            ""Instruction"": {
        //              ""type"": ""string"",
        //              ""description"": ""The hydration instruction (e.g., Drink a glass of water).""
        //            }
        //          }
        //        }
        //      },
        //      ""ProgressTracking"": {
        //        ""type"": ""array"",
        //        ""items"": {
        //          ""type"": ""object"",
        //          ""properties"": {
        //            ""Instruction"": {
        //              ""type"": ""string"",
        //              ""description"": ""The progress tracking instruction (e.g., Track weight daily).""
        //            }
        //          }
        //        }
        //      },
        //      ""Notes"": {
        //        ""type"": ""array"",
        //        ""items"": {
        //          ""type"": ""string"",
        //          ""description"": ""Additional notes or comments.""
        //        }
        //      }
        //    },
        //    ""required"": [""Duration"", ""DailyCalories"", ""MacronutrientDistribution"", ""WeeklyPlans"", ""ShoppingList"", ""HydrationPlan"", ""ProgressTracking"", ""Notes""]
        //  }
        //}";

        // Function schema for the workout plan
        private static readonly ChatResponseFormat WorkoutPlanResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
     jsonSchemaIsStrict: true,
     jsonSchemaFormatName: "workout_plan",
     jsonSchema: BinaryData.FromBytes("""
{
    "$schema": "https://json-schema.org/draft/2019-09/schema",
    "title": "workout plan",
    "description": "description of a workout plan for a week, including exercises, sets, reps, and rest times.",
    "type": "object",
    "additionalProperties": false,
    "required": [
        "duration",
        "goal",
        "daily_plans",
        "rest_days",
        "notes"
    ],
    "properties": {
        "duration": {
            "description": "The duration of the workout plan.",
            "type": "string"
        },
        "goal": {
            "description": "The goal of the workout plan (e.g., build muscle, improve strength).",
            "type": "string"
        },
        "daily_plans": {
            "description": "Daily workout plans for the week.",
            "type": "array",
            "items": {
                "$ref": "#/$defs/day"
            }
        },
        "rest_days": {
            "description": "Rest days in the workout plan.",
            "type": "array",
            "items": {
                "type": "string"
            }
        },
        "notes": {
            "description": "Additional notes or comments.",
            "type": "array",
            "items": {
                "type": "string"
            }
        }
    },
    "$defs": {
        "day": {
            "type": "object",
            "additionalProperties": false,
            "required": [
                "day",
                "focus",
                "exercises"
            ],
            "properties": {
                "day": {
                    "description": "The day of the week (e.g., Monday).",
                    "type": "string"
                },
                "focus": {
                    "description": "The focus of the workout (e.g., Upper Body Strength).",
                    "type": "string"
                },
                "exercises": {
                    "description": "List of exercises for the day.",
                    "type": "array",
                    "items": {
                        "$ref": "#/$defs/exercise"
                    }
                }
            }
        },
        "exercise": {
            "description": "Details of an exercise.",
            "type": "object",
            "additionalProperties": false,
            "required": [
                "name",
                "muscle_group",
                "sets",
                "reps",
                "rest_between_sets",
                "intensity",
                "notes"
            ],
            "properties": {
                "name": {
                    "description": "The name of the exercise.",
                    "type": "string"
                },
                "muscle_group": {
                    "description": "The muscle group targeted by the exercise.",
                    "type": "string"
                },
                "sets": {
                    "description": "The number of sets.",
                    "type": "integer"
                },
                "reps": {
                    "description": "The number of reps (e.g., 8-10).",
                    "type": "string"
                },
                "rest_between_sets": {
                    "description": "The rest time between sets (e.g., 60 seconds).",
                    "type": "string"
                },
                "intensity": {
                    "description": "The intensity level (e.g., Moderate, Heavy).",
                    "type": "string"
                },
                "notes": {
                    "description": "Additional notes or tips for the exercise.",
                    "type": "array",
                    "items": {
                        "type": "string"
                    }
                }
            }
        }
    }
}
"""u8.ToArray())
 );

        public PlanGenerationService(HttpClient httpClient, IConfiguration configuration, ILogger<PlanGenerationService> logger)
        {
            _httpClient = httpClient;
            _openAiApiKey = configuration["OpenAI:ApiKey"];
            _logger = logger;
            _chatClient = new ChatClient("gpt-4o-mini", _openAiApiKey);
        }

        public async Task<StructuredDietPlan> GenerateDietPlanAsync(User user)
        {
            string prompt = GenerateDietPlanPrompt(user);

            List<ChatMessage> messages = [
                new UserChatMessage(prompt)
            ];

            ChatCompletionOptions options = new()
            {
                ResponseFormat = DietPlanResponseFormat
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages, options);

            using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

            _logger.LogInformation(structuredJson.RootElement.ToString());

            return new StructuredDietPlan
            {
                UserId = user.Id,
                PlanJson = structuredJson.RootElement.ToString(),
            };

            //var structuredDietPlan = new StructuredDietPlan
            //{
            //    UserId = user.Id,
            //    PlanJson = structuredJson.ToString()
            //};

            //return structuredDietPlan;

            //string prompt = GenerateDietPlanPrompt(user);

            //var requestBody = new
            //{
            //    model = "gpt-4",
            //    messages = new[]
            //    {
            //        new { role = "user", content = prompt }
            //    },
            //    functions = new[]
            //    {
            //        new
            //        {
            //            name = "generate_diet_plan",
            //            description = "Generates a structured diet plan based on user input.",
            //            parameters = JsonSerializer.Deserialize<JsonElement>(DietPlanFunctionSchema).GetProperty("parameters")
            //        }
            //    },
            //    function_call = new { name = "generate_diet_plan" }
            //};

            //var jsonRequestBody = JsonSerializer.Serialize(requestBody);
            //var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            //_httpClient.DefaultRequestHeaders.Clear();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiKey);

            //// Log the outgoing request
            //_logger.LogInformation("Request body: {RequestBody}", jsonRequestBody);

            //var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            //// Log the response status
            //_logger.LogInformation("Response Status: {StatusCode}", response.StatusCode);

            //response.EnsureSuccessStatusCode(); // Throw if not a success status

            //var responseBody = await response.Content.ReadAsStringAsync();

            //// Log the response body
            //_logger.LogInformation("Response body: {ResponseBody}", responseBody);

            //var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

            //// Extract the function call arguments
            //var functionCallArguments = jsonResponse
            //    .GetProperty("choices")[0]
            //    .GetProperty("message")
            //    .GetProperty("function_call")
            //    .GetProperty("arguments")
            //    .GetString();

            //// Deserialize the arguments into a StructuredDietPlan object hhhhhhhhhhhhhhhhhhhhhhhhhhhere
            //var structuredDietPlan = new StructuredDietPlan
            //{
            //    UserId = user.Id,
            //    PlanJson = functionCallArguments // Set PlanJson directly from the response
            //};

            //return structuredDietPlan;
        }

        public async Task<StructuredWorkoutPlan> GenerateWorkoutPlanAsync(User user)
        {
            try
            {
                string prompt = GenerateWorkoutPlanPrompt(user);

                List<ChatMessage> messages = [new UserChatMessage(prompt)];
                ChatCompletionOptions options = new()
                {
                    ResponseFormat = WorkoutPlanResponseFormat
                };

                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages, options);
                using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

                _logger.LogInformation("Generated workout plan: {Plan}", structuredJson.RootElement.ToString());

                return new StructuredWorkoutPlan
                {
                    UserId = user.Id,
                    PlanJson = structuredJson.RootElement.ToString(),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate workout plan for user {UserId}", user.Id);
                throw; // Re-throw the exception or return a default/error response
            }
        }
        private string GenerateDietPlanPrompt(User user)
        {
            return $"""
The user is an {user.Age}-year-old {user.Gender} from Egypt, with a height of {user.Height} meters and a weight of {user.Weight} kg.
Their fitness goal is {user.Goal}.
They prefer a {user.PreferredDiet} diet.
They work out {user.WeeklyWorkoutDays} days a week, for {user.WorkoutDuration}.
They have the following dietary restrictions: {user.DietaryRestrictions}.
They have the following medical conditions: {user.MedicalConditions}.
The plan should use traditional, locally available ingredients and reflect cultural preferences (Egyptian cuisine).
Ensure the plan is nutritionally balanced, with a focus on whole foods, lean proteins, complex carbs, and healthy fats.
Avoid processed foods, sugary drinks, and excessive amounts of refined carbs.
Include a variety of vegetables, fruits, and whole grains in each meal.
Create a personalizable diet plan for a week. The plan should include three meals a day, each with a main dish and alternatives. Each dish should have macronutrients and total calories. Please provide the following information for each day of the week: Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, and Friday. For each day, provide a breakfast, lunch, and dinner option. For each meal, provide a main dish with its portion size, macronutrients (carbs, protein, fat in grams), and total calories. Additionally, provide at least one alternative dish for each meal. Please ensure that the data you provide adheres to the specified schema.
""";
            //return $"Create a personalized and professional diet plan for a {user.Age}-year-old {user.Gender} " +
            //       $"from Egypt with a height of {user.Height} meters and a weight of {user.Weight} kg. " +
            //       $"Their fitness goal is {user.Goal}, and they prefer a {user.PreferredDiet} diet. " +
            //       $"They work out {user.WeeklyWorkoutDays} days a week, for {user.WorkoutDuration}. " +
            //       $"They have the following dietary restrictions: {user.DietaryRestrictions}. " +
            //       $"Medical conditions: {user.MedicalConditions}. " +
            //       "The plan should use traditional, locally available ingredients and reflect cultural preferences. " +
            //       "Ensure the plan is nutritionally balanced, with a focus on whole foods, lean proteins, complex carbs, and healthy fats. " +
            //       "Avoid processed foods, sugary drinks, and excessive amounts of refined carbs. " +
            //       "Include a variety of vegetables, fruits, and whole grains in each meal. " +
            //       "**The plan must cover one full week (7 days) with meals for each day (breakfast, snack, lunch, dinner).** " +
            //       "**Days**: Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday. " +
            //       "**Meals per day**: Breakfast, Snack, Lunch, Dinner. " +
            //       "**Each meal must include at least two alternatives with complete details (portion size, calories, and macronutrient breakdown).** " +
            //       "**Do not skip any meals or days. Do not leave any fields null or incomplete.** " +
            //       "Format the plan **exactly** as follows:\n" +
            //       "### Duration ###\n[Duration]\n" +
            //       "### Daily Calorie Target ###\n[Total daily calories] calories\n" +
            //       "### Macronutrient Distribution ###\n[Macronutrient breakdown]\n" +
            //       "### Weekly Plan ###\nWeek 1:\n" +
            //       "Day 1 (Monday):\n" +
            //       "- Meal 1 (Breakfast):\n" +
            //       "  - Item 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "  - Alternatives:\n" +
            //       "    - Alternative 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "    - Alternative 2: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "- Meal 2 (Snack):\n" +
            //       "  - Item 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "  - Alternatives:\n" +
            //       "    - Alternative 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "    - Alternative 2: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "- Meal 3 (Lunch):\n" +
            //       "  - Item 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "  - Alternatives:\n" +
            //       "    - Alternative 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "    - Alternative 2: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "- Meal 4 (Dinner):\n" +
            //       "  - Item 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "  - Alternatives:\n" +
            //       "    - Alternative 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "    - Alternative 2: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "Day 2 (Tuesday):\n" +
            //       "- Meal 1 (Breakfast):\n" +
            //       "  - Item 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "  - Alternatives:\n" +
            //       "    - Alternative 1: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "    - Alternative 2: [Food name], [Portion size], [Calories], [Carbs], [Protein], [Fat]\n" +
            //       "...\n" +
            //       "### Shopping List ###\n- [Ingredient 1]: [Quantity]\n- [Ingredient 2]: [Quantity]\n" +
            //       "...\n" +
            //       "### Hydration Plan ###\n- [Time]: [Instruction]\n...\n" +
            //       "### Progress Tracking ###\n- [Instruction]\n...\n" +
            //       "### Notes ###\n- [Note 1]\n...";
        }
        private string GenerateWorkoutPlanPrompt(User user)
        {
            return $"""
The user is an {user.Age}-year-old {user.Gender} with a height of {user.Height} meters and a weight of {user.Weight} kg.
Their fitness level is {user.FitnessLevel}, and they aim to {user.Goal}.
They work out {user.WeeklyWorkoutDays} days a week, for {user.WorkoutDuration} per session.
The plan should include exercises for strength, cardio, and flexibility, with rest days included.
**Include at least 4-6 exercises per workout day, targeting different muscle groups.**
**Use specific exercise names like Fly Machine, Lat Pulldown, Bench Press, Squats, etc., based on the user's goals and fitness level.**
**Ensure the plan is tailored to their fitness level and goals.**
**The plan must cover one full week (7 days) with detailed daily plans, including exercises, sets, reps, rest times, and intensity levels.**
**Include rest days and additional notes for each day.**
**Do not leave any fields null or incomplete.**
Please ensure that the data you provide adheres to the specified schema.
""";
        }
    }
}