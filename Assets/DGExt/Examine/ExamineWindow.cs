using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;

namespace DGExt 
{
    public class ExamineWindow : UIWidget
    {
        [Header("References")]
        [SerializeField]
        protected Text _title;

        [SerializeField]
        protected Text _examineKey;

        public GameObject _postProcessingVolume;


        [SerializeField]
        protected float _rotationSpeed = 15;
        [SerializeField]
        protected float _panSpeed = 1;
        [SerializeField]
        protected float _zoomSpeed = 0.1f;

        private float _startDistance = 3f;
        private float _panX = 5f;
        private float _panY = 3f;

        private float _zoom;
        private float _minZoom = 0.3f; // maxDistance = _startDistance / _minZoom
        private float _maxZoom = 6f; // minDistance = _startDistance / _maxZoom
        private GameObject _examineCamara;
        private GameObject _examineObject;

        protected override void OnStart()
        {
            base.OnStart();
            _examineCamara = GameObject.FindGameObjectWithTag("ExamineCamara");
            if (_examineCamara == null)
            {
                Debug.Log("No gameObject tagged ExamineCamara found");
                return;
            }
            _examineCamara.transform.rotation = Quaternion.identity; // make sure we dont have any rotation, it simplifies the code
        }

        public virtual void Show(GameObject obj, string title, KeyCode examineKey, float zoom)
        {
            _title.text = title;
            _examineKey.text = examineKey.ToString();
            _zoom = zoom;

            base.Show();
            var pos = new Vector3(0,0, _startDistance / zoom); 
            if (_examineObject != null)
            {
                Destroy(_examineObject);
            }
            _examineObject = GameObject.Instantiate(obj, _examineCamara.transform);
            _examineObject.transform.localPosition = pos;
            _examineObject.transform.localRotation = Quaternion.Euler(0f,180f,0f);

            if (_examineObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.isKinematic = true;
            
            _examineObject.layer = LayerMask.NameToLayer("Examine");
            if (_postProcessingVolume != null)
                _postProcessingVolume.SetActive(true);
        }

        public override void Close()
        {
            base.Close();
            Destroy(_examineObject);
            if (_postProcessingVolume != null)
                _postProcessingVolume.SetActive(false);
        }

        protected override void Update()
        {
            if (_examineObject != null)
            {
                var pos = _examineObject.transform.localPosition;
                if (Input.GetMouseButton(0)) // rotate
                {
                    float x = Input.GetAxis("Mouse X") * _rotationSpeed;
                    float y = Input.GetAxis("Mouse Y") * _rotationSpeed;

                    _examineObject.transform.Rotate(Vector3.up, -x, Space.World);
                    _examineObject.transform.Rotate(Vector3.right, y, Space.World);
                }
                else if (Input.GetMouseButton(1)) // pan
                {
                    float x = Input.GetAxis("Mouse X") * _panSpeed/_zoom;
                    float y = Input.GetAxis("Mouse Y") * _panSpeed/_zoom;
                    pos.x = Mathf.Clamp(pos.x + x, -_panX, _panX);
                    pos.y = Mathf.Clamp(pos.y + y, -_panY, _panY);
                    _examineObject.transform.localPosition = pos;
                }
                else
                { // zoom
                    _zoom = Mathf.Clamp(Input.mouseScrollDelta.y * _zoomSpeed + _zoom, _minZoom, _maxZoom);
                    pos.z = _startDistance / _zoom;
                    _examineObject.transform.localPosition = pos;
                }
            }
            // last as it can do a close
            base.Update();
        }

    }
}
