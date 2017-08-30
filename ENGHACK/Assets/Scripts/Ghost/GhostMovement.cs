using UnityEngine;

public class GhostMovement : ComputerMovementAI
{
    GameObject pacGo;

    // Use this for initialization
    protected override void OnStart()
    {
        pacGo = GameObject.Find("PACMAN");
    }

    protected override Vector3 UpdateGoal()
    {
        return pacGo.transform.position;
    }
    
}
