using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    GameObject turretType;

    public float speed;

    public float weaponDamage;

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
