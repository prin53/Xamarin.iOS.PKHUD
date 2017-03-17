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

namespace PKHUD
{
	/// <summary>
	/// Provides a square view, which can be subclassed with additional views.
	/// </summary>
	public class SquareBaseView : UIView
	{
		public static CGRect DefaultSquareBaseViewFrame { get; }

		static SquareBaseView()
		{
			DefaultSquareBaseViewFrame = new CGRect(CGPoint.Empty, new CGSize(156, 156));
		}

		protected virtual Func<UIImageView> ImageViewFactory => () =>
		{
			return new UIImageView
			{
				Alpha = .85f,
				ClipsToBounds = true,
				ContentMode = UIViewContentMode.ScaleAspectFit
			};
		};

		protected virtual Func<UILabel> TitleLabelFactory => () =>
		{
			return new UILabel
			{
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.BoldSystemFontOfSize(17),
				TextColor = UIColor.Black.ColorWithAlpha(.85f),
				AdjustsFontSizeToFitWidth = true,
				MinimumScaleFactor = .25f
			};
		};

		protected virtual Func<UILabel> SubtitleLabelFactory => () =>
		{
			return new UILabel
			{
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(14),
				TextColor = UIColor.Black.ColorWithAlpha(.7f),
				AdjustsFontSizeToFitWidth = true,
				Lines = 2,
				MinimumScaleFactor = .25f
			};
		};

		public UIImageView ImageView { get; private set; }

		public UILabel TitleLabel { get; private set; }

		public UILabel SubtitleLabel { get; private set; }

		public SquareBaseView(CGRect frame) : base(frame)
		{
			/* Required constructor */
		}

		public SquareBaseView(NSCoder coder) : base(coder)
		{
			/* Required constructor */
		}

		public SquareBaseView(IntPtr handle) : base(handle)
		{
			/* Required constructor */
		}

		public SquareBaseView(UIImage image = default(UIImage), string title = default(string), string subtitle = default(string)) : base(DefaultSquareBaseViewFrame)
		{
			Initialize(image, title, subtitle);
		}

		protected void Initialize(UIImage image = default(UIImage), string title = default(string), string subtitle = default(string))
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

			ImageView.Image = image;
			TitleLabel.Text = title;
			SubtitleLabel.Text = subtitle;

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
