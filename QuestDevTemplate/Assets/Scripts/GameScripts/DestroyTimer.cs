using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float destroyAfter = 3;

    private void Update()
    {
        Destroy(this.gameObject, destroyAfter);
    }
}
