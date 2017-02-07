using UnityEngine;
using System.Collections;

public class DemoRay : MonoBehaviour {

	// Use this for initialization
    private RaycastHit hit;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (Network.isServer)
        {
            Debug.DrawRay(transform.position, new Vector3(5, 0, 0));
            if (Physics.Raycast(transform.position, new Vector3(5, 0, 0), out hit, 5))
                if (hit.transform.tag == "RigthRayBox")
                    Debug.Log("Hit The Rigth Spot");
        }
        else if (Network.isClient)
        {
            Debug.DrawRay(transform.position, new Vector3(-5, 0, 0));
            if (Physics.Raycast(transform.position, new Vector3(-5, 0, 0), out hit, 5))
                if (hit.transform.tag == "LeftRayBox")
                    Debug.Log("Hit The Left Spot");
        }
	}
}
