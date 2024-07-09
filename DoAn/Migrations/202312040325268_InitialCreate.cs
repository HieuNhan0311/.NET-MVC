namespace DoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        MaSp = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.SanPhams", t => t.MaSp, cascadeDelete: true)
                .Index(t => t.MaSp);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSp = c.Int(nullable: false, identity: true),
                        MaLoai = c.Int(nullable: false),
                        TenSP = c.String(),
                        DVT = c.String(),
                        MoTa = c.String(),
                        SLTon = c.Int(nullable: false),
                        Anh = c.String(),
                        GiaBan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KichThuoc = c.String(),
                    })
                .PrimaryKey(t => t.MaSp)
                .ForeignKey("dbo.Loais", t => t.MaLoai, cascadeDelete: true)
                .Index(t => t.MaLoai);
            
            CreateTable(
                "dbo.Loais",
                c => new
                    {
                        MaLoai = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                    })
                .PrimaryKey(t => t.MaLoai);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "MaSp", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "MaLoai", "dbo.Loais");
            DropIndex("dbo.SanPhams", new[] { "MaLoai" });
            DropIndex("dbo.Carts", new[] { "MaSp" });
            DropTable("dbo.Loais");
            DropTable("dbo.SanPhams");
            DropTable("dbo.Carts");
        }
    }
}
