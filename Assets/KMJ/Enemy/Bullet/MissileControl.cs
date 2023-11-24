using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour
{
    public float Speed = 10f;
    public int Power = 15;

    public GameObject BoomEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(Power);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(BoomEffect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f);
    }
}
