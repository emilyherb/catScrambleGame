using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float despawnZ;

    public void Initialize(Vector3 dir, float spd, float despawnZVal)
    {
        direction = dir.normalized;
        speed = spd;
        despawnZ = despawnZVal;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.z <= despawnZ)
        {
            Destroy(gameObject);
        }
    }
}
