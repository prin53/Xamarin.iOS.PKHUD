using System;
using System.Threading.Tasks;

namespace PKHUD
{
    public static class HudExtensions
    {
        public static async Task FlashAsync(this Hud hud, TimeSpan duration)
        {
            if (hud == null)
            {
                throw new ArgumentNullException(nameof(hud));
            }

            hud.Show();
            
            await hud.HideWithDelayAsync(duration);
        }
        
        public static Task HideAsync(this Hud hud, bool animated = true)
        {
            if (hud == null)
            {
                throw new ArgumentNullException(nameof(hud));
            }
            
            var taskCompletionSource = new TaskCompletionSource<bool>();

            hud.Hide(animated, () => taskCompletionSource.TrySetResult(true));

            return taskCompletionSource.Task;
        }
        
        public static Task HideWithDelayAsync(this Hud hud, TimeSpan delay, bool animated = true)
        {
            if (hud == null)
            {
                throw new ArgumentNullException(nameof(hud));
            }
            
            var taskCompletionSource = new TaskCompletionSource<bool>();

            hud.HideWithDelay(delay, animated, () => taskCompletionSource.TrySetResult(true));

            return taskCompletionSource.Task;
        }
    }
}