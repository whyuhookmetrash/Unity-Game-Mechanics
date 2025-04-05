using Zenject;

namespace ShootEmUp
{
    public sealed class TimerSpawnFactory :
        IFactory<Timer>
    {
        private readonly IFactory<Timer> timerFactory;

        public TimerSpawnFactory(IFactory<Timer> timerFactory)
        {
            this.timerFactory = timerFactory;
        }

        public Timer Create()
        {
            Timer timer = this.timerFactory.Create();
            return timer;
        }
    }
}