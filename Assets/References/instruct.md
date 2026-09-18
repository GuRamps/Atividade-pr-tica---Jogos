Objetivo

Desenvolver um protótipo jogável 2D na Unity que integre os principais recursos técnicos trabalhados até o momento. O projeto deverá demonstrar domínio sobre movimentação, animação, física, combate, câmera e implementação de comportamentos de inimigos utilizando Máquina de Estados Finita (State Machine).

O objetivo não é desenvolver um jogo completo, mas construir uma pequena experiência funcional em que seja possível explorar o cenário, enfrentar diferentes inimigos e utilizar as habilidades do personagem.

Cenário e exploração

Desenvolva um pequeno cenário 2D que possa ser explorado pelo jogador. O ambiente deve utilizar sprites e apresentar espaço suficiente para demonstrar as diferentes formas de movimentação e os comportamentos dos inimigos.

A câmera deverá ser implementada utilizando Cinemachine, acompanhando adequadamente o personagem durante a exploração.

Personagem jogável

O jogador deverá possuir movimentação implementada utilizando Rigidbody2D e o Old Input Manager da Unity.

O personagem deverá ser capaz de:

correr pelo cenário;
pular;
realizar double jump;
executar pelo menos um tipo de ataque;
receber e causar dano.

O personagem também deverá possuir animações básicas coerentes com suas principais ações, como movimentação, estado parado e ataque.

Inimigos

O cenário deverá apresentar dois tipos diferentes de inimigos. Ambos devem utilizar Rigidbody2D para movimentação, possuir animações básicas e ter seus comportamentos controlados através de uma State Machine.

Inimigo 1 — Dash Attack

Esse inimigo deverá patrulhar uma determinada região do cenário. Ao detectar o jogador, deverá alterar seu comportamento e realizar um dash attack, avançando rapidamente em sua direção.

Sua State Machine deve permitir identificar claramente comportamentos diferentes, como patrulha, detecção e ataque.

Inimigo 2 — Ataque à distância

Esse inimigo também deverá patrulhar uma região. Entretanto, deverá ser capaz de detectar o jogador a uma distância maior e realizar um ataque à distância.

Sua State Machine deverá controlar a transição entre os comportamentos de patrulha, detecção e ataque.

Detecção, combate e dano

Os sistemas de detecção e combate deverão explorar as técnicas estudadas em aula utilizando Raycast e Overlap.

Esses recursos poderão ser utilizados para situações como detecção do jogador, verificação de alcance de ataques, identificação de colisões ou aplicação de dano.

O objetivo é que o dano seja resultado de uma detecção implementada pelo sistema de combate, e não simplesmente da colisão física entre os personagens.

Requisitos técnicos

Ao final da atividade, o protótipo deverá demonstrar de maneira integrada:

cenário 2D explorável utilizando sprites;
câmera utilizando Cinemachine;
movimentação baseada em Rigidbody2D;
controles utilizando o Old Input Manager;
corrida, pulo e double jump;
ataque do personagem;
dois inimigos com comportamentos distintos;
State Machine aplicada à lógica dos inimigos;
animações básicas para jogador e inimigos;
detecção e combate utilizando Raycast e/ou Overlap;
sistema funcional de dano entre jogador e inimigos.


Entrega

O projeto deverá ser entregue funcional e jogável, permitindo que o professor execute a cena e verifique cada um dos recursos solicitados.

Durante a atividade, cada integrante poderá ser questionado individualmente sobre a implementação do projeto, incluindo a movimentação, State Machines, física, animações, detecção e sistema de combate. Portanto, todos os integrantes devem compreender o funcionamento do código desenvolvido, independentemente da divisão interna de tarefas.

A avaliação deve considerar principalmente a implementação e integração correta dos conceitos, e não a quantidade de conteúdo produzido. Um cenário pequeno, mas tecnicamente consistente, vale muito mais que um continente inteiro sustentado por fita adesiva e esperança.





## Quebrando o projeto em etapas práticas

A ideia é implementar por camadas: primeiro o essencial funciona (mover, pular), depois combate, depois inimigos. Não tente fazer tudo junto.

### Fase 1 — Base do cenário e câmera
- Criar cena 2D com Tilemap ou sprites simples formando uma plataforma/corredor explorável.
- Importar pacote **Cinemachine**, criar um Virtual Camera e fazer ela seguir o player (Follow).
- Isso já dá algo "visível" rodando desde o início, o que ajuda a motivar o grupo.

### Fase 2 — Movimentação do personagem
- Rigidbody2D + Collider2D no player.
- Script de movimento usando `Input.GetAxis("Horizontal")` (Old Input Manager) aplicando velocidade no Rigidbody.
- Pulo: `Input.GetButtonDown("Jump")` aplicando `AddForce` ou setando velocity.y, com checagem de chão via **Raycast/OverlapCircle** pra baixo (`isGrounded`).
- Double jump: contador de pulos (ex: `jumpsLeft`), reseta quando `isGrounded == true`, permite pular de novo enquanto `jumpsLeft > 0`.

### Fase 3 — Animações básicas do player
- Animator Controller com estados: Idle, Run, Jump, Attack.
- Transições via parâmetros (float "speed", bool "grounded", trigger "attack").
- Não precisa ser bonito, só coerente com o que está acontecendo.

### Fase 4 — Ataque e dano do player
- Um ataque simples: no input de ataque, dispara um **Overlap** (OverlapCircle/OverlapBox) numa "hitbox" na frente do personagem.
- Tudo que estiver na layer de inimigos dentro daquele overlap recebe dano (chama um método `TakeDamage()` no inimigo).
- Player também precisa de `TakeDamage()` e HP, pra poder receber dano dos inimigos depois.

### Fase 5 — State Machine dos inimigos (a base)
Façam uma State Machine simples — não precisa de framework complexo, pode ser um enum + switch, ou uma classe base `EnemyState` com métodos `Enter()`, `Update()`, `Exit()`. Os dois inimigos vão reaproveisar essa estrutura.

Estados comuns aos dois: `Patrol`, `Chase/Detect`, `Attack`.

### Fase 6 — Inimigo 1 (Dash Attack)
- Patrulha entre dois pontos (ida e volta).
- Detecção do player via **Raycast** ou **OverlapCircle** com raio curto.
- Ao detectar: muda de estado, calcula direção até o player e aplica um impulso forte (dash) via Rigidbody.
- Dano por Overlap no corpo durante o dash (não por colisão física crua).

### Fase 7 — Inimigo 2 (Ataque à distância)
- Mesma lógica de patrulha, mas raio de detecção maior.
- Ao detectar: para, "atira" (pode ser um Raycast na direção do player checando se acerta, ou instanciar um projétil simples que se move e usa Overlap/Trigger pra detectar acerto).

### Fase 8 — Integração e polimento
- Testar tudo junto na mesma cena.
- Garantir que todo mundo do grupo saiba explicar qualquer parte (o professor pode perguntar individualmente).
- Cenário pequeno e bem implementado > cenário gigante mal feito — isso está literal no enunciado.

---

**Sugestão de divisão de trabalho** (se forem 3-4 pessoas): uma pessoa cuida de player+câmera, outra da State Machine base + inimigo 1, outra do inimigo 2 + sistema de dano/combate. Mas como o professor pode perguntar individualmente, vale todo mundo revisar o código dos colegas no fim.

Quer que eu monte um exemplo de código base (State Machine + movimentação do player) pra vocês partirem daí?