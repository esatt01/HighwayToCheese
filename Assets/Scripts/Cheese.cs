using UnityEngine;

public class Cheese : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>()?.CollectCheese();
            Destroy(gameObject, 0.1f);
        }
    }
}
