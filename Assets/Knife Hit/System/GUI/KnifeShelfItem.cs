using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KnifeShelfItem : MonoBehaviour
{
    public Image sprite;
    public TMP_Text costText;

    public KnifeSpriteRegister info;
    public bool aquired = false;

    //Apenas depois de ter sua info preenchida
    public void SetupShelfItem(){
        sprite.sprite = (Resources.Load(info.id) as GameObject).GetComponentInChildren<SpriteRenderer>().sprite;
       
        costText.text = aquired? "Select":$"${info.cost}";
    }

    public void Select(){
        GetComponentInParent<KnifeStore>().SelectItem(info);
    }
}
