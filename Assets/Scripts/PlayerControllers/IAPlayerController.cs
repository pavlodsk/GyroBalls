using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPlayerController : PlayerController
{
    //Variables viejas pero necesarias
    /*private LifeController lifeController;
    
    private Reload reload;
    
    private int life;
    private int limitedBullets;
    private float input;

    private bool playable;
    private bool blocked;
    private bool shooting;*/

    //Variables de la IA
    private int rand;
    
    private bool haybalas;

    private float changetime;
    private float time;
    private bool direction;

    //variable para funcionamiento de escudo de la IA
    private bool correctBlocked;

    void Start()
    {
        //Inicialización antigua
        lifeController = gameObject.GetComponent<LifeController>();
        lifeController.setLife(3);

        reload = gameObject.GetComponent<Reload>();
        reload.setBullet(limitedBullets);

        life = 3;
        playable = false;
        blocked = false;
        shooting = true;
        correctBlocked = false;

        //Inicialización de IA
        changetime = Random.Range(0.1f, 1);
        direction = (Random.Range(0,2) == 0) ? direction = false : direction = true; //decide direccion aleatoria de arranque
    }

    private void Update()
    {
        if (playable)
        {        
            time += Time.deltaTime;
            haybalas = (GameObject.FindGameObjectsWithTag("bala1").Length > 0) ? haybalas = true : haybalas = false; //determina si hay balas en pantalla
            input = 0;
            if (!blocked)
            {
                input = direction ? -1 : 1; //Cambia el input según la dirección
                if (time > changetime)
                {
                    changetime = Random.Range(0.5f, 1f);
                    direction = !direction;
                    shoot();
                    time = 0;
                }
            }

            if (haybalas)
            {
                if (!correctBlocked)
                {
                    rand = Random.Range(0, 100);
                    Debug.Log(rand);
                    if (rand < 65)
                    {
                        escudo.GetComponent<Escudo>().block();
                        blocked = true;
                    }
                    correctBlocked = true;
                    haybalas = false;
                }
            }
            else
            {
                escudo.GetComponent<Escudo>().unblock();
                blocked = false;
                correctBlocked = false;
                haybalas = false;
            }
        }
        
    }

}
