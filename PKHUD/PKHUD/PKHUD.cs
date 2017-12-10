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
        private readonly NSObject _willEnterForegroundNotificationObserver;
        private readonly ContainerView _container;

        private NSTimer _graceTimer;

        private NSTimer _timer;

        public static PKHUD Instance { get; } = new PKHUD();

        public UIView ViewToPresentOn { get; }

        /// Grace period is the time (in seconds) that the invoked method may be run without
        /// showing the HUD. If the task finishes before the grace time runs out, the HUD will
        /// not be shown at all.
        /// This may be used to prevent HUD display for very short tasks.
        /// Defaults to Zero (no grace time).
        public TimeSpan GracePeriod { get; set; } = TimeSpan.Zero;

        public bool DimsBackground { get; set; }

        public UIView ContentView
        {
            get => _container.FrameView.Content;
            set
            {
                _container.FrameView.Content = value;

                StartAnimatingContentView();
            }
        }

        public UIVisualEffect Effect
        {
            get => _container.FrameView.Effect;
            set => _container.FrameView.Effect = value;
        }

        public bool UserInteractionOnUnderlyingViewsEnabled
        {
            get => !_container.UserInteractionEnabled;
            set => _container.UserInteractionEnabled = !value;
        }

        public bool IsVisible => !_container.Hidden;

        public PKHUD()
        {
            _willEnterForegroundNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(
                UIApplication.WillEnterForegroundNotification,
                WillEnterForeground
            );

            _container = new ContainerView
            {
                FrameView =
                {
                    AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin
                                       | UIViewAutoresizing.FlexibleRightMargin
                                       | UIViewAutoresizing.FlexibleTopMargin
                                       | UIViewAutoresizing.FlexibleBottomMargin
                },
                IsAccessibilityElement = true,
                AccessibilityIdentifier = nameof(PKHUD)
            };

            DimsBackground = true;
            UserInteractionOnUnderlyingViewsEnabled = false;

            _container.FrameView.Content = new ProgressView();
        }

        public PKHUD(UIView view) : this()
        {
            ViewToPresentOn = view ?? throw new ArgumentNullException(nameof(view));
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
                _container.Hidden = true;
            }

            if (DimsBackground)
            {
                _container.ShowBackgroundView(true);
            }

            // If the grace time is set, postpone the HUD display
            if (GracePeriod > TimeSpan.Zero)
            {
                var timer = NSTimer.CreateTimer(GracePeriod, HandleGraceTimer);
                NSRunLoop.Current.AddTimer(timer, NSRunLoopMode.Common);
                _graceTimer = timer;
            }
            else
            {
                ShowContent();
            }
        }

        private void HandleGraceTimer(NSTimer obj)
        {
            // Show the HUD only if the task is still running
            if (_graceTimer != null && _graceTimer.IsValid)
            {
                ShowContent();
            }
        }

        private void ShowContent()
        {
            _graceTimer?.Invalidate();

            _container.ShowFrameView();

            StartAnimatingContentView();
        }

        public virtual void Hide(bool animated = true, Action<bool> completion = default(Action<bool>))
        {
            _graceTimer?.Invalidate();

            _container.HideFrameView(animated, completion);

            StopAnimatingContentView();
        }

        public virtual void Hide(TimeSpan delay, bool animated = true, Action<bool> completion = default(Action<bool>))
        {
            _graceTimer?.Invalidate();

            _timer = NSTimer.CreateScheduledTimer(delay, _ =>
            {
                Hide(animated, completion);
                _timer.Invalidate();
            });
        }

        protected virtual void StartAnimatingContentView()
        {
            (ContentView as IAnimatable)?.StartAnimation();
        }

        protected virtual void StopAnimatingContentView()
        {
            (ContentView as IAnimatable)?.StopAnimation();
        }
    }
}
