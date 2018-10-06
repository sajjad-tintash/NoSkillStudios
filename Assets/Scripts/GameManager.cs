using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

    public enum World
    {
        NORMAL,
        MUTATED
    }

    public PlayerPlatformerController _normalPlayerController;
    public PlayerPlatformerController _mutatedPlayerController;

    public PostProcessingBehaviour _normalCameraPostProcessing;
    public PostProcessingBehaviour _mutatedCameraPostProcessing;

    public CameraController _normalCameraController;
    public CameraController _mutatedCameraController;

    private PostProcessingProfile _normalCameraPostProcessingProfile;
    private PostProcessingProfile _mutatedCameraPostProcessingProfile;

    private World _currentWorld;

    private void Awake()
    {
        _normalCameraPostProcessingProfile = _normalCameraPostProcessing.profile;
        _mutatedCameraPostProcessingProfile = _mutatedCameraPostProcessing.profile;
    }

    // Use this for initialization
    void Start () {
        _currentWorld = ActivateWorld(World.NORMAL);

        _normalCameraController.Init();
        _mutatedCameraController.Init();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch(_currentWorld)
            {
                case World.MUTATED:
                    _currentWorld = ActivateWorld(World.NORMAL);
                    break;

                case World.NORMAL:
                    _currentWorld = ActivateWorld(World.MUTATED);
                    break;
            }
        }
	}

    private World ActivateWorld (World world)
    {
        switch (world)
        {
            case World.MUTATED:
                _normalPlayerController.enabled = false;
                _mutatedPlayerController.enabled = true;
                MakeWorldGreyScale(World.NORMAL);
                break;

            case World.NORMAL:
                _normalPlayerController.enabled = true;
                _mutatedPlayerController.enabled = false;
                MakeWorldGreyScale(World.MUTATED);
                break;
        }

        MakeWorldColored(world);

        return world;
    }

    private void MakeWorldGreyScale (World world)
    {
        ColorGradingModel.Settings settings;

        switch (world)
        {
            case World.MUTATED:
                settings = _mutatedCameraPostProcessingProfile.colorGrading.settings;
                settings.basic.saturation = 0f;
                _mutatedCameraPostProcessingProfile.colorGrading.settings = settings;
                break;

            case World.NORMAL:
                settings = _normalCameraPostProcessingProfile.colorGrading.settings;
                settings.basic.saturation = 0f;
                _normalCameraPostProcessingProfile.colorGrading.settings = settings;
                break;
        }
    }

    private void MakeWorldColored(World world)
    {
        ColorGradingModel.Settings settings;

        switch (world)
        {
            case World.MUTATED:
                settings = _mutatedCameraPostProcessingProfile.colorGrading.settings;
                settings.basic.saturation = 1f;
                _mutatedCameraPostProcessingProfile.colorGrading.settings = settings;
                break;

            case World.NORMAL:
                settings = _normalCameraPostProcessingProfile.colorGrading.settings;
                settings.basic.saturation = 1f;
                _normalCameraPostProcessingProfile.colorGrading.settings = settings;
                break;
        }
    }
}
