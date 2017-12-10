//
// TextView.cs
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
    public class TextView : WideBaseView
    {
        private const int Padding = 10;

        protected UILabel TitleLabel { get; private set; }

        protected virtual Func<UILabel> TitleLabelFactory => () => new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.BoldSystemFontOfSize(17),
            TextColor = UIColor.Black.ColorWithAlpha(.85f),
            AdjustsFontSizeToFitWidth = true,
            Lines = 3
        };

        public TextView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public TextView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public TextView(string text)
        {
            Initialize(text);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (TitleLabel == null)
            {
                return;
            }

            TitleLabel.Frame = Bounds.Inset(Padding, Padding);
        }

        protected void Initialize(string text = default(string))
        {
            TitleLabel = TitleLabelFactory();

            if (TitleLabel == null)
            {
                throw new InvalidOperationException($"{nameof(TitleLabelFactory)} must produce a valid view");
            }

            TitleLabel.Text = text;

            AddSubview(TitleLabel);
        }
    }
}
