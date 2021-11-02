using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

                player = collision.GetComponent<Player>();

                if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            shopPanel.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        //0 = flame sword
        //1 = boots of flight
        //2 = key to castle

        switch (item)
        {

            case 0:
                currentSelectedItem = 0;
                currentItemCost = 200;
                UIManager.Instance.UpdateShopSelection(89);
                break;

            case 1:
                currentSelectedItem = 1;
                currentItemCost = 300;
                UIManager.Instance.UpdateShopSelection(0);
                break;

            case 2:
                currentItemCost = 100;
                currentSelectedItem = 2;
                UIManager.Instance.UpdateShopSelection(-92);
                break;

        }
     
    }

    public void BuyItem()
    {

        if(player.diamonds >= currentItemCost)
        {

            if(currentSelectedItem == 2)
            {
                GameManager.Instance.hasKeyToCastle = true;
            }

            player.diamonds -= currentItemCost;
            UIManager.Instance.OpenShop(player.diamonds);

        }
        else
        {
            UIManager.Instance.NotEnoughDiamonds(player.diamonds);
        }


    }

}
