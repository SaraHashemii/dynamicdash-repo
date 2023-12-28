using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _collectiblePrefab;


    private int[] _cells;
    private int[] _ruleset = { 0, 1, 0, 1, 1, 0, 1, 0 };

    private Vector3 _startPos = new Vector3(0, 0, 0);
    private float _spacing = 1.8f;

    private int _currentGeneration = 0;
    private int _maxGeneration = 200;


    private void Start()
    {
        int rowCellsNumber = 30;

        _cells = new int[rowCellsNumber];
        _cells[rowCellsNumber / 2] = 1;


        InvokeRepeating("Generate", 0.2f, 0.2f);
    }

    private float _zOffsetPerGeneration = 1.0f;

    private void Generate()
    {
        if (_currentGeneration >= _maxGeneration)
        {
            CancelInvoke("Generate");
            return;
        }


        int centerCellIndex = _cells.Length / 2;

        for (int i = 0; i < _cells.Length; i++)
        {
            float zOffset = _currentGeneration * _zOffsetPerGeneration;

            Vector3 position = _startPos + new Vector3((i - centerCellIndex) * _spacing, 0, zOffset);

            if (_cells[i] == 1)
            {
                if (Random.Range(0f, 1f) < 0.7f)
                {
                    Instantiate(_obstaclePrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_collectiblePrefab, position, Quaternion.identity);
                }
            }
        }


        int[] nextGen = new int[_cells.Length];

        for (int i = 1; i < _cells.Length - 1; i++)
        {
            int left = _cells[i - 1];
            int me = _cells[i];
            int right = _cells[i + 1];
            nextGen[i] = Rules(left, me, right);
        }
        _cells = nextGen;
        _currentGeneration++;
    }


    private int Rules(int a, int b, int c)
    {
        string s = "" + a + b + c;
        int index = System.Convert.ToInt32(s, 2);
        return _ruleset[index];
    }



}
