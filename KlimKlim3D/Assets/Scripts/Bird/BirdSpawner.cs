using UnityEngine;

namespace Game.Birds
{
    public class BirdSpawner : MonoBehaviour
    {
        public BirdConfig config;
        public BirdController birdPrefab;
        public Transform sittingPointOverride;
        public bool decideOnStart = true;

        private bool _decided;

        private void Start()
        {
            if (decideOnStart) DecideAndMaybeSpawn();
        }

        public void DecideAndMaybeSpawn()
        {
            if (_decided) return;
            _decided = true;

            if (Random.Range(0, 100) >= config.spawnChance) return;

            bool spawnSitting = Random.Range(0, 100) < config.spawnSittingChance;
            Transform sittingPoint = sittingPointOverride != null ? sittingPointOverride : transform;

            var bird = Instantiate(birdPrefab);
            bird.Init(config, sittingPoint);

            if (spawnSitting) bird.SpawnSitting();
            else bird.SpawnOffscreen();
        }
    }
}