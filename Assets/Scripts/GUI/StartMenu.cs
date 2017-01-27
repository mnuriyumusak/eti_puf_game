using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
    public MacthMaking NetworkScript;



    private bool _openMenu=true;
    
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            _openMenu = !_openMenu;
	}

    void OnGUI()
    {  
        if (!_openMenu)
            return;
        
        GUI.BeginGroup(new Rect(Screen.width / 2.5f, Screen.height / 4, 1000, 1000));
      
        GUI.Box(new Rect(0, 0, 250, 300), "Menu");
        if (GUI.Button(new Rect(75, 50, 100, 25), "Match"))
        {
            NetworkScript.CheckGame();
            _openMenu = false;
        }
        if (GUI.Button(new Rect(75, 100, 100, 25), "Profile"))
            print("In Progres");
        if (GUI.Button(new Rect(75, 150, 100, 25), "Exit"))
            NetworkScript.Disconnect();
        
        GUI.EndGroup();
    }

}
