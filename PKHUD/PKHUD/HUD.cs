//
// HUD.cs
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
	public static class HUD
	{
		public static bool DimsBackground
		{
			get
			{
				return PKHUD.Instance.DimsBackground;
			}
			set
			{
				PKHUD.Instance.DimsBackground = value;
			}
		}

		public static bool AllowsInteraction
		{
			get
			{
				return PKHUD.Instance.UserInteractionOnUnderlyingViewsEnabled;
			}
			set
			{
				PKHUD.Instance.UserInteractionOnUnderlyingViewsEnabled = value;
			}
		}

		public static bool IsVisible => PKHUD.Instance.IsVisible;

		public static void Show(UIView view, UIView onView = default(UIView))
		{
			PKHUD.Instance.ContentView = view;
			PKHUD.Instance.Show(onView);
		}

		public static void Hide(Action<bool> completion = default(Action<bool>))
		{
			PKHUD.Instance.Hide(false, completion);
		}

		public static void Hide(bool animated, Action<bool> completion = default(Action<bool>))
		{
			PKHUD.Instance.Hide(animated, completion);
		}

		public static void Hide(TimeSpan delay, bool animated = true, Action<bool> completion = default(Action<bool>))
		{
			PKHUD.Instance.Hide(delay, animated, completion);
		}

		public static void Flash(UIView view, UIView onView = default(UIView))
		{
			Show(view, onView);
			Hide(true);
		}

		public static void Flash(UIView view, TimeSpan delay, Action<bool> completion = default(Action<bool>), UIView onView = default(UIView))
		{
			Show(view, onView);
			Hide(delay, true, completion);
		}
	}
}
