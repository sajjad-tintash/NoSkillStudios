using UnityEngine;
using UnityEngine.PostProcessing;

public class GameController : MonoBehaviour {

    public enum World
    {
        NORMAL,
        MUTATED
    }

    public static bool CONTROLS_ENABLED = true;

    public LayerMask _everything;

    public PlayerPlatformerController _normalPlayerPlatformController;
    public PlayerPlatformerController _mutatedPlayerPlatformController;

    public PostProcessingBehaviour _normalCameraPostProcessing;
    public PostProcessingBehaviour _mutatedCameraPostProcessing;

    public CameraController _normalCameraController;
    public CameraController _mutatedCameraController;

    public Level _levelObject;
    public GameObject _mainCanvas;
    public GameObject _startScreen;
    public GameObject _howToPlayScreen;
    public GameObject _normalProgressBar;
    public GameObject _mutatedProgressBar;
    public GameObject _creditsScreen;

    public GameObject _controlText;
    public GameObject _hintText;

    private PostProcessingProfile _normalCameraPostProcessingProfile;
    private PostProcessingProfile _mutatedCameraPostProcessingProfile;

    private PlayerController _normalPlayerController;
    private PlayerController _mutatedPlayerController;

    public World _currentWorld;

    private bool _hintTextShowing = false;

    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
          instance = this;
        }
        else Destroy (this.gameObject);


        _normalCameraPostProcessingProfile = _normalCameraPostProcessing.profile;
        _mutatedCameraPostProcessingProfile = _mutatedCameraPostProcessing.profile;
    }

    // Use this for initialization
    void Start () {
        _normalPlayerController = _normalPlayerPlatformController.GetComponent<PlayerController>();
        _mutatedPlayerController = _mutatedPlayerPlatformController.GetComponent<PlayerController>();

        MakeWorldColored(World.MUTATED);
        MakeWorldColored(World.NORMAL);
	}

    public void PlayButtonHandler ()
    {
        //StartGame();
        _startScreen.SetActive(false);
        _howToPlayScreen.SetActive(true);
    }

    public void GotItButtonHandler ()
    {
        StartGame();
    }

    void StartGame ()
    {
        //_mainCanvas.SetActive(false);

        _normalProgressBar.SetActive(true);
        _mutatedProgressBar.SetActive(true);
        _levelObject.gameObject.SetActive(true);
        _howToPlayScreen.SetActive(false);

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
                _normalPlayerPlatformController.enabled = false;
                _mutatedPlayerPlatformController.enabled = true;
                MakeWorldGreyScale(World.NORMAL);
                break;

            case World.NORMAL:
                _normalPlayerPlatformController.enabled = true;
                _mutatedPlayerPlatformController.enabled = false;
                MakeWorldGreyScale(World.MUTATED);
                break;
        }

        MakeWorldColored(world);
        HideControlText();
        HideHintText();

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

    public World GetCurrent ()
    {
        return _currentWorld;
    }

    public bool CheckWinCondition ()
    {
        if (_normalPlayerController._mutationLevel == _mutatedPlayerController._mutationLevel)
        {
            LevelWon();
            return true;
        }
        return false;
    }


    private Rect rectFromNormalCamera;
    private Rect rectToNormalCamera;

    private Rect rectFromMutatedCamera;
    private Rect rectToMutatedCamera;

    Vector3 _normalCameraFromPos;
    Vector3 _mutatedCameraFromPos;
    Vector3 _camerasMidPoint;

    public void LevelWon ()
    {
        CONTROLS_ENABLED = false;

        HideControlText();
        HideHintText();

        MakeWorldColored(World.MUTATED);
        MakeWorldColored(World.NORMAL);

        _normalCameraController.enabled = false;
        _mutatedCameraController.enabled = false;

        Rigidbody2D normalRB = _normalPlayerController.GetComponent<Rigidbody2D>();
        Rigidbody2D mutatedRB = _mutatedPlayerController.GetComponent<Rigidbody2D>();

        normalRB.isKinematic = true;
        normalRB.simulated = false;
        mutatedRB.isKinematic = true;
        mutatedRB.simulated = false;

        _normalCameraFromPos = _normalCameraController._mainCamera.transform.position;
        _mutatedCameraFromPos = _mutatedCameraController._mainCamera.transform.position;

        _camerasMidPoint = ((_normalCameraFromPos - _mutatedCameraFromPos) * 0.5f) + _mutatedCameraFromPos;
        _camerasMidPoint = new Vector3(_camerasMidPoint.x, _camerasMidPoint.y, -10f);

        Debug.Log("Level Won");

        _normalCameraController._mainCamera.cullingMask = _everything;
        _mutatedCameraController._mainCamera.cullingMask = _everything;

        rectFromNormalCamera = _normalCameraController._mainCamera.rect;
        rectFromMutatedCamera = _mutatedCameraController._mainCamera.rect;

        rectToNormalCamera = new Rect(Vector2.zero, Vector2.one);
        rectToMutatedCamera = new Rect(Vector2.zero, Vector2.one);

        GameObject normalPlayer = _normalPlayerController.gameObject;
        GameObject mutatedPlayer = _mutatedPlayerController.gameObject;

        //LeanTween.value(normalCamera.gameObject, UpdateNormalCameraCallback1, 0.5f, 1f, 2f);
        //void updateValueExampleCallback(float val)
        //{
        //    Debug.Log("tweened value:" + val + " set this to whatever variable you are tweening...");
        //}

        LeanTween.value(gameObject, 0f, 1f, 2f).setOnUpdate(AnimateTweening);

        Vector3 playerTweenPos = new Vector3(_camerasMidPoint.x, _camerasMidPoint.y, 0f);
        LeanTween.move(_normalPlayerController.gameObject, playerTweenPos, 2f);
        LeanTween.move(_mutatedPlayerController.gameObject, playerTweenPos, 2f);

        Invoke("ShowCreditsScreen", 5f);
    }

    public void AnimateTweening (float value)
    {
        float x = Mathf.Lerp(rectFromNormalCamera.x, rectToNormalCamera.x, value);
        float y = Mathf.Lerp(rectFromNormalCamera.y, rectToNormalCamera.y, value);
        float z = Mathf.Lerp(rectFromNormalCamera.size.x, rectToNormalCamera.size.x, value);
        float w = Mathf.Lerp(rectFromNormalCamera.size.y, rectToNormalCamera.size.y, value);
        _normalCameraController._mainCamera.rect = new Rect(x, y, z, w);

        float cameraX = Mathf.Lerp(_normalCameraFromPos.x, _camerasMidPoint.x, value);
        float cameraY = Mathf.Lerp(_normalCameraFromPos.y, _camerasMidPoint.y, value);
        _normalCameraController._mainCamera.transform.position = new Vector3(cameraX, cameraY, -10f);

        x = Mathf.Lerp(rectFromMutatedCamera.x, rectToMutatedCamera.x, value);
        y = Mathf.Lerp(rectFromMutatedCamera.y, rectToMutatedCamera.y, value);
        z = Mathf.Lerp(rectFromMutatedCamera.size.x, rectToMutatedCamera.size.x, value);
        w = Mathf.Lerp(rectFromMutatedCamera.size.y, rectToMutatedCamera.size.y, value);
        _mutatedCameraController._mainCamera.rect = new Rect(x, y, z, w);

        cameraX = Mathf.Lerp(_mutatedCameraFromPos.x, _camerasMidPoint.x, value);
        cameraY = Mathf.Lerp(_mutatedCameraFromPos.y, _camerasMidPoint.y, value);
        _mutatedCameraController._mainCamera.transform.position = new Vector3(cameraX, cameraY, -10f);
    }

    public void ShowHintText ()
    {
        if (_hintTextShowing == false)
        {
            _hintTextShowing = true;
            _hintText.SetActive(true);
            Invoke("HideHintText", 5f);
        }
    }

    public void HideHintText ()
    {
        CancelInvoke("HideHintText");
        _hintText.SetActive(false);
        _hintTextShowing = false;
    }

    public void ShowControlText ()
    {
        _controlText.SetActive(true);
    }

    public void HideControlText ()
    {
        _controlText.SetActive(false);
    }

    public void ShowCreditsScreen ()
    {
        _creditsScreen.SetActive(true);
    }
}
