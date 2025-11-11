Summary #3 — AI State Machine Architecture

Technical Progress
Phase 1 – Movement Prototype

Added Rigidbody and Collider to the character prefab.

Created temporary FighterMover.cs for basic movement.

Used Rigidbody.velocity instead of AddForce for predictable control.

Updated CharacterSpawner to inject the target transform into FighterMover (basic DI).

Phase 2 – State Machine Refactor

Removed FighterMover.cs.

Created IFighterState interface with Enter, Execute, Exit.

Created FighterAI (Context / Brain).

Implemented component caching in FighterAI.Awake() (Rigidbody, RuntimeCharacter, HealthSystem).

Implemented state caching in Awake() (single instances of MovingState, AttackingState).

Implemented MovingState using IFighterState, now reads moveSpeed from ai.Stats.

Implemented AttackingState using IFighterState.

Phase 3 – State Transition Logic

Added attackRange to FighterAI.

MovingState.Execute() uses Vector3.Distance to check if target is within ai.attackRange.

First AI decision: MovingState transitions to AttackingState via ai.ChangeState(ai.attackingState).

Learning Outcomes

Implemented the State Pattern for clean, extensible AI.

Applied SOLID (especially Open/Closed, Dependency Inversion) via interfaces.

Used Dependency Injection to pass FighterAI to states (Enter(FighterAI ai)).

Learned object caching (create states in Awake) to reduce GC allocations.

Clarified Unity lifecycle roles: Awake for caching/setup, Start for initial state.

Refactored legacy movement code into a state-based AI architecture.

Current State

✅ AI architecture (state machine) complete

✅ AI movement (MovingState) functional & data-driven

✅ AI decision (transition logic) working

🟡 AI attack (AttackingState) prototyped (Debug.Log)

🔜 Next: implement real damage logic in AttackingState.Execute()