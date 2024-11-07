using System.Collections;
using System.Collections.Generic;
//using IdyllicGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static Cinemachine.DocumentationSortingAttribute;

public class BridggingRace_UI : MonoBehaviour
{
   
   public GameObject loading;
   private void Start()
    {
        loading.SetActive(false);

       Debug.Log("Level bridges start "+ PlayerPrefs.GetInt("LevelBrgs"));
        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Mode Bridge Runner", "Level " + PlayerPrefs.GetInt("LevelBrgs"));
        Invoke(nameof(next), .5f);

    }
    public void next()
    {
        cameramovement.Instance.panelController.NextBtnClick = () => BrgR_nextlevel();
    }
    public void BrgR_nextlevel()
    {
        cameramovement.Instance.canvas.SetActive(true);
        StartCoroutine(BrgR_nextscen());
       
    }
    public void policy()
    {
       
    
    
    }
    void Update()
    {

    }
      public void BrgR_MainMenubutton()
    {
        StartCoroutine(BrgR_Menu());
        
    }
    public void BrgR_startbutton()
    {
        StartCoroutine(BrgR_ret());
        
    }
        IEnumerator BrgR_Menu()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);
        cameramovement.Instance.pausePanel.SetActive(false);
        loading.SetActive(true);
        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator BrgR_ret()
    {
       
        yield return new WaitForSeconds(0.2f);
        loading.SetActive(true);
      
        
       
        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

     public void ShowBrgR_skip()
     {
        //nadeem
       // AdsManager.instance.ShowRewardedAd(BrgR_CompletedMethod);
         //AdManager_IdyllicGames.ShowRewardBasedVideo(BrgR_CompletedMethod);
    }

    void BrgR_CompletedMethod()
    {
        if (PlayerPrefs.GetInt("brglevel") >=10)
        {
            PlayerPrefs.SetInt("brglevel", PlayerPrefs.GetInt("brglevel") + 1);
            int i = Random.Range(1, 10);
            PlayerPrefs.SetInt("THISLEVEL", i);
            SceneManager.LoadScene(i);

        }
        else
        {
            PlayerPrefs.SetInt("brglevel", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        // PlayerPrefs.SetInt("LevelUnlocked", 1);
    }
    public void BrgR_reload()
    {
        StartCoroutine(BrgR_reloadscen());
        
    }
    IEnumerator BrgR_reloadscen()
    {
        //CASAds.instance?.ShowInterstitial();
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.2f);
        cameramovement.Instance.gamelost.SetActive(false);
        loading.SetActive(true);
        cameramovement.Instance.pausePanel.SetActive(false);
        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator BrgR_nextscen()
    {
        
        yield return new WaitForSeconds(0.2f);
        cameramovement.Instance.won.SetActive(false);
        loading.SetActive(true);
        Debug.Log("bridge :" + PlayerPrefs.GetInt("brglevel",1));
        if (PlayerPrefs.GetInt("LevelBrgs") == PlayerPrefs.GetInt("brglevel", 1))
        {
            PlayerPrefs.SetInt("brglevel", PlayerPrefs.GetInt("brglevel", 1) + 1);
            PlayerPrefs.SetInt("LevelBrgs", PlayerPrefs.GetInt("LevelBrgs") + 1);
        }
        //PlayerPrefs.SetInt("brglevel", PlayerPrefs.GetInt("brglevel",1) + 1);
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetInt("BridgeeLevels", PlayerPrefs.GetInt("BridgeeLevels", 16) +1);
        Debug.Log("bridge after :" + PlayerPrefs.GetInt("brglevel"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
        //if (PlayerPrefs.GetInt("brglevel") >=10)
        //{
        //    PlayerPrefs.SetInt("brglevel", PlayerPrefs.GetInt("brglevel") + 1);
        //    int i = Random.Range(1, 10);
        //    PlayerPrefs.SetInt("THISLEVEL", i);
        //    SceneManager.LoadScene(i);
        //    Debug.Log("Level Index is :"+PlayerPrefs.GetInt("brglevel"));


            //}
        //else
        //{
        //    PlayerPrefs.SetInt("brglevel", PlayerPrefs.GetInt("brglevel") + 1);
        //    //PlayerPrefs.SetInt("brglevel", SceneManager.GetActiveScene().buildIndex + 1);
        //    //Mainr_Bridge.instance.LevelNumberForLock++;
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    //Debug.Log("Level Index is :"+PlayerPrefs.GetInt("brglevel"));
        //}
        //PlayerPrefs.SetInt("LevelUnlocked", 1);
    }
}
