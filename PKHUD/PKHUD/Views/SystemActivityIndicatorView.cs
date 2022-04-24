using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    /// <summary>
    /// Provides the system UIActivityIndicatorView as an alternative.
    /// </summary>
    public class SystemActivityIndicatorView : WideBaseView, IAnimation
    {
        protected UIActivityIndicatorView? ActivityIndicatorView { get; private set; }

        protected virtual Func<UIActivityIndicatorView> ActivityIndicatorViewFactory => ()
            => new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
            {
                Color = UIColor.Black
            };

        public SystemActivityIndicatorView()
        {
            Initialize();
        }

        public SystemActivityIndicatorView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public SystemActivityIndicatorView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public SystemActivityIndicatorView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        private void Initialize()
        {
            BackgroundColor = UIColor.Clear;

            Alpha = .8f;

            ActivityIndicatorView = ActivityIndicatorViewFactory();

            if (ActivityIndicatorView == null)
            {
                throw new InvalidOperationException($"{nameof(ActivityIndicatorViewFactory)} must produce a valid view");
            }

            AddSubview(ActivityIndicatorView);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (ActivityIndicatorView == null)
            {
                return;
            }

            ActivityIndicatorView.Center = Center;
        }

        public void StartAnimation()
        {
            ActivityIndicatorView?.StartAnimating();
        }

        public void StopAnimation()
        {
            /* Nothing to do */
        }
    }
}
