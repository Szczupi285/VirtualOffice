using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualOffice.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _Priority = table.Column<int>(type: "int", nullable: false),
                    _TaskStatus = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    _title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _Permissions = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _officeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Office_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _subType = table.Column<int>(type: "int", nullable: false),
                    _subscriptionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    _isPayed = table.Column<bool>(type: "bit", nullable: false),
                    _subEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _subStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateDocuments__attachmentFilePaths",
                columns: table => new
                {
                    PrivateDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateDocuments__attachmentFilePaths", x => new { x.PrivateDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PrivateDocuments__attachmentFilePaths_PrivateDocuments_PrivateDocumentId",
                        column: x => x.PrivateDocumentId,
                        principalTable: "PrivateDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCreationDetails",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCreationDetails", x => x.PublicDocumentId);
                    table.ForeignKey(
                        name: "FK_DocumentCreationDetails_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicDocumentEligibleForRead",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDocumentEligibleForRead", x => new { x.PublicDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PublicDocumentEligibleForRead_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicDocumentEligibleForWrite",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDocumentEligibleForWrite", x => new { x.PublicDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PublicDocumentEligibleForWrite_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicDocuments__attachmentFilePaths",
                columns: table => new
                {
                    PublicDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicDocuments__attachmentFilePaths", x => new { x.PublicDocumentId, x.Id });
                    table.ForeignKey(
                        name: "FK_PublicDocuments__attachmentFilePaths_PublicDocuments_PublicDocumentId",
                        column: x => x.PublicDocumentId,
                        principalTable: "PublicDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCalendarEvent",
                columns: table => new
                {
                    CalendarEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _AssignedEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCalendarEvent", x => new { x.CalendarEventId, x._AssignedEmployeesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCalendarEvent_CalendarEvents_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "CalendarEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCalendarEvent_Employees__AssignedEmployeesId",
                        column: x => x._AssignedEmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserEmployeeTask",
                columns: table => new
                {
                    EmployeeTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _AssignedEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEmployeeTask", x => new { x.EmployeeTaskId, x._AssignedEmployeesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEmployeeTask_EmployeeTasks_EmployeeTaskId",
                        column: x => x.EmployeeTaskId,
                        principalTable: "EmployeeTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEmployeeTask_Employees__AssignedEmployeesId",
                        column: x => x._AssignedEmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserMeeting",
                columns: table => new
                {
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _AssignedEmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMeeting", x => new { x.MeetingId, x._AssignedEmployeesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_Employees__AssignedEmployeesId",
                        column: x => x._AssignedEmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserPrivateChatRoom",
                columns: table => new
                {
                    PrivateChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPrivateChatRoom", x => new { x.PrivateChatRoomId, x._ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPrivateChatRoom_Employees__ParticipantsId",
                        column: x => x._ParticipantsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPrivateChatRoom_PrivateChatRooms_PrivateChatRoomId",
                        column: x => x.PrivateChatRoomId,
                        principalTable: "PrivateChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserPublicChatRoom",
                columns: table => new
                {
                    PublicChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPublicChatRoom", x => new { x.PublicChatRoomId, x._ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPublicChatRoom_Employees__ParticipantsId",
                        column: x => x._ParticipantsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPublicChatRoom_PublicChatRooms_PublicChatRoomId",
                        column: x => x.PublicChatRoomId,
                        principalTable: "PublicChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Employees_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Employees_SentByUserId",
                        column: x => x.SentByUserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Employees_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserOffice",
                columns: table => new
                {
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _membersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserOffice", x => new { x.OfficeId, x._membersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserOffice_Employees__membersId",
                        column: x => x._membersId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserOffice_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "MessagePrivateChatRoom",
                columns: table => new
                {
                    PrivateChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _MessagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePrivateChatRoom", x => new { x.PrivateChatRoomId, x._MessagesId });
                    table.ForeignKey(
                        name: "FK_MessagePrivateChatRoom_Message__MessagesId",
                        column: x => x._MessagesId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagePrivateChatRoom_PrivateChatRooms_PrivateChatRoomId",
                        column: x => x.PrivateChatRoomId,
                        principalTable: "PrivateChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessagePublicChatRoom",
                columns: table => new
                {
                    PublicChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _MessagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePublicChatRoom", x => new { x.PublicChatRoomId, x._MessagesId });
                    table.ForeignKey(
                        name: "FK_MessagePublicChatRoom_Message__MessagesId",
                        column: x => x._MessagesId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagePublicChatRoom_PublicChatRooms_PublicChatRoomId",
                        column: x => x.PublicChatRoomId,
                        principalTable: "PublicChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCalendarEvent__AssignedEmployeesId",
                table: "ApplicationUserCalendarEvent",
                column: "_AssignedEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEmployeeTask__AssignedEmployeesId",
                table: "ApplicationUserEmployeeTask",
                column: "_AssignedEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMeeting__AssignedEmployeesId",
                table: "ApplicationUserMeeting",
                column: "_AssignedEmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserOffice__membersId",
                table: "ApplicationUserOffice",
                column: "_membersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPrivateChatRoom__ParticipantsId",
                table: "ApplicationUserPrivateChatRoom",
                column: "_ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPublicChatRoom__ParticipantsId",
                table: "ApplicationUserPublicChatRoom",
                column: "_ParticipantsId");

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
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganizationId",
                table: "Employees",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SentByUserId",
                table: "Message",
                column: "SentByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagePrivateChatRoom__MessagesId",
                table: "MessagePrivateChatRoom",
                column: "_MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagePublicChatRoom__MessagesId",
                table: "MessagePublicChatRoom",
                column: "_MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CreatedByUserId",
                table: "Notes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_OrganizationId",
                table: "Office",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCalendarEvent");

            migrationBuilder.DropTable(
                name: "ApplicationUserEmployeeTask");

            migrationBuilder.DropTable(
                name: "ApplicationUserMeeting");

            migrationBuilder.DropTable(
                name: "ApplicationUserOffice");

            migrationBuilder.DropTable(
                name: "ApplicationUserPrivateChatRoom");

            migrationBuilder.DropTable(
                name: "ApplicationUserPublicChatRoom");

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
                name: "DocumentCreationDetails");

            migrationBuilder.DropTable(
                name: "MessagePrivateChatRoom");

            migrationBuilder.DropTable(
                name: "MessagePublicChatRoom");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PrivateDocuments__attachmentFilePaths");

            migrationBuilder.DropTable(
                name: "PublicDocumentEligibleForRead");

            migrationBuilder.DropTable(
                name: "PublicDocumentEligibleForWrite");

            migrationBuilder.DropTable(
                name: "PublicDocuments__attachmentFilePaths");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "EmployeeTasks");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PrivateChatRooms");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "PublicChatRooms");

            migrationBuilder.DropTable(
                name: "PrivateDocuments");

            migrationBuilder.DropTable(
                name: "PublicDocuments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
