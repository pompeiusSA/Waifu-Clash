using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController _gameController;

    private bool isEntrouAreaCompras = false;

    void Awake()
    {
        _gameController = FindAnyObjectByType(typeof(GameController)) as GameController;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Se o player estiver no pc, esse bloco de comandos vai ser o funcional para captação de toque na tela

        if (_gameController.dispositivoAtual == dispositivo.pc)
        {
            Vector3 cursorPlayer = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cursorPlayer.z = 10f;

            transform.position = cursorPlayer;

            if (Input.GetButtonDown("Fire1") && isEntrouAreaCompras == false)
            {
                switch (_gameController.qualTropa)
                {
                    case 0: // TROPA CURTA DISTANCIA

                        if (_gameController.dinheiroAtual >= 10)
                        {
                            Instantiate(_gameController.tropaCC, cursorPlayer, transform.localRotation);

                            _gameController.dinheiroAtual -= 10;
                        }

                        break;

                    case 1: // TROPA LONGA DISTANCIA


                        break;

                    case 2: // TROPA CAVALARIA


                        break;

                    case 3: // TROPA DE CERCO


                        break;
                }
            }
        }

        //Se o player estiver no mobile, esse bloco de comandos vai ser o funcional para captação de toque na tela

        //...
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isEntrouAreaCompras = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isEntrouAreaCompras = false;
    }
}
