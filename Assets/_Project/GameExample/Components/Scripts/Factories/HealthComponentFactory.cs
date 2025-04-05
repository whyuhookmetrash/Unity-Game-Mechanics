using Zenject;

namespace ShootEmUp
{
    public sealed class HealthComponentFactory :
        IFactory<HealthComponent>
    {
        private readonly IInstantiator instantiator;

        public HealthComponentFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public HealthComponent Create()
        {
            HealthComponent instance = this.instantiator.Instantiate<HealthComponent>();
            return instance;
        }
    }
}