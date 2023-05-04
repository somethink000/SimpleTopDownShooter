using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiering;
    public BulletController bulletController;
    public Shell shell;
    public Shell magazinpref;

    public float bulletspeed = 2f;
    public int bulletdamage = 23;
    public int magazinSize = 25;
    public int magazin = 25;

    public float Spreed = 1;
    public float ReloadTime = 4;
    public float timebeervineshoot = 1;
    public float shootCounter;

    public Transform magazinPoint;
    public Transform shellPoint;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiering)
        {
            Fire();
        }
        if (shootCounter > 0)
        {
            shootCounter -= Time.deltaTime;
        }
        
        
            
        
    }

    private void Fire()
    {
        if (shootCounter <= 0 && magazin > 0)
        {
            shootCounter = timebeervineshoot;
            BulletController newBullet = Instantiate(bulletController, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletspeed;

            Shell newShell = Instantiate(shell, shellPoint.position, shellPoint.rotation) as Shell;
            newShell.GetComponent<Rigidbody>().AddForce(shellPoint.right * Random.Range(50f, 250f));

            magazin -= 1;

        }
    }
    public void Reload()
    {


        shootCounter = ReloadTime;
        magazin = magazinSize;

        Shell magaz = Instantiate(magazinpref, magazinPoint.position, magazinPoint.rotation) as Shell;
        magaz.GetComponent<Rigidbody>().AddForce(shellPoint.right * Random.Range(50f, 100f));

    }
}
