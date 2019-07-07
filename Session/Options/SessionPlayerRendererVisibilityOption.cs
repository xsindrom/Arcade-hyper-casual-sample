using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Session.Options
{
    [CreateAssetMenu(menuName ="ScriptableObjects/Session/Options/SessionPlayerRendererVisibilityOption")]
    public class SessionPlayerRendererVisibilityOption : SessionOption
    {
        [SerializeField]
        private bool visible;
        public bool Visible
        {
            get { return visible; }
        }

        public override void StartSession()
        {
            SessionController.Instance.Player.Renderer.enabled = visible;
        }
    }
}