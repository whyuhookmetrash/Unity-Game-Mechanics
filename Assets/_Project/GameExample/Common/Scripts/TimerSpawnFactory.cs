using Zenject;

namespace ShootEmUp
{
    public sealed class TimerSpawnFactory :
        IFactory<Timer>
    {
        private readonly TimerFactory timerFactory;

        public TimerSpawnFactory(TimerFactory timerFactory)
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