using System;
using UIKit;

namespace PKHUD
{
    public static class Assets
    {
        private static readonly Lazy<UIImage> CrossImageLazy = new(() => UIImage.FromFile("Cross")!);
        private static readonly Lazy<UIImage> CheckmarkImageLazy = new(() => UIImage.FromFile("Checkmark")!);
        private static readonly Lazy<UIImage> ProgressActivityImageLazy = new(() => UIImage.FromFile("ProgressActivity")!);
        private static readonly Lazy<UIImage> ProgressCircularImageLazy = new(() => UIImage.FromFile("ProgressCircular")!);

        public static UIImage CrossImage => CrossImageLazy.Value;
        public static UIImage CheckmarkImage => CheckmarkImageLazy.Value;
        public static UIImage ProgressActivity => ProgressActivityImageLazy.Value;
        public static UIImage ProgressCircular => ProgressCircularImageLazy.Value;
    }
}
