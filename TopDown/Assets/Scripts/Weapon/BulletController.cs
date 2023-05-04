using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody bulletRigidbody;
    public float speed = 50f;
    public int damage = 10;
    public float lifeTyme = 5;
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        lifeTyme -= Time.deltaTime;
        if (lifeTyme <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
   
        
        if (other.gameObject.GetComponent<Enemy>())
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(10);
        }
        Destroy(gameObject);
    }

}