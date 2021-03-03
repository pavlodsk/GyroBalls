using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    //Nota: posible parry con corrutina, activando variable

    private bool blocked;
    private bool parry;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        blocked = false;
        
    }

    public void block()
    {
        gameObject.SetActive(true);
        blocked = true;
        parry = true;
        StartCoroutine(parryCoroutine());
    }

    IEnumerator parryCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        parry = false;
    }

    public void unblock()
    {
        gameObject.SetActive(false);
        blocked = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.transform.tag;
        if (parry)
        {
            float x = (collision.transform.position.x / 4) * -1;
            float y = (collision.transform.position.y / 4) * -1;
            collision.transform.GetComponent<BalaController>().setXforce(x * 20);
            collision.transform.GetComponent<BalaController>().setYforce(y * 20);
            if(tag == "bala1")
            {
                collision.transform.tag = "bala2";
            }
            else
            {
                collision.transform.tag = "bala1";
            }
        }
        else
        {
            Pool.Instance.returnBala(collision.gameObject);
        }
        
    }

}
