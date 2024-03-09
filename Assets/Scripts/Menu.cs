using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject panelLoadding;
    public GameObject panelTransit;
    public Image loadingBar;
    public static bool isLoadding;
    public TextMeshProUGUI loaddingText;
    private void Start()
    {
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
        SceneManager.LoadScene("Exhibition");
    }
}
