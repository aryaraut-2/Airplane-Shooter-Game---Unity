using Develop.Runtime.Core.Target;
using UnityEngine;

namespace Develop.Runtime.Core.Shooting
{
    public sealed class Bullet : MonoBehaviour
    {
        private BulletConfig _config;

        public void Initialize(BulletConfig config, LayerMask collideMask)
        {
            _config = config;
            gameObject.layer = collideMask;
            Destroy(gameObject, config.LifeTime);
        }

        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _config.Speed * Time.deltaTime))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(RuntimeConstants.PhysicLayers.TargetBody))
                {
                    hit.collider.GetComponent<TargetController>()?.OnHit?.Invoke();
                }

                Destroy(gameObject);
            }

            transform.position += transform.forward * (_config.Speed * Time.deltaTime);
        }
    }
}