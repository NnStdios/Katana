using UnityEngine;
using ArtificeToolkit.Attributes;
using NnUtils.Scripts;
using Assets.Scripts.Core;
using System.Collections;

namespace Assets.Scripts.Colosseum
{
    // TODO: Move _fragmentSettings to scene manager or sm
    public class ColosseumRock : MonoBehaviour
    {
        [SerializeField, Required] private GameObject _rockObject;

        private Rigidbody[] _fracturedRigidbodies;
        [SerializeField, Required] private GameObject _fracturedObject;
        [SerializeField, Required] private FragmentScript _fragmentSettings;
        [SerializeField] private float _explosionForce = 500;

        private void Awake()
        {
            _fracturedRigidbodies = _fracturedObject.GetComponentsInChildren<Rigidbody>();
            _fracturedObject.SetActive(false);
            ColosseumSceneManager.Player.OnPerformedAction += Explode;
        }

        private void Explode(PlayerAction playerAction)
        {
            ColosseumSceneManager.Player.OnPerformedAction -= Explode;

            _rockObject.SetActive(false);
            _fracturedObject.SetActive(true);

            foreach (var r in _fracturedRigidbodies)
                r.AddExplosionForce(_explosionForce, transform.position, 10, 0, ForceMode.Impulse);

            StartCoroutine(DestroyPieces());
        }

        private IEnumerator DestroyPieces()
        {
            yield return new WaitForSeconds(_fragmentSettings.Lifetime);
            float lerpPos = 0;

            while (lerpPos < 1)
            {
                var t = Misc.Tween(ref lerpPos, _fragmentSettings.DisappearTime,
                    0, _fragmentSettings.DisappearCurve);
                // t is clamped to avoid physics errors
                t = Mathf.Clamp(t, 0.0001f, 1);

                foreach (var r in _fracturedRigidbodies)
                    r.transform.localScale = Vector3.one * t;
                yield return null;
            }

            _fracturedObject.SetActive(false);
        }
    }
}