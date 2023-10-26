using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    GameObject turretType;

    public float speed = 70f;

    public int weaponDamage;

    //[Header("Particles Input")]
    //public GameObject turretParticle, sniperParticle, flamethrowerParticle;

    public void Seek(Transform _target)
    {
        target = _target;
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //GameObject particleInstance = Instantiate(turretParticle, transform.position, transform.rotation);
        //Destroy(particleInstance, 2f);

        Damage(target);

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyBehavior e = enemy.GetComponent<EnemyBehavior>();

        if (e != null)
        {
            e.TakeDamage(weaponDamage);
        }
    }
}
