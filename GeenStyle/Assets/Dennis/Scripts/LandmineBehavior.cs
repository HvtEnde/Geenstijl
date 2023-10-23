using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject landminePrefab, landmineParticle;
    [SerializeField]
    private AudioClip explosion;

    public float weaponDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemies")
        {
            //audio.PlayOneshot(explosion);
            //GameObject particleInstance = Instantiate(landmineParticle, transform.position, transform.rotation);
            //Destroy(particleInstance, 2f);

            other.GetComponent<EnemyBehavior>().health -= weaponDamage;

            Destroy(gameObject);
        }
    }
}
