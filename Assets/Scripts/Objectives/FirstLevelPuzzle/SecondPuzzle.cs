using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzle : Objective
{
    // public List<Carrots2D> carrots2Ds;
    public GameObject reward;
    public List<GameObject> carrots;
    public List<FruitBasket> fruitBaskets;
    [SerializeField] private Transform startRewardPosition;
    [SerializeField] private Transform endRewardPosition;
    
    public override void CheckComplete()
    {
        if(complete){
            OnComplete();
        }
    }
    protected override void Update()
    {
        complete = fruitBaskets[0].correct && fruitBaskets[1].correct && //
        fruitBaskets[2].correct && fruitBaskets[3].correct;
    }
    public void SpawnReward(){
        GameObject rewardLeaf = Instantiate(reward,transform.position,Quaternion.identity,transform);
        rewardLeaf.GetComponent<Reward>().Animate(transform);
        enabled = false;
    }
}
