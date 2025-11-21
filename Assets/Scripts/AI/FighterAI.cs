using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(RuntimeCharacter))]
[RequireComponent(typeof(DamageDealer))]
public class FighterAI : MonoBehaviour
{
    // state management
    private IFighterState currentState;

    public MovingState movingState;
    public AttackingState attackingState;
    public IdleState idleState;


    // component cache
    public Rigidbody Rb {  get; private set; }
    public RuntimeCharacter Stats { get; private set; }
    public HealthSystem Health {  get; private set; }
    public Transform Target { get; private set; }

    public DamageDealer DamageDealer { get; private set; }

    public Rigidbody weaponArm;

    public Transform weaponTip;

    [Header("AI Config")]
    public float attackRange = 1.5f;

    public float chaseRange = 2.0f;

    public float attackForce = 500f;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Stats = GetComponent<RuntimeCharacter>();
        Health = GetComponent<HealthSystem>();
        DamageDealer = GetComponent<DamageDealer>();

        movingState = new MovingState();
        attackingState = new AttackingState();
        idleState = new IdleState();
    }


    private void Start()
    {
        ChangeState(movingState);
    }

    private void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IFighterState newState)

    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter(this);
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }


}
