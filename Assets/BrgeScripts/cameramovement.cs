using System.Collections;
using System.Collections.Generic;
//using IdyllicGames;

using UnityEngine;

public class cameramovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public List<GameObject> allplayers;
    public static cameramovement Instance;
    public bool aa;
    public GameObject firstbot, secondbot, thirdbot;
    public GameObject firstst, secondnd, third;
    public GameObject finalposofcamera;
    public bool ranks;
    public List<GameObject> rankpurpose;
    public GameObject level1, level2, level3;
    public int low, temp;
    public GameObject gamelost;
    public GameObject won;
    public GameObject pausePanel;
    int level;
    public GameObject UIPanel;
    public PanelController panelController;
    public GameObject canvas;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        level = PlayerPrefs.GetInt("LevelBrgs");

    }
    void Start()
    {
        GameObject uiPanels = Instantiate(UIPanel);
        panelController = uiPanels.GetComponent<PanelController>();
        aa = true;
        gamelost.SetActive(false);
        offset = transform.position - player.transform.position;
        firstbot.SetActive(false);
        secondbot.SetActive(false);
        thirdbot.SetActive(false);
        ranks = false;
        won.SetActive(false);
        low = -1;
         //nadeem
      // AdManager_IdyllicGames.ShowBanner(BannerAdPosition.BOTTOM);
    }
    void FixedUpdate()
    {
        if (aa)
        {
            transform.position = player.transform.position + offset;
        }
        if (ranks)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalposofcamera.transform.position, 150 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(25,0,0);
            if (transform.position == finalposofcamera.transform.position)
            {
               
                ranks = false;
            }
        }
    }
    public void BrgR_eliminater(List<GameObject> arethere)
    {
        List<GameObject> templist = new List<GameObject>();

        if (arethere.Count == 1)
        {
           // firstst = level3.GetComponent<Enemyr_Bridge>().colorgivento[0];
            //allplayers.Remove(firstst);
            List<GameObject> stagelist = new List<GameObject>();
            for (int i = 0; i < allplayers.Count; i++)
            {
                float a = 0, b = 0;
                a = allplayers[i].transform.position.y;
                for (int k = 0; k < allplayers.Count; k++)
                {
                    b = allplayers[k].transform.position.y;
                    if (a > b)
                    {
                        GameObject temp = allplayers[i];
                        allplayers[i] = allplayers[k];
                        allplayers[k] = temp;
                    }

                }
            }
            secondnd = allplayers[0];
            third = allplayers[1];
            
            if (arethere[0].CompareTag("Player"))
            {
                aa = false;
                ranks = true;

                finalposofcamera.GetComponent<Camera>().enabled = true;
                Invoke(nameof(showWin), 3);
                canvas.SetActive(false);
                // won.SetActive(true);
                firstbot.SetActive(true);
                arethere[0].SetActive(false);
            }
            else {
                //CASAds.instance?.ShowInterstitial();
               // gamelost.SetActive(true);
                panelController.LossPanel.SetActive(true);
                Debug.Log("Bridge Level Fail " + level);
                //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Fail, "Mode Bridge Runner", "Level " + level);
            }
              //BrgR_declarewin(firstst, secondnd, third);
            //nadeem
            //CASAds.instance?.ShowInterstitial();
                     //AdsManager.instance.ShowInterstitialWithoutConditions();
                 //AdManager_IdyllicGames.ShowInterstitial();
        }
        else
        {
            int tempcount = allplayers.Count;
            for (int i = 0; i < tempcount; i++)
            {
                if (!arethere.Contains(allplayers[i]))
                {
                    GameObject a = allplayers[i];
                    if (a.transform.tag == "Player")
                    {
                        panelController.LossPanel.SetActive(true);
                        //gamelost.SetActive(true);
                        Debug.Log("Bridge Level Fail " + level);
                        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Fail, "Mode Bridge Runner", "Level " + level);
                        aa = false;
                        // Time.timeScale = 0;
                        //nadeem
                        //AdsManager.instance.ShowInterstitialWithoutConditions();
                        //CASAds.instance?.ShowInterstitial();
                       //AdManager_IdyllicGames.ShowInterstitial();
                       
                    }
                    else
                    {
                        templist.Add(a);

                    }
                }
            }
            int j = templist.Count;
            for (int i = 0; i < j; i++)
            {
                GameObject a = templist[0];
                templist.Remove(a);
                a.SetActive(false);
            }
        }
    }

    public void showWin()
    {
        //CASAds.instance?.ShowInterstitial();
        panelController.WinPanel.SetActive(true);
        //won.SetActive(true);
        Debug.Log("Bridge LevelComplete " + level);
        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete, "Mode Bridge Runner", "Level " + level);
    }
    public void BrgR_declarewin(GameObject first, GameObject second, GameObject third)
    {

        firstbot.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = first.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        firstbot.SetActive(true);
        first.SetActive(false);
        secondbot.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = second.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        secondbot.SetActive(true);
        second.SetActive(false);
        thirdbot.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = third.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        thirdbot.SetActive(true);
        third.SetActive(false);

    }

    public void PauseGame(){
        panelController.PausePanel.SetActive(true);
        //pausePanel.SetActive(true);
        //CASAds.instance?.ShowInterstitial();
        Time.timeScale=0;
    }

    public void ResumeGame(){
        pausePanel.SetActive(false);
        //CASAds.instance?.ShowInterstitial();
        Time.timeScale=1;
    }
}
