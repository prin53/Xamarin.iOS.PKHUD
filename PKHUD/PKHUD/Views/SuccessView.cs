//
// SuccessView.cs
//
// Author:
//       Denys Fiediaiev <prineduard@gmail.com>
//
// Copyright (c) 2017 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
        private CAShapeLayer _checkmarkShapeLayer;

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

            _checkmarkShapeLayer.AddAnimation(checkmarkStrokeAnimation, "checkmarkStrokeAnimation");
        }

        public void StopAnimation()
        {
            _checkmarkShapeLayer.RemoveAnimation("checkmarkStrokeAnimation");
        }
    }
}
