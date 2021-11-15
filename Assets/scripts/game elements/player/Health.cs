using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Player player;
    [SerializeField] private Animator[] heartsInOrder;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;

    private void Awake()
    {
        player = GetComponent<Player>();

        SubscribeEvents();
    }

    private void UpdateHealthBar(int remainingLife)
    {
        Debug.Log(remainingLife);

        for (int i = 4; i > remainingLife - 1; i--)
        {
            heartsInOrder[i].SetTrigger("decrease");
            heartsInOrder[i].GetComponent<Image>().sprite = emptyHeart;
        }
    }

    public void SubscribeEvents()
    {
        player.OnDamageReceived += UpdateHealthBar;
    }

    //public void Unsubscribe
}
