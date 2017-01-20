//
// ErrorView.cs
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

namespace PKHUD
{
	/// <summary>
	/// Provides an animated error (cross) view.
	/// </summary>
	public class ErrorView : SquareBaseView, IAnimatable
	{
		protected CAShapeLayer DashOneLayer { get; private set; }

		protected CAShapeLayer DashTwoLayer { get; private set; }

		protected Func<CAShapeLayer> DashLayerFactory => () =>
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

		protected Func<float, CABasicAnimation> RotationAnimationFactory => (angle) =>
		{
			CABasicAnimation animation;
			if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
			{
				var springAnimation = (CASpringAnimation)CASpringAnimation.FromKeyPath("transform.rotation.z");
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
			animation.To = new NSNumber(angle * (float)(Math.PI / 180));
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
			Initialize();
		}

		public ErrorView(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		public ErrorView(string title = default(string), string subtitle = default(string)) : base(null, title, subtitle)
		{
			Initialize();
		}

		protected void Initialize()
		{
			DashOneLayer = DashLayerFactory();
			DashTwoLayer = DashLayerFactory();

			Layer.AddSublayer(DashOneLayer);
			Layer.AddSublayer(DashTwoLayer);

			DashOneLayer.Position = Layer.Position;
			DashTwoLayer.Position = Layer.Position;
		}

		public void StartAnimation()
		{
			DashOneLayer.Transform = CATransform3D.MakeRotation(-45 * (float)(Math.PI / 180), 0, 0, 1);
			DashTwoLayer.Transform = CATransform3D.MakeRotation(45 * (float)(Math.PI / 180), 0, 0, 1);

			DashOneLayer.AddAnimation(RotationAnimationFactory(-45), "dashOneAnimation");
			DashTwoLayer.AddAnimation(RotationAnimationFactory(45), "dashTwoAnimation");
		}

		public void StopAnimation()
		{
			DashOneLayer.RemoveAnimation("dashOneAnimation");
			DashTwoLayer.RemoveAnimation("dashTwoAnimation");
		}
	}
}
