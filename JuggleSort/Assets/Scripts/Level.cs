using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNum;

    [SerializeField] private GameObject _sortablePrefab;
    [SerializeField] private static float _ySpawn = 6f;
    [SerializeField] private int _totalSortables;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _distributionSpread;
    private int _spawnCount;

    public Level(int totalSortables, float spawnRate, float distributionSpread)
    {
        this._totalSortables = totalSortables;
        this._spawnRate = spawnRate;
        this._distributionSpread = distributionSpread;
    }

    public void SpawnSortable() {

        if (_spawnCount < _totalSortables)
        {
            GameObject newSortableObject = Instantiate(_sortablePrefab);
            float x = UnityEngine.Random.Range(-_distributionSpread, _distributionSpread);
            newSortableObject.transform.position = new Vector2(x, _ySpawn);
            newSortableObject.GetComponent<Sortable>().SetColor(Sortable.GetRandomSortColor());
            _spawnCount++;
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnSortable", 3f, _spawnRate);
    }
}
