//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Compares two tiles. Can compare a tile directly or a tile found at a XYZ coordinate on a given tilemap")]

    public class TileCompare : FsmStateAction
    {
        [ActionSection ("Tile")]

        [RequiredField]
        [Title("Tile to Compare")]
        [HideIf("HideOption")]
        [Tooltip("Tile to compare (Using this will disable getting the tile from a tilemap position)")]
        [ObjectType(typeof(Tile))]
        public FsmObject tile1;

        [ActionSection("Tile from Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        [HideIf("HidePosition")]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to get the tile from (using this will disable the compare with a set tile above)")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("")]

        [Tooltip("V3 Cell position to get the tile from (is (0,0,0) if None")]
        [Title("Cell Position Vector3")]
        [HideIf("HidePosition")]
        public FsmVector3 position;

        [ActionSection("")]

        [Tooltip("X position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        [HideIf("HidePosition")]
        public FsmInt posX;

        [Tooltip("Y position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        [HideIf("HidePosition")]
        public FsmInt posY;

        [Tooltip("Z position to get the tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        [HideIf("HidePosition")]
        public FsmInt posZ;

        [ActionSection("Compare With")]

        [RequiredField]
        [Title("Tile to Compare with")]
        [Tooltip("Tile to Compare with")]
        [ObjectType(typeof(Tile))]
        public FsmObject tile2;

        [ActionSection("Results")]

        [Title("Result")]
        [Tooltip("Bool result of the compare action.")]
        public FsmBool compareResult;

        [ActionSection("Events")]

        [Tooltip("Event to send if a Tile is found")]
        public FsmEvent isTrue;

        [Tooltip("Event to send if no Tile is found")]
        public FsmEvent isFalse;

        [ActionSection("On Update")]

        public bool everyFrame;

        private Tilemap map;
        private Vector3Int positionInt;

        //Hides Tilemap variable option * via [HideIf("HideTilemap")] * if a GameObject with a Tilemap is provided
        private bool hideTilemap = false;
        public bool HideTilemap()
        {
            if (tilemapObject.Value != null || !tile1.IsNone)
                hideTilemap = true;
            else
                hideTilemap = false;
            return hideTilemap;
        }

        //Hides Tile to Compare option * via [HideIf("HideOption")] * if a Vector3 is provided
        private bool hide = false;
        public bool HideOption()
        {
            if (!tilemap.IsNone || !tilemapObject.IsNone)
                hide = true;
            else
                hide = false;
            return hide;
        }

        //Hides XYZ variable option * via [HideIf("HidePosition")] * if a Tile to compare is provided
        private bool hidepos = false;
        public bool HidePosition()
        {
            if (!tile1.IsNone)
                hidepos = true;
            else
                hidepos = false;
            return hidepos;
        }

        //Checks for required variables
        public override string ErrorCheck()
        {
            if (tile1.Value == null || tilemap.Value == null || tilemapObject.Value == null)
                return "Either a Tile, a GameObject with Tilemap component or a Tilemap  is required.";

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
            tile1 = new FsmObject { UseVariable = true };
            tile2 = new FsmObject { UseVariable = true };
            compareResult = new FsmBool { UseVariable = true };
            map = null;
        }

        //On Enter
        public override void OnEnter()
        {
            //Error Debug Log
            if (tile1.Value == null && tilemap.Value == null && tilemapObject.Value == null)
            {
                Debug.LogWarning("Either a Tile, a GameObject with Tilemap component or a Tilemap  is required." + " @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
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

        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (tile1.IsNone)
            {
                if (position.IsNone)
                    positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);
                else
                    positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));

                var compareTile = map.GetTile(positionInt);

                if (compareTile == tile2.Value)
                    compareResult.Value = true;
                else
                    compareResult.Value = false;
            }
            else
            {
                if (tile1.Value == tile2.Value)
                    compareResult.Value = true;
                else
                    compareResult.Value = false;
            }

            Fsm.Event(compareResult.Value ? isTrue : isFalse);
        }
    }
}