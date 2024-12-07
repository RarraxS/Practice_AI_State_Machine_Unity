public class FollowPlayerState : State
{
    public FollowPlayerState(Enemy enemy) : base(enemy)
    {

    }

    public override void Think()
    {
        if (!_canSeePlayer)
            _enemy.SetState(new SuspiciousPatrolState(_enemy));
    }

    public override void Act()
    {
        Move(_enemy.Player);
    }
}
