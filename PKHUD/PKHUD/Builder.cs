using System;
using PKHUD.Views;
using UIKit;

namespace PKHUD
{
    public class Builder
    {
        private readonly Hud _hud;

        private string? _title;
        private string? _subtitle;
        private UIImage? _image;

        internal Builder(UIView contentView)
        {
            _hud = new Hud
            {
                ContentView = contentView
            };
        }
        
        public Builder WithViewToPresentOn(UIView view)
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
