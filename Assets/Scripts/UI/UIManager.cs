using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //This class handels all UI actions

    //Singleton class
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return instance;

        }
    }

    public Text playerGemCountText;
    public Image selectionImg;

    public Text gemCountText;
    public Image[] healthBars;


    private void Awake()
    {
        instance = this;
    }


    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "Diamonds: " + gemCount;
    }

    public void NotEnoughDiamonds(int gemCount)
    {
        playerGemCountText.text = "Not enough Diamonds!  "+ "Diamonds: " + gemCount;
    }

    public void UpdateShopSelection(int yPos)
    {

        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);


    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {

        for(int i =0; i <= livesRemaining; i++)
        {

            if(i == livesRemaining) 
            {
                healthBars[i].enabled = false;
            }
        }

    }



}
