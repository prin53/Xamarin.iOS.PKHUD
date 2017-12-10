//
// Builder.cs
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
using PKHUD.Views;
using UIKit;

namespace PKHUD
{
    public class Builder
    {
        private readonly Hud _hud;

        private string _title;
        private string _subtitle;
        private UIImage _image;

        internal Builder(UIView contentView)
        {
            _hud = new Hud
            {
                ContentView = contentView
            };
        }
        
        public Builder WithWiewToPresentOn(UIView view)
        {
            _hud.ViewToPresentOn = view;

            return this;
        }

        /// Grace period is the time (in seconds) that the invoked method may be run without
        /// showing the HUD. If the task finishes before the grace time runs out, the HUD will
        /// not be shown at all.
        /// This may be used to prevent HUD display for very short tasks.
        /// Defaults to Zero (no grace time).
        public Builder WithGracePeriod(TimeSpan gracePeriod)
        {
            _hud.GracePeriod = gracePeriod;

            return this;
        }
        
        public Builder WithBackgroundDimming(bool dimming)
        {
            _hud.DimsBackground = dimming;

            return this;
        }
        
        public Builder WithUserInteractionOnUnderlyingViews(bool userInteractionOnUnderlyingViews)
        {
            _hud.UserInteractionOnUnderlyingViewsEnabled = userInteractionOnUnderlyingViews;

            return this;
        }

        public Builder WithTitle(string title)
        {
            _title = title;

            return this;
        }

        public Builder WithSubtitle(string subtitle)
        {
            _subtitle = subtitle;

            return this;
        }

        public Builder WithImage(UIImage image)
        {
            _image = image;

            return this;
        }

        public Hud Build()
        {
            switch (_hud.ContentView)
            {
                case SquareBaseView squareBaseView:
                    squareBaseView.Title = _title ?? squareBaseView.Title;
                    squareBaseView.Subtitle = _subtitle ?? squareBaseView.Subtitle;
                    squareBaseView.Image = _image ?? squareBaseView.Image;
                    break;
                case TextView textView:
                    textView.Title = _title ?? textView.Title;
                    break;
            }

            return _hud;
        }
    }
}
