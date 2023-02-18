using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ESHOP.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOrderFinished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryToProducts",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryToProducts", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "موبایل", "موبایل" },
                    { 2, "ساعت هوشمند", "ساعت هوشمند" },
                    { 3, "هندزفری", "هندزفری" },
                    { 4, "پاوربانک", "پاوربانک" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 45000000m, 5 },
                    { 2, 7000000m, 10 },
                    { 3, 15000000m, 12 },
                    { 4, 30950000m, 3 },
                    { 5, 11290000m, 6 },
                    { 6, 5849000m, 8 },
                    { 7, 4927000m, 6 },
                    { 8, 564300m, 12 },
                    { 9, 995000m, 10 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "IsAdmin", "Password", "PhoneNumber", "RegisterTime" },
                values: new object[] { 1, "admin@gmail.com", "admin", true, "admin123", "09123123123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ItemId", "Name" },
                values: new object[,]
                {
                    { 1, "گوشی‌های هوشمند خانواده آیفون 13 در قالب چهار گوشی هوشمند آیفون 13 پرو مکس، آیفون 13 پرو، آیفون 13‌ و آیفون 13 مینی معرفی شدند. پرچمداران جدید اپل این بار قدرتمند‌تر از همیشه پا به عرصه رقابت گذاشته اند تا در رقابتی بسیار جذاب، عملکردی بهتر به نسبت پرچمداران اندرویدی به نمایش بگذارد. از جمله اصلی‌ترین تغییرات در نظر گرفته شده برای این گوشی های هوشمند در مقایسه با پرچمداران خانواده آیفون 12 می‌توانیم به سنسور‌های دوربین قدرتمند‌تر، پردازنده فوق العاده با عملکرد بهتر و خیره کننده به نسبت نسل قبلی، تنوع رنگی بالا، صفحه نمایش به‌مراتب با‌کیفیت‌تراشاره کنیم. در این بررسی به‌سراغ آیفون 13 پرو مکس به عنوان گل سرسبد گوشی های هوشمند این خانواده رفته ایم تا ببینیم چه مشخصاتی را با خودش به همراه داشته و به نسبت آیفون 12 پرو مکس چه تغییرات در مشخصات فنی در نظر گرفته شده دارد..", 1, "گوشی موبایل اپل مدل iPhone 13 Pro Max A2644" },
                    { 2, "باید قبول کنیم که برند شیائومی توانسته است در زمینه گوشی‌های هوشمند عملکرد بسیار درخشانی را از خود به‌نمایش بگذارد. در بخش گوشی‌های هوشمند میان‌رده این شرکت هم شاهد رونمایی محصولات بسیار با‌کیفیتی بودیم. یکی از گوشی‌های هوشمند میان‌رده و البته قدرتمند این شرکت، مدل Redmi Note 11 است که می‌توان گفت یکی از بهترین گوشی‌های هوشمند میان‌رده در این بازه قیمتی است. در نمای رو‌به‌رویی صفحه‌نمایش یکدست با طراحی ناچ اینفینیتی O پرچمدار‌گونه‌ای را شاهد هستیم که بریدگی دایره‌ای شکل ناچ در قسمت بالایی و مرکزی صفحه‌نمایش، سنسور دوربین سلفی را در خود جای داده است. در جایگاه یک گوشی میان‌رده، باید بگوییم که حاشیه‌های کمی برای صفحه‌نمایش در نظر گرفته شده و همین امر سبب شده تا ۸۴.۵ درصد از نمای رو‌به‌رویی را صفحه‌نمایش به خودش اختصاص دهد", 2, "گوشی موبایل شیائومی مدل Redmi Note 11 دو سیم‌ کارت ظرفیت 128 گیگابایت  " },
                    { 3, "سامسونگ Galaxy A53 5G یکی از گوشی‌های هوشمند میان‌رده سطح بالای این شرکت است که بدون هیچ تعریف اضافی باید گفت که در برخی از موارد، از مشخصات فنی قدرتمندی در حد و اندازه گوشی‌های پرچمدار بهره برده است. در همان نگاه اول، با توجه به طراحی در نظر گرفته شده، Galaxy A53 5G تلاش دارد تا خود را به‌عنوان یک گوشی پرچمدار معرفی کند و باید گفت که در این زمینه بسیار موفق بوده است. طراحی که ما را به یاد پرچمداران خانواده Galaxy S22 می‌اندازد. در نمای رو‌به‌رویی صفحه‌نمایش زیبا و یکدستی را شاهد هستیم که از طراحی ناچ اینفینیتی O بهره برده است. بریدگی دایره‌ای شکل ناچ در قسمت بالایی و مرکزی صفحه‌نمایش با قطر بسیار کم، سنسور دوربین سلفی را در خود جای داده است. سامسونگ سعی داشته تا کمترین میزان حاشیه را برای صفحه‌نمایش در نظر بگیرید و تا میزان قابل قبولی هم در این زمینه موفق بوده است. ۸۵.۴ درصد از نمای رو‌به‌رو را صفحه‌نمایش به خودش اختصاص داده است.", 3, "گوشی موبایل سامسونگ مدل Galaxy A53 5G SM-A536E/DS" },
                    { 4, "ساعت‌های هوشمند اپل از سری لوازم جانبی جذاب و پرکاربردی هستند که همواره طرفداران اپل برای رونمایی از آن‌ در کنار سایر دستگاه‌های اپل انتظار می‌کشند. اپل سری 7 ساعت‌های هوشمند خود را در دو سایز 41 میلی‌متر و 45 میلی‌متر به بازار روانه می‌کند. این ساعت هوشمند نسبت به ساعت‌های هوشمند سری قبل اپل با صفحه‌نمایش خمیده ارائه شده‌است. همچنین حاشیه‌ها نسبت به سری قبل کمتر خواهد بود. اپل ساعت هوشمند سری 7 خود را در رنگ‌های سبز، آبی، قرمز، مشکی و استارلایت (starlight) ارائه کرده‌است.", 4, "ساعت هوشمند اپل واچ سری 7 مدل 45mm Stainless Steel Case with Milanese Loop Steel Band" },
                    { 5, "ساعت‌های هوشمند سامسونگ، همواره توانسته‌اند با بهره بردن از مشخصات فنی مناسب، عملکرد بسیار خوبی را به‌نمایش بگذارند. سامسونگ Galaxy Watch 5 Pro هم یکی از جدید‌ترین ساعت‌های هوشمند این شرکت است که به نسبت مدل استاندارد، مشخصات قدرتمند‌تر و البته سبک طراحی جذاب‌تری را با خود به همراه دارد. طراحی و رنگ‌بندی در نظر گرفته شده برای این ساعت هوشمند سبب شده تا در کنار حالت اسپرت، حس و حال یک ساعت رسمی و رده بالا را به‌خوبی به‌شما می‌دهد", 5, "ساعت هوشمند سامسونگ مدل Galaxy Watch 5 Pro" },
                    { 6, " این سری هدفون‌های اپل که همواره با طراحی منحصربه‌فرد و با نام AirPods Pro 2021 شناخته می‌شوند، از نوع توگوشی هستند و میزان بیس و قدرت بالا را به کاربران عرضه می‌کنند. در آیفون 7 و دیگر محصولات بعد از آن، جک 3.5 میلی‌متری صدا که یک ارتباط‌دهنده‌ی آنالوگ بود، حذف شد. حذف این درگاه با تغییر نوع اتصال و بی‌سیم‌شدن ایرپاد همراه بود. این هدفون دو گوشی جدا از هم دارد که هرکدام در یکی از گوش‌ها قرار می‌گیرد.", 6, "هدفون بی سیم اپل مدل AirPods Pro 2021 همراه با محفظه شارژ" },
                    { 7, "هدفون‌های بلوتوثی سامسونگ تا به امروز توانسته‌اند عملکرد بسیار خوبی را از خود به‌نمایش بگذارند و Galaxy Buds2 Pro هم یکی از جدید‌ترین هدفون‌های بی‌سیم این شرکت است که در کنار مشخصات فنی بسیار خوب، قابلیت‌های بسیار جذابی را نیز با خود به‌همراه دارد. دو خروجی اسپیکر در نظر گرفته شده برای این هدفون سبب شده تا خروجی صدای بسیار جذابی را شاهد باشید. خروجی صدای 24 بیت هم سبب شده تا به نسبت خروجی‌ صدای 16 بیت، صدایی صاف تر و بسیار با‌کیفیت‌تری را بشنوید.", 7, "هدفون بلوتوثی سامسونگ مدل Galaxy Buds2 Pro" },
                    { 8, "پاور بانک 20000 Redmi شیائومی مدل PB200LZM به همراه یک عدد کابل شارژ 25 سانتی‌متری ارائه می‌شود، این محصول با داشتن دو پورت خروجی USB و پشتیبانی از انواع پروتکل‌های شارژ سریع، بهترین همراه شما در خارج از منزل، محل کار و یا هنگام سفر خواهد بود. پاور بانک Redmi با برخورداری از شارژ هوشمند به راحتی می‌تواند به طور هم‌زمان دو دیوایس را شارژ کند. این محصول می‌تواند به طور خودکار و هوشمند، ورودی مناسب برای هر دیوایس را در نظر بگیرد", 8, "شارژر همراه شیائومی مدل Redmi ظرفیت 20000 میلی آمپرساعت" },
                    { 9, "امروزه با توجه به اهمیتی که گوشی‌های موبایل در زندگی ما پیدا کرده‌اند و با توجه به استفاده فراوان از آن، همراه داشتن پاوربانک به امری واجب تبدیل شده است. اگر شما قصد خرید یک پاوربانک را دارید، حتما باید از قبل نیاز خود را شناسایی کرده باشید تا در انتخاب آن به مشکل برنخورید. اگر شما از آن دسته افرادی هستید که با گوشی خود زیاد کار می‌کنید و نیاز به یک پاوربانک با ظرفیت بالا دارید، پیشنهاد ما به شما پاوربانک LP29 از کمپانی لیتو است", 9, "شارژر همراه لیتو مدل LP29 ظرفیت 20000 میلی‌آمپر ساعت" }
                });

            migrationBuilder.InsertData(
                table: "CategoryToProducts",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 4, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProducts_ProductId",
                table: "CategoryToProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemId",
                table: "Products",
                column: "ItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryToProducts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
