using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Names : MonoBehaviour
{
    public TextMeshPro textm;
    public void SetName(string name)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (textm)
            {
                string userName = PlayerPrefs.GetString("UserName", null);
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "You";
                }
                textm.text = userName;
            }
        }
        else
        {
            if (textm)
            {
                textm.text = name;
            }
        }
    }
}
