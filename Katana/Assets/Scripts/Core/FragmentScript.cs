using System.Collections;
using ArtificeToolkit.Attributes;
using NnUtils.Scripts;
using SadnessMonday.BetterPhysics;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BetterRigidbody))]
    [RequireComponent(typeof(Collider))]
    public class FragmentScript : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public BetterRigidbody BetterRigidbody;
        private Collider _collider;

        public float Lifetime = 10;

        [Required]
        [SerializeField] public PhysicsMaterial PhysicsMaterial;
        [FoldoutGroup("Disappear Animation")]
        [SerializeField] public float DisappearTime = 1;
        [FoldoutGroup("Disappear Animation")]
        [SerializeField] public AnimationCurve DisappearCurve;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            BetterRigidbody = GetComponent<BetterRigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void CopySettings(FragmentScript original)
        {
            gameObject.layer = original.gameObject.layer;
            Lifetime = original.Lifetime;
            DisappearTime = original.DisappearTime;
            DisappearCurve = original.DisappearCurve;

            PhysicsMaterial = original.PhysicsMaterial;
            _collider.material = PhysicsMaterial;

            Rigidbody.interpolation = original.Rigidbody.interpolation;
            Rigidbody.collisionDetectionMode = original.Rigidbody.collisionDetectionMode;
            BetterRigidbody.PhysicsLayer = original.BetterRigidbody.PhysicsLayer;
        }

        public void GetDestroyed() => StartCoroutine(DestroyRoutine());

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(Lifetime);
            _collider.enabled = false;
            Rigidbody.useGravity = false;

            var originalScale = transform.localScale;
            float lerpPos = 0;
            while (lerpPos < 1)
            {
                var t = DisappearCurve.Evaluate(Misc.Tween(ref lerpPos, DisappearTime));
                transform.localScale = Vector3.LerpUnclamped(originalScale, Vector3.zero, t);
                yield return null;
            }

            // Parent should get destroyed so no need to cleanup here
        }
    }
}