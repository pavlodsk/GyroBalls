using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    private float xforce;
    private float yforce;
    private Rigidbody2D rb;

    public void setXforce(float force)
    {
        xforce = force;
    }

    public void setYforce(float force)
    {
        yforce = force;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(BalaCoroutine());
    }
    IEnumerator BalaCoroutine()
    {
        yield return new WaitForSeconds(2);
        Pool.Instance.returnBala(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xforce, yforce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            Pool.Instance.returnBala(gameObject);
        }
    }
}
