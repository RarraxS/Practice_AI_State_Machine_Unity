using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = MovementVector();
        tr.position += direction * speed;
        tr.rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
    }

    private static Vector3 MovementVector()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;

        direction.Normalize();

        return direction;
    }
}