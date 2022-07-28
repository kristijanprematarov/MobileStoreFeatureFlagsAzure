using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using MobileStoreFeatureFlags.Features;
using MobileStoreFeatureFlags.Models;
using MobileStoreFeatureFlags.Services;

namespace MobileStoreFeatureFlags.Controllers
{
    //[FeatureGate(nameof(FeatureFlags.MobileReview))]
    public class MobilesController : Controller
    {
        private readonly IMobileDataService _mobileDataService;
        private readonly IFeatureManager _featureManager;

        public MobilesController(IMobileDataService mobileDataService,
            IFeatureManager featureManager)
        {
            _mobileDataService = mobileDataService;
            _featureManager = featureManager;
        }

        public async Task<IActionResult> Index()
        {
            //THIS FEATURE FLAG IS ADDED IN FEATURE MANAGER in AZURE APP CONFIG as MobileDetailedReview
            if (await _featureManager.IsEnabledAsync(nameof(FeatureFlags.MobileDetailedReview)))
            {
                ViewBag.IsEnabled = "Viewing Mobile Details is enabled";
            }
            else
            {
                ViewBag.IsEnabled = "Viewing Mobile Details is not enabled";
            }

            List<Mobile> mobiles = _mobileDataService.GetAllMobiles();
            return View(mobiles);
        }

        [FeatureGate(nameof(FeatureFlags.MobileDetailedReview))]
        public IActionResult ReviewDetails(string? mobileId)
        {
            Mobile mobile = _mobileDataService.GetAllMobiles()
                .Find(p => p.Id.Equals(mobileId));

            return View(mobile);
        }
    }
}
