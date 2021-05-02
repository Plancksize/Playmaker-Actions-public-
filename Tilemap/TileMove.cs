//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Changes Position of a Tile inside a Tilemap")]

    public class TileMove : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to get the tile from\nIgnored if a GameObject with a tilemap component is provided")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Original Position")]

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

        [ActionSection("New Position")]

        [Tooltip("V3 Cell position to set the tile")]
        [Title("Cell Position")]
        public FsmVector3 newposition;

        [ActionSection("")]

        [Tooltip("X position to set the tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        public FsmInt newposX;

        [Tooltip("Y position to set the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        public FsmInt newposY;

        [Tooltip("Z position to set the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        public FsmInt newposZ;

        [ActionSection("Requirement")]

        [Tooltip("Sets Tile Flags to NONE. Needed in case LockTransform flag is set on the tile.")]
        public bool unlockTileFlags = true;

        [ActionSection("On Update")]
        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        private Tilemap map;
        private Vector3Int positionInt;
        private Vector3Int newpositionInt;

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

        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmInt { UseVariable = true };
            posY = new FsmInt { UseVariable = true };
            posZ = new FsmInt { UseVariable = true };
            newposition = new FsmVector3 { UseVariable = true };
            newposX = new FsmInt { UseVariable = true };
            newposY = new FsmInt { UseVariable = true };
            newposZ = new FsmInt { UseVariable = true };
            positionInt = new Vector3Int();
            newpositionInt = new Vector3Int();
            map = null;
            unlockTileFlags = true;
            everyFrame = false;
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

            if (!everyFrame)
            {
                Finish();
            }
        }

        //On Update
        public override void OnUpdate()
        {
            Action();
        }

        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (position.IsNone)
                positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);
            else
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));

            if (newposition.IsNone)
                newpositionInt = new Vector3Int(newposX.Value, newposY.Value, newposZ.Value);
            else
                newpositionInt = new Vector3Int(Mathf.RoundToInt(newposition.Value.x + newposX.Value), Mathf.RoundToInt(newposition.Value.y + newposY.Value), Mathf.RoundToInt(newposition.Value.z + newposZ.Value));

            if (unlockTileFlags)
                map.SetTileFlags(positionInt, TileFlags.None);

            Matrix4x4 matrix = Matrix4x4.TRS(newpositionInt, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
            map.SetTransformMatrix(positionInt, matrix);

        }
    }
}