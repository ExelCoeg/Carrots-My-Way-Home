
using UnityEngine.SceneManagement;
using UnityEngine;
public class Switch : InteractableObject
{

    public override void Interactted()
    {
        Debug.Log("Switch Interacted");
        GameManager.instance.is2D = true;
        // transition effect to 2d
        Player player = Instantiate(GameManager.instance.player2D, GameManager.instance.playerSpawnPoint.position, Quaternion.identity);
        player.enabled = true;
        
        UIManager.instance.ShowMessage("Switched to 2D mode");

    }   

}
