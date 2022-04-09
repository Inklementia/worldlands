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
        
        public override void IncreaseStat(float amount)
        {
            base.IncreaseStat(amount);

            
        }
        
        public override void DecreaseStat(float amount)
        {
            base.DecreaseStat(amount);

          
        }
        
    }
}
