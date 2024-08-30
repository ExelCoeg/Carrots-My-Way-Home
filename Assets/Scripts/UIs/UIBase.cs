using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public RectTransform rectTransform;
    public virtual void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    public virtual void Show(){
        gameObject.SetActive(true);
    }
    public virtual void Hide(){
        gameObject.SetActive(false);
    }
    public void ChangeAnchorPreset(Vector2 anchorMin, Vector2 anchorMax,Vector2 pivot) {
        Vector2 size = rectTransform.rect.size;
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
        
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

    }

    public IEnumerator AnimatePopUPShow(){
        yield return transform.DOScale(1,0.25f).WaitForCompletion();
    }
    public IEnumerator AnimatePopUPHide(){
        yield return transform.DOScale(0,0.25f).WaitForCompletion();
    }
}