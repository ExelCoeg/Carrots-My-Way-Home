using UnityEngine;
using UnityEngine.UI;
using System;

public class Numbers : MonoBehaviour
{
    public int number;
    public Button button;
    public event Action onButtonClick;
    private void Awake() {
        button = GetComponent<Button>();
    }
    private void Update() {
        Cursor.visible= true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Start()
    {
        button.onClick.AddListener(() => {
            onButtonClick?.Invoke();});
    }

    private void OnButtonClick()
    {
        UICodePanel.Instance.OnNumberButtonClick(number);
    }
    private void OnEnable() {
        onButtonClick += OnButtonClick;
    }
    private void OnDisable() {
        onButtonClick -= OnButtonClick;
    }
}