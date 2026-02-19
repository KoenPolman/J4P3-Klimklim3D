using System.Collections;
using UnityEngine;

namespace Game.Birds
{
    public class BirdController : MonoBehaviour
    {
        public enum BirdState { None, Sitting, Approaching, Circling, Fleeing }

        public SpriteRenderer sprite;

        [SerializeField] private BirdState _state;

        private BirdConfig _config;
        private Transform _sittingPoint;
        private Camera _cam;
        private Coroutine _routine;

        private float _lockedZ;

        public void Init(BirdConfig config, Transform sittingPoint)
        {
            _config = config;
            _sittingPoint = sittingPoint;
            _cam = Camera.main;
            _lockedZ = _sittingPoint.position.z;
            _state = BirdState.None;
        }

        public void SpawnSitting()
        {
            StopCurrentRoutine();

            transform.position = LockToPlane(_sittingPoint.position);
            _state = BirdState.Sitting;
        }

        public void SpawnOffscreen()
        {
            StopCurrentRoutine();

            transform.position = LockToPlane(GetOffscreenStartLeftOrRight(_sittingPoint.position));
            _state = BirdState.Approaching;

            _routine = StartCoroutine(ApproachRoutine());
        }

        public void OnHandScare()
        {
            if (_state == BirdState.Fleeing) return;

            StopCurrentRoutine();

            if (Random.Range(0, 100) < _config.circleHoldChance)
            {
                _state = BirdState.Circling;
                _routine = StartCoroutine(CircleThenFleeRoutine());
            }
            else
            {
                _state = BirdState.Fleeing;
                _routine = StartCoroutine(FleeRoutine());
            }
        }

        private IEnumerator ApproachRoutine()
        {
            while (true)
            {
                Vector3 target = LockToPlane(_sittingPoint.position);
                Vector3 current = transform.position;

                Vector3 toTarget = target - current;
                FaceDirection(toTarget);

                transform.position = Vector3.MoveTowards(current, target, _config.approachSpeed * Time.deltaTime);

                if (toTarget.magnitude <= _config.sittingSnapDistance)
                {
                    transform.position = target;
                    _state = BirdState.Sitting;
                    yield break;
                }

                yield return null;
            }
        }

        private IEnumerator CircleThenFleeRoutine()
        {
            float t = 0f;
            float angle = Random.Range(0f, 360f);
            Vector3 center = LockToPlane(_sittingPoint.position);

            while (t < _config.circleDuration)
            {
                t += Time.deltaTime;
                angle += _config.circleAngularSpeedDeg * Time.deltaTime;

                float rad = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * _config.circleRadius;
                Vector3 desired = center + offset;

                Vector3 dir = desired - transform.position;
                FaceDirection(dir);

                transform.position = Vector3.MoveTowards(transform.position, desired, _config.fleeSpeed * Time.deltaTime);
                transform.position = LockToPlane(transform.position);

                yield return null;
            }

            _state = BirdState.Fleeing;
            _routine = StartCoroutine(FleeRoutine());
        }

        private IEnumerator FleeRoutine()
        {
            Vector3 fleeTarget = LockToPlane(GetOffscreenFleeLeftOrRight(transform.position));

            float t = 0f;
            while (t < _config.fleeTime)
            {
                t += Time.deltaTime;

                Vector3 dir = fleeTarget - transform.position;
                FaceDirection(dir);

                transform.position = Vector3.MoveTowards(transform.position, fleeTarget, _config.fleeSpeed * Time.deltaTime);
                transform.position = LockToPlane(transform.position);

                yield return null;
            }

            Destroy(gameObject);
        }

        private Vector3 GetOffscreenStartLeftOrRight(Vector3 sittingWorld)
        {
            bool fromLeft = Random.Range(0, 2) == 0;

            Vector3 vp = _cam.WorldToViewportPoint(sittingWorld);

            float x = fromLeft ? -_config.offscreenPadding : 1f + _config.offscreenPadding;
            float y = Mathf.Clamp(vp.y, 0.05f, 0.95f);
            float z = vp.z;

            Vector3 world = _cam.ViewportToWorldPoint(new Vector3(x, y, z));
            world.z = _lockedZ;
            return world;
        }

        private Vector3 GetOffscreenFleeLeftOrRight(Vector3 fromWorld)
        {
            Vector3 vp = _cam.WorldToViewportPoint(fromWorld);

            bool fleeLeft = vp.x < 0.5f;

            float x = fleeLeft ? -_config.offscreenPadding : 1f + _config.offscreenPadding;
            float y = Mathf.Clamp(vp.y, 0.05f, 0.95f);
            float z = vp.z;

            Vector3 world = _cam.ViewportToWorldPoint(new Vector3(x, y, z));
            world.z = _lockedZ;
            return world;
        }

        private Vector3 LockToPlane(Vector3 pos)
        {
            pos.z = _lockedZ;
            return pos;
        }

        private void FaceDirection(Vector3 dir)
        {
            if (sprite != null && Mathf.Abs(dir.x) > 0.001f)
                sprite.flipX = dir.x < 0f;
        }

        private void StopCurrentRoutine()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand"))
                OnHandScare();
        }
    }
}

