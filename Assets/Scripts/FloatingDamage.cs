using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    private float moveSpeed = 1;
    private float alphaSpeed = 1f;
    private float destroyTime = 2;
    private Text text;
    private Color alpha = Color.white;
    private Vector3 position;
    private Vector3 offset = Vector3.up;

    private void Awake()
    {
        text = GetComponent<Text>();
        Destroy(gameObject, destroyTime);
    }

    public void Init(string damage, Vector3 position)
    {
        this.position = position;
        text.text = damage;
        offset.x += Random.Range(-1, 1);
    }

    private void Update()
    {
        offset += Vector3.up * moveSpeed * Time.deltaTime;
        Vector3 newPos = position + offset;
        newPos.z = 0;
        transform.position = newPos;
        alpha.a = Mathf.Lerp(alpha.a, 0, alphaSpeed * Time.deltaTime);
        text.color = alpha;
    }
}
