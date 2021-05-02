//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Sets a Random Tile from an Array at the given XYZ coordinates of a cell in the tilemap.")]

    public class SetRandomTile : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to place a Random Tile on.\nIgnored if a GameObject with a tilemap component is provided")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Input Array")]

        [RequiredField]
        [ArrayEditor(typeof(Tile))]
        public FsmArray array;

        [ActionSection("Repeat")]

        [Tooltip("Don't repeat twice in a row")]
        public FsmBool noRepeat;

        [ActionSection("Position")]

        [Tooltip("Cell position to place a Random Tile.")]
        [Title("Cell Position")]
        public FsmVector3 position;

        [ActionSection("")]

        [Tooltip("Cell X position to place a Random Tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        public FsmInt posX;

        [Tooltip("Cell Y position to place a Random Tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        public FsmInt posY;

        [Tooltip("Cell Z position to place a Random Tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        public FsmInt posZ;

        [ActionSection("Result")]

        [Tooltip("Random Tile picked by random")]

        [ObjectType(typeof(Tile))]
        public FsmObject RandomTile;

        [Tooltip("Random Tile index in the Array")]
        public FsmInt index;

        [ActionSection("On Update")]

        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        private FsmVar storeValue;
        private Tilemap map;
        private Vector3Int positionInt;
        private int randomIndex;
        private int lastIndex = -1;

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
            array = new FsmArray { UseVariable = true }; ;
            RandomTile = null;
            index = null;
            everyFrame = false;
            noRepeat = false;
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };         
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmInt { UseVariable = true };
            posY = new FsmInt { UseVariable = true };
            posZ = new FsmInt { UseVariable = true };
            index = new FsmInt { UseVariable = true };
            RandomTile = new FsmObject { UseVariable = true };
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

            Finish();
        }

        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (!position.IsNone)
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));
            else
                positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);

            if (!noRepeat.Value || array.Length == 1)
            {
                randomIndex = Random.Range(0, array.Length);
            }
            else
            {
                do
                {
                    randomIndex = Random.Range(0, array.Length);
                } while (randomIndex == lastIndex);

                lastIndex = randomIndex;
            }

            index.Value = randomIndex;
            RandomTile.Value = (Tile)array.Get(index.Value);

            map.SetTile(positionInt, (Tile)array.Get(randomIndex));
        }
    }
}