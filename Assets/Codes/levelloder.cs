using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelloder : MonoBehaviour
{
    //int levelNumber;

    void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        if (!PlayerPrefs.HasKey("coin"))
        {
            PlayerPrefs.SetInt("coin", 100);
        }
        if (!PlayerPrefs.HasKey("sounds"))
        {
            PlayerPrefs.SetInt("sounds", 1);
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.SetInt("vibrate", 1);
        }
        Invoke(nameof(Load), 1f);
        //AdsManager.Instance.ShowAdMobBanner();
    }

    public void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }


}
