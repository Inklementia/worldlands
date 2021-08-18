using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponOld
{
    [SerializeField] private int EnergyCostPerAttack;

   
    [SerializeField] private float fireAngle = 40f;
    [SerializeField] private int numberOfBullets = 1;
    [SerializeField] private float fireDistance = .2f;// -
    [SerializeField] private float coolDown = 0.2f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float travelDistance = 5f;
    [SerializeField] private float lifeDuration = 6f;
    [SerializeField] private float dragMultiplier = .5f;
    [SerializeField] private float speed = 8f;

    private List<Vector2> _firePoints = new List<Vector2>();

    private float _coolDownTimer = 0.0f;
    private bool _canFire = true;


    private void Start()
    {
        AssignFirePoints();

    }

    private void Update()
    {
        CheckIfCanFire();

    }



    public void CheckIfCanFire()
    {
        if(_canFire == false)
        {
            _coolDownTimer += Time.deltaTime;
            if(_coolDownTimer >= coolDown )
            {
                _canFire = true;
                _coolDownTimer = 0.0f;
            }
        }
    }

    public override void Attack()
    {
        if (_canFire)
        {
            if(_firePoints.Count > 1)
            {
                foreach (var point in _firePoints)
                {
                    GameObject bullet = Instantiate(projectilePrefab, new Vector2(firePoint.position.x + point.x, firePoint.position.y + point.y), firePoint.rotation);
                    ProjectileOld projectile = bullet.GetComponent<ProjectileOld>();

                    var direction = new Vector2(point.x, point.y);
                    projectile.FireProjectile(speed, direction, travelDistance, lifeDuration, dragMultiplier);
                }
            }
            else
            {
                GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                var direction = transform.right;
                ProjectileOld projectile = bullet.GetComponent<ProjectileOld>();
                projectile.FireProjectile(speed, direction, travelDistance, lifeDuration, dragMultiplier);
                //Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                //bulletRb.AddForce(force * firePoint.right, ForceMode2D.Impulse);
            }

            _canFire = false;
          

        }
       
    }

    private void AssignFirePoints()
    {
        if (numberOfBullets > 1)
        {
            var halfAngle = Mathf.Ceil(fireAngle / 2);
            var segments = numberOfBullets - 1;

            var startAngle = 90 - halfAngle;
            var endAngle = 90 + halfAngle;

            float angle = startAngle;
            float arcLength = endAngle - startAngle;
            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * fireDistance;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * fireDistance;

                _firePoints.Add(new Vector2(x, y));

                angle += (arcLength / segments);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // 30
        // 15
        var halfAngle = Mathf.Ceil(fireAngle / 2);
        var segments = numberOfBullets - 1;
        //var segments = numberOfBullets + 1;

        var startAngle = 90 - halfAngle;
        var endAngle = 90 + halfAngle;
        List<Vector2> arcPoints = new List<Vector2>();
        float angle = startAngle;
        float arcLength = endAngle - startAngle;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * fireDistance;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * fireDistance;

            float x1 = Mathf.Sin(Mathf.Deg2Rad * angle) * 2;
            float y1 = Mathf.Cos(Mathf.Deg2Rad * angle) * 2;

            arcPoints.Add(new Vector2(x, y));
 
            Gizmos.color = Color.white;
            Gizmos.DrawLine(firePoint.position, new Vector2(firePoint.position.x + x1, firePoint.position.y + y1));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(firePoint.position, new Vector2(firePoint.position.x + x, firePoint.position.y + y));

            angle += (arcLength / segments);
        }


    }
}