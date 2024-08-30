using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzle : Objective
{
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
        base.Update();
        complete = fruitBaskets[0].correct && fruitBaskets[1].correct && //
        fruitBaskets[2].correct && fruitBaskets[3].correct;
    }
    public override void OnEnable(){
        base.OnEnable();
        onComplete += SpawnRewardLeaf;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        onComplete -= SpawnRewardLeaf;
    }
    public void SpawnRewardLeaf(){
        GameObject rewardLeaf = Instantiate(reward,startRewardPosition.position,Quaternion.identity,transform);
        rewardLeaf.GetComponent<Reward>().Animate(endRewardPosition);
        enabled = false;
    }
}
