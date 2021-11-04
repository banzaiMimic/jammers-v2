using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {

  private void OnTriggerEnter2D(Collider2D collision) {
    Debug.Log("collision triggered--");
    if (collision.gameObject.CompareTag("collectible")) {
      Debug.Log("collectible found --");
      Destroy(collision.gameObject);
    }
  }

}
