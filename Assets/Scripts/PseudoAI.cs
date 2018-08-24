using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoAI : Singleton<PseudoAI> {
    private const int PRE_WIN_PLAYER_SUM = -8;
    private const int PRE_WIN_AI_SUM = -6;

    private Cell GetEmptyFromWinLine(Cell[] markedCells, int[] targetIndexes) {
        for (int j = 0; j < targetIndexes.Length; j++) {
            if (!markedCells[targetIndexes[j]].IsMarked()) {
                return markedCells[targetIndexes[j]];
            }
        }
        return GetAnyEmpty(markedCells);
    }

    private Cell GetEmptyFromMaxLineSum(Board board) {
        int[] solutions = board.GetLinesSum();
        int[] targetLine;

        for (int i = 0; i < solutions.Length; i++) {
            if (solutions[i] == PRE_WIN_AI_SUM || solutions[i] == PRE_WIN_PLAYER_SUM) {
                targetLine = board.winLines[i];
                return GetEmptyFromWinLine(board.cells, targetLine);
            }
        }
        return GetAnyEmpty(board.cells);
    }

    private Cell GetAnyEmpty(Cell[] markedCells) {
        Cell[] emptyIndexes = GetEmptyCells(markedCells);
        return emptyIndexes[Random.Range(0, emptyIndexes.Length - 1)];
    }

    private Cell[] GetEmptyCells(Cell[] markedCells) {
        List<Cell> emptyIndexes = new List<Cell>();

        for (int i = 0; i < markedCells.Length; i++) {
            if (!markedCells[i].IsMarked()) {
                emptyIndexes.Add(markedCells[i]);
            }
        }
        return emptyIndexes.ToArray();
    }

    public Cell AITap(Board board) {
        int turnsCount = GameManager.instance.turnsCount;  
        if (turnsCount == 1 || turnsCount == 2) {
            return GetAnyEmpty(board.cells);
        }
        return GetEmptyFromMaxLineSum(board);
    }

}
