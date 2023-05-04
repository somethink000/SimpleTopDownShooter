using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    //mowement
    public float speed = 6.0f;
   
    //stats
    public int Health = 100;
    public int Armor;
    public int Hunger = 100;
    public int Water = 100;
    



    //general
    public LayerMask groundMask;
    public GunController activeGun;
    private CharacterController controller;  
    private Camera _cam;
    public Transform baseTransform;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out RaycastHit lockPoint, 1000f, groundMask))
            {
                controller.gameObject.transform.LookAt(new Vector3(lockPoint.point.x, transform.position.y, lockPoint.point.z));
            }

            activeGun.isFiering = true;



        }

        if (Input.GetMouseButtonUp(0))
        {
            activeGun.isFiering = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            activeGun.Reload();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = baseTransform.right * horizontal + baseTransform.forward * vertical;

        controller.Move(moveDirection * speed * Time.deltaTime);
      
    }

    public void TakeDamage(int damage)
    {

        Health -= damage;

        if (Health < 0)
        {
            Destroy(gameObject);
        }


    }
}
