using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    //mowement
    public float speed = 6.0f;
    private bool watch;

    //stats
    public int Health = 100;
    public int Lvl = 1;
    public int MaxXp = 100;
    public int Xp;

    public Slider healthBar;
    public GameObject unitcanvas;


    //general
    public LayerMask groundMask;
    public GunController activeGun;
    private CharacterController controller;
    private Camera _cam;
    public Transform baseTransform;
    public Animator animator;
    private PlayerInput playerinput;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _cam = Camera.main;
        playerinput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 Lockinput = playerinput.actions["Lock"].ReadValue<Vector2>();
        Vector3 Lock = new Vector3(Lockinput.x, 0, Lockinput.y);
       

        if (unitcanvas.transform.rotation != _cam.transform.rotation)
        {
            unitcanvas.transform.rotation = _cam.transform.rotation;
        }
      


        if (Lock.x != 0 || Lock.z != 0)
        {
            
            watch = true;

            if (Lock.x  > 0.5f || Lock.x < -0.5f || Lock.z > 0.5f || Lock.z < -0.5f)
            {
                if (activeGun)
                    activeGun.isFiering = true;
            }
            else
            {
                if (activeGun)
                    activeGun.isFiering = false;
            }         
        }else
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

                controller.gameObject.transform.LookAt(new Vector3(transform.position.x + Lock.x, transform.position.y, transform.position.z + Lock.z));

        }




        Vector2 input = playerinput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        Vector3 moveDirection = baseTransform.right * move.x + baseTransform.forward * move.z;


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
    public void GetXp(int xp)
    {

        
        if (Xp + xp >= MaxXp)
        {
            Lvl += 1;
            MaxXp = MaxXp * Lvl;
            Xp += xp;
            Xp -= MaxXp;
        }else
        {
            Xp += xp;
            // healthBar.value -= xp;

        }




    }
}
