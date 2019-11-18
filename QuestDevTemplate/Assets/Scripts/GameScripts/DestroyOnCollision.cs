using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public float timeTillDestroy = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Static")
        {
            Destroy(this.gameObject, timeTillDestroy);
        }
    }
}
