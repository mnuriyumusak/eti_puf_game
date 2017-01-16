using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GolSkor : MonoBehaviour {

    //Hangi kalaeye gol atılırsa o kale bu scripten karşı tarafın scrounu arttıracak

   // NetworkView netwo = new NetworkView();//Null Reference
    
    private int _p1=0,_p2=0;
    public int MaxGoal = 3;
    private bool CanScore = true;// Oyun bitiminde ne yapılacaksa ona göre gerek olmaya bilir örn. yeni raound olucaksa kalmalı
    public SpriteRenderer P1,P2;
    public Sprite Increment;
    public Sprite[] NumberList;
	// Use this for initialization


	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (_p1 == MaxGoal || _p2 == MaxGoal)
        {
            P1.sprite = NumberList[_p1];//Skor sıfırlanmadığı için bir sonraki girişte out of array index hatası veriyor
            P2.sprite = NumberList[_p2];//Maximum skora erişilince yapılacaklara göre buraya ihtiyaç olmayabilir
            //  örn. max olunca yeni level yükle

            Debug.Log("Win");//Kazandıktan sonra ne yapılacaksa onu yaptır
            CanScore = false;
        }
        else
        {
            P1.sprite = NumberList[_p1];
            P2.sprite = NumberList[_p2];
            CanScore = true;
        }
       
	}
    public void Gol(GameObject Who)
    {
       
         if (Who.tag=="Left")//Sol kalenin tagı
         {

            _p1++;
        //    GetComponent<NetworkView>().RPC("SetScore", RPCMode.All, _p1, _p2);
            GetComponent<NetworkView>().RPC("ScoreUpAnimP1", RPCMode.All);
        }
        else
        {
            _p2++;
          //  GetComponent<NetworkView>().RPC("SetScore", RPCMode.All, _p1, _p2);
            GetComponent<NetworkView>().RPC("ScoreUpAnimP2", RPCMode.All);

        }
    }
   
  [RPC]  void SetScore(int p1, int p2)
    {
        _p1 = p1;
        _p2 = p2;
    }
  [RPC]
  void ScoreUpAnimP1()
  {
      if (!CanScore)
          return;
      GameObject go = GameObject.FindGameObjectWithTag("ScoreUpP1").gameObject;
      go.GetComponent<SpriteRenderer>().sprite = Increment;
      go.GetComponent<Animation>().Play("ScoreUp");
      GetComponent<NetworkView>().RPC("SetScore", RPCMode.All, _p1, _p2);
  }
    [RPC]
  void ScoreUpAnimP2()
  {
      if (!CanScore)
          return;
      GameObject go2 = GameObject.FindGameObjectWithTag("ScoreUpP2").gameObject;
      go2.GetComponent<SpriteRenderer>().sprite = Increment;
      go2.GetComponent<Animation>().Play("ScoreUp");
      GetComponent<NetworkView>().RPC("SetScore", RPCMode.All, _p1, _p2);
  }

    void OnGUI()
    {


    }

}