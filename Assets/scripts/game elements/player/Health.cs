using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator[] heartsInOrder;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;

    private void UpdateHealthBar(int remainingLife)
    {
        for (int i = 5; i > 0; i--)
        {
            if(i > remainingLife) 
            {
                heartsInOrder[i].SetTrigger("decrease");
                heartsInOrder[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    public void SubscribeEvents()
    {

    }

    //public void Unsubscribe
}
