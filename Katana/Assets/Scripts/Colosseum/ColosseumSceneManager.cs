using ArtificeToolkit.Attributes;
using Assets.Scripts.Fruits;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Camera;
using UnityEngine;
using UnityCommunity.UnitySingleton;

namespace Assets.Scripts.Colosseum
{
    [RequireComponent(typeof(CameraManager))]
    [RequireComponent(typeof(ExplosionManagerScript))]
    public class ColosseumSceneManager : MonoSingleton<ColosseumSceneManager>
    {
        [SerializeField, Required] private CameraManager _cameraManager;
        public static CameraManager CameraManager => Instance._cameraManager ??=
            Instance.gameObject.GetComponent<CameraManager>();

        [SerializeField, Required] private ExplosionManagerScript _explosionManager;
        public static ExplosionManagerScript ExplosionManager => Instance._explosionManager ??=
            Instance.gameObject.GetComponent<ExplosionManagerScript>();

        [SerializeField, Required] private PlayerScript _player;
        public static PlayerScript Player => Instance._player ??=
            GameObject.FindWithTag("Player")?.GetComponent<PlayerScript>();

        [FoldoutGroup("Fruit")]
        [Tooltip("Force required to destroy an object")]
        [SerializeField] private float _destructionVelocity = 50;
        public static float DestructionVelocity => Instance._destructionVelocity;

        [FoldoutGroup("Fruit")]
        [SerializeField] private float _fruitParticleLifetime = 10;
        public static float FruitParticleLifetime => Instance._fruitParticleLifetime;

        private WaitForSeconds _fruitParticleWait;
        public static WaitForSeconds FruitParticleWait => Instance._fruitParticleWait;

        [FoldoutGroup("Dev")]
        [SerializeField, ReadOnly] private bool _isDead;
        public static bool IsDead => Instance._isDead;

        [SerializeField, Required] private GameObject _deathScreen;

        private void Reset()
        {
            _cameraManager = GetComponent<CameraManager>();
            _explosionManager = GetComponent<ExplosionManagerScript>();
            _player = GameObject.FindWithTag("Player")?.GetComponent<PlayerScript>();
        }

        protected override void Awake()
        {
            base.Awake();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _fruitParticleWait = new WaitForSeconds(FruitParticleLifetime);
        }

        public static void Die()
        {
            Instance._deathScreen.SetActive(true);
            Instance._isDead = true;
        }
    }
}
