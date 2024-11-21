using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject Pause;
    public GameObject won;
    public int levelno;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        levelno = SceneManager.GetActiveScene().buildIndex - 1;
        aa = true;
        // gamelost.SetActive(false);
        offset = transform.position - player.transform.position;
        firstbot.SetActive(false);
        secondbot.SetActive(false);
        thirdbot.SetActive(false);
        ranks = false;
        won.SetActive(false);
        low = -1;
        //PlayerPrefs.SetInt("ShowInter", 0);

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
            transform.rotation = Quaternion.Euler(25, 0, 0);
            if (transform.position == finalposofcamera.transform.position)
            {
                ranks = false;
            }
        }
    }
    //public void ShowAd()
    //{
    //if (PlayerPrefs.GetInt("ShowInter") == 0)
    //{
    //    PlayerPrefs.SetInt("ShowInter", 1);
    //    if (Application.internetReachability != NetworkReachability.NotReachable)
    //    {
    // AdsManager.Instance.ShowPriorityInterstitial();
    //    }
    //}
    //}
    public void eliminater(List<GameObject> arethere)
    {
        //print("eliminater");
        List<GameObject> templist = new List<GameObject>();
        if (arethere.Count == 1)
        {
            // print("if eliminater");
            firstst = level3.GetComponent<Eliminater>().colorgivento[0];
            allplayers.Remove(firstst);
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
            if (firstst.tag == "Player" || secondnd.tag == "Player" || third.tag == "Player")
            {
                Invoke(nameof(Complete), 0.5f);
                // ShowAd();
                if (firstst.tag == "Player")
                {
                    ui_Changed.instance.LevelCompleteCoins(20);
                    //print("20 coins do");
                }
                else if (secondnd.tag == "Player")
                {
                    ui_Changed.instance.LevelCompleteCoins(15);
                    //print("15 coins doo");
                }
                else if (third.tag == "Player")
                {
                    ui_Changed.instance.LevelCompleteCoins(10);
                    //print("10 coins doo");
                }
            }
            else
            {
                Invoke(nameof(LevelFail), 0.5f);
                //  ShowAd();
                // print("if here");
            }
            declarewin(firstst, secondnd, third);
            // print("here");
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
                        aa = false;
                        // Time.timeScale = 0;
                        Invoke(nameof(LevelFail), 0.5f);
                        ui_Changed.instance.CurrentLvlNum.gameObject.SetActive(false);
                        ui_Changed.instance.PauseBtn.gameObject.SetActive(false);
                        //  ShowAd();
                        if (PlayerPrefs.GetInt("sounds") == 1)
                        {
                            gamelost.GetComponent<AudioSource>().Play();
                            //  print("if here");
                            //print("Level Fail");
                        }
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
    public void LevelFail()
    {
        if (gamelost.activeInHierarchy)
        {
            return;
        }
        //Time.timeScale = 0;
        gamelost.SetActive(true);
        ui_Changed.instance.LevelFailCoin();
        Pause.SetActive(false);
        //HereAdsManager.Instance.ShowAdMobBanner();
        //HereAdsManager.Instance.ShowPriorityInterstitial();
    }
    public void Complete()
    {
        ui_Changed.instance.CurrentLvlNum.gameObject.SetActive(false);
        ui_Changed.instance.PauseBtn.gameObject.SetActive(false);
        aa = false;
        ranks = true;
        won.SetActive(true);
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            won.GetComponent<AudioSource>().Play();
        }
        //Time.timeScale = 0;
        //HereAdsManager.Instance.ShowAdMobBanner();
    }
    public void declarewin(GameObject first, GameObject second, GameObject third)
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
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
