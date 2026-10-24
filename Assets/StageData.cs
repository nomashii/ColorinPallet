using UnityEngine;

[CreateAssetMenu(
    fileName = "StageData",
    menuName = "Stage Select/Stage Data"
)]
public class StageData : ScriptableObject
{
    [Header("保存用")]
    [SerializeField] private int stageId = 1;

    [Header("表示用")]
    [SerializeField] private string stageNumber = "1-1";
    [SerializeField] private string stageName;
    [SerializeField] private string stageDescription;
    [SerializeField] private Sprite stageImage;

    [Header("ステージ情報")]
    [SerializeField] private int targetCount;
    [SerializeField] private int onayamiCount;
    [SerializeField] private int clearReward;

    [Header("遷移先")]
    [SerializeField] private string sceneName;

    public int StageId => stageId;
    public string StageNumber => stageNumber;
    public string StageName => stageName;
    public string StageDescription => stageDescription;
    public Sprite StageImage => stageImage;
    public int TargetCount => targetCount;
    public int OnayamiCount => onayamiCount;
    public int ClearReward => clearReward;
    public string SceneName => sceneName;
}