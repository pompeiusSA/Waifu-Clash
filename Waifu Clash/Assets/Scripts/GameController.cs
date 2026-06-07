using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform[] colisoresCompraSpawn; public Vector2[] posColisoresSpawnMobile, posColisoresSpawnPC;

    [Header("Cenario")]

    public Camera mainCamera;

    public Transform[] posicoesCamera;

    [Header("Gameplay")]

    public Text quantidadeDinheiroTxt;

    public float dinheiroIncremento;

    public float dinheiroAtual;

    public GameObject tropaCC;

    public int qualTropa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        adaptandoHUD();

        //Jogo começa com nenhuma tropa selecionada!

        qualTropa = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //Ganhando dinheiro e registrando

        quantidadeDinheiroTxt.text = Mathf.RoundToInt(dinheiroAtual).ToString();

        dinheiroAtual += dinheiroIncremento * Time.deltaTime;

        //Reiniciando a seleção de personagem caso não tenha dinheiro!

        if (dinheiroAtual <= 0)
        {
            qualTropa = -1;
        }
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

            //Ajustando colisores

            colisoresCompraSpawn[0].localPosition = posColisoresSpawnMobile[0];
            colisoresCompraSpawn[1].localPosition = posColisoresSpawnMobile[1];
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

            //Ajustando colisores

            colisoresCompraSpawn[0].localPosition = posColisoresSpawnPC[0];
            colisoresCompraSpawn[1].localPosition = posColisoresSpawnPC[1];
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
