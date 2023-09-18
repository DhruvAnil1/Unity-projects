using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public  GameObject GameOverPanel;
    // Start is called before the first frame update

    public static int numberOfBananas;
    public Text BananasText;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        numberOfBananas = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (gameOver)  
      {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
      }
      BananasText.text ="Banana:"+ numberOfBananas;
    }
}
