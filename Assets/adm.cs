using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adm : MonoBehaviour
{
    public AudioClip Sel; //재생할 소리를 변수로 담습니다.
    public AudioClip main; //재생할 소리를 변수로 담습니다.
    AudioSource myAudio; //AudioSorce 컴포넌트를 변수로 담습니다.
    public static adm instance;  //자기자신을 변수로 담습니다.
    void Awake() //Start보다도 먼저, 객체가 생성될때 호출됩니다
    {
        if (adm.instance == null) //incetance가 비어있는지 검사합니다.
        {
            adm.instance = this; //자기자신을 담습니다.
        }
    }
    void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>(); //AudioSource 오브젝트를 변수로 담습니다.
    }
    public void PlaySound()
    {
        myAudio.PlayOneShot(Sel); //soundExplosion을 재생합니다.
    }
    public void PlaySound2()
    {
        myAudio.PlayOneShot(main);
    }
    public void StopPlay()
    {
        myAudio.Stop();
    }
    void Update()
    {

    }

}
