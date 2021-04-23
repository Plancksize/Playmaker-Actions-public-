//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Creates a new Tile instance from a given Sprite.")]

    public class CreateTileFromSprite : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Sprite used to create the Tile")]
        [ObjectType(typeof(Sprite))]
        public FsmObject sprite;

        [RequiredField]
        [Tooltip("Stores new created Tile instance")]
        [Title("Store Tile")]
        [ObjectType(typeof(Tile))]
        public FsmObject tile;

        [ActionSection("Optional")]

        [Tooltip("Optional - Set name to new instantiated Tile")]
        [Title("New Tile name")]
        public FsmString tileName;

        private Tile setTile;

        //On Reset
        public override void Reset()
        {
            sprite = new FsmObject { UseVariable = true };
            tile = new FsmObject { UseVariable = true };
            tileName = null;
            setTile = null;     
        }

        //On Enter
        public override void OnEnter()
        {
            Action();
            
            Finish();
        }

        //Action
        void Action()
        {
            setTile = ScriptableObject.CreateInstance<Tile>();
            setTile.name = tileName.Value;
            setTile.sprite = sprite.Value as Sprite;
            tile.Value = setTile;
        }
    }
}
