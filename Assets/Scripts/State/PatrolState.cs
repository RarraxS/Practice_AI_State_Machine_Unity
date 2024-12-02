using UnityEngine;

public class PatrolState : State
{
    public PatrolState(Enemy enemy) : base(enemy) { }

    //Think DE PATRULLAR STATE -> si ve al jugador -> debe cambiar al estado seguir 
    public override void Think()
    {
        if(this._canSeePlayer)
            this._enemy.SetState(new FollowPlayerState(this._enemy));
    }

    public override void Act()
    {
        
    }
}
