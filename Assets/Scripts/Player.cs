using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        direction.Normalize();

        direction *= speed;

        tr.position += direction;




        tr.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);

    }
}
