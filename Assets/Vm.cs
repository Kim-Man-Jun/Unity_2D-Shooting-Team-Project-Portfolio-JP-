using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Vm : MonoBehaviour
{
    public bool isClear1 = false;
    public bool isClear2 = false;
    public bool isClear3 = false;
    public bool isClear4 = false;
    public bool isMain = true;
    public GameObject pv;
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        TotalGm.instance.isClear1 = false;
        TotalGm.instance.isClear2 = false;
        TotalGm.instance.isClear3 = false;
        TotalGm.instance.isClear4 = false;
        TotalGm.instance.isMain = true;
    }

    private void Awake()
    {
        pv.Play();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene("StartScene");
        //}
        if (pv.GetComponent<VideoPlayer>().isPlaying == false)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
  
}
