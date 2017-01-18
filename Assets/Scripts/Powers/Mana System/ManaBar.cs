using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour
{

    public float BarPosX, BarPosY;
    public Texture Fill;
    [Range(0.0f, 200.0f)]
    public float MaxMana;
    [Range(1.0f, 5.0f)]
    public float RechargeSpeed;
    public float _p1, _p2;//Will be private

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Increase();

    }

    void OnGUI()
    {
        GUI.Box(new Rect(BarPosX, BarPosY, (Screen.width / 2) - 150, (Screen.width / 24)), Fill);
        // GUI.Box(new Rect((Screen.width / 2), BarPosY, (Screen.width / 2)- 150, (Screen.width / 24)), "Player A");
        GUI.Box(new Rect((Screen.width / 2) + 150, BarPosY, (Screen.width / 2) - 150, (Screen.width / 24)), Fill);

    }
    public void Increase()
    {
        if (Network.isServer)
            _p1++;
        else
            _p2++;
    }
}
