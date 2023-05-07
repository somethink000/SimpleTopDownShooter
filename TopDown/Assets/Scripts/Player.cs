using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    //mowement
    public float speed = 6.0f;
    private bool watch;

    //stats
    public int Health = 100;
    public int Armor;
    public int Hunger = 100;
    public int Water = 100;

    public Slider healthBar;
    public GameObject unitcanvas;


    //general
    public LayerMask groundMask;
    public GunController activeGun;
    private CharacterController controller;
    private Camera _cam;
    public Transform baseTransform;
    public Animator animator;   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (unitcanvas.transform.rotation != _cam.transform.rotation)
        {
            unitcanvas.transform.rotation = _cam.transform.rotation;
        }


        if (Input.GetMouseButtonDown(0))
        {
            watch = true;


            if (activeGun)
                activeGun.isFiering = true;

        }

        if (Input.GetMouseButtonUp(0))
        {
            watch = false;


            if (activeGun)
                activeGun.isFiering = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && activeGun)
        {
            activeGun.Reload();
        }

        if (watch)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out RaycastHit lockPoint, 1000f, groundMask))
            {

                controller.gameObject.transform.LookAt(new Vector3(lockPoint.point.x, transform.position.y, lockPoint.point.z));
            }
        }






        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;


        animator.SetFloat("speed", Vector3.ClampMagnitude(moveDirection, 1).magnitude);
        controller.Move(moveDirection * speed * Time.deltaTime);

    }

    public void TakeDamage(int damage)
    {

        Health -= damage;
        healthBar.value -= damage;
        if (Health < 0)
        {
            Destroy(gameObject);
        }


    }
}
