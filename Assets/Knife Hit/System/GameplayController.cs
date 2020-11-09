using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
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

    [Header("Knife")]
    
    public float throwSpeed = 5f;
    private Knife currentKnife;
    public Knife knifePrefab;
    public Transform knifeSpawnpoint;
    public Transform knifesContainer;

    [Header("Log Object")]
    public TargetLog currentLog;

    public void ThrowCurrentKnife()
    {
        if (currentKnife == null) return;

        currentKnife.transform.parent = null;
        currentKnife.ThrowKnife(throwSpeed, knifeSpawnpoint);
        if (knifesContainer.childCount > 0)
        {
            currentKnife = knifesContainer.GetChild(0).GetComponent<Knife>();
        }
        else
        {
            currentKnife = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public GameplayLevel CurrentLevel()
    {
        return allLevels[currentLevel];
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
        GameplayLevel level = allLevels[currentLevel];
        currentLog = Instantiate(level.levelLog);
        for (int i = 0; i < level.startingAmmo; i++)
        {
            KnifeSpawn();
        }
        DrawFirstKnife();
    }

    public void FillLogWithStartingKnifes()
    {
        GameplayLevel level = allLevels[currentLevel];
        if (currentLog != null)
        {
            currentLog.SetupStartingKnifes(level.startingKnifes);
        }else{
            Debug.LogWarning("Gameplay Controller has not a currentLog");
        }
    }
}
