using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddChips(1);
                scoreManager.UpdateCoinText();
            }

            Destroy(gameObject);
        }
    }
}

