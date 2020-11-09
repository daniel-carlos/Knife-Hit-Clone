using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelInfoHUD : MonoBehaviour
{
    public GameplayController gameplay;
    public TMP_Text levelTitle;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = GameObject.FindObjectOfType<GameplayController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplay.GetInitOk())
        {
            levelTitle.gameObject.SetActive(true);
            levelTitle.text = gameplay.CurrentLevel().levelName;
        }
        else
        {
            levelTitle.gameObject.SetActive(false);
        }
    }
}
