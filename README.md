# Projeto VR - Fundamentos do Metaverso

**Nome do Aluno:** Murilo Henrique Beraldo da Silva

## Apresentando o Seu Projeto
Este projeto é uma experiência de Realidade Virtual (VR) desenvolvida na Unity utilizando o Meta XR SDK. O ambiente foi montado com objetos 3D e foco na imersão do usuário. Os principais destaques técnicos do projeto são: o sistema de interação física com uma porta, que utiliza o Grab Interactor para acionar a animação de abertura, e o sistema de frutas interativas, que reagem à física e podem ser manipuladas utilizando o Ray Interactor.

## Contexto e Objetivos
O ambiente criado representa uma casa mobiliada. No contexto do Metaverso, este espaço tem o objetivo de servir como uma vitrine virtual interativa, permitindo que o cliente possa explorar, caminhar e interagir com os objetos do imóvel. A interação física com os elementos do cenário (como a porta e as frutas) foi projetada com o intuito de aumentar a sensação de presença e imersão do usuário.

## Processo de Criação e Dificuldades
O projeto foi iniciado configurando o XR Plugin Management e o Meta SDK para suporte à plataforma Android (Meta Quest). O ambiente foi populado com Assets 3D respeitando uma hierarquia organizada, iluminação e Skybox.

**Maiores Dificuldades e Resoluções:**
O maior desafio técnico encontrado durante a criação das interações foi o "Efeito Sanduíche" e a quebra da engine de física (`PhysX`) da Unity. Quando o *Character Controller* do jogador empurrava a porta além do limite do `Hinge Joint`, a física entrava em colapso devido à força infinita do colisor do jogador.

Para resolver isso de forma elegante sem causar conflitos com as juntas temporárias (`FixedJoints`) que o Meta Interaction SDK cria ao agarrar objetos, desenvolvi o script C# **`PortaVR.cs`**. 
O script gerencia dinamicamente o estado `isKinematic` da porta, transformando-a em um objeto rígido ao atingir os limites de rotação (impedindo que o jogador atravesse a parede), mas liberando a física através de funções públicas acionadas pelos eventos `When Select` e `When Unselect` do Pointable Unity Event Wrapper quando o jogador interage com as mãos.

Além desse obstáculo, enfrentei desafios com a estabilidade das ferramentas de desenvolvimento (experienciando crashes no Meta XR Simulator e na própria Unity). O ajuste fino dos colisores na simulação e a curva de aprendizado inicial com o Meta XR SDK também foram pontos que exigiram bastante pesquisa e testes iterativos.