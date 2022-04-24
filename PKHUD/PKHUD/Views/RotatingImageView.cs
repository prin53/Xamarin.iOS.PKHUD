using System;
using CoreGraphics;
using Foundation;

namespace PKHUD.Views
{
    /// <summary>
    /// Provides a content view that rotates the supplies image automatically.
    /// </summary>
    public class RotatingImageView : SquareBaseView, IAnimation
    {
        public RotatingImageView(CGRect frame) : base(frame)
        {
            /* Required constructor */
        }

        public RotatingImageView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public RotatingImageView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public RotatingImageView()
        {
            /* Required constructor */
        }

        public void StartAnimation()
        {
            ImageView?.Layer.AddAnimation(AnimationFactory.CreateContinuousRotationAnimation(), "progressAnimation");
        }

        public void StopAnimation()
        {
            /* Nothing to do */
        }
    }
}
