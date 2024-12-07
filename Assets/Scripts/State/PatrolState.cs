public class PatrolState : State
{
    private int indexPatrol = 0;

    public PatrolState(Enemy enemy) : base(enemy) { }

    public override void Think()
    {
        if(_canSeePlayer)
            _enemy.SetState(new FollowPlayerState(_enemy));
    }

    public override void Act()
    {
        Patrol();
    }

    private void Patrol()
    {
        Move(_enemy.PatrolWayPoints[indexPatrol]);

        indexPatrol = NextWayPoint(_enemy.PatrolWayPoints, indexPatrol);
    }
}
