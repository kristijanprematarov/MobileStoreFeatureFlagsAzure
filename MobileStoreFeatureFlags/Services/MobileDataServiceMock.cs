using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileStoreFeatureFlags.Models;

namespace MobileStoreFeatureFlags.Services
{
    public class MobileDataServiceMock : IMobileDataService
    {
        public List<Mobile> GetAllMobiles()
        {
            List<Mobile> mobiles = new();

            mobiles.Add(PrepareMobileObject("1", "iPhone 12", "6.1 inch Display Screen", "2 Star", "Old Model - Current Model is 13"));
            mobiles.Add(PrepareMobileObject("2", "iPhone 13 Mini", "5.4 inch Display Screen", "3 Star", "Small Display - Bigger Display Available"));
            mobiles.Add(PrepareMobileObject("3", "iPhone 13", "6.1 inch Display Screen", "3 Star", "Decent Diaplay Size but other models with better camera options available"));
            mobiles.Add(PrepareMobileObject("4", "iPhone 13 Pro", "6.1 inch Display Screen", "4 Star", "Good Display Size and best camera but other models with bigger display available"));
            mobiles.Add(PrepareMobileObject("5", "iPhone 13 Pro Max", "6.7 inch Display Screen", "5 Star",
                "Good display size and best camera"));

            return mobiles;
        }

        private Mobile PrepareMobileObject(string Id, string Name, string Specification, string Rating, string Remarks)
        {
            return new Mobile
            {
                Id = Id,
                Name = Name,
                Specification = Specification,
                MobileReview = new MobileReview
                {
                    Rating = Rating,
                    Remarks = Remarks
                }
            };
        }
    }
}
