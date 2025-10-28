using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Gameplay.Views
{
    public class BuildingGhostView : MonoBehaviour
    {
        public Transform ModelParent;
        public Material BuildingAllowed;
        public Material BuildingNotAllowed;
        private readonly List<MeshRenderer> _modelMeshes = new();
        private GameObject _model;

        public void ChangeModel(GameObject newModel)
        {
            if (_model != null)
                Destroy(_model);

            var position = newModel.transform.localPosition;

            _model = Instantiate(newModel, position, Quaternion.identity, ModelParent);
            _model.transform.localPosition = position;

            _model.GetComponentsInChildren(_modelMeshes);
        }

        public void UpdatePosition(Vector3 position) =>
            transform.position = position;

        public void SetBuildingAllowed(bool allowed)
        {
            var material = allowed ? BuildingAllowed : BuildingNotAllowed;

            foreach (var meshRenderer in _modelMeshes)
                meshRenderer.sharedMaterial = material;
        }

        public void SetActive(bool active)
        {
            ModelParent.gameObject.SetActive(active);
        }
    }
}