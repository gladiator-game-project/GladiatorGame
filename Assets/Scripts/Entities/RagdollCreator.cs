using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class RagdollCreator : MonoBehaviour
    {
        /// <summary>
        /// Instantiates a ragdoll at the position of the alive version
        /// </summary>
        public void CreateRagDoll()
        {
            GameObject dead = Instantiate(Resources.Load("Prefabs/deathNPC", typeof(GameObject))) as GameObject;

            //Copy the armor settings
            CopyArmorSettings(dead.GetComponent<ArmorHandler>());

            //Copy position and rotation to the children recursively:
            CopyTransforms(transform, dead.transform);

            //Destroy the alive version
            Destroy(gameObject);
        }

        private void CopyArmorSettings(ArmorHandler _armorHandlerDead)
        {
            ArmorHandler _armorHandlerAlive = GetComponent<ArmorHandler>();
            _armorHandlerDead.HasChest = _armorHandlerAlive.HasChest;
            _armorHandlerDead.HasHelmet = _armorHandlerAlive.HasHelmet;
            _armorHandlerDead.HasSkirt = _armorHandlerAlive.HasSkirt;
            _armorHandlerDead.HasShield = _armorHandlerAlive.HasShield;
            _armorHandlerDead.UpdateArmor();
        }

        private void CopyTransforms(Transform old, Transform newtransform)
        {
            newtransform.position = old.position;
            newtransform.rotation = old.rotation;
            foreach (Transform child in newtransform)
            {
                // match the transform with the same name
                var curSrc = old.Find(child.name);
                if (curSrc)
                    CopyTransforms(curSrc, child);
            }
        }
    }
}