using _Sources.Scripts.Core;
using _Sources.Scripts.Interfaces;
using _Sources.Scripts.Weapons;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace _Sources.Scripts.Enemies.State_Mashine
{
    public class Entity : MonoBehaviour, IDamageable
    {
        public FiniteStateMashine StateMachine;
        public D_Entity EntityData;
        public EnemyCore Core { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; }
  
        public AnimationToStateMachine AnimationToStateMachine { get; private set; }
        public GameObject Target { get; private set; }
        public Vector2 StartingPos { get; private set; }

        // Pathfinding variables // probably move to coreComponent
        public Seeker Seeker { get; private set; }
 

        //[SerializeField] private HealthBar healthBar;
  

        //public Transform MoveTarget;

        private float _knockbackStartTime;

        private int _lastDamageDirection;

        public bool IsDead {  get; protected set; }
        
        //public delegate void DropAction();

       // public static event DropAction OnDrop;
       [SerializeField] private Tag weaponGeneratorTag;
       private WeaponGenerator _weaponGenerator;
        public virtual void Awake()
        {
            Anim = GetComponent<Animator>();
            Rb = GetComponent<Rigidbody2D>();
            AnimationToStateMachine = GetComponent<AnimationToStateMachine>();
            Core = GetComponentInChildren<EnemyCore>();

            Seeker = GetComponent<Seeker>();
       
            StartingPos = transform.position;
      
        }


        public virtual void Start()
        {
            Core.Movement.SetFacingDirection(-1);
       
            StateMachine = new FiniteStateMashine();

            //Seeker.StartPath(Rb.position, MoveTarget.position, OnPathComplete);
            //InvokeRepeating("UpdatePath", 0f, 2f);
            Core.HealthSystem.SetMaxHealth(EntityData.MaxHealth);
            //healthBar.SetMaxHealth(Core.HealthSystem.MaxHealth);
            //healthBar.SetHealth(Core.HealthSystem.Health);
        
            Target = Core.PlayerDetectionSenses.Player;
            _weaponGenerator = gameObject.FindWithTag(weaponGeneratorTag).GetComponent<WeaponGenerator>();
        }
        public virtual void Update()
        {
            CheckKnockback();

            StateMachine.CurrentState.LogicUpdate();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        public void CheckKnockback()
        {
            if (Time.time >= _knockbackStartTime + EntityData.KnockbackDuration)
            {
                Core.Movement.SetVelocityZero();
            }
        }

        public virtual Vector2 GetTargetPosition()
        {
            return Target.transform.position;
        }

        public virtual void GoTo(Vector2 point, float speed)
        {
            var distance = Vector2.Distance(transform.position, point);
            Rb.DOMove(point, distance / speed);
            //transform.position = Vector2.MoveTowards(transform.position, MovePos, speed * Time.deltaTime);
        }

        public virtual void StopMovement()
        {
            Rb.DOPause();
        }

        // DAMAGE 
        public virtual void TakeDamage(AttackDetails attackDetails)
        {
  
            if(attackDetails.Position.x > transform.position.x)
            {
                _lastDamageDirection = -1;
            }
            else
            {
                _lastDamageDirection = 1;
            }
   
            _knockbackStartTime = Time.time;
   
            Core.Movement.SetVelocity(EntityData.KnockBackAngle, EntityData.KnockBackSpeed, _lastDamageDirection);
        
        }

        public virtual void OnDrawGizmos()
        {
        }
    }
}
