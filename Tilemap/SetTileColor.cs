//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Sets the color of a tile given the XYZ coordinates of a cell in the tile map.")]

    public class SetTileColor : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to get the tile from")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Position")]

        [Tooltip("V3 Cell position to get the tile from")]
        [Title("Cell Position")]
        public FsmVector3 position;

        [ActionSection("")]

        [Tooltip("X position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        public FsmInt posX;

        [Tooltip("Y position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        public FsmInt posY;

        [Tooltip("Z position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        public FsmInt posZ;

        [RequiredField]
        [Title("Set Color")]
        [Tooltip("Sets the Tile Color")]
        public FsmColor color;

        [ActionSection("Requirement")]

        [Tooltip("Sets Tile Flags to NONE. By default this is needed to be able to change a Tile's color")]
        public bool unlockTileFlags = true;

        [ActionSection("On Update")]

        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        private Tilemap map;
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
            if (tilemapObject.Value == null && tilemap.Value == null)
                return "Either a Tilemap or a GameObject with a Tilemap is required.";

            return "";
        }

        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmInt { UseVariable = true };
            posY = new FsmInt { UseVariable = true };
            posZ = new FsmInt { UseVariable = true };
            color = new FsmColor { UseVariable = true };
            map = null;
            unlockTileFlags = true;
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

            map = tilemap.Value as Tilemap;

            Action();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            Action();
        }

        void Action()
        {
            if (position.IsNone)
                positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);
            else
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));

            if (unlockTileFlags)
                map.SetTileFlags(positionInt, TileFlags.None);

            map = tilemap.Value as Tilemap;

            map.SetColor(positionInt, color.Value);


        }
    }
}