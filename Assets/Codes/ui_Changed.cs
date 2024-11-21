using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ui_Changed : MonoBehaviour
{
    public cameramovement CamMov;
    public int no;
    public GameObject[] chara;
    public GameObject[] select, selected;
    public GameObject[] pricetag;

    public GameObject player;
    public GameObject[] bots;
    public Text mainmenucoins;
    public Text LvlNumCom;
    public Text LvlNumFail;
    public Text CurrentLvlNum;
    public Text multiplierText;
    public bool music;
    public bool sound;
    public bool vibration;
    public GameObject musicon;
    public GameObject musicoff;
    public GameObject soundon;
    public GameObject soundoff;
    public GameObject vibrationOn;
    public GameObject vibrationOff;
    public GameObject shop;
    public GameObject complete;
    public GameObject mainmenu;
    // public GameObject levelSelction;
    //public Text CompeleteCoins;
    public Text On_failed, On_complete;

    public GameObject RewardMeter;
    public GameObject videoButton;
    public GameObject no_thanksBtn;
    public GameObject ContinueBtn;
    public GameObject RetryBtn;
    //   public GameObject NeedleOrigin;
    public GameObject LevelCompleteShop, FailCoins, PauseBtn, PauseMenu, CharacterPanal, homeBtn, noCoinsAnim, neddleParent;
    public Text CompleteCoinTxt, FailCoinstxt;

    public Text Obj_text;
    public InputField Display;

    public int Rewardx;
    int levelindex, stageCoins = 0;

    public static ui_Changed instance;

    DOTweenAnimation noCoinsTween;
    [SerializeField] Animator neddleAnim;

    //Transform playersParent;

    public void Awake()
    {
        instance = this;
        //playersParent = GameObject.FindGameObjectWithTag("PlayersParent").transform;
        //for (int i = 0; i < 6; i++)
        //{
        //    chara[i] = playersParent.GetChild(i).gameObject;
        //}
        player = chara[PlayerPrefs.GetInt("selection")];
        chara[PlayerPrefs.GetInt("selection")].SetActive(true);
        CamMov.player = chara[PlayerPrefs.GetInt("selection")];
        CamMov.allplayers[0] = chara[PlayerPrefs.GetInt("selection")];

    }
    public void Start()
    {
        Time.timeScale = 1;
        //PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 100000); 
        CurrentLvlNum.gameObject.SetActive(false);
        PauseBtn.gameObject.SetActive(false);
        noCoinsTween = noCoinsAnim.GetComponent<DOTweenAnimation>();
        Obj_text.text = PlayerPrefs.GetString("UserName");

        if (PlayerPrefs.GetInt("FTR") == 1)
        {
            PlayerPrefs.SetInt("FTR", 0);
            mainmenu.SetActive(false);
            StartGame();
            //AdsManager.Instance.hideBanner();
        }
        else
        {

            mainmenu.SetActive(true);
            //AdsManager.Instance.hideBanner();
        }
        PlayerPrefs.SetInt("purchase" + 0, 1);
        for (int i = 0; i < select.Length; i++)
        {
            if (PlayerPrefs.GetInt("purchase" + i) == 1)
            {
                select[i].SetActive(true);
                pricetag[i].SetActive(false);
            }
        }
        select[PlayerPrefs.GetInt("selection")].SetActive(false);
        selected[PlayerPrefs.GetInt("selection")].SetActive(true);

        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            soundoff.SetActive(false);
            soundon.SetActive(true);
        }
        else
        {
            soundoff.SetActive(true);
            soundon.SetActive(false);
        }
        if (PlayerPrefs.GetInt("vibrate") == 0)
        {
            vibrationOff.SetActive(true);
            vibrationOn.SetActive(false);
        }
        else
        {
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("music") == 1)
        {
            musicoff.SetActive(false);
            musicon.SetActive(true);
        }
        else
        {
            musicoff.SetActive(true);
            musicon.SetActive(false);
        }
        levelindex = PlayerPrefs.GetInt("level");
        //LvlNumCom.text = levelindex.ToString();
        //LvlNumFail.text = levelindex.ToString();
        CurrentLvlNum.text = "LEVEL " + (levelindex).ToString();
        On_complete.text = "LEVEL " + (levelindex).ToString();
        On_failed.text = "LEVEL " + (levelindex).ToString();
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        shop.GetComponent<Button>().interactable = true;
        UpdateText();
        //playBtn.SetActive(true);
        //pricetag.SetActive(false);
    }

    public void create()
    {
        //Obj_text.text = Display.text;
        PlayerPrefs.SetString("UserName", Obj_text.text);
        PlayerPrefs.Save();
    }
    #region store
    //public void chnum(int num)
    //{
    //    no = num;
    //    print(no);
    //}
    //public void purchase()
    //{
    //    if (PlayerPrefs.GetInt("coins") >= chPrice)
    //    {
    //        //for (int i = 0; i < pricetag.Length; i++)
    //        //{
    //        PlayerPrefs.SetInt("purchase" + no, 1);
    //        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - chPrice);
    //        pricetag.SetActive(false);
    //        playBtn.SetActive(true);
    //        characterLock[no].SetActive(false);
    //        select[no].SetActive(true);
    //        print(no);
    //        //}
    //        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    //    }

    //}
    public void Multiplier(int num)
    {
        Rewardx = num;
        multiplierText.text = num + "X";
    }
    public void chnum(int num)
    {
        no = num;
    }
    public void purchase(int price)
    {
        if (PlayerPrefs.GetInt("coins") >= price)
        {
            pricetag[no].SetActive(false);
            select[no].SetActive(true);
            PlayerPrefs.SetInt("purchase" + no, 1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - price);
            UpdateText();
        }
        else
        {
            noCoinsAnim.SetActive(true);
            noCoinsTween.DORestart();
            //Debug.Log("Not Enough Diamonds");
        }
    }

    int currentChNum = 0;
    public void WatchVideoToUnlockCharacter(int num)
    {
        currentChNum = num;
        //HereAdsManager.Instance.UnlockCharacter();
    }

    public void PurchaseOnAd()
    {
        pricetag[currentChNum].SetActive(false);
        select[currentChNum].SetActive(true);
        PlayerPrefs.SetInt("purchase" + currentChNum, 1);
    }

    public void Selection(int sel)
    {
        //print("here it is");
        for (int i = 0; i < select.Length; i++)
        {
            if (PlayerPrefs.GetInt("purchase" + i) == 1)
            {
                selected[i].SetActive(false);
                select[i].SetActive(true);
            }
        }
        select[sel].SetActive(false);
        selected[sel].SetActive(true);
        chara[sel].SetActive(true);
        player = chara[sel];
        CamMov.player = chara[sel];
        CamMov.allplayers[0] = chara[sel];
        PlayerPrefs.SetInt("selection", sel);
    }
    #endregion store
    public void SelectLevel(int levelno)
    {
        PlayerPrefs.SetInt("FTR", 1);
        //print("SelectLevel");
        SceneManager.LoadSceneAsync(levelno);
        AfterSelection();

    }
    void AfterSelection()
    {
        // levelSelction.gameObject.SetActive(false);
        StartGame();
    }

    public void nextlevel()
    {
        StartGame();
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        if (PlayerPrefs.GetInt("level") > 19)
        {
            int i = Random.Range(8, 19);
            SceneManager.LoadSceneAsync(i);
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //AdsManager.Instance.hideBanner();
    }

    public void restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        mainmenu.SetActive(false);
        StartGame();
    }
    public void shopoff()
    {
        shop.GetComponent<Button>().interactable = false;
    }

    public void reload()
    {
        Invoke(nameof(restartlevel), 0.5f);
        PlayerPrefs.SetInt("FTR", 1);
        //AdsManager.Instance.ShowChartBoostInterstitial();
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        mainmenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }
    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
        mainmenu.SetActive(true);
        PauseMenu.SetActive(false);
        PauseBtn.SetActive(false);
        CurrentLvlNum.gameObject.SetActive(false);
        //AdsManager.Instance.hideBanner();

    }
    public void PlayButtonMusic()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            GetComponent<AudioSource>().Play();
        }

    }
    public void MusicOnOff()
    {
        music = !music;
        //print(music);
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicoff.SetActive(false);
            musicon.SetActive(true);
            PlayerPrefs.SetInt("music", 1);
            AudioManager.instance.Play();
        }
        else
        {
            musicoff.SetActive(true);
            musicon.SetActive(false);
            PlayerPrefs.SetInt("music", 0);
            AudioManager.instance.Pause();
        }
    }
    public void VibrateOn()
    {
        PlayerPrefs.SetInt("vibrate", 0);
        vibrationOff.SetActive(true);
        vibrationOn.SetActive(false);
    }
    public void VibrateOff()
    {
        PlayerPrefs.SetInt("vibrate", 1);
        vibrationOff.SetActive(false);
        vibrationOn.SetActive(true);
        Vibration.Vibrate(100);
    }
    public void SoundOnOff()
    {
        sound = !sound;
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            soundoff.SetActive(false);
            soundon.SetActive(true);
            //print(0);
            PlayerPrefs.SetInt("sounds", 1);
            //print(true);
        }
        else
        {
            soundoff.SetActive(true);
            soundon.SetActive(false);
            //print(1);
            PlayerPrefs.SetInt("sounds", 0);
            //print(false);
        }
    }
    public void PlayButtonSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void WatchAdToGetCoins()
    {
        //HereAdsManager.Instance.CoinReward();
        CoinRewarded();
    }
    public void CoinRewarded()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 20);
        UpdateText();
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void StartGame()
    {
        CurrentLvlNum.gameObject.SetActive(true);
        PauseBtn.gameObject.SetActive(true);
        mainmenu.gameObject.SetActive(false);
        player.GetComponent<PLayerController>().enabled = true;

        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].GetComponent<AIcontroller>().enabled = true;
        }
        //AdsManager.Instance.hideBanner();
    }
    public void PlayBtn()
    {
        mainmenu.SetActive(false);
        StartGame();
        int sceneNumber = PlayerPrefs.GetInt("level");
        if (sceneNumber > 19)
            sceneNumber = Random.Range(8, 19);
        PlayerPrefs.SetInt("FTR", 1);
        SceneManager.LoadScene(sceneNumber);
        //HereAdsManager.Instance.HideAdMobBanner();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        //HereAdsManager.Instance.ShowAdMobBanner();
    }
    public void Stay_paused()
    {
        Time.timeScale = 0;
        //PauseMenu.SetActive(true);
        //AdsManager.Instance.showBanner();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        //HereAdsManager.Instance.HideAdMobBanner();
    }
    public void XReward()
    {
        neddleAnim.enabled = false;
        Xrewarded();
        //HereAdsManager.Instance.CoinRewardX();
    }
    public void Xrewarded()
    {
        int newCoins = stageCoins * Rewardx;
        CompleteCoinTxt.text = newCoins.ToString();
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + newCoins);
        //print("2x");
        mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        UpdateText();
        Afterwatch();
        //print(PlayerPrefs.GetInt("coins"));
    }
    public void SkipDoubleCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + stageCoins);
        //print("2x");
        mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
        UpdateText();
        Afterwatch();
        //HereAdsManager.Instance.ShowPriorityInterstitial();
    }
    public void LevelCompleteCoins(int coins)
    {
        stageCoins = coins;
        //PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coins);
        LevelCompleteShop.SetActive(true);
        CompleteCoinTxt.text = coins.ToString();
        //HereAdsManager.Instance.ShowAdMobBanner();
        //CompeleteCoins.text = PlayerPrefs.GetInt("coins").ToString();
        //print(PlayerPrefs.GetInt("coins"));
        //mainmenucoins.gameObject.SetActive(false);
    }
    public void LevelFailCoin()
    {
        FailCoins.gameObject.SetActive(true);
        FailCoinstxt.text = "0".ToString();
        //HereAdsManager.Instance.ShowAdMobBanner();
    }

    //public void Removeads()
    //{
    //    AdsManager.Instance.inAppDeals(0);
    //}
    //public void Deal1()
    //{
    //    AdsManager.Instance.inAppDeals(1);
    //}
    //public void Deal2()
    //{
    //    AdsManager.Instance.inAppDeals(2);
    //}
    //public void Deal3()
    //{
    //    AdsManager.Instance.inAppDeals(3);
    //}
    //public void Deal4()
    //{
    //    AdsManager.Instance.inAppDeals(4);
    //}
    public void unlockall()
    {
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("purchase" + i, 1);
        }
    }
    public void fCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 500);
        UpdateText();
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void thousandcoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1000);
        //mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void Fifteen()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1500);
        UpdateText();

        // mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();

    }
    public void SelectionBack()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);

    }
    private void UpdateText()
    {
        mainmenucoins.text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void no_thanksAd()
    {
        //AdsManager.Instance.ShowPriorityInterstitial();
    }
    public void Afterwatch()
    {
        neddleParent.SetActive(false);
        videoButton.gameObject.SetActive(false);
        no_thanksBtn.gameObject.SetActive(false);
        ContinueBtn.gameObject.SetActive(true);
        RetryBtn.gameObject.SetActive(true);
        homeBtn.SetActive(true);
    }
    public void Rate_Us()
    {
        Application.OpenURL("amzn://apps/android?p=" + Application.identifier);
    }

    public void ShowBanner()
    {
        //HereAdsManager.Instance.ShowAdMobBanner();
    }
    public void HideBanner()
    {
        //HereAdsManager.Instance.HideAdMobBanner();
    }
}

