//
// AnimationFactory.cs
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
using CoreAnimation;
using Foundation;

namespace PKHUD
{
    public static class AnimationFactory
    {
        public static CAAnimation CreateContinuousRotationAnimation()
        {
            var animation = CABasicAnimation.FromKeyPath("transform.rotation.z");
            {
                animation.From = new NSNumber(0);
                animation.To = new NSNumber(2 * Math.PI);
                animation.Duration = 1.2f;
                animation.RepeatCount = float.MaxValue;
            }
            return animation;
        }

        public static CAAnimation CreateDiscreteRotationAnimation()
        {
            var animation = CAKeyFrameAnimation.FromKeyPath("transform.rotation.z");
            {
                animation.Values = new NSObject[]
                {
                    new NSNumber(0f),
                    new NSNumber(1f * Math.PI / 6f),
                    new NSNumber(2f * Math.PI / 6f),
                    new NSNumber(3f * Math.PI / 6f),
                    new NSNumber(4f * Math.PI / 6f),
                    new NSNumber(5f * Math.PI / 6f),
                    new NSNumber(6f * Math.PI / 6f),
                    new NSNumber(7f * Math.PI / 6f),
                    new NSNumber(8f * Math.PI / 6f),
                    new NSNumber(9f * Math.PI / 6f),
                    new NSNumber(10f * Math.PI / 6f),
                    new NSNumber(11f * Math.PI / 6f),
                    new NSNumber(2f * Math.PI)
                };
                animation.KeyTimes = new[]
                {
                    new NSNumber(0f),
                    new NSNumber(1f / 12f),
                    new NSNumber(2f / 12f),
                    new NSNumber(3f / 12f),
                    new NSNumber(4f / 12f),
                    new NSNumber(5f / 12f),
                    new NSNumber(.5f),
                    new NSNumber(7f / 12f),
                    new NSNumber(8f / 12f),
                    new NSNumber(9f / 12f),
                    new NSNumber(10f / 12f),
                    new NSNumber(11f / 12f),
                    new NSNumber(1f)
                };
                animation.Duration = 1.2f;
                animation.CalculationMode = "discrete";
                animation.RepeatCount = float.MaxValue;
            }
            return animation;
        }
    }
}
