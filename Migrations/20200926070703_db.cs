using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicData.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    LibraryGuid = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LibraryType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LinkLibrary",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkLibrary", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Create = table.Column<int>(nullable: false),
                    Read = table.Column<int>(nullable: false),
                    Update = table.Column<int>(nullable: false),
                    Delete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DefaultField",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FieldTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DefaultField_FieldType_FieldTypeID",
                        column: x => x.FieldTypeID,
                        principalTable: "FieldType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemFile",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(maxLength: 500, nullable: true),
                    FileLocation = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemFile_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PermissionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SecurityGroup_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    EditedByID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemLog_User_EditedByID",
                        column: x => x.EditedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemLog_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    EditedDate = table.Column<DateTime>(nullable: true),
                    GroupBy = table.Column<string>(nullable: true),
                    OrderBy = table.Column<string>(nullable: true),
                    LibraryTypeID = table.Column<int>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    CreatedByID = table.Column<int>(nullable: true),
                    EditedByID = table.Column<int>(nullable: true),
                    Deleted = table.Column<int>(nullable: true),
                    Visible = table.Column<int>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    MenuType = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Library_User_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Library_User_EditedByID",
                        column: x => x.EditedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Library_LibraryType_LibraryTypeID",
                        column: x => x.LibraryTypeID,
                        principalTable: "LibraryType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    States = table.Column<int>(nullable: false),
                    Zip = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    POC_FirstName = table.Column<string>(nullable: true),
                    POC_LastName = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Organization_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryID = table.Column<int>(nullable: true),
                    SecurityGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authorization_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authorization_SecurityGroup_SecurityGroupID",
                        column: x => x.SecurityGroupID,
                        principalTable: "SecurityGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Editable = table.Column<int>(nullable: true),
                    Visible = table.Column<int>(nullable: true),
                    LibraryGuid = table.Column<Guid>(nullable: false),
                    LibraryID = table.Column<int>(nullable: true),
                    FieldTypeID = table.Column<int>(nullable: true),
                    LookupTable = table.Column<Guid>(nullable: true),
                    LookUpId = table.Column<Guid>(nullable: true),
                    LookUpValue = table.Column<string>(nullable: true),
                    DropdownValue = table.Column<string>(nullable: true),
                    ActionButonOpenLibraryID = table.Column<int>(nullable: true),
                    Deleted = table.Column<int>(nullable: true),
                    Required = table.Column<int>(nullable: true),
                    Grouping = table.Column<int>(nullable: true),
                    SortOrder = table.Column<int>(nullable: true),
                    DefaultSort = table.Column<int>(nullable: true),
                    CheckDubplicate = table.Column<int>(nullable: true),
                    SortDirection = table.Column<string>(nullable: true),
                    ValueFromOtherLibrary = table.Column<string>(nullable: true),
                    Formular = table.Column<string>(nullable: true),
                    FormularView = table.Column<string>(nullable: true),
                    ItemID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Field_Library_ActionButonOpenLibraryID",
                        column: x => x.ActionButonOpenLibraryID,
                        principalTable: "Library",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Field_FieldType_FieldTypeID",
                        column: x => x.FieldTypeID,
                        principalTable: "FieldType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Field_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Field_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibraryLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    EditedDate = table.Column<DateTime>(nullable: false),
                    LibraryID = table.Column<int>(nullable: true),
                    EditedByID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LibraryLog_User_EditedByID",
                        column: x => x.EditedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibraryLog_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOrganization",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    OrganizationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserOrganization_Organization_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organization",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrganization_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    FieldID = table.Column<int>(nullable: true),
                    LibraryID = table.Column<int>(nullable: true),
                    EditedByID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FieldLog_User_EditedByID",
                        column: x => x.EditedByID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldLog_Field_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Field",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldLog_Library_LibraryID",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldValue",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(nullable: false),
                    FieldID = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LibraryGuid = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    ItemGuid = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldValue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FieldValue_Field_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Field",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldValue_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_LibraryID",
                table: "Authorization",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_Authorization_SecurityGroupID",
                table: "Authorization",
                column: "SecurityGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultField_FieldTypeID",
                table: "DefaultField",
                column: "FieldTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Field_ActionButonOpenLibraryID",
                table: "Field",
                column: "ActionButonOpenLibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_Field_FieldTypeID",
                table: "Field",
                column: "FieldTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Field_ItemID",
                table: "Field",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Field_LibraryID",
                table: "Field",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLog_EditedByID",
                table: "FieldLog",
                column: "EditedByID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLog_FieldID",
                table: "FieldLog",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldLog_LibraryID",
                table: "FieldLog",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_FieldID",
                table: "FieldValue",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValue_ItemID",
                table: "FieldValue",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFile_ItemID",
                table: "ItemFile",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLog_EditedByID",
                table: "ItemLog",
                column: "EditedByID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLog_ItemID",
                table: "ItemLog",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Library_CreatedByID",
                table: "Library",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Library_EditedByID",
                table: "Library",
                column: "EditedByID");

            migrationBuilder.CreateIndex(
                name: "IX_Library_LibraryTypeID",
                table: "Library",
                column: "LibraryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryLog_EditedByID",
                table: "LibraryLog",
                column: "EditedByID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryLog_LibraryID",
                table: "LibraryLog",
                column: "LibraryID");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_UserID",
                table: "Organization",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_UserID",
                table: "Role",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityGroup_PermissionID",
                table: "SecurityGroup",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_OrganizationID",
                table: "UserOrganization",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_UserID",
                table: "UserOrganization",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.DropTable(
                name: "DefaultField");

            migrationBuilder.DropTable(
                name: "FieldLog");

            migrationBuilder.DropTable(
                name: "FieldValue");

            migrationBuilder.DropTable(
                name: "ItemFile");

            migrationBuilder.DropTable(
                name: "ItemLog");

            migrationBuilder.DropTable(
                name: "LibraryLog");

            migrationBuilder.DropTable(
                name: "LinkLibrary");

            migrationBuilder.DropTable(
                name: "UserOrganization");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "SecurityGroup");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Library");

            migrationBuilder.DropTable(
                name: "FieldType");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "LibraryType");
        }
    }
}
