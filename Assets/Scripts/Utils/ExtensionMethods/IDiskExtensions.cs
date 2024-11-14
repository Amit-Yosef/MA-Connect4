using Cysharp.Threading.Tasks;
using MoonActive.Connect4;

namespace Utils.ExtensionMethods
{
    public static class IDiskExtensions
    {
        public static async UniTask WaitForDiskToStopFalling(this IDisk disk)
        {
            var tcs = new UniTaskCompletionSource();

            void OnStoppedFalling()
            {
                disk.StoppedFalling -= OnStoppedFalling;
                tcs.TrySetResult();
            }

            disk.StoppedFalling += OnStoppedFalling;
            await tcs.Task;
        }
    }
}