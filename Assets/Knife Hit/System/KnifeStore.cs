using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KnifeStore : MonoBehaviour
{
    public KnifeSpriteDatabase database;
    public TMP_Text balanceText;

    public Button buyButton;
    public TMP_Text buyButtonText;
    bool acquired = false;
    public int fruits;
    KnifeSpriteRegister info;
    public string inventory;

    private void Awake() {
        if (PlayerPrefs.GetString("inventory") == "")
        {
            PlayerPrefs.SetString("inventory", "|kn_1|");
        }
        if (PlayerPrefs.GetString("knife") == "")
        {
            PlayerPrefs.SetString("knife", "kn_1");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fruits = PlayerPrefs.GetInt("fruits");
        inventory = PlayerPrefs.GetString("inventory");
        string selectedKnife = PlayerPrefs.GetString("knife");

       

        balanceText.text = $"${fruits}";

        

        for (int i = 0; i < transform.childCount; i++)
        {
            KnifeShelfItem item = transform.GetChild(i).GetComponent<KnifeShelfItem>();
            item.info = database.knives[i];
            item.aquired = inventory.Contains(item.info.id);
            if(item.info.id == selectedKnife){
                item.GetComponent<Button>().Select();
                SelectItem(item.info);
            }
            item.SetupShelfItem();
        }
    }

    public void SelectItem(KnifeSpriteRegister info)
    {
        this.info = info;
        acquired = inventory.Contains(this.info.id);
        if (!acquired)
        {
            buyButtonText.text = $"Buy For\n${this.info.cost}";
            buyButton.interactable = fruits >= this.info.cost;
        }
        else
        {
            buyButton.interactable = true;
            buyButtonText.text = "Select";
        }
    }

    public void BuyItem()
    {
        if (!acquired)
        {
            PlayerPrefs.SetString("inventory", inventory + $"|{info.id}|");
            fruits -= info.cost;
            PlayerPrefs.SetInt("fruits", fruits);
        }
        PlayerPrefs.SetString("knife", info.id);
        BackToMenu();
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
