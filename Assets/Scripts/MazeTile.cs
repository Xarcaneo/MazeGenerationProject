using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MazeTile : MonoBehaviour
{
    public enum TileState { Wall, Passage }
    public TileState CurrentState;
    public Vector2Int position;

    private void Start()
    {
        CurrentState = TileState.Wall;
    }

    public void ChangeState(TileState newState)
    {
        CurrentState = newState;

        if (CurrentState == TileState.Passage)
            this.gameObject.SetActive(false);
    }

}
