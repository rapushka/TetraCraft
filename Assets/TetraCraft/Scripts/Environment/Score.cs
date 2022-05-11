﻿using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private const int LinesCountForSpeedUp = 10;

    [SerializeField] private Field _field;

    private int _clearedLinesCount;
    private int _scoreValue;
    private int _combo;

    public event Action ScoreUpdated;

    public int ScoreValue => _scoreValue;
    public int ClearedLinesCount => _clearedLinesCount;
    public int Level 
        => _clearedLinesCount / LinesCountForSpeedUp + 1;

    private void OnEnable()
    {
        _field.LineCleared += OnLineCleared;
        _field.TurnDone += OnTurnDone;
    }

    private void OnDisable()
    {
        _field.LineCleared -= OnLineCleared;
        _field.TurnDone -= OnTurnDone;
    }

    private void OnLineCleared()
    {
        int oldLevel = Level;
        _combo++;

        _clearedLinesCount++;
        int lineCoast = GetLineCoastByCombo(_combo);
        _scoreValue += lineCoast * Level;

        ScoreUpdated?.Invoke();
    }

    private int GetLineCoastByCombo(int combo)
    {
        return combo switch
        {
            1 => 40,
            2 => 60,
            3 => 100,
            4 => 600,
            _ => throw new Exception()
        };
    }

    private void OnTurnDone()
    {
        _combo = 0;
    }
}