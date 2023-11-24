using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalGm : MonoBehaviour
{
    public static TotalGm instance = null;

    public bool isClear1 = true;
    public bool isClear2 = true;
    public bool isClear3 = true;
    public bool isClear4 = false;
    public bool isMain = true; //�����̹��� Ȱ��ȭ �Ǿ��ִ���
    public string playingPlayer;


    void Awake()
    {
     
        if (instance == null)
            instance = this;

        // �ν��Ͻ��� �̹� �ִ� ��� ������Ʈ ����
        else if (instance != this)
            Destroy(gameObject);

        // �̷��� �ϸ� ���� scene���� �Ѿ�� ������Ʈ�� ������� �ʽ��ϴ�.
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isClear1 == true && isClear2 == true && isClear3 == true && isClear4 == true)
        {
            SceneManager.LoadScene("Ending");
        }
    }


}
