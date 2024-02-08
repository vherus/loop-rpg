using UnityEngine;

public class HomeTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " entered the home tile");
    }
}
