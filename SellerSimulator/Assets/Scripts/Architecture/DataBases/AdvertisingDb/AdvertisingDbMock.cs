using Assets.Scripts.Architecture.DataBases.AdvertisingDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    public static class AdvertisingDbMock
    {
        public static List<ModelAdvertising> ListAds { get; set; } = new List<ModelAdvertising>()
        {
            new ModelAdvertising()
            {
                id = 1,
                nameAds = "Реклама в соц. сетях",
                imageName = "iconForАdvertising",
                description = "Ликвидность: +10%",
                priceWatchAds = true,
                buffLiquidity = 1.1
            },
            new ModelAdvertising()
            {
                id = 2,
                nameAds = "Нативная реклама",
                imageName = "iconForАdvertising",
                description = "Ликвидность: +20%",
                priceWatchAds = false,
                goldenPrice = 10,
                buffLiquidity = 1.20
            },
            new ModelAdvertising()
            {
                id = 3,
                nameAds = "Мобильная реклама",
                imageName = "iconForАdvertising",
                description = "Ликвидность: +30%",
                priceWatchAds = false,
                goldenPrice = 20,
                buffLiquidity = 1.30
            },
        };



    }
}
