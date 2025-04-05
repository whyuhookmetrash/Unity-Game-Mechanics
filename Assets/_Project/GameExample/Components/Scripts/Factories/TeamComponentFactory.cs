using Zenject;

namespace ShootEmUp
{
    public sealed class TeamComponentFactory :
        IFactory<TeamComponent>
    {
        private readonly IInstantiator instantiator;

        public TeamComponentFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public TeamComponent Create()
        {
            TeamComponent instance = this.instantiator.Instantiate<TeamComponent>();
            return instance;
        }
    }
}