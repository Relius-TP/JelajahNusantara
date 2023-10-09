using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private Rarity[] gacha;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject characterCardGo;

    GameObject characterCard;
    Gacha card;

    public void Gachaa()
    {
        if(characterCard == null )
        {
            characterCard = Instantiate(characterCardGo, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<Gacha>();

        }
        
        int rnd = UnityEngine.Random.Range(1, 101);
        for(int i = 0; i < gacha.Length; i++)
        {
            if (rnd <= gacha[i].rate) 
            {
                card.hero = Reward(gacha[i].rarity);
                return;
            }
        }
    }

    CharDatabase Reward(string rarity)
    {
        Rarity gr = Array.Find(gacha, rt => rt.rarity == rarity);
        CharDatabase[] reward = gr.reward;
        int rnd = UnityEngine.Random.Range(0, reward.Length);
        return reward[rnd];
    }
}
