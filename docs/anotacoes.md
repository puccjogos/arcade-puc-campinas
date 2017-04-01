# ArcadePUCCampinas - devlog

## design

- `ArcadePUCCampinas`: namespace do projeto
  - `InputArcade`: classe estática com toda a API de controles (com reset em `Arcade` ou `ArcadeMenu`)
  - `ArcadeJogo`: (só roda nos jogos) singleton para gerenciamento de inputs e chamada de `Menu`, será transformado em `unitypackage` no final
  - `ArcadeMenu` : singleton que gera e cuida do `Menu` (só roda no `Menu`)
  - `JogoMenu` : classe que controla a exibição de um jogo no `Menu`

Além de um `unitypackage` com o prefab `ArcadeJogo` e lógica de `InputArcade`, é necessário copir o `InputManager.asset` para a paste `ProjectSettings` de cada jogo.

## limitações

- builds para WebGL demoram muito para serem feitos e parecem bastante frágeis;
- deploy em `release` fica rápido o suficiente para ser usado, mas demora bastante para ser criado;
- usar o mesmo servidor para o menu e para as pastas e assets a serem carregados dinamicamente;

## ideias


- [x] json funciona numa boa para carregar infos de cada jogo, integrado com WWW e um localhost com os arquivos (servidos via http);
- [x] o mesmo esquema funciona legal para carregar texturas e criar sprites;
- [ ] ter no build `Menu` um json com os ids de todos os jogos (que também são as pastas no localhost);

## inputs

| `EControle` | jogador 0 | jogador 1 |
| --- | --- | --- |
| HORIZONTAL | A - D | LeftArrow - RightArrow |
| VERTICAL | S - W | DownArrow - UpArrow |
| VERDE | R | O |
| VERMELHO | T | I |
| AMARELO | G | K |
| AZUL | F | L |
| BRANCO | H | J |
| PRETO | Y | U |

- [ ] falta colocar os joystick buttons;
- para copiar as configurações do `InputManager` tem que copiar o arquivo na pasta `ProjectSettings`

## snippets

### carregar texturas em sprites via WWW

```csharp
IEnumerator CarregarCapa (string urlCapa) {
  // exemplo de url: "http://localhost:8080/capa.png"
  WWW tex = new WWW(urlCapa);
  yield return tex;
  Texture2D texCapa = new Texture2D(1920, 1080);
  tex.LoadImageIntoTexture(texCapa);
  Sprite spriteCapa = Sprite.Create(texCapa, new Rect(0, 0, 1920, 1080), new Vector2(0.5f, 0.5f));
  GetComponent<SpriteRenderer>().sprite = spriteCapa;
}
```

### carregar infos de jogo em JSON via WWW

```csharp
IEnumerator CarregarInfos (string urlInfo) {  
  // exemplo de url: "http://localhost:8080/infos.json"
  WWW jsonData = new WWW(urlInfo);
  yield return jsonData;
  var infos = JogoInfo.CarregarJson(jsonData.text);
  Debug.Log(infos.nome + " " + infos.urlCapa);
}
```
