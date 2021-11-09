using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/StateData/ChargeState")]
public class SO_ChargeState : ScriptableObject {
  public float chargeSpeed = 6f;
  public float chargeTime = 2f;
}
