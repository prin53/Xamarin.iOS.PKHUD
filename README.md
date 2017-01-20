[![NuGet version](https://badge.fury.io/nu/Prin53.Xamarin.iOS.PKHUD.svg)](https://badge.fury.io/nu/Prin53.Xamarin.iOS.PKHUD)
## Xamarin.iOS port for [PKHUD](https://github.com/pkluz/PKHUD) Swift library
## Usage Example
```cs
HUD.DimsBackground = false;
HUD.AllowsInteraction = false;

HUD.Flash(ContentFactory.CreateSuccessContent(), TimeSpan.FromSeconds(2));
```
## Custom Usage Example
```cs
PKHUD.Instance.DimsBackground = false;
PKHUD.Instance.AllowsInteraction = false;

PKHUD.Instance.ContentView = ContentFactory.CreateErrorContent();
PKHUD.Instance.Show();
PKHUD.Instance.Hide(TimeSpan.FromSeconds(2), true);
```