using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private FSM fsm;

    [Header("Player Control")]
    public bool playerControl = false;
    private bool initialized = false;
    public bool GetInitOk()
    {
        return initialized;
    }
    public void SetInitOk(bool ok)
    {
        initialized = ok;
    }

    [Header("Level")]
    public List<GameplayLevel> allLevels;
    private int currentLevel = 0;
    private int currentLevelScore = 0; //Escores adqiridos no level atual
    private int fruits = 0; //Escores adqiridos no level atual
    private int totalScore = 0; //Total de score acumulado de todos os levels
    private int levelTargetScore = 0; //score alvo para terminar o level


    [Header("Knife")]

    public float throwSpeed = 5f;
    private Knife currentKnife;
    public Knife knifePrefab;
    public Transform knifeSpawnpoint;
    public Transform knifesContainer;

    [Header("Log Object")]
    public TargetLog currentLog;

    public int TotalScore { get => totalScore; set => totalScore = value; }
    public int Fruits { get => fruits; set => fruits = value; }


    public float throwCooldownTime = 0.1f;
    bool cooldown = true;

    public void ThrowCurrentKnife()
    {
        if (currentKnife == null || !cooldown) return;

        // currentKnife.transform.parent = null;
        currentKnife.ThrowKnife(throwSpeed, knifeSpawnpoint, this);
        currentKnife = null;

        fsm.currentState.SendMessage("OnThrowKnife");

        cooldown = true;
        Invoke("RestoreCooldown", throwCooldownTime);
    }

    void RestoreCooldown(){
        cooldown = true;
    }

    //Return false if has no level to run in
    public bool NextLevel()
    {
        currentLevel++;
        return currentLevel < allLevels.Count;

    }

    public void OnKnifeHitLog(Knife knife)
    {
        TotalScore++;
        currentLevelScore++;
        fsm.currentState.SendMessage("OnKnifeHitLog");

        if (currentLevelScore == levelTargetScore)
        {
            fsm.currentState.SendMessage("OnLevelCleared");
        }
    }

    public void OnDefeat()
    {
        fsm.currentState.SendMessage("OnDefeat");
    }

    // Start is called before the first frame update
    void Start()
    {
        fsm = GetComponent<FSM>();
        Fruits = PlayerPrefs.GetInt("fruits");
    }

    public GameplayLevel CurrentLevel()
    {
        if (currentLevel < allLevels.Count)
            return allLevels[currentLevel];
        else
            return null;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: configurar um script externo para controlar os inputs
        if (playerControl)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                ThrowCurrentKnife();
            }
        }

        //CHEAT
        if(Input.GetKeyDown(KeyCode.P)){
            Fruits += Random.Range(1,20);
            PlayerPrefs.SetInt("fruits", Fruits);
        }
    }

    public void DrawFirstKnife()
    {
        if (knifesContainer.childCount > 0)
        {
            currentKnife = knifesContainer.GetChild(0).GetComponent<Knife>();
        }
        else
        {
            Debug.LogWarning("You\'re trying to draw a knife but container has run dry.");
        }
    }

    public Knife KnifeSpawn()
    {
        Knife knife = Instantiate(knifePrefab, knifeSpawnpoint.position, Quaternion.identity, knifesContainer);
        return knife;
    }

    public void LoadCurrentLevel()
    {
        currentLevelScore = 0;
        GameplayLevel level = allLevels[currentLevel];
        levelTargetScore = level.startingAmmo;
        currentLog = Instantiate(level.levelLog);
        currentLog.gameplayLevel = level;
        for (int i = 0; i < level.startingAmmo; i++)
        {
            KnifeSpawn();
        }
    }

    public void CollectFruit(int value)
    {
        Fruits += value;
        PlayerPrefs.SetInt("fruits", Fruits);
    }

    public void FillLogWithStartingKnifes()
    {
        GameplayLevel level = allLevels[currentLevel];
        if (currentLog != null)
        {
            currentLog.SetupStartingKnifes(level.startingKnifes);
        }
        else
        {
            Debug.LogWarning("Gameplay Controller has not a currentLog");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
