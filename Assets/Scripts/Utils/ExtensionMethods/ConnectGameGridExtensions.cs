using System.Threading;
using Cysharp.Threading.Tasks;
using MoonActive.Connect4;

namespace Utils.ExtensionMethods
{
    public static class ConnectGameGridExtensions
    {
        public static async UniTask<int> WaitForColumnSelect(this ConnectGameGrid grid, CancellationTokenSource cts)
        {
            var tcs = new UniTaskCompletionSource<int>();

            void OnClick(int column)
            {
                grid.ColumnClicked -= OnClick;
                tcs.TrySetResult(column);
            }

            void OnCanceled()
            {
                grid.ColumnClicked -= OnClick;
                tcs.TrySetCanceled();
            }

            grid.ColumnClicked += OnClick;
            using (cts.Token.Register(OnCanceled))
            {
                return await tcs.Task;
            }
        }
    }
}