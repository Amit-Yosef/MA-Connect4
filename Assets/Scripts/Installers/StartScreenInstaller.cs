using Controllers;
using Controllers.Players;
using Managers;
using MoonActive.Connect4;
using Zenject;

namespace Installers
{
    public class StartScreenInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerTurnStrategyService>().AsSingle();
        }
    }
}