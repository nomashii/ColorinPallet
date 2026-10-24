// 注意：これはMonoBehaviour（コンポーネント）ではありません！
// ゲーム全体で使う「色のリスト」の定義です。

public enum ColorType
{
    None,
    White,  // ノーマル（消費なし・リセット用）
    Red,    // 原色
    Blue,   // 原色
    Yellow, // 原色
    Purple, // 混色（赤＋青）
    Green,  // 混色（青＋黄）
    Orange,  // 混色（赤＋黄）
    AllColor //３原色（赤、青、黄）ショップのAllColorItem実装に伴い追加
}