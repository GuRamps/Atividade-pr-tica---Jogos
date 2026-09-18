# Project R — Atividade Prática Unity 2D

Protótipo jogável 2D na Unity que integra movimentação, animação, física, combate, câmera e comportamentos de inimigos utilizando **Máquina de Estados Finita (State Machine)**.

## 🎮 Features

- **Personagem jogável** com corrida, pulo e double jump
- **Ataque** com hitbox via OverlapCircle
- **Câmera** com Cinemachine
- **2 tipos de inimigos** com State Machine:
  - Inimigo 1 — Dash Attack (patrulha + detecção + avanço rápido)
  - Inimigo 2 — Ataque à distância (patrulha + detecção + projétil)
- **Sistema de combate** com Raycast/Overlap (não por colisão física)
- **Animações** básicas coerentes com as ações

## 🛠️ Requisitos Técnicos

| Requisito | Implementação |
|---|---|
| Cenário 2D | Sprites / Tilemap |
| Câmera | Cinemachine 3.x |
| Movimentação | Rigidbody2D |
| Controles | Old Input Manager |
| Pulo | Jump + Double Jump |
| Inimigos | State Machine (Patrol, Detect, Attack) |
| Detecção/Combate | Raycast e OverlapCircle/Box |
| Dano | TakeDamage() via sistema de combate |

## 📁 Estrutura do Projeto

```
Assets/
├── Animations/         # Animator Controllers e Animation Clips
│   ├── Player/
│   └── Enemies/
├── Prefabs/            # Prefabs de player, inimigos, projéteis
│   ├── Player/
│   └── Enemies/
├── References/         # Código de referência e documentação
├── Scenes/             # Cenas do jogo
├── Scripts/            # Código fonte
│   ├── Player/         # PlayerController, PlayerHealth
│   ├── Enemies/        # Scripts dos inimigos
│   ├── StateMachine/   # State Machine base
│   └── Combat/         # Sistema de combate e dano
├── Sprites/            # Assets visuais
│   ├── Player/         # Sprites do Mega Man X
│   ├── Enemies/        # Sprites do Samurai
│   └── Environment/    # Tiles e plataformas
└── Settings/           # Configurações do URP
```

## 🚀 Como executar

1. Abra o projeto no **Unity 6** (6000.x).
2. Abra a cena `Assets/Scenes/SampleScene.unity`.
3. Aperte **Play**.

## ⌨️ Controles

| Ação | Tecla |
|---|---|
| Mover | A/D ou ←/→ |
| Pular | Espaço |
| Double Jump | Espaço (no ar) |
| Atacar | *(a definir)* |

## 📦 Dependências

- Unity 6 (6000.x)
- Universal Render Pipeline (URP) 17.6
- Cinemachine 3.1.4
- 2D Animation, Tilemap, Sprite packages

## 👥 Equipe

*(Adicionar nomes dos integrantes aqui)*
