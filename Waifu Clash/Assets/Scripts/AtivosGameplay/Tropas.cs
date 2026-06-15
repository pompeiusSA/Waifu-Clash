using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tropas : MonoBehaviour
{
    GameController _gameController;

    Rigidbody2D rb;

    private float vel;

    public float velMax;

    bool isInimigoProximo;

    GameObject inimigoAlvo = null;

    [SerializeField] List<GameObject> inimigos = new List<GameObject>();

    public GameObject colisorAtaque;

    bool isAtacando = false;

    public float minhaVida;

    void Awake()
    {
        _gameController = FindAnyObjectByType(typeof(GameController)) as GameController;

        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vel = velMax;
    }

    // Update is called once per frame
    void Update()
    {
        seguindoBase();

        if (minhaVida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void seguindoBase()
    {
        if (this.gameObject.tag == "player")
        {
            if (isInimigoProximo == false)
            {
                transform.right = _gameController.bases[1].transform.position - transform.position;

                transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[1].transform.position, vel * Time.deltaTime);
            }

            existeInimigo("inimigo");
        }
        else
        {
            if (isInimigoProximo == false)
            {
                transform.right = _gameController.bases[0].transform.position - transform.position;

                transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[0].transform.position, vel * Time.deltaTime);
            }

            existeInimigo("player");
        }
    }

    void existeInimigo(string tag)
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));

        if (inimigoAlvo == null)
        {
            isInimigoProximo = false;
            vel = velMax;

            for (int i = 0; i < inimigos.Count; i++)
            {
                if (Vector2.Distance(transform.position, inimigos[i].transform.position) <= 10)
                {
                    inimigoAlvo = inimigos[i];
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, inimigoAlvo.transform.position) <= 10) //Ataco inimigo
            {
                isInimigoProximo = true;

                if (inimigoAlvo != null)
                {
                    transform.right = inimigoAlvo.transform.position - transform.position;

                    transform.position = Vector2.MoveTowards(transform.position, inimigoAlvo.transform.position, vel * Time.deltaTime);

                    if (Vector2.Distance(transform.position, inimigoAlvo.transform.position) <= 1)
                    {
                        vel = 0;

                        if (isAtacando == false)
                        {
                            atacando();
                        }
                    }
                    else
                    {
                        vel = velMax;

                        StopCoroutine("ataqueDelay");
                    }
                }
            }
            else
            {
                isInimigoProximo = false;
            }
        }
    }

    void atacando()
    {
        isAtacando = true;
        StartCoroutine("ataqueDelay");
    }

    IEnumerator ataqueDelay()
    {
        yield return new WaitForSeconds(_gameController.delayDanoCC);

        if (inimigoAlvo != null)
        {
            switch (inimigoAlvo.gameObject.tag)
            {
                case "player":

                    inimigoAlvo.gameObject.GetComponent<Tropas>().minhaVida -= 10;

                    break;


                case "inimigo":

                    inimigoAlvo.gameObject.GetComponent<Tropas>().minhaVida -= 9;

                    break;
            }
        }

        yield return new WaitForSeconds(_gameController.delayDanoCC);

        StartCoroutine("ataqueDelay");
    }
}

