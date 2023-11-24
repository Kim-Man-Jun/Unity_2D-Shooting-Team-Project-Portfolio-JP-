using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{
    //���� ü�� ����ä
    public static float BossNowHp;
    public float BossMaxHp = 20000;
    public Image BossNowHpBar;

    //���� ź�� ����
    public GameObject Bullet1;
    public GameObject Bullet2;

    //���� �߻籸 ����
    public Transform FirePos1 = null;
    public Transform FirePos2 = null;
    public Transform FirePos3 = null;
    public Transform FirePos4 = null;

    //���� ������ ����Ʈ
    public GameObject BoomEffect;

    //������ �븮�� ���
    public Transform PlayerPos;

    //ź�� ��Ÿ�� �ֱ�
    float curTime = 0;
    float curTime2 = 0;
    float curTime3 = 0;

    //������ ���Խ� �ѷ��ִ� ȸ����
    public GameObject HpItem;
    int Phase = 0;

    //���� ���� ���� ������
    public static int BossAppear = 0;
    [SerializeField] GameObject textBossWarning;
    [SerializeField] GameObject BossMaxHpBar;
    [SerializeField] GameObject Boss;

    public static bool BossClear = false;

    //���� ����
    AudioSource BS;
    AudioSource Oneshot;
    public AudioClip Patern1;
    public AudioClip Patern2;
    public AudioClip Patern3;
    public AudioClip Patern4;

    public AudioClip StageClaer;
    public AudioClip BossBoom;

    private void Awake()
    {
        textBossWarning.SetActive(false);
        BossMaxHpBar.SetActive(false);
        Boss.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //���� ü�� ����
        BossNowHp = BossMaxHp;
        BS = GetComponent<AudioSource>();
        Oneshot = GetComponent<AudioSource>();

        BS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //���� ü�� ����
        BossNowHpBar.fillAmount = (float)BossNowHp / (float)BossMaxHp;

        //�� �̵����� �ɱ�
        if (transform.position.x <= -1.9f)
        {
            transform.position = new Vector3(-1.9f, transform.position.y, 0);
        }
        if (transform.position.x >= 1.9f)
        {
            transform.position = new Vector3(1.9f, transform.position.y, 0);
        }

        //�Ϲ��� ȣ�� ź��
        if (BossNowHp <= 20000 && BossNowHp > 18000)
        {
            //1�� pos �����̻��� �߻�
            curTime += Time.deltaTime;

            if (curTime >= 0.7f)
            {
                BossHoming();
                curTime = 0;

                Oneshot.PlayOneShot(Patern1);
            }

            //2�� pos �����̻��� �߻�
            curTime2 += Time.deltaTime;

            if (curTime2 >= 0.9f)
            {
                BossHoming();
                curTime2 = 0;

                Oneshot.PlayOneShot(Patern1);
            }
        }

        //��ä�÷� ��� ź��
        if (BossNowHp <= 18000 && BossNowHp > 12000)
        {
            //������ ����
            if (Phase == 0)
            {
                StartCoroutine("HpItemDrop");
            }

            //FirePos 3�� �߻� 360��
            curTime += Time.deltaTime;

            if (curTime >= 0.5f)
            {
                List<Transform> Bullets = new List<Transform>();

                for (int i = 0; i < 360; i += 20)
                {
                    if ((i >= 340 && i < 360) || (i >= 0 && i <= 40))
                    {
                        GameObject go = Instantiate(Bullet1);

                        go.transform.position = FirePos3.position;

                        Destroy(go, 5f);

                        Bullets.Add(go.transform);

                        go.transform.rotation = Quaternion.Euler(0, 0, i);
                    }
                }
                curTime = 0;
                Oneshot.PlayOneShot(Patern2);
            }

            //FirePos 4�� �߻� 360��
            curTime2 += Time.deltaTime;

            if (curTime2 >= 0.5f)
            {
                List<Transform> Bullets = new List<Transform>();

                for (int i = 0; i < 360; i += 20)
                {
                    if ((i >= 340 && i < 360) || (i >= 0 && i <= 40))
                    {
                        GameObject go = Instantiate(Bullet1);

                        go.transform.position = FirePos4.position;

                        Destroy(go, 5f);

                        Bullets.Add(go.transform);

                        go.transform.rotation = Quaternion.Euler(0, 0, i);
                    }
                }
                curTime2 = 0;
                Oneshot.PlayOneShot(Patern2);
            }

        }

        //360�� ź���� �幮�幮 ��� ȣ��ź��
        if (BossNowHp <= 12000 && BossNowHp > 6000)
        {
            //������ ����
            if (Phase == 1)
            {
                StartCoroutine("HpItemDrop");
            }

            //FirePos 3�� �߻� ��������
            FirePos3.transform.Rotate(Vector3.forward * 300 * Time.deltaTime);

            curTime += Time.deltaTime;

            if (curTime >= 0.05f)
            {
                GameObject go = Instantiate(Bullet1);
                go.transform.position = FirePos3.position;
                go.transform.rotation = FirePos3.rotation;
                curTime = 0;
            }

            //FirePos 4�� �߻� 360��
            curTime2 += Time.deltaTime;

            if (curTime2 >= 0.7f)
            {
                List<Transform> Bullets = new List<Transform>();

                for (int i = 0; i < 360; i += 10)
                {
                    GameObject go = Instantiate(Bullet1);

                    go.transform.position = FirePos4.position;

                    Destroy(go, 5f);

                    Bullets.Add(go.transform);

                    go.transform.rotation = Quaternion.Euler(0, 0, i);
                }
                curTime2 = 0;
            }

            //FirePos 1�� �߻� ���� ȣ�� �̻���
            curTime3 += Time.deltaTime;

            if (curTime3 >= 2.5f)
            {
                BossHoming();
                curTime3 = 0;
                Oneshot.PlayOneShot(Patern1);
            }
        }

        //360�� �� ���� �����ϴ� ź��
        if (BossNowHp <= 6000)
        {
            //������ ����
            if (Phase == 2)
            {
                StartCoroutine("HpItemDrop");
            }
            curTime += Time.deltaTime;

            if (curTime >= 1f)
            {
                //�Ѿ� ������Ʈ ����
                List<Transform> Bullets = new List<Transform>();

                for (int i = 0; i < 360; i += 10)
                {
                    //����
                    GameObject go = Instantiate(Bullet1);
                    //�߻� ��ġ�� firepos3
                    go.transform.position = FirePos3.position;

                    Destroy(go, 5f);

                    Bullets.Add(go.transform);
                    //z�� ���� ���ؾ� ȸ���ϴ� z�� i�� ����
                    go.transform.rotation = Quaternion.Euler(0, 0, i);
                }

                StartCoroutine(BulletToTarget(Bullets));
                curTime = 0;

                Oneshot.PlayOneShot(Patern4);
            }

            curTime2 += Time.deltaTime;

            if (curTime2 >= 0.8f)
            {
                List<Transform> Bullets = new List<Transform>();

                for (int i = 0; i < 360; i += 20)
                {
                    GameObject go = Instantiate(Bullet1);

                    go.transform.position = FirePos4.position;

                    Destroy(go, 5f);

                    Bullets.Add(go.transform);

                    go.transform.rotation = Quaternion.Euler(0, 0, i);
                }

                StartCoroutine(BulletToTarget(Bullets));
                curTime2 = 0;

                Oneshot.PlayOneShot(Patern4);
            }
        }
    }

    //���� ü�� ���ϸ� ȸ���� �ϳ� ���� ���� �ʿ�
    IEnumerator HpItemDrop()
    {
        if (HpItem != null)
        {
            Instantiate(HpItem, FirePos3.transform.position, Quaternion.identity);
        }
        Phase++;
        yield return null;
    }

    //�����̻��� �߻�
    void BossHoming()
    {
        Instantiate(Bullet2, FirePos1.position, Quaternion.identity);
    }

    //�÷��̾� ����ź �ڷ�ƾ
    IEnumerator BulletToTarget(IList<Transform> objects)
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < objects.Count; i++)
        {
            //���� �Ѿ��� ��ġ���� �÷��̾� ��ġ�� ���Ͱ��� ����
            Vector3 targetDirection = PlayerPos.transform.position - objects[i].position;
            //x,y ���� �����Ͽ� z���� ������ ���� �� ~�� ������ ��ȯ
            float angle = Mathf.Atan2(targetDirection.x, -targetDirection.y) * Mathf.Rad2Deg;
            //������Ʈ �̵�
            objects[i].rotation = Quaternion.Euler(0, 0, angle);
        }

        objects.Clear();
    }


    //������ �������� ���� ��� ����Ǵ� �޼���
    public void Damage(int attack)
    {
        BossNowHp -= attack;

        //������ ���������� �ڷ�ƾ ����
        StartCoroutine("BossDamage");

        //���� ü���� 0�϶�
        if (BossNowHp <= 0)
        {
            BS.Stop();

            BossClear = true;
            //���� ü�¹ٰ� ������ ����� �ƿ� �������
            BossNowHpBar.fillAmount = 0;

            //������ ���� ��ũ��Ʈ�� ã�Ƽ�
            BossController bossController = GetComponent<BossController>();
            //���ֹ����鼭 �Ѿ� �߻� ����
            Destroy(bossController);

            //�밡��1��
            GameObject go = Instantiate(BoomEffect, transform.position, Quaternion.identity);
            
            GameObject go1 = Instantiate(BoomEffect, new Vector3(transform.position.x + 1, transform.position.y + 1.3f),
                Quaternion.identity);
            GameObject go2 = Instantiate(BoomEffect, new Vector3(transform.position.x - 1.1f, transform.position.y - 1.2f),
                Quaternion.identity);
            GameObject go3 = Instantiate(BoomEffect, new Vector3(transform.position.x - 1.6f, transform.position.y + 1.7f),
                Quaternion.identity);
            GameObject go4 = Instantiate(BoomEffect, new Vector3(transform.position.x + 1.2f, transform.position.y - 1.6f),
                Quaternion.identity);
            GameObject go5 = Instantiate(BoomEffect, new Vector3(transform.position.x - 1.4f, transform.position.y - 1.5f),
                Quaternion.identity);
            GameObject go6 = Instantiate(BoomEffect, new Vector3(transform.position.x - 2f, transform.position.y - 1.4f),
                Quaternion.identity);

            Oneshot.PlayOneShot(BossBoom);

            //�밡�� 2��
            Destroy(go, 3f);
            Destroy(go1, 3f);
            Destroy(go2, 3f);
            Destroy(go3, 3f);
            Destroy(go4, 3f);
            Destroy(go5, 3f);
            Destroy(go6, 3f);
            Destroy(gameObject, 3.2f);

            Oneshot.PlayOneShot(StageClaer);
        }
    }

    IEnumerator BossDamage()
    {
        //���� ������Ʈ�� �޸� �ڽ� ������Ʈ�� ��������Ʈ �������� �����ͼ�
        //�ǰݽ� ���������� �����ϴ� ȿ��
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.15f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.15f);
    }
}
