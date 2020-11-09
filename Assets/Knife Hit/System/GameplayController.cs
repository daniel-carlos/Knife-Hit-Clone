using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Knife currentKnife;
    public Transform knifeSpawnpoint;
    public Transform knifesContainer;

    public void ThrowCurrentKnife()
    {
        if(currentKnife == null) return;
        
        currentKnife.transform.parent = null;
        currentKnife.ThrowKnife(knifeSpawnpoint);
        if (knifesContainer.childCount > 0)
        {
            currentKnife = knifesContainer.GetChild(0).GetComponent<Knife>();
        }else{
            currentKnife = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: teste. aoague isso pelo amor de Deus
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ThrowCurrentKnife();
        }
    }
}
