using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Session;

namespace Shop
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Shop/SkinShopItem")]
    public class SkinShopItem : ShopItem
    {
        [SerializeField]
        protected Mesh mesh;
        [SerializeField]
        protected Material material;

        public override void Activate()
        {
            base.Activate();
            var player = SessionController.Instance.Player;
            player.MeshFilter.mesh = mesh;
            player.Renderer.material = material;
        }
    }
}