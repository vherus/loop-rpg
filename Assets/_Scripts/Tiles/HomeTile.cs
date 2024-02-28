using UnityEngine;

public class HomeTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.EnemyLevel++;
        Debug.Log(other.tag + " entered the home tile. Leveling enemies up.");
    }
}
