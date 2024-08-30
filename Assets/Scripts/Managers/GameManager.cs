using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
public class GameManager : MonoBehaviour

{
    public static GameManager instance;
    public bool is2D;
    public bool isPaused;
    [Header("Players")]
    public Player player2D;
    public Player player3D;
    [Header("Camera")]
    public Camera mainCamera;
    [Header("Spawn Points")]

    public Transform playerSpawnPoint;

    public Material outlineMaterial2D;
    public Material spriteDefaultMaterial2D;
    

    public Transform objectiveTransform;

    public event Action onSwitchDimension;
    private void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        isPaused = Time.timeScale == 0;
        if(isPaused){
            MusicManager.Instance.PauseMusic();
            SoundManager.Instance.PauseSound2D();
        }
        else{
            MusicManager.Instance.UnPauseMusic();
            SoundManager.Instance.UnPauseSound2D();
        }
    }

    public void StartGame(){
        PlayGame();
    }
    public void PlayGame(){
        SceneManager.LoadScene(1);
        MusicManager.Instance.PlayMusic("bg");
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        UIManager.instance.HideUI(UI.PAUSE);
    }
    
    public void PauseGame(){
        UIManager.instance.ShowUI(UI.PAUSE);
        Time.timeScale = 00;
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
    [ContextMenu("Switch Dimension")]
    public void SwitchDimension(){
        // transition between 2D and 3D
        is2D = !is2D;
        if(is2D){
            MusicManager.Instance.StopMusic();
            StartCoroutine(Transitionto2D());
        }
        else{
            MusicManager.Instance.PlayMusic("bg");
            StartCoroutine(Transitionto3D());
        }
        
    }

    public IEnumerator Transitionto2D(){
        player2D.enabled = true;
        player3D.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        objectiveTransform.SetParent(player2D.transform.parent);
        objectiveTransform.localPosition = Vector3.zero;
        player2D.GetComponent<Inventory>().enabled = true;
        UIInventory.instance.Show();
        yield return null;
    }
    public IEnumerator Transitionto3D(){
        player2D.enabled = false;
        player3D.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        objectiveTransform.SetParent(null);
        objectiveTransform.position = player3D.transform.position;
        player2D.GetComponent<Inventory>().enabled = false;
        UIInventory.instance.Hide();
        yield return null;
    }
    public void OnSwitchDimension(){
        onSwitchDimension?.Invoke();
    }
    private void OnEnable() {
        onSwitchDimension += SwitchDimension;
    }
    private void OnDisable() {
        onSwitchDimension -= SwitchDimension;
    }
}
