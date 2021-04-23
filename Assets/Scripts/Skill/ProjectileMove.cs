using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField]
    private bool isVector = false;
    [SerializeField]
    private Vector2 velocity;
    [SerializeField]
    private float speed = 1f;

    private void Update()
    {
        if (isVector)
        {
            GetComponent<Movement>().Execute(velocity.normalized, velocity.magnitude * speed);
        }
        else
        {
            GetComponent<Movement>().Execute(new Vector3(0, 1), speed);
        }
    }

}
