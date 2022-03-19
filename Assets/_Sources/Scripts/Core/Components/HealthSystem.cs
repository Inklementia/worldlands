namespace _Sources.Scripts.Core.Components
{
    public class HealthSystem : StatsComponent
    {
        public bool IsDead { get; set; }

        protected override void Awake()
        {
            base.Awake();
            IsDead = false;
        }
    }
}
