//
// SquareBaseView.cs
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

namespace PKHUD.Views
{
    /// <summary>
    /// Provides a square view, which can be subclassed with additional views.
    /// </summary>
    public class SquareBaseView : UIView
    {
        private static readonly CGRect DefaultSquareBaseViewFrame;

        private string _title;
        private string _subtitle;
        private UIImage _image;

        public virtual string Title
        {
            get => _title;
            set
            {
                _title = value;

                if (TitleLabel != null)
                {
                    TitleLabel.Text = _title;
                }
            }
        }

        public virtual string Subtitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;

                if (SubtitleLabel != null)
                {
                    SubtitleLabel.Text = _subtitle;
                }
            }
        }

        public virtual UIImage Image
        {
            get => _image;
            set
            {
                _image = value;

                if (ImageView != null)
                {
                    ImageView.Image = _image;
                }
            }
        }

        static SquareBaseView()
        {
            DefaultSquareBaseViewFrame = new CGRect(CGPoint.Empty, new CGSize(156, 156));
        }

        protected virtual Func<UIImageView> ImageViewFactory => () => new UIImageView
        {
            Alpha = .85f,
            ClipsToBounds = true,
            ContentMode = UIViewContentMode.ScaleAspectFit
        };

        protected virtual Func<UILabel> TitleLabelFactory => () => new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.BoldSystemFontOfSize(17),
            TextColor = UIColor.Black.ColorWithAlpha(.85f),
            AdjustsFontSizeToFitWidth = true,
            MinimumScaleFactor = .25f
        };

        protected virtual Func<UILabel> SubtitleLabelFactory => () => new UILabel
        {
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.SystemFontOfSize(14),
            TextColor = UIColor.Black.ColorWithAlpha(.7f),
            AdjustsFontSizeToFitWidth = true,
            Lines = 2,
            MinimumScaleFactor = .25f
        };

        protected UIImageView ImageView { get; private set; }

        protected UILabel TitleLabel { get; private set; }

        protected UILabel SubtitleLabel { get; private set; }

        public SquareBaseView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public SquareBaseView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public SquareBaseView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public SquareBaseView() : base(DefaultSquareBaseViewFrame)
        {
            Initialize();
        }

        private void Initialize()
        {
            ImageView = ImageViewFactory();

            if (ImageView == null)
            {
                throw new InvalidOperationException($"{nameof(ImageViewFactory)} must produce a valid view");
            }

            TitleLabel = TitleLabelFactory();

            if (TitleLabel == null)
            {
                throw new InvalidOperationException($"{nameof(TitleLabelFactory)} must produce a valid view");
            }

            SubtitleLabel = SubtitleLabelFactory();

            if (SubtitleLabel == null)
            {
                throw new InvalidOperationException($"{nameof(SubtitleLabelFactory)} must produce a valid view");
            }

            ImageView.Image = Image;
            TitleLabel.Text = Title;
            SubtitleLabel.Text = Subtitle;

            AddSubviews(ImageView, TitleLabel, SubtitleLabel);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var viewWidth = Bounds.Size.Width;
            var viewHeight = Bounds.Size.Height;
            var halfHeight = viewHeight / 2;
            var quarterHeight = viewHeight / 4;
            var threeQuarterHeight = viewHeight / 4 * 3;

            TitleLabel.Frame = new CGRect(CGPoint.Empty, new CGSize(viewWidth, quarterHeight));
            ImageView.Frame = new CGRect(new CGPoint(0, quarterHeight), new CGSize(viewWidth, halfHeight));
            SubtitleLabel.Frame = new CGRect(new CGPoint(0, threeQuarterHeight), new CGSize(viewWidth, quarterHeight));
        }
    }
}
