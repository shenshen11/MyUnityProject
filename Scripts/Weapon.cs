using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    private Sprite sprite;
    private Color color;
    private int durability;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void ActivateWeapon(Sprite sprite, Color color, int durabilityValue, int damage)
    {

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        durability = durabilityValue;
        GetComponent<Attack>().damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            durability--;
            if(durability <= 0)
            {
                //Destroy(weapon);
                spriteRenderer.sprite = null;
                boxCollider.enabled = false;
                GetComponentInParent<Player>().SetHoldingWeaponToFalse();
            }
        }
    }
}
