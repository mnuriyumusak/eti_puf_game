using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "ball")
        {
            //Kod buraya gelecek
            GameObject.FindGameObjectWithTag("SkorTab").GetComponent<GolSkor>().Gol(gameObject);
        }
    }
}
