//
// Assets.cs
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
using UIKit;

namespace PKHUD
{
    public static class Assets
    {
        private static readonly Lazy<UIImage> CrossImageLazy = 
            new Lazy<UIImage>(() => UIImage.FromFile("Cross"));

        private static readonly Lazy<UIImage> CheckmarkImageLazy =
            new Lazy<UIImage>(() => UIImage.FromFile("Checkmark"));

        private static readonly Lazy<UIImage> ProgressActivityImageLazy =
            new Lazy<UIImage>(() => UIImage.FromFile("ProgressActivity"));

        private static readonly Lazy<UIImage> ProgressCircularImageLazy =
            new Lazy<UIImage>(() => UIImage.FromFile("ProgressCircular"));

        public static UIImage CrossImage => CrossImageLazy.Value;

        public static UIImage CheckmarkImage => CheckmarkImageLazy.Value;

        public static UIImage ProgressActivity => ProgressActivityImageLazy.Value;

        public static UIImage ProgressCircular => ProgressCircularImageLazy.Value;
    }
}
