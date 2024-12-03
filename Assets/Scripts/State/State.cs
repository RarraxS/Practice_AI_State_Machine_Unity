using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class State
{
    protected Enemy _enemy;
    protected bool _canSeePlayer;

    public State(Enemy enemy) 
    { 
        this._enemy = enemy;
    }

    public void Perceive()
    {
        _enemy.SetState(new PatrolState(this._enemy));
        //this._canSeePlayer = 
    }
    //Perceive DE PATRULLAR STATE -> HA VISTO AL JUGADOR? (LO GUARDA EN UN BOOL)
    //Perceive DE SEGUIR STATE => SIGUE VIENDO AL JUGADOR O LO HA PERDIDO (LO GUARDA EN UN BOOL)
    //Perceive DE SUSPICIOUS STATE => HA VISTO AL JUGADOR

    public abstract void Think();
    //Think DE PATRULLAR STATE -> si ve al jugador -> debe cambiar al estado seguir 
    //Think DE SEGUIR STATE => si no ve al jugador => debe cambiar al estado suspicious
    //Think DE SUSPICIOUS STATE => ya he terminado de recorrer los elementos de la lista? => si es asi, cambiar al estado patrullar
    //                                  o si veo al jugador vuelvo al estado seguir 

    public abstract void Act();
    //Act DE PATRULLAR STATE -> enviar al navmeshagent a la posicion que le toca
    //Act DE SEGUIR STATE => enviar al navmeshagent a la posicion del jugador
    //Act DE SUSPICIOUS STATE => enviar a la poisicion que le toque
}
