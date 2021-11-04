using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour {

  BaseState currentState;

  void Start() {
    currentState = GetInitialState();
    if (currentState != null) {
      currentState.Enter();
    }
  }

  void Update() {
    if (currentState != null) {
      currentState.UpdateLogic();
    }
  }

  void LateUpdate() {
    if (currentState != null) {
      currentState.UpdatePhysics();
    }
  }

  public void ChangeState(BaseState newState) {
    //@Todo miiight have to let this get called multiple times ? shouldn't though since the update methods of our
    // currentState will be called already ... 
    if (currentState.name != newState.name) {
      Debug.Log("[ChangeState] : " + currentState.name + " -> " + newState.name);
      currentState.Exit();
      currentState = newState;
      currentState.Enter();
    }
  }

  protected virtual BaseState GetInitialState() {
    return null;
  }

  private void OnGUI() {
    string content = currentState != null ? currentState.name : "(no current state)";
    GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
  }
}
