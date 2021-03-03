using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{

    public GameObject[] bulletsArray = new GameObject[6];

    public void setBullet(int n)
    {
        //Activa los sprites hasta que llega a n que los desactiva
        for (int i = 0; i < 6; i++)
        {
            if(i < n)
            {
                bulletsArray[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                bulletsArray[i].GetComponent<Image>().enabled = false;
            }
        }
    }

}
