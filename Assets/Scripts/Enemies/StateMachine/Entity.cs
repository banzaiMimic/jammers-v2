using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
  
  public FiniteStateMachine stateMachine;
  public SO_Entity entityData;
  public int facingDirection { get; private set; }
  public Rigidbody2D rb { get; private set; }
  public Animator animator { get; private set; }
  public GameObject aliveGO { get; private set; }

  [SerializeField]
  private Transform wallCheck;
  [SerializeField]
  private Transform ledgeCheck;

  private Vector2 velocityWorkspace;

  public virtual void Start() {
    facingDirection = 1;
    
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    stateMachine = new FiniteStateMachine();
    aliveGO = GetComponent<GameObject>();
  }

  public virtual void Update() {
    stateMachine.currentState.LogicUpdate();
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual void SetVelocity(float velocity) {
    velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
    rb.velocity = velocityWorkspace;
  }

  public virtual bool CheckWall() {
    return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckLedge() {
    return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
  }

  public virtual void Flip() {
    facingDirection *= -1;
    aliveGO.transform.Rotate(0f, 180f, 0f);
  }

  public virtual void OnDrawGizmos() {
    Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
    Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
  }

}
