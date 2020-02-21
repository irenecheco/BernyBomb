using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 1.5f)] public float firerRate = 0.3f;
    [SerializeField] [Range(1, 10)] public int damage = 1;
   
    [SerializeField]private Transform firePoint;

    [SerializeField] private ParticleSystem muzzleParticle;

    [SerializeField] private AudioSource gunFireSource;

    public float speedFire;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime*speedFire;
        if (timer >= firerRate && Time.deltaTime != 0)
        {
            if (Input.GetMouseButton(0))
            {
                timer = 0f;
                FireGun();
            }
        }
    }
    private void FireGun()
    {
        //Debug.DrawRay(transform.position, firePoint.forward * 100, Color.red, 2f);


        muzzleParticle.Play();
        gunFireSource.Play();
        Ray ray = new Ray(firePoint.position, firePoint.forward);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<EnemyHealth>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
