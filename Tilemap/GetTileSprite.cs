﻿//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Gets the Sprite used in a tile given the XYZ coordinates of a cell in the tile map.")]

    public class GetTileSprite : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to get the tile from\nIgnored if a GameObject with a tilemap component is provided")]
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

        [ActionSection("Result")]

        [Title("Store Tile")]
        [Tooltip("Stores the Sprite")]
        [ObjectType(typeof(Sprite))]
        public FsmObject sprite;

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
            sprite = new FsmObject { UseVariable = true };
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

            if (!everyFrame)
            {
                Finish();
            }
        }

        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (position.IsNone)
                positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);
            else
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z + posZ.Value));

            sprite.Value = map.GetSprite(positionInt);
        }
    }
}