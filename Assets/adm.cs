using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adm : MonoBehaviour
{
    public AudioClip Sel; //����� �Ҹ��� ������ ����ϴ�.
    public AudioClip main; //����� �Ҹ��� ������ ����ϴ�.
    AudioSource myAudio; //AudioSorce ������Ʈ�� ������ ����ϴ�.
    public static adm instance;  //�ڱ��ڽ��� ������ ����ϴ�.
    void Awake() //Start���ٵ� ����, ��ü�� �����ɶ� ȣ��˴ϴ�
    {
        if (adm.instance == null) //incetance�� ����ִ��� �˻��մϴ�.
        {
            adm.instance = this; //�ڱ��ڽ��� ����ϴ�.
        }
    }
    void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>(); //AudioSource ������Ʈ�� ������ ����ϴ�.
    }
    public void PlaySound()
    {
        myAudio.PlayOneShot(Sel); //soundExplosion�� ����մϴ�.
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
