using HiddenSetting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace HiddenSetting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TwilioSettings _twilioSettings;
        private SocialLoginSettings _socialLoginSettings;
        private readonly IConfiguration configuration;
        public HomeController(ILogger<HomeController> logger, IOptions <TwilioSettings> twilioSettings,IOptions<SocialLoginSettings> socialSettings,IConfiguration configuration)
        {
            _logger = logger;
            _twilioSettings = twilioSettings.Value;
            _socialLoginSettings = socialSettings.Value;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.TwilioAuthToken = _twilioSettings.AuthToken;
            ViewBag.TwilioAccountSid = _twilioSettings.AccountSid;
            ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;

            ViewBag.FacebookKey = _socialLoginSettings.FacebookSettings.Key;
            ViewBag.GoogleKey = _socialLoginSettings.GoogleSettings.Key;

            ViewBag.ThirdLevelSettingValue = configuration.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
                                         .GetSection("BottomLevelSetting").Value;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
