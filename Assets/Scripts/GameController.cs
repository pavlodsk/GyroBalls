using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private bool endgame;
    private bool playable;

    public bool singleplayer;

    public PlayerController player1;
    public PlayerController player2;

    public Animator animator;
    public TextMeshProUGUI numbers;
    public TextMeshProUGUI winner;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI restart2;

    public Sprite[] skins;

    public AudioClip gameoverSound;
    public AudioClip startSound;

    IEnumerator initCoroutine()
    {
        numbers.text = "3";
        yield return new WaitForSeconds(1);
        numbers.text = "2";
        yield return new WaitForSeconds(1);
        numbers.text = "1";
        yield return new WaitForSeconds(1);
        numbers.text = "GO";
        animator.SetBool("dissapear", true);
        
        player1.startGame();
        player2.startGame();

    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        winner.text = "";
        restart.text = "";
        restart2.text = "";
        endgame = false;

        setSkins();
        gameObject.GetComponent<AudioSource>().PlayOneShot(startSound, 0.75f);
        StartCoroutine(initCoroutine());
    }

    private void Update()
    {
        if (endgame)
        {
            if (Input.GetKey("space"))
            {
                if (singleplayer)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }

            if (Input.GetKey("m"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void setSkins()
    {
        int x = Random.Range(0, skins.Length);
        int y = Random.Range(0, skins.Length);

        player1.setSkin(skins[x]);
        player2.setSkin(skins[y]);
    }
    public void endGame(bool playerone)
    {
        restart.text = "Press space to restart the game";
        restart2.text = "Press m to return to menu";
        endgame = true;
        if (playerone)
        {
            player2.endgame();
            winner.text = "PLAYER 2 WINS";
        }
        else{
            player1.endgame();
            winner.text = "PLAYER 1 WINS";
        }

        gameObject.GetComponent<AudioSource>().clip = gameoverSound;
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
