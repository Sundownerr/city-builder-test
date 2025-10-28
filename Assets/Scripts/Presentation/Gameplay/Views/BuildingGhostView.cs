using UnityEngine;

namespace Presentation.Gameplay.Views
{
    public class BuildingGhostView : MonoBehaviour
    {
        public Transform ModelParent;
        private GameObject _model;
       
        public void ChangeModel(GameObject newModel)
        {
            if (_model != null) {
                Destroy(_model);
            }

            var position = newModel.transform.localPosition;
            
            _model = Instantiate(newModel, position, Quaternion.identity, ModelParent);
            _model.transform.localPosition = position;
        }

        public void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}