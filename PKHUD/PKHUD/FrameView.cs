//
// FrameView.cs
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
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD
{
	internal class FrameView : UIVisualEffectView
	{
		private const int _motionOffset = 20;

		private UIView _content;

		public UIView Content
		{
			get
			{
				return _content;
			}
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
			Initialize();
		}

		public FrameView(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public FrameView(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		public FrameView() : base(UIBlurEffect.FromStyle(UIBlurEffectStyle.Light))
		{
			Initialize();
		}

		protected void Initialize()
		{
			Content = new UIView();

			BackgroundColor = UIColor.FromWhiteAlpha(.8f, .36f);

			Layer.CornerRadius = 9;
			Layer.MasksToBounds = true;

			ContentView.AddSubview(Content);

			AddMotionEffect(new UIMotionEffectGroup
			{
				MotionEffects = new[]
				{
					new UIInterpolatingMotionEffect("center.x", UIInterpolatingMotionEffectType.TiltAlongHorizontalAxis)
					{
						MaximumRelativeValue = new NSNumber(_motionOffset),
						MinimumRelativeValue = new NSNumber(-_motionOffset)
					},
					new UIInterpolatingMotionEffect("center.y", UIInterpolatingMotionEffectType.TiltAlongVerticalAxis)
					{
						MaximumRelativeValue = new NSNumber(_motionOffset),
						MinimumRelativeValue = new NSNumber(-_motionOffset)
					}
				}
			});
		}
	}
}
