
using UnityEngine;

public class Background : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.instance.player2D.transform.position;
    }
}
