using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    private int rotationSpeed;
    private float angle = 0;
    [SerializeField]
    private bool left;

    private void Update()
    {
        if (left) angle -= Time.deltaTime * rotationSpeed;
        else angle += Time.deltaTime * rotationSpeed;
        if (angle < 0) angle = 360;
        if (angle > 360) angle = 0;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
