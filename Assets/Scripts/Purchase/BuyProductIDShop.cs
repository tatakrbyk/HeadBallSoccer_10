using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyProductIDShop : MonoBehaviour
{
    public static BuyProductIDShop instance;
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void Buy(GameObject pack)
    {
        /*if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            //SoundManager.Instance.buttonClick.Play();
        }*/
        /*
        if (pack.name == "Pack1")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack1");
        }

        if (pack.name == "Pack2")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack2");
        }

        if (pack.name == "Pack3")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack3");
        }

        if (pack.name == "Pack4")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack4");
        }
        if (pack.name == "Pack5")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack5");
        }
        if (pack.name == "Pack6")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack6");
        }
        if (pack.name == "PackSale1")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack7");
        }
        if (pack.name == "PackSale2")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack8");
        }
        if (pack.name == "PackSale3")
        {
            Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack9");
        }
        */
    }

    private void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("money").ToString();
    }
    public void ButtonExit()
    {
        SceneManager.LoadScene("Menu");
    }
}
