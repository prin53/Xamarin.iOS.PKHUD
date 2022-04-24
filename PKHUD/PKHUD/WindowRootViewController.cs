using UIKit;

namespace PKHUD
{
    /// <summary>
    /// Serves as a configuration relay controller, tapping into the main window's rootViewController settings.
    /// </summary>
    internal class WindowRootViewController : UIViewController
    {
        private static UIViewController? RootViewController => UIApplication.SharedApplication.Delegate?.GetWindow()?.RootViewController;
        
        public override UIStatusBarAnimation PreferredStatusBarUpdateAnimation 
            => RootViewController?.PreferredStatusBarUpdateAnimation ?? UIStatusBarAnimation.None;

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return RootViewController?.GetSupportedInterfaceOrientations() ?? UIInterfaceOrientationMask.Portrait;
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return PresentingViewController?.PreferredStatusBarStyle() ?? UIApplication.SharedApplication.StatusBarStyle;
        }

        public override bool PrefersStatusBarHidden()
        {
            return PresentingViewController?.PrefersStatusBarHidden() ?? UIApplication.SharedApplication.StatusBarHidden;
        }

        public override bool ShouldAutorotate()
        {
            return RootViewController?.ShouldAutorotate() ?? false;
        }
    }
}
