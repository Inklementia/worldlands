using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Sources.Scripts.Dungeon 
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class ExitDoor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BoxCollider2D collider;
        private void Reset()
        {
            rb.isKinematic = true;
            collider.isTrigger = true;
            collider.size = Vector2.one * 0.1f;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}