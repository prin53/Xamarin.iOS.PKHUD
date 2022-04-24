using Foundation;
using UIKit;

namespace PKHUD.Sample
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; } = null!;

        private static void Main(string[] args)
        {
            UIApplication.Main(args, null, typeof(AppDelegate));
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds)
            {
                RootViewController = new ViewController()
            };

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
