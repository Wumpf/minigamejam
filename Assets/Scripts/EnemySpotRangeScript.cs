using UnityEngine;
using System.Collections;

public class EnemySpotRangeScript : MonoBehaviour
{
    
    public bool spotted = false;
    float timer;
    public float spotTime=0.1f;
    
    void Update(){
        
        timer -= Time.deltaTime;
        spotted = timer > 0;
        
    }
    
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            timer = spotTime;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            timer = spotTime;
    }
}
