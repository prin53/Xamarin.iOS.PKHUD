﻿//
// ViewController.cs
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
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace PKHUD.Sample
{
	/*
	* Please note that the blow demonstrates the "porcelain" interface
	* - a more concise and clean way to work with the HUD.
	* If you need more options and flexbility, 
	* feel free to use the underlying "plumbing". E.g.:
	* 	PKHUD.Instance.Show();
	* 	PKHUD.Instance.ContentView = new SuccessView("Success!");
	* 	PKHUD.Instance.Hide(TimeSpan.FromSeconds(2));
    */
	public class ViewController : UIViewController
	{
		protected UIImageView BackgroundImage { get; private set; }

		protected UIStackView ButtonsStack { get; private set; }

		protected UIButton AnimatedSuccessButton { get; private set; }

		protected UIButton AnimatedErrorButton { get; private set; }

		protected UIButton AnimatedProgressButton { get; private set; }

		protected UIButton AnimatedStatusProgressButton { get; private set; }

		protected UIButton TextButton { get; private set; }

		private UIButton CreateButton(string title)
		{
			var button = UIButton.FromType(UIButtonType.System);
			{
				button.TranslatesAutoresizingMaskIntoConstraints = false;
				button.SetTitleColor(UIColor.FromRGB(64, 64, 64), UIControlState.Normal);
				button.SetTitleShadowColor(UIColor.FromRGB(128, 128, 128), UIControlState.Normal);
				button.SetTitle(title, UIControlState.Normal);
				button.BackgroundColor = UIColor.FromRGB(255, 255, 255).ColorWithAlpha(.66f);
			}
			return button;
		}

		private void AnimatedSuccessButton_TouchUpInside(object sender, EventArgs e)
		{
			HUD.Flash(ContentFactory.CreateSuccessContent(), TimeSpan.FromSeconds(2));
		}

		private void AnimatedErrorButton_TouchUpInside(object sender, EventArgs e)
		{
			HUD.Show(ContentFactory.CreateErrorContent());
			HUD.Hide(TimeSpan.FromSeconds(2), true);
		}

		private async void AnimatedProgressButton_TouchUpInside(object sender, EventArgs e)
		{
			HUD.Show(ContentFactory.CreateProgressContent());

			await Task.Delay(TimeSpan.FromSeconds(2));

			HUD.Flash(ContentFactory.CreateSuccessContent(), TimeSpan.FromSeconds(1));
		}

		private void AnimatedStatusProgressButton_TouchUpInside(object sender, EventArgs e)
		{
			HUD.Flash(ContentFactory.CreateProgressContent("Title", "Subtitle"), TimeSpan.FromSeconds(2));
		}

		private void TextButton_TouchUpInside(object sender, EventArgs e)
		{
			HUD.Flash(ContentFactory.CreateLabelContent("Requesting Licence…"), TimeSpan.FromSeconds(2), completed =>
			{
				Debug.WriteLine("License Obtained");
			});
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				AnimatedSuccessButton.TouchUpInside -= AnimatedSuccessButton_TouchUpInside;
				AnimatedErrorButton.TouchUpInside -= AnimatedErrorButton_TouchUpInside;
				AnimatedProgressButton.TouchUpInside -= AnimatedProgressButton_TouchUpInside;
				AnimatedStatusProgressButton.TouchUpInside -= AnimatedStatusProgressButton_TouchUpInside;
				TextButton.TouchUpInside -= TextButton_TouchUpInside;
			}

			base.Dispose(disposing);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			HUD.DimsBackground = false;
			HUD.AllowsInteraction = false;

			View.BackgroundColor = UIColor.White;

			BackgroundImage = new UIImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Image = UIImage.FromBundle("Background"),
				ContentMode = UIViewContentMode.ScaleAspectFill
			};

			ButtonsStack = new UIStackView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Vertical,
				Spacing = 8
			};

			AnimatedSuccessButton = CreateButton("Animated Success HUD");
			AnimatedSuccessButton.TouchUpInside += AnimatedSuccessButton_TouchUpInside;

			AnimatedErrorButton = CreateButton("Animated Error HUD");
			AnimatedErrorButton.TouchUpInside += AnimatedErrorButton_TouchUpInside;

			AnimatedProgressButton = CreateButton("Animated Progress HUD");
			AnimatedProgressButton.TouchUpInside += AnimatedProgressButton_TouchUpInside;

			AnimatedStatusProgressButton = CreateButton("Animated Status Progress HUD");
			AnimatedStatusProgressButton.TouchUpInside += AnimatedStatusProgressButton_TouchUpInside;

			TextButton = CreateButton("Text HUD");
			TextButton.TouchUpInside += TextButton_TouchUpInside;

			ButtonsStack.AddArrangedSubview(AnimatedSuccessButton);
			ButtonsStack.AddArrangedSubview(AnimatedErrorButton);
			ButtonsStack.AddArrangedSubview(AnimatedProgressButton);
			ButtonsStack.AddArrangedSubview(AnimatedStatusProgressButton);
			ButtonsStack.AddArrangedSubview(TextButton);

			View.AddSubviews(BackgroundImage, ButtonsStack);

			View.AddConstraints(new[]
			{
				BackgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor),
				BackgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
				BackgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
				BackgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
				ButtonsStack.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
				ButtonsStack.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),
				ButtonsStack.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -16),
				AnimatedSuccessButton.HeightAnchor.ConstraintEqualTo(44),
				AnimatedErrorButton.HeightAnchor.ConstraintEqualTo(44),
				AnimatedProgressButton.HeightAnchor.ConstraintEqualTo(44),
				AnimatedStatusProgressButton.HeightAnchor.ConstraintEqualTo(44),
				TextButton.HeightAnchor.ConstraintEqualTo(44)
			});
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.AllButUpsideDown;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}