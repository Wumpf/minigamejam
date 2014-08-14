using UnityEngine;
using System.Collections;

public class Dest : MonoBehaviour {

    public bool win = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            GameSingelton.instance.mHealth++;
        }
    }
}
