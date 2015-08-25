using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonCommandment : MonoBehaviour {
    List<List<TileBase>> currentFloor;
    
    [SerializeField]
    float standardMovementDuration = 0.1f;

    [SerializeField]
    GraphicsParent graphicsParent;

    DungeonInputMediator inputMediator;
    PlayerCharacter playerCharacter;
    // Use this for initialization
	IEnumerator Start () {
        inputMediator = new DungeonInputMediator();

        var generator = new SimpleTerrain();
        generator.Width = 20;
        generator.Height = 15;
        currentFloor = generator.Generate();

        yield return 0;

        //playerCharacter = PutObject<PlayerCharacter>(2, 2);
        //TerrainGraphicController.Instance.Reset(1, 1, currentFloor);
        graphicsParent.Terrain.Reset(new DungeonAxis(1, 1), currentFloor);
    }
	
    public void PlayerMove(int vertical, int horizontal)
    {
        graphicsParent.PlayerGraphicsMove(new DungeonAxis(horizontal, vertical), playerCharacter.axis);
        /*
        TerrainGraphicController.Instance.Move(
            vertical, 
            horizontal, 
            playerCharacter.x, 
            playerCharacter.y, 
            currentFloor, 
            standardMovementDuration, 
            () => inputMediator.IsEnabled = true);
            */
        playerCharacter.axis.x++;
        playerCharacter.axis.y++;
    }
}
