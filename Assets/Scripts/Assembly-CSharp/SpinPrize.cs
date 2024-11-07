using System.Collections;
using TMPro;
using UnityEngine;
//using IdyllicGames;

public class SpinPrize : MonoBehaviour
{
	public RectTransform arrow;

	public GameObject no_thanks_btn;

	public float speed_arrow;

	public float min_dist_arrow;

	public float max_dist_arrow;

	public TextMeshProUGUI coin_text,coin_text2;

	private Vector2 direction;

	public bool active;

	public bool click_reward_bool;

	public int total_earning;

	public int multi;

	public GameObject coins;

	public GameObject msg_reward_video;

	public GameObject nextpanel;

	private void Awake()
	{
         
	}

	private void Start()
	{
		direction = Vector2.right;
		StartCoroutine(no_thanks_wait());
		total_earning=Random.Range(15,50);
		coin_text2.text=total_earning.ToString();
	}

	private void Update()
	{
		if (active)
		{
			arrow_spin();
			multi_number_spin();
		}
	}

	public void arrow_spin()
	{
		float num = speed_arrow * Time.deltaTime;
		Vector3 localPosition = arrow.localPosition;
		if (direction == Vector2.right)
		{
			localPosition.x += num;
		}
		else
		{
			localPosition.x -= num;
		}
		if (localPosition.x >= max_dist_arrow)
		{
			direction = Vector2.left;
		}
		else if (localPosition.x <= min_dist_arrow)
		{
			direction = Vector2.right;
		}
		localPosition.x = Mathf.Clamp(localPosition.x, min_dist_arrow, max_dist_arrow);
		arrow.localPosition = localPosition;
	}

	public void multi_number_spin()
	{
		
		if (arrow.localPosition.x >= -250f && arrow.localPosition.x < -160f)
		{
			coin_text.text = total_earning * 2 + "M";
			multi = 2;
		}
		else if (arrow.localPosition.x >= -160f && arrow.localPosition.x < -52f)
		{
			coin_text.text = total_earning * 3 + "M";
			multi = 3;
		}
		else if (arrow.localPosition.x >= -52f && arrow.localPosition.x < 57f)
		{
			coin_text.text = total_earning * 5 + "M";
			multi = 5;
		}
		else if (arrow.localPosition.x >= 57f && arrow.localPosition.x < 165f)
		{
			coin_text.text = total_earning * 4 + "M";
			multi = 4;
		}
		else if (arrow.localPosition.x >= 165f && arrow.localPosition.x < 250f)
		{
			coin_text.text = total_earning * 2 + "M";
			multi = 2;
		}
	}

	public void button_Claim()
	{
		active = false;
		StartCoroutine(wait_ads_video());
	}

	public void button_NoThanks()
	{
		// SoundManager.instance.Play("coin");
		coins.SetActive(true);
		nextpanel.SetActive(true);
		active = false;
		StartCoroutine(wait_ads_interstatial());
	}

	private IEnumerator no_thanks_wait()
	{
		yield return new WaitForSeconds(2.0f);
		no_thanks_btn.SetActive(true);
	}

	private IEnumerator wait_ads_video()
	{
		coins.SetActive(true);
			yield return new WaitForSeconds(1f);
			coins.SetActive(false);
			//AdsManager.instance.ShowRewardedAd(CompletedMethod);
			
		// if (AdManager_IdyllicGames.isRewardedVideoAvailable())
		// {
		// 	// SoundManager.instance.Play("coin");
		// 	coins.SetActive(true);
		// 	yield return new WaitForSeconds(1f);
		// 	coins.SetActive(false);
		// 	 //nadeem
		// 	  //AdManager_IdyllicGames.ShowRewardBasedVideo(CompletedMethod);
		// }
		
		 if (click_reward_bool)
		{
			click_reward_bool = false;
			MonoBehaviour.print("reward video not available");
			msg_reward_video.SetActive(value: true);
			yield return new WaitForSeconds(1.5f);
			click_reward_bool = true;
			msg_reward_video.SetActive(value: false);
		}
	}
	void CompletedMethod()
	{
       int nbr = total_earning * (multi - 1);
	   nextpanel.SetActive(true);
    }

	private IEnumerator wait_ads_interstatial()
	{
		yield return new WaitForSeconds(1f);
		coins.SetActive(false);
		 //nadeem
		  //AdsManager.instance.ShowInterstitialWithoutConditions();
	   //AdManager_IdyllicGames.ShowInterstitial();
	}

	
	// private void didCompleteRewardedVideo(CBLocation location, int reward)
	// {
	// 	Debug.Log("Completed video with reward: " + reward);
	// 	int nbr = total_earning * (multi - 1);
	// 	otherUiManager.instance.increase_money(nbr);
	// }
}
