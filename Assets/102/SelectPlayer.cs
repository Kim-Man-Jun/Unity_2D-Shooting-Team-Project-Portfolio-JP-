using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    public GameObject main; //����ȭ�� �̹���
    public GameObject SPanel; //1�� ĳ�� �̹���
    public GameObject s1; //1�� ĳ�� �̹���
    public GameObject s2; // ���� ����
    public GameObject s3;
    public GameObject s4;
    public GameObject clear1;
    public GameObject clear2;
    public GameObject clear3;
    public GameObject clear4;


    public int SelectNum = 0; //ĳ�� ���� ��ȣ 0 > 1�� ���� 3 > 4��
    bool isSound=true;
   
    void Start()
    {
        Time.timeScale = 1f;
    }
    private void Awake()
    {
        SPanel.SetActive(false);
        Screen.SetResolution(1920, 1080, true);

    }
    // Update is called once per frame
    void Update()
    {

        if (TotalGm.instance.isMain == true)
        { //�ѹ��� ����ǰ��ϱ�����
            if(isSound == true) { 
            adm.instance.PlaySound2();
                isSound = false;
            }
            if (Input.anyKeyDown) //�ƹ�Ű�� ������
            {
                SPanel.SetActive(true);
                main.SetActive(false);
                TotalGm.instance.isMain = false;
                adm.instance.StopPlay();
            }
        }
        if(TotalGm.instance.isMain == false)
        {
            
            SPanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) //�Ʒ�Ű ������ ������
        {
            if(Input.GetKeyDown(KeyCode.Space) == false) { 
            SelectNum += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) //��Ű ������ �ö�
        {
            if (Input.GetKeyDown(KeyCode.Space) == false)
            {
                SelectNum -= 1;
            }
        }
        if (SelectNum == 0)  //0�̸� 1�� ĳ������
        {
            if (TotalGm.instance.isMain == false)  //
            {

                s1p();

            }

        }
        if (SelectNum == 1)
        {
            s2p();
        }
        if (SelectNum == 2)
        {
            s3p();
        }
        if (SelectNum == 3)
        {
            s4p();
        }
        Select();
        Init();
        Cleardisplay();
        if (TotalGm.instance.isClear1 == true)
        {
            clear1.SetActive(true);
        }
        if (TotalGm.instance.isClear2 == true)
        {
            clear2.SetActive(true);
        }

        if (TotalGm.instance.isClear3 == true)
        {
            clear3.SetActive(true);
        }
        if (TotalGm.instance.isClear4 == true)
        {
            clear4.SetActive(true);
        }

        
        
    }
    void Init()
    {
        if (SelectNum > 3)
        {
            SelectNum = 0;
        }
        if (SelectNum < 0)
        {
            SelectNum = 3;
        }
    }
    void s1p()
    {
        s1.SetActive(true);
        s4.SetActive(false);
        s2.SetActive(false);
    }
    void s2p()
    {
        s2.SetActive(true);
        s3.SetActive(false);
        s1.SetActive(false);
    }
    void s3p()
    {
        s3.SetActive(true);
        s4.SetActive(false);
        s2.SetActive(false);
    }
    void s4p()
    {
        s4.SetActive(true);
        s1.SetActive(false);
        s3.SetActive(false);
    }

    void Select() 
    {
        if (TotalGm.instance.isMain == false)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(SelectNum == 0 && TotalGm.instance.isClear1 == false) { 
                adm.instance.PlaySound();
                }
                if (SelectNum == 1 && TotalGm.instance.isClear2 == false)
                {
                    adm.instance.PlaySound();
                }
                if (SelectNum == 2 && TotalGm.instance.isClear3 == false)
                {
                    adm.instance.PlaySound();
                }
                if (SelectNum == 3 && TotalGm.instance.isClear4 == false)
                {
                    adm.instance.PlaySound();
                }
                Invoke("SceneChanger",1f);
            }
        }
    }
    void SceneChanger() //SelectNum������ ����ȯ 
    {
       


        if (SelectNum == 0)
        {
           
            TotalGm.instance.playingPlayer = "player1";
            SceneManager.LoadScene("Wonjae");
        }
        if (SelectNum == 1)
        {
           
            TotalGm.instance.playingPlayer = "player2";
            SceneManager.LoadScene("KMJ_Stage");
        }
        if (SelectNum == 2)
        {

            TotalGm.instance.playingPlayer = "player3";
            SceneManager.LoadScene("LHS_Scene");
        }
        if (SelectNum == 3)
        { 
            if(TotalGm.instance.isClear4 == false) {
                
                TotalGm.instance.playingPlayer = "player4";
            SceneManager.LoadScene("102_Scene");
            }
        }
    }

    void Cleardisplay() 
    {
        
        
    }
    
}
