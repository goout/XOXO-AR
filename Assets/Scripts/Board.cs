using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour {

    public Cell[] cells;

    private int[][] _winLines = new int[][]
 {
    new int[] {0,1,2}, new int[] {3,4,5}, new int[] {6,7,8},
    new int[] {0,3,6}, new int[] {1,4,7}, new int[] {2,5,8},
    new int[] {0,4,8}, new int[] {2,4,6}
 };

    public int[][] winLines { get { return _winLines; } }

    public int[] GetLinesSum() {
        int[] winnerLinesSum = new int[8];
        int sum;

        for (int i = 0; i < _winLines.Length; i++) {
            sum = 0;
            for (int j = 0; j < _winLines[i].Length; j++) {
                sum += cells[_winLines[i][j]].getValue();
            }
            winnerLinesSum[i] = sum;
        }
        return winnerLinesSum;
    }

    public void RemoveMarkers() {
        foreach (var cell in cells)
            cell.RemoveMarker();
    }

    public void SetColiders(bool enabled) {
        foreach (var cell in cells)
            cell.SetColliderEnabled(enabled);
    }

}
