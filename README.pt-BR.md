# Per Farmhand Woods

Per Farmhand Woods é um mod de Stardew Valley que dá a cada convidado do modo
multijogador sua própria Floresta Secreta instanciada. O anfitrião mantém a
clareira original e os visitantes são redirecionados de forma transparente para
seu mapa pessoal, evitando corridas por madeira de lei ou itens de coleta.

## Por que este mod existe

Stardew Valley foi originalmente equilibrado pensando em um único fazendeiro.
Mesmo com o cooperativo adicionado depois, grande parte do mundo continua
compartilhada. Em fazendas com carteiras separadas isso gera atritos: madeira de
lei é um recurso limitado, a Floresta Secreta só gera seis tocos por dia e os
convidados muitas vezes ficam sem nada. Ver amigos correrem para a clareira toda
manhã foi a motivação para esculpir uma floresta dedicada para cada jogador.

## Principais recursos

- Florestas Secretas exclusivas por convidado, criadas sob demanda e reutilizadas
  nos salvamentos.
- Tiles de warp na floresta usam uma `TouchAction` personalizada que redireciona
  jogadores não anfitriões para sua floresta pessoal preservando o tile de destino.
- Convidados que pisam na Woods compartilhada são automaticamente enviados para
  sua instância privada mesmo se outro mod disparar um warp direto.
- A posse das florestas é salva, restaurada e limpa entre sessões para evitar
  localizações órfãs.

## Como funciona

- O mod escuta requisições de assets para `Maps/Forest*` e injeta uma
  `TouchAction` personalizada (`DerexSV.PerFarmhandWoods.EnterWoods`) nos tiles
  de warp padrão que levam à floresta.
- Essa ação verifica o fazendeiro visitante e o teleporta para `Woods_<PlayerID>`,
  criando o mapa automaticamente quando necessário.
- Eventos de ciclo de vida (`SaveLoaded`, `Saving`, `LoadStageChanged`, etc.)
  garantem que as instâncias sejam registradas no jogo, salvas nos dados do mod
  e limpas ao retornar para a tela título.

## Instalação

1. Instale a versão mais recente do [SMAPI](https://smapi.io/) (4.3.2 ou superior).
2. Baixe a última versão deste mod.
3. Extraia e mova a pasta `PerFarmhandWoods` para o diretório `Mods` de Stardew Valley.
4. Inicie Stardew Valley através do SMAPI.

### Compilar a partir do código

```bash
dotnet build PerFarmhandWoods/PerFarmhandWoods.csproj
```

A compilação utiliza o pacote [Mod Build Config](https://github.com/Pathoschild/SMAPI-mod-build-config)
para enviar o mod diretamente à pasta `Mods` do jogo.

## Compatibilidade

- **SMAPI**: Requer SMAPI 4.3.2+ (conforme `manifest.json`).
- **Versão do jogo**: Desenvolvido para Stardew Valley 1.6.x.
- **Edições de mapas/warps**: Mods que reescrevem os tiles de warp de
  `Maps/Forest*` ou substituem totalmente a Floresta Secreta podem conflitar. O
  patch roda com prioridade tardia, então a maioria dos content packs deve
  coexistir, mas valide no jogo.
- **Warps personalizados**: Qualquer coisa que teleporte convidados diretamente
  para o mapa compartilhado `Woods` é interceptado pelo handler de `Warped` e
  redirecionado, mantendo a experiência consistente.
- **Um jogador**: Nenhuma mudança de gameplay; o anfitrião continua com o mapa
  padrão.

## Limitações conhecidas

- O anfitrião ainda compartilha a Floresta Secreta padrão com fazendeiros
  convidados se eles seguirem o anfitrião pela saída. Convidados podem voltar
  para sua instância pelos warps da floresta.
- Mods que substituem profundamente os dados do mapa da floresta podem exigir
  ajustes manuais no patch.

## Solução de problemas

- Convidados entrando em loop entre mapas normalmente significa que outro mod
  removeu a `TouchAction` injetada. Confira o log do SMAPI em busca de avisos do
  Per Farmhand Woods.
- Se uma instância da floresta não for criada, remova a entrada de dados do mod
  `derexsv.pfw.locations` (*.json) do salvamento afetado e recarregue. O mod vai
  reconstruir as instâncias automaticamente no próximo início de dia ou warp.

## Créditos e agradecimentos

- **DerexSV** – conceito original e implementação.
- Comunidade SMAPI e Pathoschild pelo toolkit de modding e pelo build config.
- Amigos que perdiam a corrida pela madeira de lei e inspiraram o conjunto de recursos.
