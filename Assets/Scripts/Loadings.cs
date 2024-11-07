using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Loadings : MonoBehaviour
{
    public Image fillBar;
	public Text LoadingTxt;
	[SerializeField] GameObject LoadingPanel;
	public void Start()
	{
      LoadingTxt.text="0"+"%";
	 
	  StartCoroutine(Txt());
	  
	}
	public IEnumerator Txt()
	{
		 
			for (int i = 0; i < 101; i++)
			{
			 yield return new WaitForSeconds(0.07f);
			LoadingTxt.text=i.ToString()+"%";
		}
	}
	public void Update()
	{
		if(fillBar.fillAmount<9)
		{
	      fillBar.fillAmount += 0.06f * Time.deltaTime;
		}
	}
	
}
