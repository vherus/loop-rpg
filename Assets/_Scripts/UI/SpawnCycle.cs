using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SpawnCycle : MonoBehaviour
{
    public UnityEvent SpawnCycleComplete;

    [SerializeField]
    private Image bar;

    [SerializeField]
    private float fillTime = 15f;
    
    void Start()
    {
        SpawnCycleComplete.AddListener(GameManager.Instance.HandleSpawnCycleComplete);
        SpawnCycleComplete.AddListener(GameObject.FindWithTag("Environment").GetComponent<Environment>().InitiateSpawn);
    }

    void Update()
    {
        if (bar.fillAmount == 1f) {
            bar.fillAmount = 0f;
            SpawnCycleComplete.Invoke();
            return;
        }

        bar.fillAmount += 1f / fillTime * Time.deltaTime;
    }
}
