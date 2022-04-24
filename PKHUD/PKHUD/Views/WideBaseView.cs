using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    /// <summary>
    /// A wide base view, which can be subclassed with additional views.
    /// </summary>
    public class WideBaseView : UIView
    {
        public static CGRect DefaultWideBaseViewFrame { get; }

        static WideBaseView()
        {
            DefaultWideBaseViewFrame = new CGRect(CGPoint.Empty, new CGSize(265, 90));
        }

        public WideBaseView(CGRect frame) : base(frame)
        {
            /* Required constructor */
        }

        public WideBaseView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public WideBaseView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public WideBaseView() : base(DefaultWideBaseViewFrame)
        {
            /* Required constructor */
        }
    }
}
