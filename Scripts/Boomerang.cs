using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    public int direction = 1;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveBoomerang());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(6 * direction, 0, 2 * direction);
    }

    IEnumerator MoveBoomerang()
    {
        yield return new WaitForSeconds(2f);
        direction *= -1;
    }
}
