namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Long(nullable: false, identity: true),
                        ReferenceNo = c.String(),
                        Title = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        LoanDate = c.DateTime(nullable: false),
                        ExpectedReturnDate = c.DateTime(nullable: false),
                        ActualReturnDate = c.DateTime(),
                        MemberId = c.Long(nullable: false),
                        BookId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LoanId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "BookId", "dbo.Books");
            DropIndex("dbo.Loans", new[] { "BookId" });
            DropTable("dbo.Members");
            DropTable("dbo.Loans");
            DropTable("dbo.Books");
        }
    }
}
