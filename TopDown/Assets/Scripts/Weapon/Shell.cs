using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float lifeTyme = 5;

   

    
    void Update()
    {
        lifeTyme -= Time.deltaTime;
        if (lifeTyme <= 0)
        {
            Destroy(gameObject);
        }
    }
}
