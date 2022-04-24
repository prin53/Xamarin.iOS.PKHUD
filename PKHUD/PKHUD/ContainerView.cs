using System;
using Foundation;
using UIKit;

namespace PKHUD
{
    internal class ContainerView : UIView
    {
        public FrameView? FrameView { get; private set; }

        public UIView? BackgroundView { get; private set; }

        public ContainerView(FrameView? frameView = default)
        {
            Initialize(frameView);
        }

        public ContainerView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public ContainerView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (FrameView != null)
            {
                FrameView!.Center = Center;
            }

            if (BackgroundView != null)
            {
                BackgroundView.Frame = Bounds;
            }
        }

        protected void Initialize(FrameView? frameView = default)
        {
            FrameView = frameView ?? new FrameView();

            BackgroundView = new UIView
            {
                BackgroundColor = new UIColor(0, .25f),
                Alpha = 0
            };

            BackgroundColor = UIColor.Clear;

            AddSubviews(BackgroundView, FrameView);
        }

        public void ShowFrameView()
        {
            Layer.RemoveAllAnimations();

            if (FrameView != null)
            {
                FrameView.Center = Center;
                FrameView.Alpha = 1;
            }

            Hidden = false;
        }

        public void HideFrameView(bool animated, Action? completion)
        {
            if (Hidden)
            {
                return;
            }

            if (animated)
            {
                AnimateNotify(.8f, () =>
                {
                    if (FrameView != null)
                    {
                        FrameView.Alpha = 0;
                    }

                    HideBackgroundView();
                }, _ =>
                {
                    Hidden = true;

                    RemoveFromSuperview();

                    completion?.Invoke();
                });
            }
            else
            {
                if (FrameView != null)
                {
                    FrameView.Alpha = 0;
                }

                completion?.Invoke();
            }
        }

        public void ShowBackgroundView(bool animated = false)
        {
            if (animated)
            {
                Animate(
                    .175f,
                    () =>
                    {
                        if (BackgroundView != null)
                        {
                            BackgroundView.Alpha = 1;
                        }
                    });
            }
            else
            {
                if (BackgroundView != null)
                {
                    BackgroundView.Alpha = 1;
                }
            }
        }

        public void HideBackgroundView(bool animated = false)
        {
            if (animated)
            {
                Animate(
                    .65f,
                    () =>
                    {
                        if (BackgroundView != null)
                        {
                            BackgroundView.Alpha = 0;
                        }
                    });
            }
            else
            {
                if (BackgroundView != null)
                {
                    BackgroundView.Alpha = 0;
                }
            }
        }
    }
}
