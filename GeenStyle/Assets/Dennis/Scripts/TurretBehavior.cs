using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    private Transform target;
    private EnemyBehavior targetEnemy;

    [Header("General")]
    public float range;
    public int turretCost;

    [Header("Upgrade")]
    public GameObject turretUpgrade;
    public bool turretUpgraded = false;

    [Header("Bullets (default)")]
    public bool useBullet = false;
    public float fireRate;
    private float fireCountdown = 0f;
    public ParticleSystem muzzleFlash;
    public AudioSource bulletSFX;

    [Header("Flamethrower")]
    public bool useFlamethrower = false;
    public LineRenderer lineRenderer;
    public AudioSource flamethrowerSFX;
    public ParticleSystem flamethrowerParticle;

    public float weaponDamage;

    [Header("Landmine")]
    public bool useLandmine = false;
    [SerializeField]
    private GameObject landmineSFXGO;
    [SerializeField]
    private AudioSource landmineSFX;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemies";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    #region Start and Update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {

            if (useFlamethrower)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    flamethrowerSFX.Stop();
                    flamethrowerParticle.Stop();
                }
            }
            return;
        }

        if (useBullet || useFlamethrower)
        {
            LockOnTarget();
        }

        if (useBullet)
        {
            if (fireCountdown <= 0f)
            {
                ShootTurret();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        if (useFlamethrower)
        {

            FlamethrowerShoot();
        }

        if (useLandmine)
        {
            Landmine();
        }
    }
    #endregion

    #region Turret Mechanics
    void ShootTurret()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletBehavior bullet = bulletGO.GetComponent<BulletBehavior>();

        muzzleFlash.Play();

        bulletSFX.Play();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void FlamethrowerShoot()
    {
        targetEnemy.TakeDamage(weaponDamage * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            flamethrowerSFX.Play();
            flamethrowerParticle.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

    }

    void Landmine()
    {
        GameObject landmineParticle = GameObject.Find("Landmine Particle");
            
        Instantiate(landmineParticle, transform.position, transform.rotation);

        targetEnemy = target.GetComponent<EnemyBehavior>();

        GameObject landmineSFXGO = GameObject.Find("LandmineSFX");
        landmineSFX = landmineSFXGO.GetComponent<AudioSource>();
        landmineSFX.Play();

        targetEnemy.TakeDamage(weaponDamage);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider targetEnemy)
    {
        if (useLandmine)
        {
            if (targetEnemy.tag == "Enemies")
            {
                target = targetEnemy.GetComponent<Transform>();
                Landmine();
            }
        }
    }
    #endregion

    #region Turret Lock On
    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distancetoEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distancetoEnemy < shortestDistance)
            {
                shortestDistance = distancetoEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyBehavior>();
        }
        else
        {
            target = null;
        }
    }
    #endregion

    #region Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion
}
