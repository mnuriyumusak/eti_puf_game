using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour
{

    [Range(0.0f, 200.0f)]
    public int MaxMana;
    [Range(1.0f, 10.0f)]
    public float RechargeSpeed;
    public int UseSpeed;
    public float Player1Mana, Player2Mana;//Will be private

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Increase();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Decrease();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, (Screen.width / 2) - 150, (Screen.width / 24)),(int)Player1Mana + "/" + MaxMana);

        GUI.Box(new Rect((Screen.width / 2) + 150, 0, (Screen.width / 2) - 150, (Screen.width / 24)), (int)Player2Mana + "/" + MaxMana);

    }
    public void Increase()
    {
        if (Network.isServer)
        {
            if (Player1Mana >= MaxMana)
                Player1Mana = MaxMana;
            else
                 Player1Mana += RechargeSpeed * Time.deltaTime;
        }
        else
        {
            if (Player2Mana >= MaxMana)
                Player2Mana = MaxMana;
            else
                Player2Mana += RechargeSpeed * Time.deltaTime;
        }
    }
    public void Decrease()
    {
        if (Network.isServer)
        {
            if (Player1Mana <= 0 || UseSpeed > Player1Mana)
                Player1Mana = 0;
            else
                Player1Mana -= UseSpeed;
        }
        else
        {
            if (Player2Mana <= 0||UseSpeed>Player2Mana)
                Player2Mana = 0;
            else
                Player2Mana -= UseSpeed;
        }
    }
}
