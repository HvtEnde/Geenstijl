using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    GameObject turretType;

    public float speed = 70f;

    //[Header("Particles Input")]
    //public GameObject turretParticle;
    //public GameObject sniperParticle;
    //public GameObject flamethrowerParticle;
    //public GameObject landmineParticle;

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
        //Damage Gebaseerd op basis van turret type.
        
        float weaponDamage = turretType.GetComponent<TurretBehavior>().weaponDamage;
        target.GetComponent<EnemyBehavior>().health -= weaponDamage;

        Destroy(gameObject);
    }
}
