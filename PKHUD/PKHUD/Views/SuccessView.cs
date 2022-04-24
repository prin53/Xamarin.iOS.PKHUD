using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    /// <summary>
    /// Provides an animated success (checkmark) view.
    /// </summary>
    public class SuccessView : SquareBaseView, IAnimation
    {
        private CAShapeLayer? _checkmarkShapeLayer;

        public SuccessView()
        {
            Initialize();
        }

        public SuccessView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public SuccessView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        private void Initialize()
        {
            var checkmarkPath = new UIBezierPath();
            checkmarkPath.MoveTo(new CGPoint(4, 27));
            checkmarkPath.AddLineTo(new CGPoint(34, 56));
            checkmarkPath.AddLineTo(new CGPoint(88, 0));

            _checkmarkShapeLayer = new CAShapeLayer
            {
                Frame = new CGRect(3, 3, 88, 56),
                Path = checkmarkPath.CGPath,
                FillMode = CAFillMode.Forwards,
                LineCap = CAShapeLayer.CapRound,
                LineJoin = CAShapeLayer.JoinRound,
                FillColor = null,
                StrokeColor = new UIColor(.15f, .15f, .15f, 1).CGColor,
                LineWidth = 6
            };

            Layer.AddSublayer(_checkmarkShapeLayer);

            _checkmarkShapeLayer.Position = Layer.Position;
        }

        public void StartAnimation()
        {
            var checkmarkStrokeAnimation = CAKeyFrameAnimation.FromKeyPath("strokeEnd");
            checkmarkStrokeAnimation.Values = new NSObject[] {new NSNumber(0), new NSNumber(1)};
            checkmarkStrokeAnimation.KeyTimes = new[] {new NSNumber(0), new NSNumber(1)};
            checkmarkStrokeAnimation.Duration = .35f;

            _checkmarkShapeLayer?.AddAnimation(checkmarkStrokeAnimation, "checkmarkStrokeAnimation");
        }

        public void StopAnimation()
        {
            _checkmarkShapeLayer?.RemoveAnimation("checkmarkStrokeAnimation");
        }
    }
}
