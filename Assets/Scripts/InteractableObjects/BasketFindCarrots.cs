using System.Collections.Generic;
using UnityEngine;

public class BasketFindCarrots : MonoBehaviour
{
    public List<GameObject> carrots;
    public int currentIndex = -1;
    
    private void Update() {
        if(currentIndex >= 0)
        carrots[currentIndex].SetActive(true);
    }
}
