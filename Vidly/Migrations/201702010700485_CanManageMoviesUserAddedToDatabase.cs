namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CanManageMoviesUserAddedToDatabase : DbMigration
    {
        public override void Up()
        {
            //Adding Role
            Sql("INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'31132640-81ac-4e7f-9aa8-f96e4eaf9d41', N'CanManageMovies')");

            //Adding Users
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4cb077e4-47f9-460f-bfc6-ff7f5a3709a1', N'guest@vidly.com', 0, N'AKWX+2kp3KOTBMw6RgjcpmjZd4n6vgXeaiPR81WtSUFbGleMwtC3ySpQfEURNj8BkA==', N'fa2fe34a-0f09-48f1-9cdf-5b3fd0c66a46', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f98b0527-fb59-4e51-9218-73a2d92e2711', N'admin@vidly.com', 0, N'AKBUSI52T7ejoFt2bvJLLHRBh+wcUPfoQwSc61XqkkNKlHdoTnM6ENbPyXOdH+A8pQ==', N'40084827-3bff-4a19-98b8-0ff24cbcce3d', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
            ");

            //Adding UserRoles
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f98b0527-fb59-4e51-9218-73a2d92e2711', N'31132640-81ac-4e7f-9aa8-f96e4eaf9d41')");

        }
        
        public override void Down()
        {
        }
    }
}
