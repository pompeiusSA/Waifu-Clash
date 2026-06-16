using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotoesMenu : MonoBehaviour
{
    public void trocandoCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
