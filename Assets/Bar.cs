using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bar : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    [SerializeField] TMPro.TextMeshProUGUI timer;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Door door;
    float _time;
    private void Start()
    {
        _time = ResourceManager.instance.workTime;
        StartCoroutine(WorkTime());
        StartCoroutine(ResourceManager.instance.Work());
        StartCoroutine(SpawnController());
    }
    IEnumerator SpawnController()
    {
        while (_time > 0)
        {
            foreach (Transform point in spawnPoints)
            {
                if (point.childCount == 0)
                {
                    float spawnTime = Random.Range(2f, 5f);
                    print("Yes");
                    yield return new WaitForSecondsRealtime(spawnTime);
                    print("after");
                    GameObject clone = Instantiate(customerPrefab, point);
                    clone.transform.localPosition = Vector3.zero;
                    break;
                }
            }
            yield return null;
        }
    }
    IEnumerator WorkTime()
    {
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            timer.text = $"{Mathf.RoundToInt(_time / 60f)}:{Mathf.RoundToInt(_time % 60f)}";

            yield return null;
        }
        StartCoroutine(door.LoadSceneAsync(1));

    }
}
