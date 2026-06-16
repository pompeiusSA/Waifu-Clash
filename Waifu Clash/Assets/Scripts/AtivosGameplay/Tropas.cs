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

    Coroutine rotinaAtaqueInimigo;

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

    void existeInimigo(string tag)
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));

        if (isBasePerto == false) //PEGANDO INIMIGO
        {
            if (inimigoAlvo == null)
            {
                isInimigoProximo = false; //Não há inimigos próximos, por não existir na cena

                vel = velMax; //Reinicia a vel por não ter inimigos na cena

                isAtacando = false; //Reinicia o ataque porque não tem inimigos na cena

                distMin = Mathf.Infinity; //Reinicia a distancia mínima  porque não tem inimigos na cena

                for (int i = 0; i < inimigos.Count; i++) //Ler todos os inimigos que estão na cena
                {
                    float distInimigoIndex = Vector2.Distance(transform.position, inimigos[i].transform.position);

                    if (distInimigoIndex <= 10 && distInimigoIndex <= distMin)
                    {
                        distMin = distInimigoIndex;

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
                    inimigoAlvo = null;
                    vel = velMax;
                }
            }
        }
    }

    void atacandoInimigos()
    {
        if (rotinaAtaqueInimigo != null)
            return;

        isAtacando = true;
        rotinaAtaqueInimigo = StartCoroutine(ataqueDelayInimigos());
    }

    IEnumerator ataqueDelayInimigos()
    {
        while (isAtacando && inimigoAlvo != null)
        {
            float distancia = Vector2.Distance(transform.position, inimigoAlvo.transform.position);

            if (distancia > 1.2f)
            {
                break;
            }

            yield return new WaitForSeconds(_gameController.delayDanoCC);

            if (inimigoAlvo == null)
                break;

            Tropas tropaAlvo = inimigoAlvo.GetComponent<Tropas>();

            if (tropaAlvo == null)
                break;

            tropaAlvo.minhaVida -= 10;

            if (tropaAlvo.minhaVida <= 0)
            {
                break;
            }
        }

        isAtacando = false;
        inimigoAlvo = null;
        vel = velMax;
        rotinaAtaqueInimigo = null;
    }

    void seguindoBase()
    {
        if (this.gameObject.tag == "player") //Player
        {
            if (_gameController.bases[1] != null) //se a base inimiga existe
            {
                if (isInimigoProximo == false && isBasePerto == false) //se não existir inimigo proximo e a base estiver longe
                {
                    transform.right = _gameController.bases[1].transform.position - transform.position;

                    transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[1].transform.position, vel * Time.deltaTime);
                }

                float distBase = Vector2.Distance(transform.position, _gameController.bases[1].transform.position);

                if (distBase <= 2) //Se a distancia da base estiver muito próxima, foca na base!
                {
                    if (isAtacando == false)
                    {
                        atacandoBases();
                    }
                }
                else //Se a distancia for longe, ele desfoca 100% da base
                {
                    isBasePerto = false;
                    isAtacando = false;
                    StopCoroutine("ataqueDelayBases");
                }
            }
            else //Se a base não existe mais, seu ataque na base acaba!
            {
                StopCoroutine("ataqueDelayBases");
            }

            existeInimigo("inimigo"); //Checando se existe inimigos na cena
        }
        else //INIMIGO
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

