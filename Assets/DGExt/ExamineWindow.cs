using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;

namespace DGExt 
{
    public class ExamineWindow : UIWidget
    {
        [Header("References")]
        [SerializeField]
        protected string _title;

        [SerializeField]
        protected RenderTexture View;

        public GameObject _postProcessingVolume;

        // An empty gameObject tagged "ExamineCamara" with a Camara as a child
        private GameObject _examineCamara;

        private GameObject _examineObject;
        private float _scrollScale = 0.1f;
        private float _zoomMin = 1f;
        private float _zoomMax = 10f;
        private float _distance = 3f;

        protected override void OnStart()
        {
            base.OnStart();
            _examineCamara = GameObject.FindGameObjectWithTag("ExamineCamara");
            if (_examineCamara == null)
            {
                Debug.Log("No gameObject tagged ExamineCamara found");
                return;
            }
        }

        public virtual void Show(GameObject obj)
        {
            base.Show();
            var pos = _examineCamara.transform.position + (_examineCamara.transform.forward * _distance);
            _examineObject = GameObject.Instantiate(obj, pos, Quaternion.identity);
            _examineObject.layer = LayerMask.NameToLayer("Examine");
            _postProcessingVolume.SetActive(true);
        }

        public override void Close()
        {
            base.Close();
            Destroy(_examineObject);
            _postProcessingVolume.SetActive(false);
        }

        protected override void Update()
        {
            if (_examineObject != null)
            {
                if (Input.GetMouseButton(0))
                {
                    float rotationSpeed = 15;

                    float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
                    float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

                    _examineObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
                    _examineObject.transform.Rotate(Vector3.right, yAxis, Space.World);
                }

                float z = Input.mouseScrollDelta.y * _scrollScale;
                var pos = _examineObject.transform.position;
                pos.z = Mathf.Clamp(_examineObject.transform.position.z + z, _zoomMin, _zoomMax);
                _examineObject.transform.position = pos;
            }
            // last as it can do a close
            base.Update();
        }

    }
}
