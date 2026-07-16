# DGS2020 卒業制作 プロジェクト概要

デジタルゲーム開発2020 卒業制作。Unity 製の **ロケットレースゲーム**。
プレイヤーはロケットを操縦し、燃料(ブースト)を管理しながらチェックポイントを通過してゴールを目指す。ソロプレイとマルチプレイ(対戦)の2モードを備える。

## 基本情報

| 項目 | 内容 |
| --- | --- |
| エンジン | Unity 2020.3.4f1 |
| 言語 | C#(MonoBehaviour ベース) |
| 主要パッケージ | Post Processing |
| 対応言語 | 日本語 / 英語(ScriptableObject によるローカライズ) |
| プラットフォーム | PC(キーボード操作) |

## ゲームモード

### ソロプレイ(1P)
- 操作タイプ選択 → 難易度選択 → 名前入力 → 操作説明 → ステージ
- 難易度: **Easy / Normal / Hard**
- 制限時間内にゴールを目指し、タイム・順位をランキング保存

### マルチプレイ(対戦)
- 人数選択(2P / 3P / 4P) → チーム選択 → 難易度選択 → 名前入力 → 操作説明 → ステージ
- 各難易度 × 各人数のステージを用意(例: Easy 2P/3P/4P など)
- 画面分割で複数プレイヤーが同時にレース
- **アイテムシステム**で妨害・補助の駆け引きを行う

## アイテム種別(マルチプレイ)

`MultiplayerItems.ItemCategories` で定義(ランダムボックスから取得):

| カテゴリ | 効果 |
| --- | --- |
| AttackItem | 攻撃(ミサイル等で相手を妨害) |
| DefenseItem | 防御(シールド) |
| AbnormalConditionItem | 状態異常付与 |
| BoostCanItem | ブースト回復 |
| DropbackItem | 相手を後退させる |
| ReduceSpeedItem | 相手の速度を低下 |
| ReduceBoostItem | 相手のブーストを減少 |

## シーン構成

```
Title                         タイトル
├─ Options                    設定(BGM/SE音量・言語)
├─ Credits                    クレジット
├─ Stage_Solo/                ソロ
│   ├─ 1P_Choose_Control_Type / Choose_Difficulty / Enter_Player_Name / Instruction
│   └─ Easy / Normal / Hard   ステージ本編
└─ Stage_Multiplay/           マルチプレイ
    ├─ Num_of_Players / TeamSelection / Choose_Difficulty / Enter_Player_Name / Instruction
    └─ Easy / Normal / Hard × 2P / 3P / 4P
```

## スクリプト構成(`Assets/Scripts/`)

### Gameplay_Solo
ソロプレイのゲーム進行。`GameManagerSolo`(進行・UI・ランキング管理)、`MovementSolo`(ロケット操作・推力)、`PlayerStatusSolo`(ライフ・ブースト)、`CameraManagementSolo`、`CollisionHandlerSolo`、`CheckPointFlagSolo`、各種設定・一時保存スクリプト(難易度・名前・タイム)。

### Gameplay_Multiplayer
マルチプレイのゲーム進行。`GameManagerMultiplay`、`MovementMultiplay`、`PlayerStatusMultiplay`、`CameraManagementMultiplay`(分割画面)、アイテム系(`MultiplayerItems` / `PlayerItem` / `RandomItemBox` / `RandomItemSettings`)、攻撃・防御要素(`Missile` / `Shield` / `RockStone`)、チーム選択・人数選択・名前入力など。

### Gameplay_Elements
ステージ共通ギミック。`Waypoint` / `PathMovement` / `CircularMovement` / `Oscillator`(移動障害物)、`Fuel`(燃料アイテム)、`TrapTrigger`、`Robot`、`Tube`、`FlyFabrics`、`FollowPlayerCamera`、`PlayerCurrentPositionArrow`(進行度表示)など。

### Others
横断的機能。`Localization` / `Language`(多言語化)、`BGMController`(BGM/SE 音量・シーン跨ぎ永続化)、`Title` / `Option` / `Credits`、`BackgroundController`、`FadeOutSprite`。

## アセット構成

- `Assets/Scenes` — シーン
- `Assets/Scripts` — C# スクリプト(上記4カテゴリ)
- `Assets/Prefabs` — Solo / Multiplay / Map 別プレハブ
- `Assets/Settings/Localization` — 日英ローカライズ用 ScriptableObject(`*_EN.asset` / `*_JP.asset`)
- `Assets/Audio` — BGM / SE
- `Assets/Asset Packs` — 外部素材(ロケット、爆発エフェクト、ミサイル、ロボット、岩、SF コリドー、UI 素材、シェーダー等)

## 設計上のポイント

- **ローカライズ**: 各画面ごとに `Localization` ScriptableObject(ラベル名→テキストのリスト)を `_EN` / `_JP` で持ち、`Language.gameDisplayLanguage` に応じて切り替え。
- **設定の永続化**: 音量・言語は `PlayerPrefs` に保存。`BGMController` は `DontDestroyOnLoad` でシーンを跨いで存続。
- **GameManager**: ソロ/マルチとも各 GameManager がシングルトン(`Instance`)で進行・UI・チェックポイント・ランキングを統括。
- **一時保存**: 難易度・プレイヤー名・プレイタイムをシーン間で受け渡すための TempSave スクリプト群。
```

---
*このファイルはコードベースの調査結果を基に自動生成した概要です。*
