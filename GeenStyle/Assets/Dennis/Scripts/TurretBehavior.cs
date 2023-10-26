using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class TurretBehavior : MonoBehaviour
{
    private Transform target;
    private EnemyBehavior targetEnemy;

    [Header("General")]
    public float range;

    [Header("Bullets (default)")]
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Flamethrower")]
    public bool useFlamethrower = false;
    public LineRenderer lineRenderer;

    public float weaponDamage;

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
                }
            }
            return;
        }

        LockOnTarget();

        if (useFlamethrower)
        {
            FlamethrowerShoot();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                ShootTurret();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }
    #endregion

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    #region Turret Shooting
    void ShootTurret()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletBehavior bullet = bulletGO.GetComponent<BulletBehavior>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void FlamethrowerShoot()
    {
        targetEnemy.TakeDamage(weaponDamage * Time.deltaTime);

        if (lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }
    #endregion

    #region Turret Lock On
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
