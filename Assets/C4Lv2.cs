using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class C4Lv2 : MonoBehaviour
{

    public GameObject Lv1;
    public GameObject Lv2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("충돌");
            //페이드 아웃
            Lv1.SetActive(false);
            Lv2.SetActive(true);
        }
    }
}
