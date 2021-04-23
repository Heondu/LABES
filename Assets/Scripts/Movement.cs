using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;


    public void Execute(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Execute(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
