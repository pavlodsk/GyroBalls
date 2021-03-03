using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public GameObject[] heartsArray = new GameObject[3];

    public void setLife(int x)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < x)
            {
                heartsArray[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                heartsArray[i].GetComponent<Image>().enabled = false;
            }
        }
    }
    
}
