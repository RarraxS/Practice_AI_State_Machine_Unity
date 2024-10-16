using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int Rutina;
    public float Clock;
    public float Grado;


    public Quaternion Angulo;


    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ComprtamientoEnemigo();
        Target = GameObject.Find("Player");
    }

    public void ComprtamientoEnemigo()
    {

        if(Vector3.Distance(transform.position, Target.transform.position) > 5)
        {
            Clock += 1 * Time.deltaTime;
            if (Clock >= 4)
            {
                Rutina = Random.Range(0, 2);
                Clock = 0;

            }
            switch (Rutina)
            {
                case 0:
                    //
                    break;

                case 1:
                    
                    Grado = Random.Range(0, 360);
                    Rutina++;
                    
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Angulo, 0.5f);
                    break;
                
            }

        }
        else
        {

            if(Vector3.Distance(transform.position, Target.transform.position) > 1)
            {
                var lookPos = Target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);



                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            }
            
        }

    }
}
