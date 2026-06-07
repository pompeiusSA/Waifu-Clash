using System.Diagnostics;
using UnityEngine;

public class TropasBotao : MonoBehaviour
{
    GameController _gameController;

    void Awake()
    {
        _gameController = FindAnyObjectByType(typeof(GameController)) as GameController;
    }

    public void comprandoTropa(GameObject botao)
    {
        switch (botao.tag)
        {
            case "TropaCC":

                _gameController.qualTropa = 0;

                break;

            case "TropaAD":

                _gameController.qualTropa = 1;

                break;

            case "TropaCA":

                _gameController.qualTropa = 2;

                break;

            case "TropaDC":

                _gameController.qualTropa = 3;

                break;
        }
    }
}
