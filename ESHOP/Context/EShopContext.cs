using ESHOP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESHOP.Data
{
    public class EShopContext : DbContext
    {
        public EShopContext(DbContextOptions<EShopContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>()
                .HasKey(k => new { k.CategoryId, k.ProductId });

            modelBuilder.Entity<Item>(
                i =>
                {
                    i.Property(w => w.Price).HasColumnType("Money");
                    i.HasKey(w => w.Id);
                }
            );

            #region Seed Data Item

            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Price = 45000000,
                    QuantityInStock = 5
                },

                new Item()
                {
                    Id = 2,
                    Price = 7000000,
                    QuantityInStock = 10
                },
                new Item()
                {
                    Id = 3,
                    Price = 15000000,
                    QuantityInStock = 12
                },
                new Item()
                {
                    Id = 4,
                    Price = 30950000,
                    QuantityInStock = 3,
                },
                new Item()
                {
                    Id = 5,
                    Price = 11290000,
                    QuantityInStock = 6,
                },
                new Item()
                {
                    Id = 6,
                    Price = 5849000,
                    QuantityInStock = 8,
                },
                new Item()
                {
                    Id = 7,
                    Price = 4927000,
                    QuantityInStock = 6,
                },
                new Item()
                {
                    Id = 8,
                    Price = 564300,
                    QuantityInStock = 12,
                },
                new Item()
                {
                    Id = 9,
                    Price = 995000,
                    QuantityInStock = 10,
                }
            );

            #endregion

            #region Seed Data Product

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "گوشی موبایل اپل مدل iPhone 13 Pro Max A2644",
                    Description = "گوشی‌های هوشمند خانواده آیفون 13 در قالب چهار گوشی هوشمند آیفون 13 پرو مکس، آیفون 13 پرو، آیفون 13‌ و آیفون 13 مینی معرفی شدند. پرچمداران جدید اپل این بار قدرتمند‌تر از همیشه پا به عرصه رقابت گذاشته اند تا در رقابتی بسیار جذاب، عملکردی بهتر به نسبت پرچمداران اندرویدی به نمایش بگذارد. از جمله اصلی‌ترین تغییرات در نظر گرفته شده برای این گوشی های هوشمند در مقایسه با پرچمداران خانواده آیفون 12 می‌توانیم به سنسور‌های دوربین قدرتمند‌تر، پردازنده فوق العاده با عملکرد بهتر و خیره کننده به نسبت نسل قبلی، تنوع رنگی بالا، صفحه نمایش به‌مراتب با‌کیفیت‌تراشاره کنیم. در این بررسی به‌سراغ آیفون 13 پرو مکس به عنوان گل سرسبد گوشی های هوشمند این خانواده رفته ایم تا ببینیم چه مشخصاتی را با خودش به همراه داشته و به نسبت آیفون 12 پرو مکس چه تغییرات در مشخصات فنی در نظر گرفته شده دارد..",
                    ItemId = 1
                },

                new Product()
                {
                    Id = 2,
                    Name = "گوشی موبایل شیائومی مدل Redmi Note 11 دو سیم‌ کارت ظرفیت 128 گیگابایت  ",
                    Description = "باید قبول کنیم که برند شیائومی توانسته است در زمینه گوشی‌های هوشمند عملکرد بسیار درخشانی را از خود به‌نمایش بگذارد. در بخش گوشی‌های هوشمند میان‌رده این شرکت هم شاهد رونمایی محصولات بسیار با‌کیفیتی بودیم. یکی از گوشی‌های هوشمند میان‌رده و البته قدرتمند این شرکت، مدل Redmi Note 11 است که می‌توان گفت یکی از بهترین گوشی‌های هوشمند میان‌رده در این بازه قیمتی است. در نمای رو‌به‌رویی صفحه‌نمایش یکدست با طراحی ناچ اینفینیتی O پرچمدار‌گونه‌ای را شاهد هستیم که بریدگی دایره‌ای شکل ناچ در قسمت بالایی و مرکزی صفحه‌نمایش، سنسور دوربین سلفی را در خود جای داده است. در جایگاه یک گوشی میان‌رده، باید بگوییم که حاشیه‌های کمی برای صفحه‌نمایش در نظر گرفته شده و همین امر سبب شده تا ۸۴.۵ درصد از نمای رو‌به‌رویی را صفحه‌نمایش به خودش اختصاص دهد",
                    ItemId = 2
                },

                new Product()
                {
                    Id = 3,
                    Name = "گوشی موبایل سامسونگ مدل Galaxy A53 5G SM-A536E/DS",
                    Description = "سامسونگ Galaxy A53 5G یکی از گوشی‌های هوشمند میان‌رده سطح بالای این شرکت است که بدون هیچ تعریف اضافی باید گفت که در برخی از موارد، از مشخصات فنی قدرتمندی در حد و اندازه گوشی‌های پرچمدار بهره برده است. در همان نگاه اول، با توجه به طراحی در نظر گرفته شده، Galaxy A53 5G تلاش دارد تا خود را به‌عنوان یک گوشی پرچمدار معرفی کند و باید گفت که در این زمینه بسیار موفق بوده است. طراحی که ما را به یاد پرچمداران خانواده Galaxy S22 می‌اندازد. در نمای رو‌به‌رویی صفحه‌نمایش زیبا و یکدستی را شاهد هستیم که از طراحی ناچ اینفینیتی O بهره برده است. بریدگی دایره‌ای شکل ناچ در قسمت بالایی و مرکزی صفحه‌نمایش با قطر بسیار کم، سنسور دوربین سلفی را در خود جای داده است. سامسونگ سعی داشته تا کمترین میزان حاشیه را برای صفحه‌نمایش در نظر بگیرید و تا میزان قابل قبولی هم در این زمینه موفق بوده است. ۸۵.۴ درصد از نمای رو‌به‌رو را صفحه‌نمایش به خودش اختصاص داده است.",
                    ItemId = 3
                },

                new Product()
                {
                    Id = 4,
                    Name = "ساعت هوشمند اپل واچ سری 7 مدل 45mm Stainless Steel Case with Milanese Loop Steel Band",
                    Description = "ساعت‌های هوشمند اپل از سری لوازم جانبی جذاب و پرکاربردی هستند که همواره طرفداران اپل برای رونمایی از آن‌ در کنار سایر دستگاه‌های اپل انتظار می‌کشند. اپل سری 7 ساعت‌های هوشمند خود را در دو سایز 41 میلی‌متر و 45 میلی‌متر به بازار روانه می‌کند. این ساعت هوشمند نسبت به ساعت‌های هوشمند سری قبل اپل با صفحه‌نمایش خمیده ارائه شده‌است. همچنین حاشیه‌ها نسبت به سری قبل کمتر خواهد بود. اپل ساعت هوشمند سری 7 خود را در رنگ‌های سبز، آبی، قرمز، مشکی و استارلایت (starlight) ارائه کرده‌است.",
                    ItemId = 4
                },

                new Product()
                {
                    Id = 5,
                    Name = "ساعت هوشمند سامسونگ مدل Galaxy Watch 5 Pro",
                    Description = "ساعت‌های هوشمند سامسونگ، همواره توانسته‌اند با بهره بردن از مشخصات فنی مناسب، عملکرد بسیار خوبی را به‌نمایش بگذارند. سامسونگ Galaxy Watch 5 Pro هم یکی از جدید‌ترین ساعت‌های هوشمند این شرکت است که به نسبت مدل استاندارد، مشخصات قدرتمند‌تر و البته سبک طراحی جذاب‌تری را با خود به همراه دارد. طراحی و رنگ‌بندی در نظر گرفته شده برای این ساعت هوشمند سبب شده تا در کنار حالت اسپرت، حس و حال یک ساعت رسمی و رده بالا را به‌خوبی به‌شما می‌دهد",
                    ItemId = 5
                },

                new Product()
                {
                    Id = 6,
                    Name = "هدفون بی سیم اپل مدل AirPods Pro 2021 همراه با محفظه شارژ",
                    Description = " این سری هدفون‌های اپل که همواره با طراحی منحصربه‌فرد و با نام AirPods Pro 2021 شناخته می‌شوند، از نوع توگوشی هستند و میزان بیس و قدرت بالا را به کاربران عرضه می‌کنند. در آیفون 7 و دیگر محصولات بعد از آن، جک 3.5 میلی‌متری صدا که یک ارتباط‌دهنده‌ی آنالوگ بود، حذف شد. حذف این درگاه با تغییر نوع اتصال و بی‌سیم‌شدن ایرپاد همراه بود. این هدفون دو گوشی جدا از هم دارد که هرکدام در یکی از گوش‌ها قرار می‌گیرد.",
                    ItemId = 6,
                },

                new Product()
                {
                    Id = 7,
                    Name = "هدفون بلوتوثی سامسونگ مدل Galaxy Buds2 Pro",
                    Description = "هدفون‌های بلوتوثی سامسونگ تا به امروز توانسته‌اند عملکرد بسیار خوبی را از خود به‌نمایش بگذارند و Galaxy Buds2 Pro هم یکی از جدید‌ترین هدفون‌های بی‌سیم این شرکت است که در کنار مشخصات فنی بسیار خوب، قابلیت‌های بسیار جذابی را نیز با خود به‌همراه دارد. دو خروجی اسپیکر در نظر گرفته شده برای این هدفون سبب شده تا خروجی صدای بسیار جذابی را شاهد باشید. خروجی صدای 24 بیت هم سبب شده تا به نسبت خروجی‌ صدای 16 بیت، صدایی صاف تر و بسیار با‌کیفیت‌تری را بشنوید.",
                    ItemId = 7,
                },

                new Product()
                {
                    Id = 8,
                    Name = "شارژر همراه شیائومی مدل Redmi ظرفیت 20000 میلی آمپرساعت",
                    Description = "پاور بانک 20000 Redmi شیائومی مدل PB200LZM به همراه یک عدد کابل شارژ 25 سانتی‌متری ارائه می‌شود، این محصول با داشتن دو پورت خروجی USB و پشتیبانی از انواع پروتکل‌های شارژ سریع، بهترین همراه شما در خارج از منزل، محل کار و یا هنگام سفر خواهد بود. پاور بانک Redmi با برخورداری از شارژ هوشمند به راحتی می‌تواند به طور هم‌زمان دو دیوایس را شارژ کند. این محصول می‌تواند به طور خودکار و هوشمند، ورودی مناسب برای هر دیوایس را در نظر بگیرد",
                    ItemId = 8,
                },

                new Product()
                {
                    Id = 9,
                    Name = "شارژر همراه لیتو مدل LP29 ظرفیت 20000 میلی‌آمپر ساعت",
                    Description = "امروزه با توجه به اهمیتی که گوشی‌های موبایل در زندگی ما پیدا کرده‌اند و با توجه به استفاده فراوان از آن، همراه داشتن پاوربانک به امری واجب تبدیل شده است. اگر شما قصد خرید یک پاوربانک را دارید، حتما باید از قبل نیاز خود را شناسایی کرده باشید تا در انتخاب آن به مشکل برنخورید. اگر شما از آن دسته افرادی هستید که با گوشی خود زیاد کار می‌کنید و نیاز به یک پاوربانک با ظرفیت بالا دارید، پیشنهاد ما به شما پاوربانک LP29 از کمپانی لیتو است",
                    ItemId = 9,
                }
            );

            #endregion

            #region Seed Data Category

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "موبایل",
                    Description = "موبایل"
                },

                new Category()
                {
                    Id = 2,
                    Name = "ساعت هوشمند",
                    Description = "ساعت هوشمند"
                },

                new Category()
                {
                    Id = 3,
                    Name = "هندزفری",
                    Description = "هندزفری"
                },

                new Category()
                {
                    Id = 4,
                    Name = "پاوربانک",
                    Description = "پاوربانک"
                }
                );

            #endregion

            #region Seed Data CategoryToProduct

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1, ProductId = 1},
                new CategoryToProduct() { CategoryId = 1, ProductId = 2},
                new CategoryToProduct() { CategoryId = 1, ProductId = 3},

                new CategoryToProduct() { CategoryId = 2, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 5 },

                new CategoryToProduct() { CategoryId = 3, ProductId = 6 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 7 },

                new CategoryToProduct() { CategoryId = 4, ProductId = 8 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 9 }
            );

            #endregion

            #region Admin SeedData

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    FullName = "admin",
                    Email = "admin@gmail.com",
                    Password = "admin123",
                    IsAdmin = true,
                    PhoneNumber = "09123123123",
                }
                );

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}