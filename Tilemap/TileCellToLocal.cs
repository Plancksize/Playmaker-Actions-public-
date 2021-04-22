//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Converts a cell position to local position space.")]

    public class TileCellToLocal : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [ObjectType(typeof(Tilemap))]
        [Tooltip("Tilemap to find the Cell position")]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Position")]

        [Tooltip("Cell position to convert into Local position")]
        [Title("Cell Position")]
        public FsmVector3 position;

        [Tooltip("Cell X position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        public FsmInt cellX;

        [Tooltip("Cell Y position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        public FsmInt cellY;

        [Tooltip("Cell Z position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        public FsmInt cellZ;

        [ActionSection("Result")]

        [Tooltip("Stores the Local position as Vector3")]
        [Title("Store Local Position")]
        public FsmVector3 localPosition;

        [Tooltip("Stores the Local position X")]
        [Title("Store Local X")]
        public FsmFloat posX;

        [Tooltip("Stores the Local position Y")]
        [Title("Store Local Y")]
        public FsmFloat posY;

        [Tooltip("Stores the Local position Z")]
        [Title("Store Local Z")]
        public FsmFloat posZ;

        [ActionSection("On Update")]

        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        private Vector3 positionCell;
        private Vector3Int positionInt;
        private Tilemap map;
        private GridLayout grid;

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

        //On Reset
        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            localPosition = new FsmVector3 { UseVariable = true };
            posX = new FsmFloat { UseVariable = true };
            posY = new FsmFloat { UseVariable = true };
            posZ = new FsmFloat { UseVariable = true };
            cellX = new FsmInt { UseVariable = true };
            cellY = new FsmInt { UseVariable = true };
            cellZ = new FsmInt { UseVariable = true };
            positionCell = new Vector3(0, 0, 0);
            positionInt = new Vector3Int(0, 0, 0);
            grid = null;
            map = null;
            everyFrame = false;
        }

        //On Enter
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
                Finish();
        }

        //On Update
        public override void OnUpdate()
        {
            Action();
        }

        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (position.IsNone)
                positionCell = new Vector3(cellX.Value, cellY.Value, cellZ.Value);
            else
                positionCell = new Vector3(position.Value.x + cellX.Value, position.Value.y + cellY.Value, position.Value.z + cellZ.Value);

            positionInt = new Vector3Int(Mathf.RoundToInt(positionCell.x), Mathf.RoundToInt(positionCell.y), Mathf.RoundToInt(positionCell.z));

            grid = map.layoutGrid;

            localPosition.Value = grid.CellToLocal(positionInt);
            posX.Value = localPosition.Value.x;
            posY.Value = localPosition.Value.y;
            posZ.Value = localPosition.Value.z;
        }
    }
}