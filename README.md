[![NuGet version](https://badge.fury.io/nu/Prin53.Xamarin.iOS.PKHUD.svg)](https://badge.fury.io/nu/Prin53.Xamarin.iOS.PKHUD)
## Xamarin.iOS port for [PKHUD](https://github.com/pkluz/PKHUD) Swift library
A **Xamarin.iOS** based reimplementation of the Apple HUD (Volume, Ringer, Rotation,…) **for iOS 8** and up.
## Features
- Official iOS 8 blur effect via **UIVisualEffectsView**.
- Proper **rotation support**.
- Size / **Device agnostic**.
- Works on top of presented view controllers, alerts,...
- Comes with several *free* resources - Checkmark, Cross, Progress Indicator,…
- …as well as **animated** ones.
- Builds as an **iOS 8 framework**.

## Installation
Install PKHUD by adding the [NuGet](https://www.nuget.org/) package:
```
Install-Package Prin53.Xamarin.iOS.PKHUD
```
## How To
You can proceed to show an arbitrary HUD (and have it automatically disappear a second later) like this:
```cs
Hud.Create()
    .WithSuccessContent()
    .Build()
    .Flash(TimeSpan.FromSeconds(1));
```
_or_ by asynchronous call:
```cs
await Hud.Create()
    .WithSuccessContent()
    .Build()
    .FlashAsync(TimeSpan.FromSeconds(1));
```
you can use the “plumbing” API:
```cs
var hud = Hud.Create()
    .WithProgressContent()
    .WithBackgroundDimming(true)
    .WithTitle("Title")
    .WithSubtitle("Subtitle")
    .Build();

hud.Show();

// Some work.
await Task.Delay(TimeSpan.FromSeconds(2));

hud.Hide();
```

You can also hot-swap content views - this can prove useful if you want to display a progress HUD first and transform it into a success or error HUD after an asynchronous operation has finished.
```cs
Hud.Create()
    .WithProgressContent()
    .Build()
    .Show();

// Some work.
await Task.Delay(TimeSpan.FromSeconds(2));

Hud.Create()
    .WithSuccessContent()
    .Build()
    .Flash(TimeSpan.FromSeconds(1));
```
## Customization
- `WithBackgroundDimming(bool)` defines whether the background is slightly dimmed when the HUD is shown.
- `WithUserInteractionOnUnderlyingViews(bool)` defines whether the underlying views respond to touches while the HUD is shown.

There are _multiple_ types of content views that ship with PKHUD. Custom views can descend from any `UIView` type or the predefined base classes `SquareBaseView` and `WideBaseView`. Then you can use the custom content:
```cs
Hud.Create()
    .WithContent(new CustomView())
    .Build()
    .Show();
```

**Note**: It's neither possible to customize the general look and feel, nor do I plan to add that feature. You are free to provide any content views you wish but the blurring, corner radius and shading will remain the same.
## Credits
Xamarin PKHUD implementation is based on [native PKHUD](https://github.com/pkluz/PKHUD).
