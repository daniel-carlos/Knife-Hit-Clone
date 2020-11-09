using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHUD : MonoBehaviour
{
    public GameplayController gc;
    private int startAmmo;
    private int currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        startAmmo = gc.knifesContainer.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        currentAmmo = gc.knifesContainer.childCount;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < startAmmo);
            transform.GetChild(i).GetComponent<Image>().color = startAmmo - currentAmmo > i ? Color.black : Color.white;
        }
    }
}
