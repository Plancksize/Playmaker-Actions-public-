//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Converts a world position to cell position.")]

    public class TileWorldToCell : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [ObjectType(typeof(Tilemap))]
        [Tooltip("Tilemap to find the Cell position\nIgnored if a GameObject with a tilemap component is provide")]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Input")]

        [Tooltip("World position V3 to convert into Cell position")]
        [Title("World Position")]
        public FsmVector3 position;

        [ActionSection("")]

        [Tooltip("World X position to get the tile. Offsets Vector3 if provided")]
        [Title("World Position X")]
        public FsmFloat posX;

        [Tooltip("World Y position to get the tile. Offsets Vector3 if provided")]
        [Title("World Position Y")]
        public FsmFloat posY;

        [Tooltip("World Z position to get the tile. Offsets Vector3 if provided")]
        [Title("World Position Z")]
        public FsmFloat posZ;

        [ActionSection("Result")]

        [Tooltip("Stores the Cell value as Vector3")]
        [Title("Store Cell Position")]
        public FsmVector3 cellPosition;

        [ActionSection("")]

        [Tooltip("Stores the Cell X value")]
        [Title("Store Cell X")]
        public FsmInt cellX;

        [Tooltip("Stores the Cell Y value")]
        [Title("Store Cell Y")]
        public FsmInt cellY;

        [Tooltip("Stores the Cell Z value")]
        [Title("Store Cell Z")]
        public FsmInt cellZ;

        [ActionSection("On Update")]

        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        private Vector3 positionWorld;
        private GridLayout gridLayout;
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
            if (tilemapObject.Value == null || tilemap.Value == null)
                return "Either a Tilemap or a GameObject with a Tilemap is required.";

            return "";
        }

        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmFloat { UseVariable = true };
            posY = new FsmFloat { UseVariable = true };
            posZ = new FsmFloat { UseVariable = true };
            cellX = new FsmInt { UseVariable = true };
            cellY = new FsmInt { UseVariable = true };
            cellZ = new FsmInt { UseVariable = true };
            cellPosition = new FsmVector3 { UseVariable = true };
            positionWorld = new Vector3(0, 0, 0);
            grid = null;
            map = null;
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
                Finish();
        }

        public override void OnUpdate()
        {
            Action();
        }

        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (position.IsNone)
                positionWorld = new Vector3(posX.Value, posY.Value, posZ.Value);
            else
                positionWorld = new Vector3(position.Value.x + posX.Value, position.Value.y + posY.Value, position.Value.z + posZ.Value);

            grid = map.layoutGrid;

            Vector3Int intCellPos = grid.WorldToCell(positionWorld);

            cellPosition.Value = intCellPos;
            cellX.Value = intCellPos.x;
            cellY.Value = intCellPos.y;
            cellZ.Value = intCellPos.z;
        }
    }
}