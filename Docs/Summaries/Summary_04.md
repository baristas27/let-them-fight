Summary #4 — Physics-Based Character System (Active Ragdoll)
Focus: Transitioning from kinematic animation to a physics-driven "Active Ragdoll" architecture using Unity's ConfigurableJoints and State Pattern.

🏗️ Technical Architecture
Active Ragdoll Implementation:

Replaced standard CharacterJoints with ConfigurableJoints to utilize Target Rotation Drives, effectively turning bones into servo-motors that attempt to match a target pose.

Developed a modular CopyMotion script that calculates rotation deltas using Quaternion.Inverse logic to drive the physical rig based on a kinematic target (Ghost) in real-time.

State Machine Refinement:

Refactored AttackingState to use Physics Forces (AddForceAtPosition, AddTorque) instead of kinematic translations, ensuring attacks interact naturally with the environment.

Implemented Dependency Injection for state initialization to maintain a decoupled and testable codebase.

🔧 Engineering Challenges & Solutions (Troubleshooting Log)
Nested Rigidbody Jitter:

Problem: Severe character jittering occurred upon ragdoll activation.

Diagnosis: Identified a physics feedback loop caused by nested Rigidbodies (Root & Spine) lacking a direct Joint connection.

Solution: Implemented a logic in RagdollController to switch the Root Rigidbody to isKinematic = true immediately upon death, stabilizing the simulation.

Joint Separation ("Spaghetti Effect"):

Problem: Limbs stretching unnaturally during collisions.

Solution: Optimized Solver Iterations (increased to 30) and enforced Locked linear motion on ConfigurableJoints to maintain skeletal integrity under high stress.

🎓 Key Competencies Demonstrated
Advanced Unity Physics: Proficient use of ConfigurableJoints, Solver Iterations, and Collision Layers.

Clean Code Principles: Applied DRY (Don't Repeat Yourself) by abstracting physics state toggling into helper methods and SRP (Single Responsibility Principle) by isolating motion copying logic.

Mathematical Application: Utilized Quaternion math for relative rotation mapping and vector projections for character orientation.