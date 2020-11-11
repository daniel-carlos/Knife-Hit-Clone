using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutModal;

    public void OpenAboutModal(){
        aboutModal.SetActive(true);
    }

    public void CloseAboutModal(){
        aboutModal.SetActive(false);
    }
    
    public void Play(){
        SceneManager.LoadScene("Gameplay");
    }
    
    public void Store(){
        SceneManager.LoadScene("Store");
    }
}
