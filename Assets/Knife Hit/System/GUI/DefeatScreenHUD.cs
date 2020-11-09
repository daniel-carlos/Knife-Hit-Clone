using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefeatScreenHUD : MonoBehaviour
{

    GameplayController gameplay;
    public TMP_Text levelName;
    public TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = FindObjectOfType<GameplayController>();
    }

    // Update is called once per frame
    void Update()
    {
        levelName.text = gameplay.CurrentLevel().levelName;
        score.text = gameplay.TotalScore.ToString();
    }
}
