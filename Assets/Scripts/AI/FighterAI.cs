using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(Rigidbody), typeof(HealthSystem), typeof(RuntimeCharacter))]
public class FighterAI : MonoBehaviour
{
    // state management
    private IFighterState currentState;

    public MovingState movingState;
    public AttackingState attackingState;


    // component cache
    public Rigidbody Rb {  get; private set; }
    public RuntimeCharacter Stats { get; private set; }
    public HealthSystem Health {  get; private set; }
    public Transform Target { get; private set; }

    [Header("AI Config")]
    public float attackRange = 1.5f;



    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Stats = GetComponent<RuntimeCharacter>();
        Health = GetComponent<HealthSystem>();

        movingState = new MovingState();
        attackingState = new AttackingState();
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
