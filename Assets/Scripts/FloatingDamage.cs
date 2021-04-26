using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float alphaSpeed = 2f;
    private float destroyTime = 2f;
    private Text text;
    private Color alpha = Color.white;
    private Vector3 originPos;
    private Vector3 offset = Vector3.up;
    private GameObject executor;

    private void Awake()
    {
        text = GetComponent<Text>();
        Destroy(gameObject, destroyTime);
    }

    public void Init(GameObject executor, string damage, Vector3 originPos)
    {
        this.executor = executor;
        this.originPos = originPos;
        text.text = damage;
        offset.x += Random.Range(-0.5f, 0.5f);
        StartCoroutine("FadeOut");
    }

    private void Update()
    {
        offset += Vector3.up * moveSpeed * Time.deltaTime;
        Vector3 newPos = originPos + offset;
        newPos.z = 0;
        transform.position = newPos;
    }

    private IEnumerator FadeOut()
    {
        float percent = 0;
        while (percent < 1)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, percent);
            text.color = alpha;

            percent += Time.deltaTime / alphaSpeed;
            yield return null;
        }
    }

    public void SetPos(float moveValue)
    {
        offset += Vector3.up * moveValue;
    }

    private void OnDestroy()
    {
        FloatingDamageManager.instance.RemoveDamage(executor, this);
    }
}
