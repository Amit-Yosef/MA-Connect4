using Cysharp.Threading.Tasks;
using MoonActive.Connect4;
using UnityEngine;

namespace Project.Utils.ExtensionMethods
{
    public static class DiskExtensions
    {
        public static async UniTask WaitForDiskToStopFalling(this IDisk disk)
        {
            var tcs = new UniTaskCompletionSource();

            void OnStoppedFalling()
            {
                Debug.Log("on stop falling");
                disk.StoppedFalling -= OnStoppedFalling;
                tcs.TrySetResult();
            }

            disk.StoppedFalling += OnStoppedFalling;
            await tcs.Task;
        }
    }
}