using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class CEnemy : MonoBehaviour {
    
    public Vector3 m_Pos;
    public float m_speed;
    public float mScale =0.1f;
    public bool isseen;
    Vector3 direction;

    public EnemySpotRangeScript spotting;
    public Standing stand;
    float lastUpdate;
    void Start () {
        isseen = false;
        this.transform.localScale = new Vector3 (0.05f,0.05f,0.05f);
    }
    
    // Update is called once per frame
    void Update () {
        if (spotting.spotted)
            Debug.Log("Player spotted");
        if (stand.isSeen)
            Debug.Log("Stand Still");

        if (stand.isSeen) 
        {
            m_speed = 0;
            return;
        }
        else 
        {
            m_speed =1f;
            if(spotting.spotted)
            {
                Vector3 p= GameObject.Find("Player").transform.position;
                direction = (this.transform.localPosition - p).normalized;
                
                this.transform.localPosition -=direction.normalized*m_speed*Time.deltaTime;
                return;
            }
            

           // if(lifetime%2==0){
               
                
                if(Time.time-lastUpdate>0.5f)
                {
                   
                    float rand=Mathf.Sin(2*Mathf.PI*Random.value);
                    float rand2=Mathf.Cos(2*Mathf.PI*Random.value);
                    direction = new Vector3(rand,rand2,0);
                    lastUpdate=Time.time;
                }
                this.transform.localPosition += direction*m_speed*Time.deltaTime;
           // }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            Player p = coll.gameObject.GetComponent<Player>();
            GameSingelton.instance.mHealth--;
			GameObject.Destroy (this.gameObject);
           // Debug.Log("Delete this");
        }
    }

 
        
}
