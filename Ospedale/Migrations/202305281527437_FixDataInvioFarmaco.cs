namespace Ospedale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDataInvioFarmaco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Farmaco",
                c => new
                    {
                        IdFarmaco = c.Int(nullable: false, identity: true),
                        NomeFarmaco = c.String(nullable: false, maxLength: 50),
                        Quantià = c.Int(nullable: false),
                        IdReparto = c.Int(nullable: false),
                        DataInvioFarmaco = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdFarmaco)
                .ForeignKey("dbo.Reparto", t => t.IdReparto)
                .Index(t => t.IdReparto);
            
            CreateTable(
                "dbo.Reparto",
                c => new
                    {
                        IdReparto = c.Int(nullable: false, identity: true),
                        NomeReparto = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdReparto);
            
            CreateTable(
                "dbo.Visita",
                c => new
                    {
                        IdVisita = c.Int(nullable: false, identity: true),
                        IdMedico = c.Int(nullable: false),
                        IdReparto = c.Int(nullable: false),
                        IdPaziente = c.Int(nullable: false),
                        DataVisita = c.DateTime(nullable: false),
                        StatoPaziente = c.String(nullable: false),
                        PressioneMinima = c.Int(nullable: false),
                        PressioneMassima = c.Int(nullable: false),
                        Temperatura = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FrequenzaCardiaca = c.Int(nullable: false),
                        IdFarmaco = c.Int(),
                        NomeVisita = c.String(),
                        Posologia = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdVisita)
                .ForeignKey("dbo.Farmaco", t => t.IdFarmaco)
                .ForeignKey("dbo.Medico", t => t.IdMedico)
                .ForeignKey("dbo.Paziente", t => t.IdPaziente)
                .ForeignKey("dbo.Reparto", t => t.IdReparto, cascadeDelete: true)
                .Index(t => t.IdMedico)
                .Index(t => t.IdReparto)
                .Index(t => t.IdPaziente)
                .Index(t => t.IdFarmaco);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        IdMedico = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdMedico);
            
            CreateTable(
                "dbo.Paziente",
                c => new
                    {
                        IdPaziente = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        CodiceFiscale = c.String(nullable: false, maxLength: 50),
                        DataNascita = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.IdPaziente);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Ruolo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visita", "IdReparto", "dbo.Reparto");
            DropForeignKey("dbo.Visita", "IdPaziente", "dbo.Paziente");
            DropForeignKey("dbo.Visita", "IdMedico", "dbo.Medico");
            DropForeignKey("dbo.Visita", "IdFarmaco", "dbo.Farmaco");
            DropForeignKey("dbo.Farmaco", "IdReparto", "dbo.Reparto");
            DropIndex("dbo.Visita", new[] { "IdFarmaco" });
            DropIndex("dbo.Visita", new[] { "IdPaziente" });
            DropIndex("dbo.Visita", new[] { "IdReparto" });
            DropIndex("dbo.Visita", new[] { "IdMedico" });
            DropIndex("dbo.Farmaco", new[] { "IdReparto" });
            DropTable("dbo.User");
            DropTable("dbo.Paziente");
            DropTable("dbo.Medico");
            DropTable("dbo.Visita");
            DropTable("dbo.Reparto");
            DropTable("dbo.Farmaco");
        }
    }
}
