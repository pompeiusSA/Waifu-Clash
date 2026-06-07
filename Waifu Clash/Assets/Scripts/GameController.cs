using Unity.VisualScripting;
using UnityEngine;

public enum dispositivo
{
    mobile,
    pc,
}

public class GameController : MonoBehaviour
{
    [Header("HUD")]

    [SerializeField] dispositivo dispositivoAtual;

    public Transform[] posBases; public Vector2 scaleBasesMobile, scaleBasesPC;

    public RectTransform[] botoesSuperiores; public Vector2[] posBataoSuperiorMobile, posBataoSuperiorPC;

    public RectTransform molduraWaifu; public Vector2 posMolduraMobile, posMolduraPC;

    [Header("Cenario")]

    public Camera mainCamera;

    public Transform[] posicoesCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        adaptandoHUD();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void adaptandoHUD()
    {
        //Dependendo do dispositivo,a escala do HUD e objetos mudam

        if (dispositivoAtual == dispositivo.mobile)
        {
            for (int i = 0; i < posBases.Length; i++)
            {
                posBases[i].localScale = scaleBasesMobile;
            }

            //0 é moeda, 1 é upgrades e 2 é configurações

            botoesSuperiores[0].anchoredPosition = posBataoSuperiorMobile[0];
            botoesSuperiores[1].anchoredPosition = posBataoSuperiorMobile[1];
            botoesSuperiores[2].anchoredPosition = posBataoSuperiorMobile[2];

            //Ajustando o moldura

            molduraWaifu.anchoredPosition = posMolduraMobile;
        }
        else
        {
            for (int i = 0; i < posBases.Length; i++)
            {
                posBases[i].localScale = scaleBasesPC;
            }

            //0 é moeda, 1 é upgrades e 2 é configurações

            botoesSuperiores[0].anchoredPosition = posBataoSuperiorPC[0];
            botoesSuperiores[1].anchoredPosition = posBataoSuperiorPC[1];
            botoesSuperiores[2].anchoredPosition = posBataoSuperiorPC[2];

            //Ajustando o moldura

            molduraWaifu.anchoredPosition = posMolduraPC;
        }
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
