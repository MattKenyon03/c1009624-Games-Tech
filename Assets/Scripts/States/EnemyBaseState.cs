using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(PlayerTrackerAI enemy);

    public abstract void UpdateState(PlayerTrackerAI enemy);

}
