using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //stats
    public int Health = 100;

    public void TakeDamage(int damage)
    {

        Health -= damage;

        if (Health < 0)
        {
            Destroy(gameObject);
        }


    }
}
