using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject[] player;


    // Start is called before the first frame update
    void Awake()
    {
        int index = FindObjectOfType<GameManager>().characterIndex - 1;
        Instantiate(player[index], transform.position, transform.rotation);
    }

    // Update is called once per frame
}
