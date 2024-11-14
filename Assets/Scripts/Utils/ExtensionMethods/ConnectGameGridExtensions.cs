using Cysharp.Threading.Tasks;
using MoonActive.Connect4;

namespace Utils.ExtensionMethods
{
    public static class ConnectGameGridExtensions
    {
        public static async UniTask<int> WaitForColumnSelect(this ConnectGameGrid grid)
        {
            var tcs = new UniTaskCompletionSource<int>();

            void OnClick(int column)
            {
                grid.ColumnClicked -= OnClick;
                tcs.TrySetResult(column);
            }

            grid.ColumnClicked += OnClick;
            return await tcs.Task;
        }
    }
}