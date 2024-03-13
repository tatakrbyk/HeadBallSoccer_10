using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITeam : MonoBehaviour
{
    private static UITeam _instance; public static UITeam Instance {  get { return _instance; } }

    public Sprite[] flagTeam;
    public string[] nameTeam;
    public Sprite[] star;

    public Sprite[] head;
    public Sprite[] body;
    public Sprite[] shoe;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
