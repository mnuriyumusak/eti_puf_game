using UnityEngine;
using System.Collections;
/*
Bu class oyunda basılan tuşların anlamlandırıldığı classtır. Butonların içindeki OnClick(), OnEntered() gibi methodların yerine
burdaki methodlar atanmaktadır. 
*/
public class myInput : MonoBehaviour {
    public MyCharacterController character1Controller;
    public UnityEngine.UI.Button butonRight, butonLeft; //butonların olduğu yer. 

    //Bu method jump butonuna basılı olduğunda çalışacak butondur. Eğer karakterin canjump değişkeni true ise jump yapabilir
    //değilse yapamaz. Ama havada olduğu durumda jump'a basınca ilerlemesi duruyor onun durmaması için havadayken canJump
    //false olmasına rağmen sağa veya sola gidiyorsa gittiği yönde devam etmesini sağlayan koda var aşağıda.
    public void jumpButtonOn()
    {
        if (character1Controller.canJump)
        {
            character1Controller.Jump();
        }
        else
        {
            if (!character1Controller.stopMovingLeft)
                moveLeftPushed();
            if (!character1Controller.stopMovingRight)
                moveRightPushed();
        }

    }

    //Karakter şimdi yerde düz bir biçimde sağa gidior da olabilir, havadayken zıplamış haldeyken sağa gitmek istiyor da olabilir.
    //eğer düz biçimdeyse ilk if'e giriyor değilse eğer sağa zıplamış sağa çapraz gidiyor da tekrar sağa basılmışsa bişey
    //olmayacak ama sola çapraz gidip sağa basmışsa yön değişmesi lazım yani havada çapraz giderken yön değiştirmiş oluyor
    //ona göre ayarlayan bir kod var. 
    public void moveRight()
    {
        if (!character1Controller.caprazSagaGidiyor)
        {
            if (!character1Controller.stopMovingRight && character1Controller.stopMovingLeft)
                character1Controller.movingActionItself(MyCharacterController.Directions.Forwards);
        }
        else
        {
            if (character1Controller.poi.rbOfCharacter.velocity.x < 0)
            {
                character1Controller.movingActionItself(MyCharacterController.Directions.Forwards);
                character1Controller.caprazdaYonDegisti = true;
            }
            else if (character1Controller.caprazdaYonDegisti)
            {
                character1Controller.movingActionItself(MyCharacterController.Directions.Forwards);
            }
        }

    }
    //moveRight metodunun aynısı.
    public void moveLeft()
    {
        if (!character1Controller.caprazSolaGidiyor)
        {
            if (!character1Controller.stopMovingLeft && character1Controller.stopMovingRight)
                character1Controller.movingActionItself(MyCharacterController.Directions.Backwards);
        }
        else
        {
            if (character1Controller.poi.rbOfCharacter.velocity.x > 0)
            {
                character1Controller.movingActionItself(MyCharacterController.Directions.Backwards);
                character1Controller.caprazdaYonDegisti = true;
            }
            else if (character1Controller.caprazdaYonDegisti)
            {
                character1Controller.movingActionItself(MyCharacterController.Directions.Backwards);
            }
        }

    }

    //bu method sağa git butonuna basıldığında etkinleştirilir
    public void moveRightPushed()
    {
        character1Controller.stopMovingRight = false;
        butonRight.Select();
    }

    public void moveLeftPushed()
    {
        character1Controller.stopMovingLeft = false;
        butonLeft.Select();
    }

    public void stopMovingRightFunc()
    {
        character1Controller.stopMovingRight = true;
    }
    public void stopMovingLeftFunc()
    {
        character1Controller.stopMovingLeft = true;
    }

    void Start()
    {

    }

    void Update()
    {
    }


    public void KnowMe()
    {
        character1Controller = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<MyCharacterController>();
    }
}
