using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
public class CEnemy : MonoBehaviour {
    
    public Vector3 m_Pos;
    public float m_speed=0.5f;
    public float mScale =0.1f;
    public bool isseen;
    Vector3 direction;
    float lifetime;
    public EnemySpotRangeScript spotting;
    
    void Start () {
        isseen = false;
        lifetime = 0;
        this.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);
    }
    
    // Update is called once per frame
    void Update () {
        if (spotting.spotted)
            Debug.Log("Player spotted");
        
        lifetime ++;
        if (isseen) 
        {
            m_speed = 0;
        }
        else 
        {
            if(spotting.spotted)
            {
                Vector3 p= GameObject.Find("Player").transform.position;
                direction = (this.transform.localPosition - p).normalized;
                
                this.transform.localPosition -=direction.normalized*m_speed;
                return;
            }
            
            
            m_speed = 0.1f;
            if(lifetime%2==0){
                int i = 1;
                
                if(lifetime%5==0)
                {
                    i=(Random.value * 10 > 5)?-1:1;
                    float rand=Mathf.Sin(Mathf.PI*Random.value);
                    float rand2=Mathf.Cos(Mathf.PI*Random.value);
                    direction = i*m_speed*new Vector3(rand,rand2,0);
                }
                this.transform.localPosition += direction;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            Player p = coll.gameObject.GetComponent<Player>();
            GameSingelton.instance.mHealth--;
        }
        
        
    }
}
