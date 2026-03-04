using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase.Migrations
{
    /// <inheritdoc />
    public partial class AddedExplicitDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_DoctorId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_UserId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DoctorSessionSlot_DoctorSessionSlotId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Booking_BookingId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Chat_ChatId",
                table: "ChatMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSessionSlot_AspNetUsers_DoctorId",
                table: "DoctorSessionSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWeeklySchedule_AspNetUsers_DoctorId",
                table: "DoctorWeeklySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeEntries_AspNetUsers_UserId",
                table: "ModeEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDoctor_AspNetUsers_DoctorId",
                table: "UserDoctor");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDoctor_AspNetUsers_UserId",
                table: "UserDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDoctor",
                table: "UserDoctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModeEntries",
                table: "ModeEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorWeeklySchedule",
                table: "DoctorWeeklySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSessionSlot",
                table: "DoctorSessionSlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "UserDoctor",
                newName: "UserDoctors");

            migrationBuilder.RenameTable(
                name: "ModeEntries",
                newName: "MoodEntry");

            migrationBuilder.RenameTable(
                name: "DoctorWeeklySchedule",
                newName: "DoctorWeeklySchedules");

            migrationBuilder.RenameTable(
                name: "DoctorSessionSlot",
                newName: "DoctorSessionSlots");

            migrationBuilder.RenameTable(
                name: "ChatMessage",
                newName: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "Chats");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_UserDoctor_DoctorId",
                table: "UserDoctors",
                newName: "IX_UserDoctors_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_ModeEntries_UserId_Date",
                table: "MoodEntry",
                newName: "IX_MoodEntry_UserId_Date");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorWeeklySchedule_DoctorId",
                table: "DoctorWeeklySchedules",
                newName: "IX_DoctorWeeklySchedules_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSessionSlot_DoctorId_StartDateTime",
                table: "DoctorSessionSlots",
                newName: "IX_DoctorSessionSlots_DoctorId_StartDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_BookingId",
                table: "Chats",
                newName: "IX_Chats_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UserId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DoctorSessionSlotId",
                table: "Bookings",
                newName: "IX_Bookings_DoctorSessionSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DoctorId",
                table: "Bookings",
                newName: "IX_Bookings_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDoctors",
                table: "UserDoctors",
                columns: new[] { "UserId", "DoctorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoodEntry",
                table: "MoodEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorWeeklySchedules",
                table: "DoctorWeeklySchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSessionSlots",
                table: "DoctorSessionSlots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_DoctorId",
                table: "Bookings",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings",
                column: "DoctorSessionSlotId",
                principalTable: "DoctorSessionSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Bookings_BookingId",
                table: "Chats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSessionSlots_AspNetUsers_DoctorId",
                table: "DoctorSessionSlots",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWeeklySchedules_AspNetUsers_DoctorId",
                table: "DoctorWeeklySchedules",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodEntry_AspNetUsers_UserId",
                table: "MoodEntry",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDoctors_AspNetUsers_DoctorId",
                table: "UserDoctors",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDoctors_AspNetUsers_UserId",
                table: "UserDoctors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_DoctorId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DoctorSessionSlots_DoctorSessionSlotId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Chats_ChatId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Bookings_BookingId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSessionSlots_AspNetUsers_DoctorId",
                table: "DoctorSessionSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWeeklySchedules_AspNetUsers_DoctorId",
                table: "DoctorWeeklySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_MoodEntry_AspNetUsers_UserId",
                table: "MoodEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDoctors_AspNetUsers_DoctorId",
                table: "UserDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDoctors_AspNetUsers_UserId",
                table: "UserDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDoctors",
                table: "UserDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoodEntry",
                table: "MoodEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorWeeklySchedules",
                table: "DoctorWeeklySchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSessionSlots",
                table: "DoctorSessionSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "UserDoctors",
                newName: "UserDoctor");

            migrationBuilder.RenameTable(
                name: "MoodEntry",
                newName: "ModeEntries");

            migrationBuilder.RenameTable(
                name: "DoctorWeeklySchedules",
                newName: "DoctorWeeklySchedule");

            migrationBuilder.RenameTable(
                name: "DoctorSessionSlots",
                newName: "DoctorSessionSlot");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chat");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "ChatMessage");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_UserDoctors_DoctorId",
                table: "UserDoctor",
                newName: "IX_UserDoctor_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_MoodEntry_UserId_Date",
                table: "ModeEntries",
                newName: "IX_ModeEntries_UserId_Date");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorWeeklySchedules_DoctorId",
                table: "DoctorWeeklySchedule",
                newName: "IX_DoctorWeeklySchedule_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSessionSlots_DoctorId_StartDateTime",
                table: "DoctorSessionSlot",
                newName: "IX_DoctorSessionSlot_DoctorId_StartDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_BookingId",
                table: "Chat",
                newName: "IX_Chat_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "Booking",
                newName: "IX_Booking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_DoctorSessionSlotId",
                table: "Booking",
                newName: "IX_Booking_DoctorSessionSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_DoctorId",
                table: "Booking",
                newName: "IX_Booking_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDoctor",
                table: "UserDoctor",
                columns: new[] { "UserId", "DoctorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModeEntries",
                table: "ModeEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorWeeklySchedule",
                table: "DoctorWeeklySchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSessionSlot",
                table: "DoctorSessionSlot",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_DoctorId",
                table: "Booking",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_UserId",
                table: "Booking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DoctorSessionSlot_DoctorSessionSlotId",
                table: "Booking",
                column: "DoctorSessionSlotId",
                principalTable: "DoctorSessionSlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Booking_BookingId",
                table: "Chat",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Chat_ChatId",
                table: "ChatMessage",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSessionSlot_AspNetUsers_DoctorId",
                table: "DoctorSessionSlot",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWeeklySchedule_AspNetUsers_DoctorId",
                table: "DoctorWeeklySchedule",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeEntries_AspNetUsers_UserId",
                table: "ModeEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDoctor_AspNetUsers_DoctorId",
                table: "UserDoctor",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDoctor_AspNetUsers_UserId",
                table: "UserDoctor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
