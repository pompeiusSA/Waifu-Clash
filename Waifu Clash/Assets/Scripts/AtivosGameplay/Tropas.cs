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

    [SerializeField] bool isAtacando = false;

    public float minhaVida;

    private bool isBasePerto = false;

    float distMin = Mathf.Infinity;

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
            if (_gameController.bases[1] != null)
            {
                if (isInimigoProximo == false && isBasePerto == false)
                {
                    transform.right = _gameController.bases[1].transform.position - transform.position;

                    transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[1].transform.position, vel * Time.deltaTime);
                }

                float distBase = Vector2.Distance(transform.position, _gameController.bases[1].transform.position);

                if (distBase <= 2)
                {
                    if (isAtacando == false)
                    {
                        atacandoBases();
                    }
                }
                else
                {
                    isBasePerto = false;
                    isAtacando = false;
                    StopCoroutine("ataqueDelayBases");
                }
            }
            else
            {
                StopCoroutine("ataqueDelayBases");
            }

            existeInimigo("inimigo");
        }
        else
        {
            if (_gameController.bases[0] != null)
            {
                if (isInimigoProximo == false && isBasePerto == false)
                {
                    transform.right = _gameController.bases[0].transform.position - transform.position;

                    transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[0].transform.position, vel * Time.deltaTime);
                }

                float distBase = Vector2.Distance(transform.position, _gameController.bases[0].transform.position);

                if (distBase <= 2)
                {
                    if (isAtacando == false)
                    {
                        atacandoBases();
                    }
                }
                else
                {
                    isBasePerto = false;
                    isAtacando = false;
                    StopCoroutine("ataqueDelayBases");
                }
            }
            else
            {
                StopCoroutine("ataqueDelayBases");
            }

            existeInimigo("player");
        }
    }

    void existeInimigo(string tag)
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));

        if (isBasePerto == false) //PEGANDO INIMIGO
        {
            if (inimigoAlvo == null)
            {
                isInimigoProximo = false;

                vel = velMax;

                isAtacando = false;

                distMin = Mathf.Infinity;

                for (int i = 0; i < inimigos.Count; i++)
                {
                    if (Vector2.Distance(transform.position, inimigos[i].transform.position) <= 10 && Vector2.Distance(transform.position, inimigos[i].transform.position) <= distMin)
                    {
                        distMin = Vector2.Distance(transform.position, inimigos[i].transform.position);

                        inimigoAlvo = inimigos[i];
                    }
                }
            }
            else //ATACANDO INIMIGO
            {
                if (Vector2.Distance(transform.position, inimigoAlvo.transform.position) <= 10)
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
                                atacandoInimigos();
                            }
                        }
                        else
                        {
                            vel = velMax;

                            isAtacando = false;

                            distMin = Mathf.Infinity;

                            StopCoroutine("ataqueDelayInimigos");
                        }
                    }
                }
                else
                {
                    isInimigoProximo = false;

                    isAtacando = false;
                }
            }
        }
    }

    void atacandoInimigos()
    {
        isAtacando = true;
        StartCoroutine("ataqueDelayInimigos");
    }

    IEnumerator ataqueDelayInimigos()
    {
        if (_gameController.bases[0] != null && _gameController.bases[1] != null)
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

            StartCoroutine("ataqueDelayInimigos");
        }
        else
        {
            StopCoroutine("ataqueDelayInimigos");
        }
    }

    public void atacandoBases()
    {
        isBasePerto = true;
        isAtacando = true;
        StartCoroutine("ataqueDelayBases");
    }

    IEnumerator ataqueDelayBases()
    {
        yield return new WaitForSeconds(_gameController.delayDanoCC);

        switch (this.gameObject.tag)
        {
            case "player":

                if (_gameController.bases[1] != null)

                {
                    _gameController.bases[1].gameObject.GetComponent<Base>().minhaVidaBase -= 10;
                }

                break;

            case "inimigo":

                if (_gameController.bases[0] != null)

                {
                    _gameController.bases[0].gameObject.GetComponent<Base>().minhaVidaBase -= 10;
                }

                break;
        }

        yield return new WaitForSeconds(_gameController.delayDanoCC);

        StartCoroutine("ataqueDelayBases");
    }
}

