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
    public class Hud : NSObject
    {
        private static Hud _currentHud;

        private readonly NSObject _willEnterForegroundNotificationObserver;
        private readonly ContainerView _container;

        private NSTimer _currentGraceTimer;
        private NSTimer _timer;

        public UIView ViewToPresentOn { get; internal set; }

        public TimeSpan GracePeriod { get; internal set; }

        public bool DimsBackground { get; internal set; }

        public UIView ContentView
        {
            get => _container.FrameView.Content;
            internal set
            {
                _container.FrameView.Content = value;

                TryStartAnimatingContentView();
            }
        }

        public UIVisualEffect Effect
        {
            get => _container.FrameView.Effect;
            internal set => _container.FrameView.Effect = value;
        }

        public bool UserInteractionOnUnderlyingViewsEnabled
        {
            get => !_container.UserInteractionEnabled;
            internal set => _container.UserInteractionEnabled = !value;
        }

        public bool IsVisible => !_container.Hidden;

        internal Hud()
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
                AccessibilityIdentifier = nameof(Hud)
            };
        }

        private void TryStartAnimatingContentView()
        {
            (ContentView as IAnimation)?.StartAnimation();
        }

        private void TryStopAnimatingContentView()
        {
            (ContentView as IAnimation)?.StopAnimation();
        }

        private void WillEnterForeground(NSNotification notification)
        {
            TryStartAnimatingContentView();
        }

        private void HandleGraceTime(NSTimer _)
        {
            if (_currentGraceTimer == null || !_currentGraceTimer.IsValid)
            {
                return;
            }

            ShowContent();
        }

        private void ShowContent()
        {
            _currentGraceTimer?.Invalidate();

            _container.ShowFrameView();

            TryStartAnimatingContentView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_willEnterForegroundNotificationObserver);
            }

            base.Dispose(disposing);
        }

        public void Show()
        {
            _currentHud?.Hide();
            _currentHud = this;

            var view = ViewToPresentOn ?? UIApplication.SharedApplication.KeyWindow;

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

            if (GracePeriod > TimeSpan.Zero)
            {
                var timer = NSTimer.CreateTimer(GracePeriod, HandleGraceTime);

                NSRunLoop.Current.AddTimer(timer, NSRunLoopMode.Common);

                _currentGraceTimer = timer;
            }
            else
            {
                ShowContent();
            }
        }

        public void Flash(TimeSpan duration)
        {
            Show();
            HideWithDelay(duration);
        }
        
        public void Hide(bool animated = true, Action completion = default(Action))
        {
            _currentGraceTimer?.Invalidate();

            _container.HideFrameView(animated, completion);

            TryStopAnimatingContentView();
        }

        public void HideWithDelay(TimeSpan delay, bool animated = true, Action completion = default(Action))
        {
            _currentGraceTimer?.Invalidate();

            _timer = NSTimer.CreateScheduledTimer(delay, _ =>
            {
                Hide(animated, completion);
                _timer.Invalidate();
            });
        }

        public static ContentBuilder Create()
        {
            return new ContentBuilder();
        }
    }
}
