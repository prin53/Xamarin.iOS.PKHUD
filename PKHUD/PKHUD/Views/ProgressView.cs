using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PKHUD.Views
{
    /// <summary>
    /// Provides an indeterminate progress view.
    /// </summary>
    public class ProgressView : SquareBaseView, IAnimation
    {
        public override UIImage? Image
        {
            get => Assets.ProgressActivity;
            // ReSharper disable once ValueParameterNotUsed
            set => base.Image = Assets.ProgressActivity;
        }
        
        public ProgressView(CGRect frame) : base(frame)
        {
            /* Required constructor */
        }

        public ProgressView(NSCoder coder) : base(coder)
        {
            /* Required constructor */
        }

        public ProgressView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public ProgressView()
        {
            /* Required constructor */
        }

        public void StartAnimation()
        {
            ImageView?.Layer.AddAnimation(AnimationFactory.CreateDiscreteRotationAnimation(), "progressAnimation");
        }

        public void StopAnimation()
        {
            /* Nothing to do */
        }
    }
}
