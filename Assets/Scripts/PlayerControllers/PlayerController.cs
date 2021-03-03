using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Ajustes personaje
    public bool player1;
    public int maxLife = 3;
    public int maxBullets = 6;
    public float speed = 3;
    public float shootSpeed = 10;
    

    //Referencias de objetos
    public GameObject bala;
    public GameObject escudo;
    public ParticleSystem particles;

    //Jugabilidad activada o no
    protected bool playable;
    protected bool blocked;
    protected bool shooting;
    protected int limitedBullets = 6;

    //Relacionado con la vida
    protected int life;
    protected LifeController lifeController;

    //Relacionado con la recarga
    protected Reload reload;

    //Variables de input
    protected float input;
    private string axissetting;
    private string inputsetting;
    private string blocksetting;

    // Start is called before the first frame update
    void Start()
    {
        if (player1)
        {
            axissetting = "Horizontal";
            inputsetting = "w";
            blocksetting = "s";
        }
        else
        {
            axissetting = "Vertical";
            inputsetting = "up";
            blocksetting = "down";
        }

        lifeController = gameObject.GetComponent<LifeController>();
        lifeController.setLife(maxLife);

        reload = gameObject.GetComponent<Reload>();
        reload.setBullet(limitedBullets);

        life = maxLife;
        playable = false;
        blocked = false;
        shooting = true;
        
    }
    private void Update()
    {

        input = Input.GetAxis(axissetting);
        if (!playable || blocked)
        {
            input = 0;
        }
        if (Input.GetKeyDown(inputsetting) && (playable && !blocked && shooting))
        {
            shoot();
        }
        if (Input.GetKey(blocksetting) && playable)
        {
            escudo.GetComponent<Escudo>().block();
            blocked = true;
        }
        else
        {
            escudo.GetComponent<Escudo>().unblock();
            blocked = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if ((input < 0 && transform.rotation.z < -0.5) || (input > 0 && transform.rotation.z > 0.5))
        { }
        else
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, speed * input);
            
        }


    }

    public void setLife(int var)
    {
        life = var;
    }

    public void setSkin(Sprite skin)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = skin;

    }
    public void shoot()
    {
        
        GameObject inst = Pool.Instance.getBala();

        inst.transform.position = transform.position;
        float xforce = (transform.position.x / 4) * -1;
        float yforce = (transform.position.y / 4) * -1;

        inst.GetComponent<BalaController>().setXforce(xforce * shootSpeed);
        inst.GetComponent<BalaController>().setYforce(yforce * shootSpeed);
        if (player1)
        {
            inst.transform.tag = "bala1";
        }
        else
        {
            inst.transform.tag = "bala2";
        }

        limitedBullets--;
        reload.setBullet(limitedBullets);
        if (limitedBullets <= 0)
        {
            shooting = false;
            StartCoroutine(shootCoroutine());
        }
    }

    IEnumerator shootCoroutine()
    {
        yield return new WaitForSeconds(1);
        shooting = true;
        limitedBullets = maxBullets;
        reload.setBullet(limitedBullets);
    }

    public void shooted()
    {
        if (life > 0)
        {
            life--;
            lifeController.setLife(life);
            particles.transform.position = transform.position;
            particles.Play();
        }
        if(life == 0)
        {
            life--;
            GameController.Instance.endGame(player1);
            endgame();
        }
    }
    
    public void endgame()
    {
        playable = false;
    }
    public void startGame()
    {
        playable = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (player1 && collision.tag == "bala2")
        {
            shooted();
            Pool.Instance.returnBala(collision.gameObject);
        }

        if (!player1 && collision.tag == "bala1")
        {
            shooted();
            Pool.Instance.returnBala(collision.gameObject);
        }
    }
}
