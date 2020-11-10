using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSprite : MonoBehaviour
{
    string knifeName;

    // Start is called before the first frame update
    void Start()
    {
        knifeName = PlayerPrefs.GetString("knife");
        if(knifeName == ""){
            knifeName = "kn_1";
        }

        GameObject knife = Instantiate<GameObject>((GameObject)Resources.Load(knifeName), transform.position,transform.rotation, transform);
        // knife.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
