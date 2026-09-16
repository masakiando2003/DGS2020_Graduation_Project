# DGS2020 卒業制作 改善提案

コードベースを調査した上での改善点を、**影響度 × 着手しやすさ**で優先度付けして整理します。
各項目には根拠となるファイル・行番号を付記しています。

> 注: 卒業制作として完成済みのプロジェクトです。以下は「次に手を入れるなら」という観点の提案であり、致命的な不具合の指摘ではありません。

---

## 優先度: 高(設計・保守性に大きく効く)

### 1. 巨大な God Class の分割
いくつかのクラスが肥大化し、単一責任の原則から外れています。

| ファイル | 行数 | 抱えている責務(混在) |
| --- | --- | --- |
| `GameManagerMultiplay.cs` | 1299 | 進行管理・UI更新・カメラ・アイテム・ランキング・ローカライズ |
| `GameManagerSolo.cs` | 1091 | 同上 |
| `Instructions_1P.cs` | 752 | 操作説明の表示・遷移 |
| `Option.cs` | 658 | 設定・ランキング表示・永続化 |

**改善案**: `GameManager` は「ゲーム状態の遷移」に専念させ、UI更新は `UIManager`、ランキングは `RankingService`、ローカライズ適用は専用コンポーネントへ委譲する。`[SerializeField] Text` が数十個並ぶ状態(`GameManagerSolo.cs:25-40` 付近)は、Inspector での配線ミスを誘発します。

### 2. Solo と Multiplay の重複コード
ほぼ並行したクラス群が存在し、ロジックが二重管理になっています。

- `MovementSolo` ↔ `MovementMultiplay`
- `GameManagerSolo` ↔ `GameManagerMultiplay`
- `CameraManagementSolo` ↔ `CameraManagementMultiplay`
- `CollisionHandlerSolo` ↔ `CollisionHandlerMultiplay`
- `PlayerStatusSolo` ↔ `PlayerStatusMultiplay`

**改善案**: 共通部分を基底クラス(例 `MovementBase`, `PlayerStatusBase`)に抽出し、差分のみ派生クラスで実装する。片方を直したらもう片方も直す、という手間とバグの温床を減らせます。

### 3. ローカライズ機構の堅牢化
`Localization.cs` の `GetLabelContent` は線形検索 + 例外非対応です。

```csharp
int labelIndex = labelName.FindIndex(n => n == specifiedLabel);
return labelText[labelIndex];   // ラベル未定義だと labelIndex=-1 → 例外
```

**改善案**:
- `Dictionary<string,string>` 化(毎回 `FindIndex` する線形コストを排除)
- ラベル未定義時はキー名をそのまま返すなどのフォールバックを用意(クラッシュ回避)
- 画面ごとに `_EN` / `_JP` の ScriptableObject を二重管理しているため、1ファイルに言語列を持たせる構成も検討余地あり

---

## 優先度: 中(パフォーマンス・安定性)

### 4. `Update()` 内での `FindObjectOfType` 多用
毎フレーム実行されるループ内でシーン全走査が走っています。

- `MovementMultiplay.cs:45` — `Update()` 内で `FindObjectOfType<GameManagerMultiplay>()`
- `CameraManagementSolo.cs` / `CameraManagementMultiplay.cs` — 同様

**改善案**: GameManager はシングルトン(`Instance`)を実装済みなので、`FindObjectOfType<>()` ではなく `GameManagerMultiplay.Instance` を使う。`CollisionHandler` / `PlayerItem` でも同じ呼び出しを何度も繰り返している(`PlayerItem.cs:172,249-258,334-355`)ため、ローカル変数にキャッシュする。

### 5. 文字列連結による `GameObject.Find`
`PlayerItem.cs:308`:
```csharp
Camera targetPlayerCamera = GameObject.Find("FollowPlayer" + targetPlayerID + "Camera").GetComponent<Camera>();
```
名前変更に弱く、`Find` 自体も低速です。**改善案**: カメラ配列を GameManager が保持し、ID をインデックスとして参照する。

### 6. タグ比較を `CompareTag` に
`Missile.cs:72`, `RockStone.cs:9` などで `other.gameObject.tag == "Player"` を使用。
`==` 比較は内部で文字列割り当てが発生し、タグ名のタイプミスも実行時まで気付けません。
**改善案**: `other.CompareTag("Player")` を使い、タグ名は定数化する。

### 7. エラーハンドリングの不在
コード全体で `try/catch` がゼロ件。特にリスクが高いのは:
- ランキング JSON のパース(`Option.cs` の `JsonUtility.FromJson<RankingJson>(rankingDataFile.text)`)
- 前述のローカライズのラベル参照

**改善案**: 外部データ(JSON・PlayerPrefs)に依存する箇所だけでも、不正データ時のフォールバックを入れる。

### 8. ランキング永続化モデルの整理
`TextAsset`(ビルド後は読み取り専用)をデフォルト値として、`PlayerPrefs` で上書きする二層構造になっています(`Option.cs:347` 付近)。動作はしますが、保存元が2系統に分かれ追いにくい状態です。
**改善案**: 実行時に書き込めない `TextAsset` を「初期値」、`PlayerPrefs`(または `persistentDataPath` の JSON)を「実データ」と役割を明文化し、`RankingService` に集約する。

---

## 優先度: 低(コード品質・体裁)

### 9. 難易度 × 言語の switch/case 重複
`Option.cs` の `ReadRankingData` は Easy / Normal / Hard × EN / JP の分岐がほぼ同一コードで繰り返されています(数百行)。
**改善案**: 難易度を列挙やデータテーブルで扱い、共通処理を1つにまとめる(データ駆動化)。

### 10. グローバルな static 可変状態
シーン間のデータ受け渡しに `public static` フィールドを多用:
`Difficulty_1P_TempSave.chosenDifficulty`, `PlayerNameTempSaveMultiplay.playerName`, `PlayTimeTempSaveSolo.totalTimeElapsed`, さらに `PlayerStatusSolo.currentLife` / `playerCurrentBoost` も `static`。
**改善案**: static は Editor 上で再生をまたいで値が残り、リセット漏れの原因になります。`ScriptableObject` ベースの共有データか、明示的な初期化を入れる。

### 11. 命名のタイプミス(公開APIに波及)
`DisplayLanauge`(Language)、`ClosetPlayer...`(Closest)、`thrustSppedNormalFactor`、`TimeElasped`、`SelectMulitplayerNumbers.cs`(ファイル名)、`Activiate...` など。
**改善案**: 公開メソッド名・フィールド名のタイプミスは呼び出し側にも伝播するため、リファクタのタイミングで一括修正する。

### 12. 本番コードに残る `Debug.Log`
`PlayerItem.cs`(3件)、`SelectMulitplayerNumbers.cs`、`Tube.cs`、`ChooseDifficultyMultiplayer.cs` など複数。
**改善案**: 削除するか `#if UNITY_EDITOR` / `[Conditional("DEBUG")]` で囲う。

### 13. コメントアウトされた死コード
`PlayerStatusSolo.cs` の `//currentBoostFillArea...` 等。読みづらさの元なので削除推奨。

### 14. 自動テストの不在
テストコードがありません。最低限、純粋ロジック(ランキングのソート、ブースト計算、タイム整形)だけでも Unity Test Framework で EditMode テストを用意すると、リファクタ時の安全網になります。

---

## 優先度: 補足(プロジェクト運用)

### 15. リポジトリ衛生
- `Asset Packs/` に `.unitypackage`(`DGS2020..._Prefabs_20210422.unitypackage`)を含む大容量バイナリがコミットされている。Git LFS の活用を検討。
- `git status` 上で多数の `.png` が変更扱い(改行コード/LFS 設定の影響と思われる)。`.gitattributes` の見直しで差分ノイズを減らせます。

---

## おすすめの着手順

1. **#4・#6・#12**(`Instance` 化、`CompareTag`、`Debug.Log` 除去)— 低リスクで効果が出る小改善から
2. **#3・#7**(ローカライズと例外対応)— クラッシュ要因の予防
3. **#1・#2**(God Class 分割と Solo/Multiplay 共通化)— 大きいが保守性を最も改善
4. **#14**(テスト導入)— 3 の前に最低限入れておくとリファクタが安全

---
*このファイルはコードベースの静的調査に基づく提案です。実行時の挙動は別途検証してください。*
