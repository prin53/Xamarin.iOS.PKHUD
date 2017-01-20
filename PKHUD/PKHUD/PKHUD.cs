//
// PKHUD.cs
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
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD
{
	public class PKHUD : NSObject
	{
		private NSObject _willEnterForegroundNotificationObserver;

		private ContainerView _container;
		private NSTimer _timer;

		public static PKHUD Instance { get; } = new PKHUD();

		public UIView ViewToPresentOn { get; }

		public bool DimsBackground { get; set; }

		public UIView ContentView
		{
			get
			{
				return _container.FrameView.Content;
			}
			set
			{
				_container.FrameView.Content = value;

				StartAnimatingContentView();
			}
		}

		public UIVisualEffect Effect
		{
			get
			{
				return _container.FrameView.Effect;
			}
			set
			{
				_container.FrameView.Effect = value;
			}
		}

		public bool UserInteractionOnUnderlyingViewsEnabled
		{
			get
			{
				return !_container.UserInteractionEnabled;
			}
			set
			{
				_container.UserInteractionEnabled = !value;
			}
		}

		public bool IsVisible => !_container.Hidden;

		public PKHUD()
		{
			_willEnterForegroundNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(
				UIApplication.WillEnterForegroundNotification,
				WillEnterForeground
			);

			_container = new ContainerView();
			_container.FrameView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin
				| UIViewAutoresizing.FlexibleRightMargin
				| UIViewAutoresizing.FlexibleTopMargin
				| UIViewAutoresizing.FlexibleBottomMargin;

			DimsBackground = true;
			UserInteractionOnUnderlyingViewsEnabled = false;
		}

		public PKHUD(UIView view) : this()
		{
			if (view == null)
			{
				throw new ArgumentNullException(nameof(view));
			}

			ViewToPresentOn = view;
		}

		private void WillEnterForeground(NSNotification notification)
		{
			StartAnimatingContentView();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_willEnterForegroundNotificationObserver);
			}

			base.Dispose(disposing);
		}

		public virtual void Show(UIView onView = default(UIView))
		{
			var view = onView ?? ViewToPresentOn ?? UIApplication.SharedApplication.KeyWindow;

			if (!view.Subviews.Contains(_container))
			{
				view.AddSubview(_container);

				_container.Frame = new CGRect(CGPoint.Empty, view.Frame.Size);
				_container.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			}

			_container.ShowFrameView();

			if (DimsBackground)
			{
				_container.ShowBackgroundView(true);
			}

			StartAnimatingContentView();
		}

		public virtual void Hide(bool animated = true, Action<bool> completion = default(Action<bool>))
		{
			_container.HideFrameView(animated, completion);

			StopAnimatingContentView();
		}

		public virtual void Hide(TimeSpan delay, bool animated = true, Action<bool> completion = default(Action<bool>))
		{
			_timer = NSTimer.CreateScheduledTimer(delay, _ =>
			{
				Hide(animated, completion);
				_timer.Invalidate();
			});
		}

		protected virtual void StartAnimatingContentView()
		{
			var animatable = ContentView as IAnimatable;
			if (animatable != null)
			{
				animatable.StartAnimation();
			}
		}

		protected virtual void StopAnimatingContentView()
		{
			var animatable = ContentView as IAnimatable;
			if (animatable != null)
			{
				animatable.StopAnimation();
			}
		}
	}
}
