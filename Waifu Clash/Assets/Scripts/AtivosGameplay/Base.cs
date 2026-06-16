using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public float minhaVidaBase;

    // Update is called once per frame
    void Update()
    {
        if (minhaVidaBase <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Menu");
        }
    }

    void OnCollisionEnter2D(Collision2D colidido)
    {
        switch (colidido.gameObject.tag)
        {
            case "player":
                colidido.gameObject.GetComponent<Tropas>().atacandoBases();

                break;

            case "inimigo":
                colidido.gameObject.GetComponent<Tropas>().atacandoBases();

                break;
        }
    }
}
