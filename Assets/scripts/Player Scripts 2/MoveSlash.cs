using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlash : MonoBehaviour
{
    public GameObject player;

    public bool isSlashing;

    void Start()
    {
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }


}
