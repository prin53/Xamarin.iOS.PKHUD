using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    /// <summary>
    /// Provides an animated error (cross) view.
    /// </summary>
    public class ErrorView : SquareBaseView, IAnimation
    {
        private CAShapeLayer? _dashOneLayer;
        private CAShapeLayer? _dashTwoLayer;

        public override UIImage? Image
        {
            get => null;
            // ReSharper disable once ValueParameterNotUsed
            set => base.Image = null;
        }

        private static Func<CAShapeLayer> DashLayerFactory => () =>
        {
            var path = new UIBezierPath();
            path.MoveTo(new CGPoint(0, 44));
            path.AddLineTo(new CGPoint(88, 44));

            return new CAShapeLayer
            {
                Frame = new CGRect(CGPoint.Empty, new CGSize(88, 88)),
                Path = path.CGPath,
                LineCap = CAShapeLayer.CapRound,
                LineJoin = CAShapeLayer.JoinRound,
                FillColor = null,
                StrokeColor = UIColor.FromRGB(.15f, .15f, .15f).CGColor,
                LineWidth = 6,
                FillMode = CAFillMode.Forwards
            };
        };

        private static Func<float, CABasicAnimation> RotationAnimationFactory => angle =>
        {
            CABasicAnimation animation;
            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                var springAnimation = (CASpringAnimation) CASpringAnimation.FromKeyPath("transform.rotation.z");
                {
                    springAnimation.Damping = 1.5f;
                    springAnimation.Mass = .22f;
                    springAnimation.InitialVelocity = .5f;
                }
                animation = springAnimation;
            }
            else
            {
                animation = CABasicAnimation.FromKeyPath("transform.rotation.z");
            }

            animation.From = new NSNumber(0);
            animation.To = new NSNumber(angle * (float) (Math.PI / 180));
            animation.Duration = 1;
            animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);

            return animation;
        };

        public ErrorView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public ErrorView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public ErrorView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public ErrorView()
        {
            Initialize();
        }

        private void Initialize()
        {
            _dashOneLayer = DashLayerFactory();
            _dashTwoLayer = DashLayerFactory();

            Layer.AddSublayer(_dashOneLayer);
            Layer.AddSublayer(_dashTwoLayer);

            _dashOneLayer.Position = Layer.Position;
            _dashTwoLayer.Position = Layer.Position;
        }

        public void StartAnimation()
        {
            if (_dashOneLayer == null || _dashTwoLayer == null)
            {
                return;
            }

            _dashOneLayer.Transform = CATransform3D.MakeRotation(-45 * (float) (Math.PI / 180), 0, 0, 1);
            _dashTwoLayer.Transform = CATransform3D.MakeRotation(45 * (float) (Math.PI / 180), 0, 0, 1);

            _dashOneLayer.AddAnimation(RotationAnimationFactory(-45), "dashOneAnimation");
            _dashTwoLayer.AddAnimation(RotationAnimationFactory(45), "dashTwoAnimation");
        }

        public void StopAnimation()
        {
            if (_dashOneLayer == null || _dashTwoLayer == null)
            {
                return;
            }

            _dashOneLayer.RemoveAnimation("dashOneAnimation");
            _dashTwoLayer.RemoveAnimation("dashTwoAnimation");
        }
    }
}
