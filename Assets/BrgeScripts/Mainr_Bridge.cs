using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using IdyllicGames;

public class Mainr_Bridge : MonoBehaviour
{
    public GameObject main, loading;
    public AudioSource audiosrce;
    void Start()
    {
        if(CASAds.instance)
        {
            CASAds.instance.HideMrecBanner();
        }
       main.SetActive(true);
        // loading.SetActive(false);
        // audiosrce.enabled=false;
        Invoke(nameof(DisableLoadingPanel), 10f);
    }

    void DisableLoadingPanel()
    {
       // loading.SetActive(false);
        //SceneManager.LoadScene(0);
    }

    public void BrgR_play()
    {
        //StartCoroutine(BrgR_playbutton());
        playButton();
     //AdManager_IdyllicGames.ShowBanner(BannerAdPosition.BOTTOM);

        if(CASAds.instance)
        {
            CASAds.instance.HideMrecBanner();
            CASAds.instance.ShowBanner(CAS.AdPosition.TopCenter);
        }
    }

    void playButton()
    {
        loading.SetActive(true);
        main.SetActive(false);
        // audiosrce.enabled = true;

        Invoke(nameof(LoadScene), 8f);
        //if (PlayerPrefs.GetInt("level") > 5)
        //{
        //    SceneManager.LoadScene(PlayerPrefs.GetInt("THISLEVEL"));
        //}
        //else
        //{
        //    SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
        //}
    }

    void LoadScene()
    {
        if (PlayerPrefs.GetInt("level") > 5)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("THISLEVEL"));
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
        }
    }
    IEnumerator BrgR_playbutton()
    {
        yield return new WaitForSeconds(0.2f);
        loading.SetActive(true);
        main.SetActive(false);
        audiosrce.enabled=true;
       
        yield return new WaitForSeconds(8.0f);
        if (PlayerPrefs.GetInt("level") > 5)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("THISLEVEL"));
            }
            else
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
            }

    }
    public void PrivacyPolicy()
    {
      //  Application.OpenURL("https://idyllicgamesstudio.blogspot.com/2022/12/idyllic-games-studio-privacy-policy.html");

        Application.OpenURL("https://orbitgamesglobal-privacy-policy.blogspot.com/");

    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Orbit+Games+Global");
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ogg.bridge");
    }
}
