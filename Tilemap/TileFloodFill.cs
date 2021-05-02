//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tilemap")]
    [Tooltip("Does a flood fill with the given tile to place. on the tile map starting from the given coordinates.")]

    public class TileFloodFill : FsmStateAction
	{
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [ObjectType(typeof(Tilemap))]
        [Tooltip("Tilemap used for the flood action\nIgnored if a GameObject with a tilemap component is provided")]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Tile")]

        [ObjectType(typeof(Tile))]
        [Tooltip("Will Flood with empty tile if None.")]
        [Title("Tile to flood")]
        public FsmObject tile;

        [ActionSection("Position")]

        [Tooltip("Cell position to start the flood action")]
        [Title("Starting Cell Position")]
        public FsmVector3 position;

        [ActionSection("")]

        [Tooltip("Starting Cell X coordinate. Offsets Vector3 if provided")]
        [Title("Starting Cell X")]
        public FsmInt posX;

        [Tooltip("Starting Cell Y coordinate. Offsets Vector3 if provided")]
        [Title("Starting Cell Y")]
        public FsmInt posY;

        [Tooltip("Starting Cell Z coordinate. Offsets Vector3 if provided")]
        [Title("Starting Cell Z")]
        public FsmInt posZ;
        
        private Tilemap map;
        private Tile item;
        private Vector3Int positionInt;

        //Hides Tilemap variable option * via [HideIf("HideTilemap")] * if a GameObject with a Tilemap is provided
        private bool hideTilemap = false;
        public bool HideTilemap()
        {
            if (tilemapObject.Value != null)
                hideTilemap = true;
            else
                hideTilemap = false;
            return hideTilemap;
        }

        //Checks for required variables
        public override string ErrorCheck()
        {
            if (tilemapObject.Value == null || tilemap.Value == null)
                return "Either a Tilemap or a GameObject with a Tilemap is required.";

            return "";
        }

        //On Reset
        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmInt { UseVariable = true };
            posY = new FsmInt { UseVariable = true };
            posZ = new FsmInt { UseVariable = true };
            tile = new FsmObject { UseVariable = true };
            map = null;
        }

        public override void OnEnter()
		{
            //Error Debug Log
            if (tilemapObject.Value == null && tilemap.Value == null)
            {
                Debug.LogWarning("Either a Tilemap or a GameObject with a Tilemap is required." + " @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            if (tilemapObject.Value != null)
                tilemap = tilemapObject.Value.GetComponent<Tilemap>();

            Action();

            Finish();
		}

        void Action()
        {
            map = tilemap.Value as Tilemap;
            item = tile.Value as Tile;

            if (!position.IsNone)
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));
            else
                positionInt = new Vector3Int(posX.Value, posY.Value, posY.Value);

            map.FloodFill(positionInt, item);
        }
    }

}
