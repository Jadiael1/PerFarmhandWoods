*[English](README.md) · **Português (Brasil)***

# Per Farmhand Woods

Per Farmhand Woods é um mod de Stardew Valley que dá a cada convidado do modo
multijogador sua própria `Floresta Secreta` instanciada. O anfitrião mantém o
teleporte para a `Floresta Secreta` original e os visitantes são redirecionados de forma transparente para
seu mapa pessoal, evitando corridas por madeira de lei ou itens de coleta.

## Por que este mod existe

Stardew Valley foi originalmente equilibrado pensando em um único fazendeiro.
Mesmo com o cooperativo adicionado depois, grande parte do mundo continua
compartilhada. Em multiplayer com dinheiro separados por jogador, isso gera atritos: madeira de
lei é um recurso limitado, a `Floresta Secreta` só gera seis tocos por dia e os
convidados muitas vezes ficam sem nada. Ver amigos correrem para a `Floresta Secreta` toda
manhã foi a motivação para esculpir uma `Floresta Secreta` dedicada para cada jogador.

## Principais recursos

- Florestas Secretas exclusivas por convidado, criadas sob demanda e reutilizadas
  nos salvamentos.
- Tiles de warp na floresta usam uma `TouchAction` personalizada que redireciona
  jogadores não anfitriões para sua floresta pessoal preservando o tile de destino.
- Convidados que pisam na Woods compartilhada são automaticamente enviados para
  sua instância privada.
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
dotnet build PerFarmhandWoods.sln -c release
```

A compilação utiliza o pacote [Mod Build Config](https://www.nuget.org/packages/Pathoschild.Stardew.ModBuildConfig),
que tambem envia o mod diretamente à pasta `Mods` do jogo.

## Compatibilidade

- **SMAPI**: Requer SMAPI 4.3.2+ (conforme `manifest.json`).
- **Versão do jogo**: Desenvolvido para Stardew Valley 1.6.x.
- **Edições de mapas/warps**: Mods que reescrevem os tiles de warp de
  `Maps/Forest*` ou substituem totalmente a `Floresta Secreta` podem conflitar. O
  patch roda com prioridade tardia, então a maioria dos content packs deve
  coexistir, mas valide no jogo.
- **Warps personalizados**: Qualquer coisa que teleporte convidados diretamente
  para o mapa compartilhado `Woods` é interceptado pelo handler de `Warped` e
  redirecionado, mantendo a experiência consistente (Atualmente desativado).
- **Um jogador**: Nenhuma mudança de gameplay; o anfitrião continua com o mapa
  padrão.

## Limitações conhecidas

- O anfitrião ainda compartilha a `Floresta Secreta` padrão com fazendeiros
  convidados se eles seguirem o anfitrião pela saída. Convidados podem voltar
  para sua instância pelos warps da floresta.
- Mods que substituem profundamente os dados do mapa da floresta podem exigir
  ajustes manuais no patch.

## Desativar ou desinstalar

Se quiser aposentar o mod no meio do salvamento, execute o comando de console do
SMAPI `pfw_purge`. Ele remove todas as instâncias pessoais da floresta, interrompe
a lógica de salvamento do mod e desativa os warps personalizados pelo restante da
sessão. Depois de rodá-lo, durma para salvar o dia, feche o jogo e exclua o mod
antes de iniciar Stardew Valley novamente.

## Política de contribuições e redistribuição

- Não autorizo forks ou redistribuições que alterem o mod para lançar “edições
  alternativas”. Quero manter uma única versão oficial derivada deste código.
- Contribuições são bem-vindas, mas qualquer ideia de melhoria ou nova
  implementação deve ser alinhada previamente por meio de issues (ou outro
  canal oficial do repositório) antes de iniciar o desenvolvimento.
- Abra uma issue explicando a proposta, aguarde o alinhamento e só então invista
  tempo no código para evitar frustrações com pull requests rejeitados.

## Créditos e agradecimentos

- **DerexSV** – conceito original e implementação.
- Comunidade SMAPI e Pathoschild pelo toolkit de modding e pelo build config.
- Amigos que perdiam a corrida pela madeira de lei e inspiraram o conjunto de recursos.
