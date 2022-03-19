using _Sources.Scripts.Core;
using _Sources.Scripts.Interfaces;
using _Sources.Scripts.Weapons;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace _Sources.Scripts.Enemies.State_Mashine
{
    public class Entity : MonoBehaviour
    {
        public FiniteStateMashine StateMachine;
        public D_Entity EntityData;
        public EnemyCore Core { get; private set; }
        public GameObject Dead { get; private set; }
        public GameObject Alive { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; }
  
        public AnimationToStateMachine AnimationToStateMachine { get; private set; }
        //public GameObject Target { get; private set; }
        public Vector2 StartingPos { get; private set; }

        // Pathfinding variables // probably move to coreComponent
        public Seeker Seeker { get; private set; }

        //public delegate void DropAction();

       // public static event DropAction OnDrop;

       [SerializeField] private Tag weaponGeneratorTag;
       private WeaponGenerator _weaponGenerator;
        public virtual void Awake()
        {
            Anim = GetComponentInChildren<Animator>();
            Rb = GetComponentInChildren<Rigidbody2D>();
            AnimationToStateMachine = GetComponentInChildren<AnimationToStateMachine>();
            Core = GetComponentInChildren<EnemyCore>();

            Seeker = GetComponentInChildren<Seeker>();
       
            StartingPos = transform.position;
      
        }


        public virtual void Start()
        {
            Core.HealthSystem.IsDead = false;
            Core.Movement.SetFacingDirection(-1);
       
            StateMachine = new FiniteStateMashine();

            //Seeker.StartPath(Rb.position, MoveTarget.position, OnPathComplete);
            //InvokeRepeating("UpdatePath", 0f, 2f);
            Core.HealthSystem.SetMaxStat(EntityData.MaxHealth);
            //healthBar.SetMaxHealth(Core.HealthSystem.MaxHealth);
            //healthBar.SetHealth(Core.HealthSystem.Health);
        
            //Target = Core.PlayerDetectionSenses.Player;
            _weaponGenerator = gameObject.FindWithTag(weaponGeneratorTag).GetComponent<WeaponGenerator>();
            Dead = transform.Find("Dead").gameObject;
            Alive = transform.Find("Alive").gameObject;
        }
        public virtual void Update()
        {
       

            StateMachine.CurrentState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        

        public Vector3 GetTargetPosition()
        {
            return Core.PlayerDetectionSenses.Player.transform.position;
        }
        
        // DAMAGE 
        public virtual void Damage(AttackDetails attackDetails)
        {

            Core.CombatSystem.TakeDamage(attackDetails);

            if (Core.HealthSystem.IsDead)
            {
                GameActions.Current.EnemyKilledTrigger(this.gameObject);
                Debug.Log("Enemy KIlled");
            }
            
            
        }

        public virtual void OnDrawGizmos()
        {
        }
    }
}
