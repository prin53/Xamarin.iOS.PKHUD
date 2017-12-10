//
// SystemActivityIndicatorView.cs
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
    /// <summary>
    /// Provides the system UIActivityIndicatorView as an alternative.
    /// </summary>
    public class SystemActivityIndicatorView : WideBaseView, IAnimatable
    {
        protected UIActivityIndicatorView ActivityIndicatorView { get; private set; }

        protected virtual Func<UIActivityIndicatorView> ActivityIndicatorViewFactory => ()
            => new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
            {
                Color = UIColor.Black
            };

        public SystemActivityIndicatorView()
        {
            Initialize();
        }

        public SystemActivityIndicatorView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public SystemActivityIndicatorView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public SystemActivityIndicatorView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (ActivityIndicatorView == null)
            {
                return;
            }

            ActivityIndicatorView.Center = Center;
        }

        public void StartAnimation()
        {
            ActivityIndicatorView?.StartAnimating();
        }

        public void StopAnimation()
        {
            /* Nothing to do */
        }

        protected void Initialize()
        {
            BackgroundColor = UIColor.Clear;

            Alpha = .8f;

            ActivityIndicatorView = ActivityIndicatorViewFactory();

            if (ActivityIndicatorView == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(ActivityIndicatorViewFactory)} must produce a valid view");
            }

            AddSubview(ActivityIndicatorView);
        }
    }
}
