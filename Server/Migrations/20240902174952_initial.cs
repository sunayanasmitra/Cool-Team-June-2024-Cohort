using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthCareApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDirectory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllergyDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllergyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyDirectory", x => x.Id);
                });

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
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisDirectory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugDirectory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "SymptomDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomDirectory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccineDirectory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineDirectory", x => x.Id);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "BasicInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicInformation_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfPlan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthPlan_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifestyleRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifestyleRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifestyleRecord_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalDocument_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodPressure = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    BloodGlucoseLevel = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    WeightInPounds = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HeightInInches = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccineRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VaccineDirectoryId = table.Column<int>(type: "int", nullable: false),
                    DateAdministered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRelation_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthPlanID = table.Column<int>(type: "int", nullable: false),
                    DietGoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPlan_HealthPlan_HealthPlanID",
                        column: x => x.HealthPlanID,
                        principalTable: "HealthPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRisk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthPlanID = table.Column<int>(type: "int", nullable: false),
                    Risk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRisk_HealthPlan_HealthPlanID",
                        column: x => x.HealthPlanID,
                        principalTable: "HealthPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthPlanID = table.Column<int>(type: "int", nullable: false),
                    Reminder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_HealthPlan_HealthPlanID",
                        column: x => x.HealthPlanID,
                        principalTable: "HealthPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthPlanID = table.Column<int>(type: "int", nullable: false),
                    WorkoutGoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlan_HealthPlan_HealthPlanID",
                        column: x => x.HealthPlanID,
                        principalTable: "HealthPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlcoholHabits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifestyleRecordID = table.Column<int>(type: "int", nullable: false),
                    DrinksPerWeek = table.Column<int>(type: "int", nullable: false),
                    DrinksPerMonth = table.Column<int>(type: "int", nullable: false),
                    PrimaryAlcoholType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholHabits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlcoholHabits_LifestyleRecord_LifestyleRecordID",
                        column: x => x.LifestyleRecordID,
                        principalTable: "LifestyleRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugHabits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifestyleRecordID = table.Column<int>(type: "int", nullable: false),
                    DrugDirectoryId = table.Column<int>(type: "int", nullable: false),
                    DosesPerWeek = table.Column<int>(type: "int", nullable: false),
                    DosesPerMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugHabits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugHabits_DrugDirectory_DrugDirectoryId",
                        column: x => x.DrugDirectoryId,
                        principalTable: "DrugDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugHabits_LifestyleRecord_LifestyleRecordID",
                        column: x => x.LifestyleRecordID,
                        principalTable: "LifestyleRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EatingHabits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifestyleRecordID = table.Column<int>(type: "int", nullable: false),
                    CaloriesPerDay = table.Column<int>(type: "int", nullable: false),
                    SugarIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    FatIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    ProteinIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    CholesterolIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    CarbIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    SodiumIntakePerDay = table.Column<int>(type: "int", nullable: false),
                    VegetablePercentOfIntake = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    MeatPercentOfIntake = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    CerealsPercentofIntake = table.Column<decimal>(type: "decimal(2,2)", nullable: false),
                    FoodRestriction = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EatingHabits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EatingHabits_LifestyleRecord_LifestyleRecordID",
                        column: x => x.LifestyleRecordID,
                        principalTable: "LifestyleRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifestyleRecordID = table.Column<int>(type: "int", nullable: false),
                    ActivityDirectoryId = table.Column<int>(type: "int", nullable: false),
                    TimesPerWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalActivities_ActivityDirectory_ActivityDirectoryId",
                        column: x => x.ActivityDirectoryId,
                        principalTable: "ActivityDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysicalActivities_LifestyleRecord_LifestyleRecordID",
                        column: x => x.LifestyleRecordID,
                        principalTable: "LifestyleRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    AllergyDirectoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allergy_AllergyDirectory_AllergyDirectoryId",
                        column: x => x.AllergyDirectoryId,
                        principalTable: "AllergyDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergy_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDirectoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosis_DiagnosisDirectory_DiagnosisDirectoryId",
                        column: x => x.DiagnosisDirectoryId,
                        principalTable: "DiagnosisDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnosis_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    DrugDirectoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_DrugDirectory_DrugDirectoryId",
                        column: x => x.DrugDirectoryId,
                        principalTable: "DrugDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medication_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Symptom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    SymptomDirectoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptom_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Symptom_SymptomDirectory_SymptomDirectoryId",
                        column: x => x.SymptomDirectoryId,
                        principalTable: "SymptomDirectory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityDirectory",
                columns: new[] { "Id", "ActivityName" },
                values: new object[,]
                {
                    { 1, "Running" },
                    { 2, "Walking" },
                    { 3, "Yoga" },
                    { 4, "Cycling" },
                    { 5, "Swimming" },
                    { 6, "Hiking" },
                    { 7, "Pilates" },
                    { 8, "Rowing" },
                    { 9, "Elliptical" },
                    { 10, "Stair Stepper" },
                    { 11, "HIIT (High Intensity Interval Training" },
                    { 12, "Strength Training" },
                    { 13, "Core Training" },
                    { 14, "Dance" },
                    { 15, "Water Fitness" },
                    { 16, "CrossFit" },
                    { 17, "Functional Training" },
                    { 18, "Skiing/Snowboarding" },
                    { 19, "Golf" },
                    { 20, "Tennis" },
                    { 21, "Basketball" },
                    { 22, "Soccer" },
                    { 23, "Table Tennis" },
                    { 24, "Badminton" },
                    { 25, "Volleyball" },
                    { 26, "Baseball/Softball" },
                    { 27, "Rock Climbing" },
                    { 28, "Martial Arts" },
                    { 29, "Tai Chi" },
                    { 30, "Mixed Cardio" },
                    { 31, "Other" }
                });

            migrationBuilder.InsertData(
                table: "AllergyDirectory",
                columns: new[] { "Id", "AllergyName" },
                values: new object[,]
                {
                    { 1, "Peanuts" },
                    { 2, "Shellfish" },
                    { 3, "Tree Nuts" },
                    { 4, "Milk" },
                    { 5, "Eggs" },
                    { 6, "Soy" },
                    { 7, "Wheat" },
                    { 8, "Fish" },
                    { 9, "Sesame" },
                    { 10, "Mustard" },
                    { 11, "Sulfites" },
                    { 12, "Latex" },
                    { 13, "Pollen" },
                    { 14, "Dust Mites" },
                    { 15, "Mold" },
                    { 16, "Pet Dander" },
                    { 17, "Insect Stings" },
                    { 18, "Medications" },
                    { 19, "Fragrances" },
                    { 20, "Nickel" }
                });

            migrationBuilder.InsertData(
                table: "DiagnosisDirectory",
                columns: new[] { "Id", "DiagnosisName" },
                values: new object[,]
                {
                    { 1, "Diabetes Mellitus" },
                    { 2, "Hypertension" },
                    { 3, "Asthma" },
                    { 4, "Chronic Obstructive Pulmonary Disease (COPD)" },
                    { 5, "Coronary Artery Disease" },
                    { 6, "Stroke" },
                    { 7, "Parkinson's Disease" },
                    { 8, "Rheumatoid Arthritis" },
                    { 9, "Multiple Sclerosis" },
                    { 10, "Alzheimer's Disease" },
                    { 11, "Epilepsy" },
                    { 12, "Cancer" },
                    { 13, "Chronic Kidney Disease" },
                    { 14, "Gastroesophageal Reflux Disease (GERD)" },
                    { 15, "Peptic Ulcer Disease" },
                    { 16, "Psoriasis" },
                    { 17, "Hypothyroidism" },
                    { 18, "Hyperthyroidism" },
                    { 19, "Obesity" },
                    { 20, "Sleep Apnea" },
                    { 21, "Chronic Sinusitis" },
                    { 22, "Bronchitis" },
                    { 23, "Anemia" },
                    { 24, "Tuberculosis" },
                    { 25, "Hepatitis B" },
                    { 26, "Hepatitis C" },
                    { 27, "Scleroderma" },
                    { 28, "Lupus" },
                    { 29, "Chronic Fatigue Syndrome" },
                    { 30, "Cystic Fibrosis" }
                });

            migrationBuilder.InsertData(
                table: "DrugDirectory",
                columns: new[] { "Id", "DrugName" },
                values: new object[,]
                {
                    { 1, "Aspirin" },
                    { 2, "Ibuprofen" },
                    { 3, "Acetaminophen" },
                    { 4, "Amoxicillin" },
                    { 5, "Metformin" },
                    { 6, "Lisinopril" },
                    { 7, "Atorvastatin" },
                    { 8, "Simvastatin" },
                    { 9, "Hydrochlorothiazide" },
                    { 10, "Losartan" },
                    { 11, "Omeprazole" },
                    { 12, "Esomeprazole" },
                    { 13, "Gabapentin" },
                    { 14, "Hydrocodone" },
                    { 15, "Oxycodone" },
                    { 16, "Sertraline" },
                    { 17, "Fluoxetine" },
                    { 18, "Alprazolam" },
                    { 19, "Clonazepam" },
                    { 20, "Levothyroxine" },
                    { 21, "Metoprolol" },
                    { 22, "Amlodipine" },
                    { 23, "Furosemide" },
                    { 24, "Azithromycin" },
                    { 25, "Ciprofloxacin" },
                    { 26, "Doxycycline" },
                    { 27, "Prednisone" },
                    { 28, "Methylprednisolone" },
                    { 29, "Insulin" },
                    { 30, "Ceftriaxone" }
                });

            migrationBuilder.InsertData(
                table: "SymptomDirectory",
                columns: new[] { "Id", "SymptomName" },
                values: new object[,]
                {
                    { 1, "Fever" },
                    { 2, "Cough" },
                    { 3, "Shortness of Breath" },
                    { 4, "Fatigue" },
                    { 5, "Headache" },
                    { 6, "Sore Throat" },
                    { 7, "Runny Nose" },
                    { 8, "Muscle Aches" },
                    { 9, "Chills" },
                    { 10, "Nausea" },
                    { 11, "Vomiting" },
                    { 12, "Diarrhea" },
                    { 13, "Chest Pain" },
                    { 14, "Abdominal Pain" },
                    { 15, "Rash" },
                    { 16, "Dizziness" },
                    { 17, "Swelling" },
                    { 18, "Joint Pain" },
                    { 19, "Confusion" },
                    { 20, "Loss of Appetite" },
                    { 21, "Coughing Up Blood" },
                    { 22, "Wheezing" },
                    { 23, "Difficulty Swallowing" },
                    { 24, "Loss of Taste" },
                    { 25, "Loss of Smell" },
                    { 26, "Frequent Urination" },
                    { 27, "Bloody Urine" },
                    { 28, "Yellowing of Skin" },
                    { 29, "Difficulty Breathing" },
                    { 30, "Chest Tightness" }
                });

            migrationBuilder.InsertData(
                table: "VaccineDirectory",
                columns: new[] { "Id", "VaccineName" },
                values: new object[,]
                {
                    { 1, "Covid Pfizer" },
                    { 2, "Moderna Covid" },
                    { 3, "Johnson and Johnson Covid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlcoholHabits_LifestyleRecordID",
                table: "AlcoholHabits",
                column: "LifestyleRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_AllergyDirectoryId",
                table: "Allergy",
                column: "AllergyDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_MedicalRecordId",
                table: "Allergy",
                column: "MedicalRecordId");

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
                name: "IX_BasicInformation_ApplicationUserId",
                table: "BasicInformation",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DiagnosisDirectoryId",
                table: "Diagnosis",
                column: "DiagnosisDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_MedicalRecordId",
                table: "Diagnosis",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPlan_HealthPlanID",
                table: "DietPlan",
                column: "HealthPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_DrugHabits_DrugDirectoryId",
                table: "DrugHabits",
                column: "DrugDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugHabits_LifestyleRecordID",
                table: "DrugHabits",
                column: "LifestyleRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_EatingHabits_LifestyleRecordID",
                table: "EatingHabits",
                column: "LifestyleRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthPlan_ApplicationUserId",
                table: "HealthPlan",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthRisk_HealthPlanID",
                table: "HealthRisk",
                column: "HealthPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_LifestyleRecord_ApplicationUserId",
                table: "LifestyleRecord",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDocument_ApplicationUserId",
                table: "MedicalDocument",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_ApplicationUserId",
                table: "MedicalRecord",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medication_DrugDirectoryId",
                table: "Medication",
                column: "DrugDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_MedicalRecordId",
                table: "Medication",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivities_ActivityDirectoryId",
                table: "PhysicalActivities",
                column: "ActivityDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivities_LifestyleRecordID",
                table: "PhysicalActivities",
                column: "LifestyleRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_HealthPlanID",
                table: "Reminders",
                column: "HealthPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Symptom_MedicalRecordId",
                table: "Symptom",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptom_SymptomDirectoryId",
                table: "Symptom",
                column: "SymptomDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRelation_ApplicationUserId",
                table: "VaccineRelation",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlan_HealthPlanID",
                table: "WorkoutPlan",
                column: "HealthPlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlcoholHabits");

            migrationBuilder.DropTable(
                name: "Allergy");

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
                name: "BasicInformation");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "DietPlan");

            migrationBuilder.DropTable(
                name: "DrugHabits");

            migrationBuilder.DropTable(
                name: "EatingHabits");

            migrationBuilder.DropTable(
                name: "HealthRisk");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "MedicalDocument");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "PhysicalActivities");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Symptom");

            migrationBuilder.DropTable(
                name: "VaccineDirectory");

            migrationBuilder.DropTable(
                name: "VaccineRelation");

            migrationBuilder.DropTable(
                name: "WorkoutPlan");

            migrationBuilder.DropTable(
                name: "AllergyDirectory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DiagnosisDirectory");

            migrationBuilder.DropTable(
                name: "DrugDirectory");

            migrationBuilder.DropTable(
                name: "ActivityDirectory");

            migrationBuilder.DropTable(
                name: "LifestyleRecord");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "SymptomDirectory");

            migrationBuilder.DropTable(
                name: "HealthPlan");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
