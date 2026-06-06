using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("HUD")]

    public GameObject[] botoesCamera;

    public Camera mainCamera;

    public Transform[] posicoesCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mudandoPosCamera(GameObject botao)
    {
        switch (botao.tag)
        {
            case "cima":

                mainCamera.transform.position = posicoesCamera[0].position;

                break;

            case "meio":

                mainCamera.transform.position = posicoesCamera[1].position;

                break;

            case "baixo":

                mainCamera.transform.position = posicoesCamera[2].position;

                break;

            default:

                mainCamera.transform.position = posicoesCamera[0].position;

                break;
        }
    }
}
