using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace PKHUD.Sample
{
    /// <summary>
    /// Please note that the blow demonstrates the "porcelain" interface
    /// - a more concise and clean way to work with the HUD.
    /// If you need more options and flexibility, 
    /// feel free to use the underlying "plumbing". E.g.:
    /// PKHUD.Instance.Show();
    /// PKHUD.Instance.ContentView = new SuccessView("Success!");
    /// PKHUD.Instance.Hide(TimeSpan.FromSeconds(2));
    /// </summary>
    public class ViewController : UIViewController
    {
        private const int Padding = 16;
        private const int ButtonHeight = 44;

        private UIImageView _backgroundImage = null!;
        private UIStackView _buttonsStack = null!;
        private UIButton _animatedSuccessButton = null!;
        private UIButton _animatedErrorButton = null!;
        private UIButton _animatedProgressButton = null!;
        private UIButton _graceAnimatedProgressButton = null!;
        private UIButton _animatedStatusProgressButton = null!;
        private UIButton _textButton = null!;

        private static UIButton CreateButton(string title)
        {
            var button = UIButton.FromType(UIButtonType.System);
            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.SetTitleColor(UIColor.FromRGB(64, 64, 64), UIControlState.Normal);
            button.SetTitleShadowColor(UIColor.FromRGB(128, 128, 128), UIControlState.Normal);
            button.SetTitle(title, UIControlState.Normal);
            button.BackgroundColor = UIColor.FromRGB(255, 255, 255).ColorWithAlpha(.66f);
            return button;
        }

        private static void AnimatedSuccessButtonOnTouchUpInside(object sender, EventArgs e)
        {
            Hud.Create()
                .WithSuccessContent()
                .Build()
                .Flash(TimeSpan.FromSeconds(2));
        }

        private static void AnimatedErrorButtonOnTouchUpInside(object sender, EventArgs e)
        {
            var hud = Hud.Create().WithErrorContent().Build();
            hud.Show();
            hud.HideWithDelay(TimeSpan.FromSeconds(2));
        }

        private static async void AnimatedProgressButtonOnTouchUpInside(object sender, EventArgs e)
        {
            Hud.Create().WithProgressContent()
                .WithBackgroundDimming(true)
                .Build()
                .Show();

            await Task.Delay(TimeSpan.FromSeconds(2));

            Hud.Create().WithSuccessContent()
                .Build()
                .Flash(TimeSpan.FromSeconds(1));
        }

        private static void GraceAnimatedProgressButtonOnTouchUpInside(object sender, EventArgs e)
        {
            Hud.Create().WithProgressContent()
                .WithGracePeriod(TimeSpan.FromSeconds(2))
                .Build()
                .Flash(TimeSpan.FromSeconds(1));
        }

        private static void AnimatedStatusProgressButtonOnTouchUpInside(object sender, EventArgs e)
        {
            Hud.Create().WithProgressContent()
                .WithTitle("Title")
                .WithSubtitle("Subtitle")
                .Build()
                .Flash(TimeSpan.FromSeconds(2));
        }

        private static async void TextButtonOnTouchUpInside(object sender, EventArgs e)
        {
            await Hud.Create().WithLabelContent()
                .WithTitle("Requesting Licence…")
                .Build()
                .FlashAsync(TimeSpan.FromSeconds(2));

            Debug.WriteLine("License Obtained");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animatedSuccessButton.TouchUpInside -= AnimatedSuccessButtonOnTouchUpInside;
                _animatedErrorButton.TouchUpInside -= AnimatedErrorButtonOnTouchUpInside;
                _animatedProgressButton.TouchUpInside -= AnimatedProgressButtonOnTouchUpInside;
                _graceAnimatedProgressButton.TouchUpInside -= GraceAnimatedProgressButtonOnTouchUpInside;
                _animatedStatusProgressButton.TouchUpInside -= AnimatedStatusProgressButtonOnTouchUpInside;
                _textButton.TouchUpInside -= TextButtonOnTouchUpInside;
            }

            base.Dispose(disposing);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View!.BackgroundColor = UIColor.White;

            _backgroundImage = new UIImageView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Image = UIImage.FromBundle("Background"),
                ContentMode = UIViewContentMode.ScaleAspectFill
            };

            _buttonsStack = new UIStackView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Spacing = 8
            };

            _animatedSuccessButton = CreateButton("Animated Success HUD");
            _animatedSuccessButton.TouchUpInside += AnimatedSuccessButtonOnTouchUpInside;

            _animatedErrorButton = CreateButton("Animated Error HUD");
            _animatedErrorButton.TouchUpInside += AnimatedErrorButtonOnTouchUpInside;

            _animatedProgressButton = CreateButton("Animated Progress HUD");
            _animatedProgressButton.TouchUpInside += AnimatedProgressButtonOnTouchUpInside;

            _graceAnimatedProgressButton = CreateButton("Grace Animated Progress HUD");
            _graceAnimatedProgressButton.TouchUpInside += GraceAnimatedProgressButtonOnTouchUpInside;

            _animatedStatusProgressButton = CreateButton("Animated Status Progress HUD");
            _animatedStatusProgressButton.TouchUpInside += AnimatedStatusProgressButtonOnTouchUpInside;

            _textButton = CreateButton("Text HUD");
            _textButton.TouchUpInside += TextButtonOnTouchUpInside;

            _buttonsStack.AddArrangedSubview(_animatedSuccessButton);
            _buttonsStack.AddArrangedSubview(_animatedErrorButton);
            _buttonsStack.AddArrangedSubview(_animatedProgressButton);
            _buttonsStack.AddArrangedSubview(_graceAnimatedProgressButton);
            _buttonsStack.AddArrangedSubview(_animatedStatusProgressButton);
            _buttonsStack.AddArrangedSubview(_textButton);

            View.AddSubviews(_backgroundImage, _buttonsStack);

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                View.AddConstraint(_buttonsStack.LeadingAnchor.ConstraintEqualTo(
                    View.SafeAreaLayoutGuide.LeadingAnchor,
                    Padding
                ));
                View.AddConstraint(_buttonsStack.TrailingAnchor.ConstraintEqualTo(
                    View.SafeAreaLayoutGuide.TrailingAnchor,
                    -Padding
                ));
                View.AddConstraint(_buttonsStack.BottomAnchor.ConstraintEqualTo(
                    View.SafeAreaLayoutGuide.BottomAnchor,
                    -Padding
                ));
            }
            else
            {
                View.AddConstraint(_buttonsStack.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, Padding));
                View.AddConstraint(_buttonsStack.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -Padding));
                View.AddConstraint(_buttonsStack.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -Padding));
            }

            View.AddConstraints(new[]
            {
                _backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor),
                _backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
                _backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                _backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                _animatedSuccessButton.HeightAnchor.ConstraintEqualTo(ButtonHeight),
                _animatedErrorButton.HeightAnchor.ConstraintEqualTo(ButtonHeight),
                _animatedProgressButton.HeightAnchor.ConstraintEqualTo(ButtonHeight),
                _graceAnimatedProgressButton.HeightAnchor.ConstraintEqualTo(ButtonHeight),
                _animatedStatusProgressButton.HeightAnchor.ConstraintEqualTo(ButtonHeight),
                _textButton.HeightAnchor.ConstraintEqualTo(ButtonHeight)
            });
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.AllButUpsideDown;
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
