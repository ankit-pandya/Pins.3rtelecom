// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionsData.Data;

namespace TransactionsData.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220622194114_emailBulk-added")]
    partial class emailBulkadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TransactionsData.Models.MTProductModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product3r")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productCode3r")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("provider3r")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("tblMTProducts");
                });

            modelBuilder.Entity("TransactionsData.Models.MerchantBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MerchantID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblMerchantBalance");
                });

            modelBuilder.Entity("TransactionsData.Models.MerchantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BalWarning")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FriOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MerchantID")
                        .HasColumnType("int");

                    b.Property<string>("MonCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OldMID")
                        .HasColumnType("int");

                    b.Property<string>("PinMode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SunCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SunCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SunOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SunOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThuCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThuCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThuOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThuOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TueCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TueCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TueOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TueOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WedCH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WedCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WedOH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WedOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("allowEE")
                        .HasColumnType("bit");

                    b.Property<bool>("emailbulk")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("TransactionsData.Models.MerchantPayments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("merchantID")
                        .HasColumnType("int");

                    b.Property<int>("mid")
                        .HasColumnType("int");

                    b.Property<DateTime>("paydatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("merchantID");

                    b.ToTable("MerchantPayments");
                });

            modelBuilder.Entity("TransactionsData.Models.MerchantTerminals", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MacId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MerchantID")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerminalRef")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("MerchantTerminal");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductDenominations", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("dateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("denomination")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("expiryPeriod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("followOnCall")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("freephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("generalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("helplineNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("londonNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numberAdded")
                        .HasColumnType("int");

                    b.Property<int>("numberRemaining")
                        .HasColumnType("int");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("productsid")
                        .HasColumnType("int");

                    b.Property<string>("providerCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("productsid");

                    b.ToTable("tblproductdenoms");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("providerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("providertblid")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("topupLevel")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("providertblid");

                    b.ToTable("tblproducts");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductPinsModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("dateSold")
                        .HasColumnType("datetime2");

                    b.Property<string>("pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productDenominationsid")
                        .HasColumnType("int");

                    b.Property<string>("serial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("productDenominationsid");

                    b.ToTable("tblproductpins");
                });

            modelBuilder.Entity("TransactionsData.Models.ProviderModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("providerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("providerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblproviders");
                });

            modelBuilder.Entity("TransactionsData.Models.TransactionsModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateandTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MerchantID")
                        .HasColumnType("int");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TerminalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionID")
                        .HasColumnType("int");

                    b.Property<string>("TxnNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("requestData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactionsData.Models.MerchantPayments", b =>
                {
                    b.HasOne("TransactionsData.Models.MerchantModel", "merchant")
                        .WithMany()
                        .HasForeignKey("merchantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("merchant");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductDenominations", b =>
                {
                    b.HasOne("TransactionsData.Models.ProductModel", "products")
                        .WithMany()
                        .HasForeignKey("productsid");

                    b.Navigation("products");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductModel", b =>
                {
                    b.HasOne("TransactionsData.Models.ProviderModel", "providertbl")
                        .WithMany()
                        .HasForeignKey("providertblid");

                    b.Navigation("providertbl");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductPinsModel", b =>
                {
                    b.HasOne("TransactionsData.Models.ProductDenominations", "productDenominations")
                        .WithMany("Pins")
                        .HasForeignKey("productDenominationsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("productDenominations");
                });

            modelBuilder.Entity("TransactionsData.Models.ProductDenominations", b =>
                {
                    b.Navigation("Pins");
                });
#pragma warning restore 612, 618
        }
    }
}
