///
/// MaterialFlashBehavior.cs
///

using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace PlayableDesign
{
    /// <summary>
    /// Use MaterialFlashBehavior to flash colors using the
    /// material specified in the settings.
    /// </summary>
    public class MaterialFlashBehavior : MonoBehaviour
    {
        // ----- Inspector

        [Space(10)]
        [Header(AttributeConstants.HEADER_SHARED_SETTINGS)]

        [Tooltip("Designates how long an object should flash after being hit")]
        public FloatVariable flashTime;

        [Space(10)]
        [Header(AttributeConstants.HEADER_INSTANCE_SETTINGS)]

        [Tooltip("The material to use for flashing on hit")]
        public Material flashMaterial;

        [Space(10)]
        [Header(AttributeConstants.HEADER_STATE)]

        [ReadOnly]
        public int _materialCount;

        // ----- Cache

        private Renderer[] _renderers;
        private List<SwappableMaterial> _materials;

        // ----- LifeCycle
        private void Awake()
        {
            _materials = new List<SwappableMaterial>();

            _renderers = GetComponentsInChildren<Renderer>();

            foreach(Renderer r in _renderers)
            {
                foreach(Material m in r.materials)
                {
                    _materials.Add(new SwappableMaterial(m));
                }
            }

            _materialCount = _materials.Count;
        }

        // ----- API

        /// <summary>
        /// Call Flash either directly or as a CoRoutine
        /// </summary>
        /// <returns>null</returns>
        public IEnumerator Flash()
        {
            _materials.ForEach(m => m.Swap(flashMaterial));
            yield return new WaitForSecondsRealtime(flashTime);
            _materials.ForEach(m => m.Revert());
            yield return null;
        }
    }
}


