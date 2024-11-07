using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSAcewne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DisableLoadingPanel), 10f);
    }

    void DisableLoadingPanel()
    {
        // loading.SetActive(false);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
