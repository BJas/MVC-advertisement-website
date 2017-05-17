using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKrisha.Models;

namespace MvcKrisha.DAL
{
    public class KrishaInitializer : System.Data.Entity.DropCreateDatabaseAlways<KrishaContext>
    {
        protected override void Seed(KrishaContext context)
        {
            var users = new List<User>
            {
                new User { UserID=1, Login="Admin", Password="Admin", RePassword="Admin", Role= 1, Name="Owner", Email="admin@gmail.com", PhoneNumber="87072073266"},
                new User { UserID=2, Login="seller1", Password="seller1", RePassword="seller1", Role= 0, Name="Asxat", Email="seller1@gmail.com", PhoneNumber="87758420262"},
                new User { UserID=3, Login="seller2", Password="seller2", RePassword="seller2", Role= 0, Name="Aslan", Email="seller2@gmail.com", PhoneNumber="87758420263"},
                new User { UserID=4, Login="seller3", Password="seller3", RePassword="seller3", Role= 0, Name="Bekarys", Email="seller3@gmail.com", PhoneNumber="87758420264"},
                new User { UserID=5, Login="seller4", Password="seller4", RePassword="seller4", Role= 0, Name="Alixan", Email="seller4@gmail.com", PhoneNumber="87758420265"},
                new User { UserID=6, Login="seller5", Password="seller5", RePassword="seller5", Role= 0, Name="Alua", Email="seller5@gmail.com", PhoneNumber="87758420266"}
            };
            users.ForEach(a => context.Users.Add(a));
            context.SaveChanges();

            var regions = new List<Region>
            {
                new Region { RegionID=1, City="Almaty"},
                new Region { RegionID=2, City="Astana"},
                new Region { RegionID=3, City="Shymkent"},
                new Region { RegionID=4, City="Taraz"},
                new Region { RegionID=5, City="Atyrau"},
                new Region { RegionID=6, City="Aktobe"},
                new Region { RegionID=7, City="Aktau"},
                new Region { RegionID=8, City="Kyzylorda"},
                new Region { RegionID=9, City="Karaganda"},
                new Region { RegionID=10, City="Pavlodar"},
                new Region { RegionID=11, City="Semey"},
                new Region { RegionID=12, City="Taldykorgan"},
                new Region { RegionID=13, City="Uralsk"},
                new Region { RegionID=14, City="Kostanai"},
                new Region { RegionID=15, City="Petropavlovsk"}
            };
            regions.ForEach(a => context.Regions.Add(a));
            context.SaveChanges();

            var flats = new List<Flat>
            {
                new Flat { UserID=6, RegionID=1, BuildTime=2005, FloorAll=5, Floor=1, Area=50, State="Good", Address="Al-Farabi-Askarova", Room=2, Price=68000, Type="monolithic", Image=@"\Images\0_8c1d2_65ac1f61_orig.jpg", PhoneNumb="87758420266", Description="Unangular, improved, separate rooms, quiet courtyard", PublishedDate=DateTime.Parse("2016-05-26"), Views=0 },
                new Flat { UserID=6, RegionID=1, BuildTime=2012, FloorAll=18, Floor=8, Area=73, State="New", Address="Seifullina-Mametova", Room=2, Price=95000, Type="monolithic", Image=@"\Images\092386698b085a3f9f436166cff55867.jpg",PhoneNumb="87758420266", Description="plastic windows, Unangular, improved, separate rooms, new plumbing, counters, air conditioning", PublishedDate=DateTime.Parse("2016-05-25"), Views=0 },
                new Flat { UserID=6, RegionID=2, BuildTime=1978, FloorAll=5, Floor=3, Area=48, State="Good", Address="Gogolya-Zheltoksan", Room=2, Price=50000, Type="panel", Image=@"\Images\1400158544_a.jpg",PhoneNumb="87758420266", Description="The spacious, isolation rooms, balcony, large closet / dressing room.Joinery apartment overlooking the street.", PublishedDate=DateTime.Parse("2016-05-22"), Views=0 },
                new Flat { UserID=6, RegionID=3, BuildTime=1986, FloorAll=5, Floor=3, Area=43, State="New", Address="Tolebi-Gagarina", Room=2, Price=65000, Type="panel", Image=@"\Images\15-full.jpg",PhoneNumb="87758420266", Description="Infrastructure, within walking distance of a diagnostic center, apartment in excellent condition, rooms are isolated", PublishedDate=DateTime.Parse("2016-05-21"), Views=0 },
                new Flat { UserID=2, RegionID=5, BuildTime=1975, FloorAll=5, Floor=2, Area=52, State="New", Address="Abai-Baitursynova", Room=2, Price=55000, Type="stone", Image=@"\Images\5-full.jpg",PhoneNumb="87758420262", Description="Bright clean apartment, renovation done in the past year, with no alterations.", PublishedDate=DateTime.Parse("2016-05-21"), Views=0 },
                new Flat { UserID=2, RegionID=6, BuildTime=1983, FloorAll=5, Floor=4, Area=53.2, State="School", Address="Aitekebi-Seifullina", Room=2, Price=60000, Type="stone", Image=@"\Images\6482b.jpg",PhoneNumb="87758420262", Description="Spacious, comfortable. Near the metro station Alatau SEC Globe, the banks, alley and parks for walking. ", PublishedDate=DateTime.Parse("2016-05-21"), Views=0 },
                new Flat { UserID=2, RegionID=7, BuildTime=2011, FloorAll=22, Floor=2, Area=39, State="New", Address="Kabanbai batyra-AbylaiHana", Room=1, Price=50000, Type="monolithic", Image=@"\Images\6da396a2f44174a6e9f5b3e5405ebbc1.jpg",PhoneNumb="87758420262", Description="nice courtyard with a playground, near the metro station Sayran TC ADC, grammar school 140 school 65 children nursery 90, lake and Sayran", PublishedDate=DateTime.Parse("2016-05-20"), Views=0 },
                new Flat { UserID=2, RegionID=8, BuildTime=1984, FloorAll=8, Floor=7, Area=38, State="Good", Address="Gogolya-Panfilova", Room=1, Price=55000, Type="stone", Image=@"\Images\1-full (4).jpg",PhoneNumb="87758420262", Description="warm, bright apartment, elite area, developed infrastructure.", PublishedDate=DateTime.Parse("2016-05-20"), Views=0 },
                new Flat {UserID=3, RegionID=9, BuildTime=1979, FloorAll=9, Floor=5, Area=37, State="Good", Address="Lenina-Tolebi", Room=1, Price=57000, Type="panel", Image=@"\Images\9507660035016812_e4337c11.jpg",PhoneNumb="87758420263", Description="investment! PVC windows, not corner, advanced, built-in kitchen, counters, quiet courtyard, air conditioning!", PublishedDate=DateTime.Parse("2016-05-20"), Views=0 },
                new Flat {UserID=3, RegionID=10, BuildTime=1978, FloorAll=5, Floor=3, Area=33, State="Good", Address="Gogolya- Auezova", Room=1, Price=50000, Type="panel", Image=@"\Images\чатем-6.jpg",PhoneNumb="87758420263", Description="1 bedroom apartment, with fresh good repair. Balcony closed with plastic. Shower. not angular. Do not pledged.", PublishedDate=DateTime.Parse("2016-05-19"), Views=0 },
                new Flat {UserID=3, RegionID=11, BuildTime=1996, FloorAll=5, Floor=3, Area=42, State="Good", Address="Tolebi-Baizakova", Room=1, Price=53000, Type="panel", Image=@"\Images\c0b1994d04a460c2_7735-w746-h559-b0-p0-rustic-living-room-min.jpg",PhoneNumb="87758420263", Description="The windows offer beautiful views of the city and mountains. Bargaining is pertinent within reasonable limits. ", PublishedDate=DateTime.Parse("2016-05-19"), Views=0 },
                new Flat {UserID=3, RegionID=12, BuildTime=2012, FloorAll=22, Floor=9, Area=40, State="New", Address="Gogolya- Auezova", Room=1, Price=57000, Type="ohm monolithic", Image=@"\Images\912789.jpg",PhoneNumb="87758420263", Description="plastic windows, Unangular, improved, built-in kitchen, new plumbing, counters, quiet courtyard.", PublishedDate=DateTime.Parse("2016-05-18"), Views=0 },
                new Flat {UserID=4, RegionID=13, BuildTime=2007, FloorAll=20, Floor=13, Area=47, State="New", Address="Bogenbai batyra-Panfilova", Room=1, Price=62000, Type="monolithic", Image=@"\Images\ef66852baee6045a448651414cd.jpg",PhoneNumb="87758420264", Description="The apartment is in very good condition. Renovation. The apartment is a studio layout. Very good option for a young family that wants to live in a modern residential complex", PublishedDate=DateTime.Parse("2016-05-17"), Views=0 },
                new Flat {UserID=4, RegionID=14, BuildTime=2016, FloorAll=17, Floor=7, Area=55, State="Good", Address="Tolebi-Gaidara", Room=1, Price=50000, Type="brick", Image=@"\Images\fee2427735f718539d82c160196c98e9.jpg",PhoneNumb="87758420264", Description="Royal Expo Apartments - is a unique project of the complex of new technologies and the urban environment ", PublishedDate=DateTime.Parse("2016-05-17"), Views=0 },
                new Flat {UserID=4, RegionID=15, BuildTime=2016, FloorAll=6, Floor=3, Area=62, State="Good", Address="Gogolya- Auezova", Room=2, Price=65000, Type="panel", Image=@"\Images\images.jpg",PhoneNumb="87758420264", Description="plastic windows, Unangular, improved, separate rooms, new plumbing, counters, quiet courtyard", PublishedDate=DateTime.Parse("2016-05-16"), Views=0 },
                new Flat {UserID=1, RegionID=3, BuildTime=2016, FloorAll=14, Floor=2, Area=67, State="Good", Address="KabanbaiBatyr 48 - Janibek", Room=2, Price=75000, Type="monolithic", Image=@"\Images\photo_36646.jpg",PhoneNumb="87758420261", Description="Residential complex Four Seasons is located in the square of streets KabanbaiBatyr - Janibek and Kerey khans-Sauran. ", PublishedDate=DateTime.Parse("2016-05-15"), Views=0 },
                new Flat {UserID=1, RegionID=2, BuildTime=2014, FloorAll=18, Floor=8, Area=59, State="New", Address="Hussein bin Talal - Orynbor ", Room=2, Price=80000, Type="monolithic", Image=@"\Images\upload-008-pic700-700x467-76069.jpg",PhoneNumb="87758420223", Description="plastic windows, separate rooms, kitchen, quiet courtyard ", PublishedDate=DateTime.Parse("2016-05-14"), Views=0 },
                new Flat {UserID=5, RegionID=1, BuildTime=2014, FloorAll=9, Floor=4, Area=69, State="New", Address="Hussein bin Talal - Kazybekbi ", Room=2, Price=78000, Type="monolithic", Image=@"\Images\zam1.jpg",PhoneNumb="87758420265", Description="area of dendropark, the keys on your hands, you can make repairs, the developer BI-CROUP.  ", PublishedDate=DateTime.Parse("2016-05-14"), Views=0 },
                new Flat {UserID=5, RegionID=1, BuildTime=2014, FloorAll=18, Floor=8, Area=69, State="New", Address="Esil district ", Room=2, Price=76000, Type="monolithic", Image=@"\Images\резиденция_в_Beverly_Hills.jpg",PhoneNumb="87758420265", Description="area of EXPO-2017, the keys on your hands, you can make repairs, the developer BI-CROUP.  ", PublishedDate=DateTime.Parse("2016-05-13"), Views=0 },
                new Flat {UserID=5, RegionID=2, BuildTime=2006, FloorAll=9, Floor=8, Area=74, State="Good", Address="Kabanbai Batyr-Orynbor", Room=2, Price=76000, Type="monolithic", Image=@"\Images\скачанные файлы.jpg",PhoneNumb="87758420265", Description="Excellent location, the center of the left bank, 5 minutes.walk to the shopping center Keruen.  ", PublishedDate=DateTime.Parse("2016-05-10"), Views=0 },
            };
            flats.ForEach(a => context.Flats.Add(a));
            context.SaveChanges();
        }
    }
}