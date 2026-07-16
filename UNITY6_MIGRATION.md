# Unity 6 (6000.5.1f1) 移行手順書

`2020.3.4f1` → `6000.5.1f1` への移行手順。
ブランチ `upgrade/unity6` で作業します。

> **重要**: アセットの再インポート・パッケージ解決・レンダーパイプライン設定など、移行作業の大部分は **Windows の Unity エディタ上でしか実行できません**。本書はそのための手順書です。コード側で先行対応できる部分は済ませてあります(後述「済んでいる対応」)。

---

## 0. 前提・バックアップ(必須)

- [ ] **一方向変換**である点を理解する(Unity 6 で開いて保存すると 2020.3 では二度と開けない)
- [ ] プロジェクトフォルダを**まるごとコピー**してバックアップを取る
- [ ] git で全変更をコミット済みにする(PNG等の作業ツリー差分も含めて退避)
- [ ] 作業は `upgrade/unity6` ブランチ上で行う

---

## 1. ⚠️ パッケージ依存(Post Processing)— 最重要

このリポジトリは **`Packages/` を `.gitignore` 対象**にしているため、`Packages/manifest.json`(パッケージ依存の定義)が**バージョン管理されていません**。

- コードは `UnityEngine.Rendering.PostProcessing`(**Post Processing Stack v2** パッケージ)を `GameManagerMultiplay.cs` で使用しています。
- そのため、**`com.unity.postprocessing` パッケージが manifest に無いとコンパイルが通りません**。

### 対応
1. これまで開発に使っていた**元の Windows 作業コピー**(`Packages/manifest.json` が存在するもの)で開くなら、そのまま依存は引き継がれます。
2. このリポジトリを**新規 clone した環境**で開く場合は、`Packages/manifest.json` が無いため、Unity 6 で開いた後に **Package Manager から手動で再追加**が必要:
   - `Window > Package Manager` → `+` → `Add package by name...`
   - `com.unity.postprocessing` を追加
3. Post Processing Stack v2 は Unity 6 でも利用可能ですが**メンテナンスモード**の旧パッケージです。中長期的には URP の組み込みポストプロセスへの移行を推奨(本プロジェクトは Built-in RP・URP/HDRP アセットは未使用)。

> 補足: `Packages/` を gitignore するのは Unity の標準的な運用ではありません(本来 `manifest.json` と `packages-lock.json` はコミットすべき)。移行を機に `.gitignore` から `/Packages` を外し、manifest をコミットする運用に変えることを推奨します。

---

## 2. Unity 6 で開く

1. Unity Hub に `6000.5.1f1` がインストール済みであることを確認
2. Hub にこのプロジェクトを追加し、エディタバージョンに `6000.5.1f1` を指定して開く
3. 「より新しいバージョンで開きますか?」の確認 → 続行(バックアップ済みであることを再確認)
4. 初回起動で**全アセットの再インポート**が走る(数分〜数十分かかる場合あり)
5. 完了後、`ProjectSettings/ProjectVersion.txt` は Unity により自動的に `6000.5.1f1` に更新される(手動編集不要)

---

## 3. 開いた後のチェックリスト

### コンソール
- [ ] `Console` ウィンドウでコンパイル**エラー(赤)**を確認 → エラーが出ているとプレイ不可。順に対応
- [ ] **警告(黄)**も確認(非推奨APIなど)

### レンダリング / 見た目
- [ ] Post Processing の効果(ブルーム等)が効いているか各ステージで確認
- [ ] スカイボックス/マテリアルの表示崩れがないか(シェーダ再コンパイルで色味が変わることがある)
- [ ] `Ultimate 10 Plus Shaders` 等のサードパーティシェーダが Unity 6 で動作するか

### 物理 / 挙動
- [ ] ロケットの操作・速度・ブーストが従来通りか(`linearVelocity` 置換の影響確認)
- [ ] 当たり判定(チェックポイント・ゴール・トラップ・ミサイル)が機能するか

### 入力 / UI
- [ ] 旧 Input Manager は Unity 6 でも動作(変更不要)
- [ ] Legacy UI(`UnityEngine.UI.Text`)は動作するが非推奨。表示崩れがないか確認
- [ ] 日本語/英語フォントの表示

### マルチプレイ
- [ ] 画面分割カメラが正しく表示されるか
- [ ] アイテム挙動(攻撃・防御・ブースト等)

---

## 4. 済んでいる対応(コード先行修正)

`upgrade/unity6` ブランチで以下を適用済み:

| 対応 | 内容 |
| --- | --- |
| `Rigidbody.velocity` → `linearVelocity` | Unity 6 で非推奨化。13箇所すべて置換(`MovementSolo` / `MovementMultiplay` / `FlyFabrics` / `PlayerItem`) |
| `FindObjectOfType<>()` → `Instance` | Unity 6 で非推奨。Step 1 リファクタで置換済み(27箇所) |
| `angularVelocity` | Unity 6 でも有効なため**変更不要**(確認済み) |

調査の結果、`WWW` / `GUIText` / `Application.LoadLevel` / `transform.FindChild` などの**削除済みAPIは使用なし**でした。

---

## 5. このプロジェクト固有のリスク箇所

- **Post Processing Stack v2**(§1 参照)— 最大の懸念
- **サードパーティアセット**(`Asset Packs/` 内のシェーダ・エフェクト)が Unity 6 非対応の可能性。特にカスタムシェーダ(`Ultimate 10 Plus Shaders`, `Particle Dissolve Shader`)
- **Legacy UI Text** の全面使用 — 動作はするが将来的に TextMeshPro 移行を検討
- **シリアライズ形式の変更** — シーン/プレハブの YAML が更新される(差分が大きくなる)

---

## 6. 環境に関する注意(WSL)

このリポジトリは WSL 側のパス(`/home/...`)にあります。Windows の Unity で WSL パス上のプロジェクトを直接開くと、ファイル監視・インポートが**遅く・不安定**になることがあります。
→ Windows 側ドライブ(例 `C:\Users\<user>\...`)にプロジェクトを置いて作業する方が安定します。

---

## 7. 移行後のコミット方針

- 再インポートで更新される `.meta` / シーン / プレハブ / `ProjectSettings` の差分は大きくなります。**意味のある単位で分けてコミット**することを推奨
- §1 の通り、`Packages/manifest.json` と `packages-lock.json` をこの機にコミット対象へ含める運用変更を推奨

---
*コード先行修正は `upgrade/unity6` ブランチに適用済み。エディタ側の変換作業は本書の手順で実施してください。*
