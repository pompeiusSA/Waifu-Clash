using UnityEngine;

public class Base : MonoBehaviour
{
    public float minhaVidaBase;

    // Update is called once per frame
    void Update()
    {
        if (minhaVidaBase <= 0)
        {
            Destroy(this.gameObject);
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
