//
// ContainerView.cs
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
using Foundation;
using UIKit;

namespace PKHUD
{
    internal class ContainerView : UIView
    {
        public FrameView FrameView { get; private set; }

        public UIView BackgroundView { get; private set; }

        public ContainerView(FrameView frameView = null)
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

            FrameView.Center = Center;

            BackgroundView.Frame = Bounds;
        }

        protected void Initialize(FrameView frameView = null)
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

            FrameView.Center = Center;
            FrameView.Alpha = 1;

            Hidden = false;
        }

        public void HideFrameView(bool animated, Action completion)
        {
            if (Hidden)
            {
                return;
            }

            if (animated)
            {
                AnimateNotify(.8f, () =>
                {
                    FrameView.Alpha = 0;
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
                FrameView.Alpha = 0;
                completion?.Invoke();
            }
        }

        public void ShowBackgroundView(bool animated = false)
        {
            if (animated)
            {
                Animate(.175f, () => { BackgroundView.Alpha = 1; });
            }
            else
            {
                BackgroundView.Alpha = 1;
            }
        }

        public void HideBackgroundView(bool animated = false)
        {
            if (animated)
            {
                Animate(.65f, () => { BackgroundView.Alpha = 0; });
            }
            else
            {
                BackgroundView.Alpha = 0;
            }
        }
    }
}
