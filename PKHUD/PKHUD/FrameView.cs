using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD
{
    internal class FrameView : UIVisualEffectView
    {
        private const int MotionOffset = 20;

        private UIView? _content;

        public UIView? Content
        {
            get => _content;
            set
            {
                _content?.RemoveFromSuperview();

                _content = value;

                if (_content == null)
                {
                    return;
                }

                _content.Alpha = .85f;
                _content.ClipsToBounds = true;
                _content.ContentMode = UIViewContentMode.Center;

                Frame = new CGRect(Frame.Location, _content.Bounds.Size);

                ContentView.AddSubview(_content);
            }
        }

        public FrameView(NSObjectFlag t) : base(t)
        {
            /* Required constructor */
        }

        public FrameView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public FrameView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public FrameView() : base(UIBlurEffect.FromStyle(UIBlurEffectStyle.Light))
        {
            Initialize();
        }

        private void Initialize()
        {
            Content = new UIView();

            BackgroundColor = UIColor.FromWhiteAlpha(.8f, .36f);

            Layer.CornerRadius = 9;
            Layer.MasksToBounds = true;

            ContentView.AddSubview(Content);

            AddMotionEffect(new UIMotionEffectGroup
            {
                MotionEffects = new UIMotionEffect[]
                {
                    new UIInterpolatingMotionEffect("center.x", UIInterpolatingMotionEffectType.TiltAlongHorizontalAxis)
                    {
                        MaximumRelativeValue = new NSNumber(MotionOffset),
                        MinimumRelativeValue = new NSNumber(-MotionOffset)
                    },
                    new UIInterpolatingMotionEffect("center.y", UIInterpolatingMotionEffectType.TiltAlongVerticalAxis)
                    {
                        MaximumRelativeValue = new NSNumber(MotionOffset),
                        MinimumRelativeValue = new NSNumber(-MotionOffset)
                    }
                }
            });
        }
    }
}
