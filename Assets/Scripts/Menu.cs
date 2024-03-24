using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public static Menu instance;
    public GameObject panelLoadding;
    public GameObject panelTransit;
    public Image loadingBar;
    public static bool isLoadding;
    public static int mode;
    public TextMeshProUGUI loaddingText;
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        PlayerPrefs.DeleteAll();

        // TODO (taha): Show ads
        if (isLoadding == false)
        {
            StartCoroutine(WaitLoaddingMenu());
        }
        else
        {
            panelLoadding.SetActive(false);
        }
    }

    private void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("money").ToString();
        if(loadingBar.fillAmount < 1)
        {
            loadingBar.fillAmount += 0.01f;
        }
        if(loadingBar.fillAmount >= 1)
        {
            isLoadding = true;
        }
        loaddingText.text = (int)loadingBar.fillAmount * 100 + " %";   
    }

    private IEnumerator WaitLoaddingMenu()
    {
        yield return new WaitForSeconds(3f);
        panelTransit.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        panelLoadding.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        panelTransit.SetActive(false);
    }

    public void ExhibitionButton()
    {
        mode = (int)MODE.EXHIBITION;

        SceneManager.LoadScene("Exhibition");
    }
    public void WorldCupButton()
    {
        mode = (int)MODE.WORLDCUP;

        SceneManager.LoadScene("WC");
    }
    public void ShopButton()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ButtonAdd()
    {
        SceneManager.LoadScene("Shop");
    }

    public enum MODE
    {
        EXHIBITION, WORLDCUP
    }
}
