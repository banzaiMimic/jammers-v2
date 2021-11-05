## Input System
we're using updated Input System, however Input Actions are not added via ui

### InputActions
- reference -> MovementSM.cs -> AddInputBindings

## collectibles

### adding a new collectible
- add sprite
- tag as `collectible`
- layer as `Collectible`
- make sure Player has `ItemCollector` component on it


# States

## Enemy
- create new state class `states/NewState`
- create state data class (scriptable obj) `data/SO_NewState`
- create enemy specific state `EnemyName_NewState`

### states/NewState
- extend `State`
- `protected SO_NewState stateData;`
- append to constructor -> `this.stateData = stateData;`

### data/SO_NewState
- extend `ScriptableObject`
- set public config params

- declare state on enemy class
- setup animator
  - create enemyx_someAnimation animator
  - add it to the Alive component on enemy
  - drag png(s) into it via Animator window (open Animator window -> Select 'Alive' component -> select animation from dropdown)