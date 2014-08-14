using UnityEngine;
using System.Collections;

public class GameSingelton : Singelton<GameSingelton>  {

	public GameObject[] mEnemy;
	public GUISkin mguiskin;
    public Dest d;

	const int kmaxHealth = 1;
	public int mHealth = kmaxHealth;
	void Start(){
		GameObject.DontDestroyOnLoad (this.gameObject);
	}
	// Update is called once per frame
	void update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}


	}

	void OnGUI(){
		GUI.skin = mguiskin;
		//GUILayout.Label (mHealth.ToString());
		GUI.skin.label.fontSize = 26;
		GUI.Label (new Rect (50, 50, 100, 30), mHealth.ToString ());

        if (mHealth == 0)
        {
            GUI.skin.label.fontSize=30;
            GUI.Label(new Rect(200,200,300,90),"You Lose!");
            //Application.Quit();
       
        }
        if(mHealth>1)
        {
            GUI.skin.label.fontSize=30;
            GUI.Label(new Rect(200,200,300,90),"You Win!");
        }

	}
}
