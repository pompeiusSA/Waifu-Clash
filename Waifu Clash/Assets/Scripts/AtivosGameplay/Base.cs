using UnityEngine;

public class Base : MonoBehaviour
{
    public float minhaVidaBase = 300;

    // Update is called once per frame
    void Update()
    {
        if (minhaVidaBase <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
