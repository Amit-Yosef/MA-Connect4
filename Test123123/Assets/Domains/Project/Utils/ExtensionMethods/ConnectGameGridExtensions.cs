using System.Threading;
using Cysharp.Threading.Tasks;
using MoonActive.Connect4;

namespace Project.Utils.ExtensionMethods
{
    public static class ConnectGameGridExtensions
    {
        public static async UniTask<int> WaitForColumnSelect(this ConnectGameGrid grid, CancellationToken cancellationToken)
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
            await using (cancellationToken.Register(OnCanceled))
            {
                return await tcs.Task;
            }
        }
    }
}