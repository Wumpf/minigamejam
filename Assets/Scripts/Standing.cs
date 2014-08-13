using UnityEngine;
using System.Collections;

public class Standing : MonoBehaviour {

    public bool isSeen=false;
    float timer;
    float isSeenTimer = 0.1f;

    void Update(){
        
        timer -= Time.deltaTime;
        isSeen = timer > 0;
        
    }



    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "InvisTriangle")
            timer = isSeenTimer;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "InvisTriangle")
            timer = isSeenTimer;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "InvisTriangle")
            isSeen=false;
    }
}
