using UnityEngine;
using UnityEngine.UI;

public class BlendingController : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private GameObject _cube;
    [SerializeField] private Slider _sphereBlendingSlider;
    [SerializeField] private Slider _cubeBlendingSlider;
    private Material _sphereMaterial;
    private Material _cubeMaterial;
    private void Start()
    {
        _sphereBlendingSlider.onValueChanged.AddListener(OnSphereBlendingSliderChanged);
        _cubeBlendingSlider.onValueChanged.AddListener(OnCubeBlendingSliderChanged);
        _sphereMaterial = _sphere.GetComponent<Renderer>().material;
        _cubeMaterial = _cube.GetComponent<Renderer>().material;
    }

    private void OnSphereBlendingSliderChanged(float value)
    {
        _sphereMaterial.SetFloat("_BlendFactor", value);
    }
    private void OnCubeBlendingSliderChanged(float value)
    {
        _cubeMaterial.SetFloat("_BlendFactor", value);
    }
}
