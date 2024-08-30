using UnityEngine;

public class GoldenReward : Reward
{
    public bool interacted;
    public override void Interacted()
    {
        interacted = true;
        UIInventory.instance.AddItem(gameObject);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}