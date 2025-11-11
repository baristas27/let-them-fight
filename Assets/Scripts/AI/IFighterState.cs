using UnityEngine;

public interface IFighterState
{
    void Enter(FighterAI ai);

    void Execute();

    void Exit();
}
