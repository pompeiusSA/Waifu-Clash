using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tropas : MonoBehaviour
{
    GameController _gameController;

    Rigidbody2D rb;

    private float vel;

    public float velMax;

    bool isInimigoProximo;

    [SerializeField] List<GameObject> inimigos = new List<GameObject>();
    [SerializeField] List<Vector2> posicoesInimigos = new List<Vector2>();

    [SerializeField] List<float> distancias = new List<float>();

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
        if (this.gameObject.tag == "player")
        {
            if (isInimigoProximo == false)
            {
                transform.right = _gameController.bases[1].transform.position - transform.position;

                transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[1].transform.position, vel * Time.deltaTime);
            }

            //ExisteObjetoComTag("inimigo");

            existeInimigo("inimigo");
        }
        else
        {
            if (isInimigoProximo == false)
            {
                transform.right = _gameController.bases[0].transform.position - transform.position;

                transform.position = Vector2.MoveTowards(transform.position, _gameController.bases[0].transform.position, vel * Time.deltaTime);
            }

            //ExisteObjetoComTag("player");
        }
    }

    private void ExisteObjetoComTag(string tag)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tag);

        if (obj != null)
        {
            float distancia = Vector2.Distance(transform.position, obj.transform.position);

            if (distancia <= 10)
            {
                isInimigoProximo = true;

                transform.right = obj.transform.position - transform.position;

                transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, vel * Time.deltaTime);

                if (distancia <= 1)
                {
                    vel = 0;
                }
            }
        }
        else
        {
            isInimigoProximo = false;

            vel = velMax;
        }
    }

    void existeInimigo(string tag)
    {
        inimigos = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));

        posicoesInimigos.Clear();
        distancias.Clear();

        foreach (var inimigo in inimigos)
        {
            posicoesInimigos.Add(inimigo.transform.position);

            distancias.Add(Vector2.Distance(transform.position, inimigo.transform.position));
        }
    }

}

