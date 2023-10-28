using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LandmineBehavior : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private GameObject landmineParticle;
    [SerializeField]
    private AudioClip explosion;

    public float weaponDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemies")
        {
            target = other.GetComponent<Transform>();
            HitTarget();
        }
    }

    void HitTarget()
    {
        //audio.PlayOneshot(explosion);
        //GameObject particleInstance = Instantiate(landmineParticle, transform.position, transform.rotation);
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
