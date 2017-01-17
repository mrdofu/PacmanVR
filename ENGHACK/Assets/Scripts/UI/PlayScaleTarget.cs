using UnityEngine;
using System.Collections;

public class PlayScaleTarget : ScaleTarget {
    private GameManager gameManager;
    
    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	public void onPointerDown()
    {
        gameManager.GamePaused = false;
    }
}
