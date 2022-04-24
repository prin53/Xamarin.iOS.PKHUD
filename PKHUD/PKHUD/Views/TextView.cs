using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    public class TextView : WideBaseView
    {
        private const int Padding = 10;
        
        private string? _title;
        
        public virtual string? Title
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

        protected UILabel? TitleLabel { get; private set; }

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
            /* Required constructor */
        }

        public TextView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public TextView()
        {
            Initialize();
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

        private void Initialize()
        {
            TitleLabel = TitleLabelFactory();

            if (TitleLabel == null)
            {
                throw new InvalidOperationException($"{nameof(TitleLabelFactory)} must produce a valid view");
            }

            TitleLabel.Text = Title;

            AddSubview(TitleLabel);
        }
    }
}
