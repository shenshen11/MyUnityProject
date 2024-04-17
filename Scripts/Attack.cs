using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("调用了1");
        Enemy enemy = other.GetComponent<Enemy>();
        Player player = other.GetComponent<Player>();
        if(enemy != null)
        {
            enemy.TookDamage(damage);
        }
        if(player != null)
        {
            //Debug.Log("调用了2");
            player.TookDamage(damage);
        }
    }
}
