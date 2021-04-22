using System.Collections;
using UnityEngine;

public class LazyCamera : MonoBehaviour
{
    public static LazyCamera instance;
    [SerializeField]
    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;
    private float originSize;
    private bool isShake = false;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        if (!target)
        {
            target = GameObject.Find("Player").transform;
        }

        originSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 destination = new Vector3(target.position.x, target.position.y + 0.5f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
    }

    public IEnumerator Shake(float amount, float duration)
    {
        if (isShake == false)
        {
            isShake = true;
            float time = 0;
            while (time < duration)
            {
                transform.position += (Vector3)Random.insideUnitCircle * amount;
                time += Time.deltaTime;
                yield return null;
            }
        }
        isShake = false;
    }

    public IEnumerator ZoomIn(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize / amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ZoomOut(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize * amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }
}
