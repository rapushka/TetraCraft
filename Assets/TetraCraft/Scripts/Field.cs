﻿using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ActiveTetramino _tetramino;
    private BlockMaterial[,] _blocks;
    private List<Vector2Int> _tetraminoPosition;

    private void Start()
    {
        _blocks = FillArray();
        _tetraminoPosition = new List<Vector2Int>();
    }

    private BlockMaterial[,] FillArray()
    {
        const int ClassicTetrisFieldWidth = 10;
        const int ClassicTetrisFieldHeigth = 20;
        BlockMaterial[,] array = new BlockMaterial
            [ClassicTetrisFieldWidth, ClassicTetrisFieldHeigth];
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = Air.Instance;
            }
        }

        return array;
    }

    private void OnEnable()
    {
        _spawner.TetraminoSpawned += OnTetraminoSpawned;
        _tetramino.Moved += OnTetraminoMoved;
        _tetramino.Falled += OnTetraminoFalled;
    }

    private void OnDisable()
    {
        _spawner.TetraminoSpawned -= OnTetraminoSpawned;
        _tetramino.Moved -= OnTetraminoMoved;
        _tetramino.Falled -= OnTetraminoFalled;
    }

    public void OnTetraminoSpawned(ActiveTetramino tetramino)
    {
        _tetraminoPosition.Clear();
        foreach (Block block in tetramino.Blocks)
        {
            int x = block.Position.x;
            int y = block.Position.y;

            if ((_blocks[x, y] is Air) == false)
            {
                GameOver();
            }

            _blocks[x, y] = block.Material;
            _tetraminoPosition.Add(block.Position);
        }
    }

    private void OnTetraminoFalled()
    {
        for (int i = 0; i < _blocks.GetLength(0); i++)
        {
            bool isFullRow = false;
            for (int j = 0; j < _blocks.GetLength(1); j++)
            {
                if ((_blocks[i, j] is Air) == false)
                {
                    isFullRow = true;
                }
            }
            if (isFullRow)
            {
                ClearLine(i);
            }
        }
    }

    private void OnTetraminoMoved(Vector2Int newPosition)
    {
        foreach (Vector2Int oldPosition in _tetraminoPosition)
        {
            _blocks[oldPosition.x, oldPosition.y] = Air.Instance;
        }
    }

    public void ClearLine(int indexOfRow)
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
        throw new System.NotImplementedException();
    }
}
