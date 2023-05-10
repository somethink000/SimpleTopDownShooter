using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //stats

    public float radius = 1f;
    public int endamage = 5; 
    public int Health = 100;
    private Rigidbody enregid;
    public Player ply;
    public float moveSpeed = 1.0f;
    private Camera _cam;


    public Slider healthBar;
    public GameObject unitcanvas;
    public Animator animator;




    private Coroutine _corotin;
    public void Start()
    {
        animator = GetComponent<Animator>();
        _cam = Camera.main;
            enregid = GetComponent<Rigidbody>();

        ply = FindObjectOfType<Player>();


    }

    private void FixedUpdate()
    {
        enregid.velocity = (transform.forward * moveSpeed);
    }

    private void Update()
    {
        if (unitcanvas.transform.rotation != _cam.transform.rotation)
        {
            unitcanvas.transform.rotation = _cam.transform.rotation;
        }

        DetectCollision();

        animator.SetFloat("Speed", Vector3.ClampMagnitude(transform.forward, 1).magnitude);
        transform.LookAt(new Vector3(ply.transform.position.x, 0 , ply.transform.position.z));


        
    }

    private void DetectCollision()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);

        if (hitCollider.Length == 0 && _corotin != null)
        {
            StopCoroutine(_corotin);
            _corotin = null;



        }

        foreach (var el in hitCollider)
        {

            if (_corotin == null && el.gameObject.GetComponent<Player>())
            {
                _corotin = StartCoroutine(StartAttack(el));
            }

        }
            
    }

    IEnumerator StartAttack(Collider other)
    {
        other.gameObject.GetComponent<Player>().TakeDamage(endamage);



        yield return new WaitForSeconds(3f);
        StopCoroutine(_corotin);
        _corotin = null;

    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        healthBar.value -= damage;

        if (Health < 0)
        {
            Destroy(gameObject);
            ply.gameObject.GetComponent<Player>().GetXp(10);
        }

    }

  

}
